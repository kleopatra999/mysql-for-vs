﻿using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.TextManager.Interop;
using MySql.Data.Common;
using MySql.Data.VisualStudio.LanguageService;

namespace MySql.Data.VisualStudio 
{
    internal class MySqlColorizer : Colorizer
    {
        private IScanner scanner;
        private IVsTextLines buffer;
        private List<TokenInfo>[] tokenList;
        private string savedSqlText;
        private SqlTokenizer tokenizer;

        public MySqlColorizer(LanguageService service, IVsTextLines buffer, IScanner scanner)
            : base(service, buffer, scanner)
        {
            this.scanner = scanner;
            this.buffer = buffer;
            tokenizer = new SqlTokenizer();
        }

        public override TokenInfo[] GetLineInfo(IVsTextLines buffer, int line, IVsTextColorState colorState)
        {
            LexBuffer();
            return tokenList[line].ToArray();
        }

        public override int GetColorInfo(string line, int length, int state)
        {
            return 0; 
        }

        public override int ColorizeLine(int line, int length, System.IntPtr ptr, 
            int state, uint[] attrs)
        {
            int linepos = 0;

            // only relex if we need to
            LexBuffer();

            if (scanner != null && tokenList.Length > line)
            {
                TokenInfo[] tokens = tokenList[line].ToArray();
                foreach (TokenInfo ti in tokens)
                {
                    for (; linepos < ti.StartIndex; linepos++)
                        attrs[linepos] = (uint)TokenColor.Text;

                    uint color = (uint)ti.Color;
                    if (ti.Type == TokenType.Comment ||
                        ti.Type == TokenType.LineComment ||
                        ti.Type == TokenType.String ||
                        ti.Type == TokenType.Text)
                    {
                        color |= (uint)COLORIZER_ATTRIBUTE.HUMAN_TEXT_ATTR;
                    }
                    for (; linepos <= ti.EndIndex; linepos++)
                        attrs[linepos] = (uint)ti.Color;
                }
            }
            else if (attrs != null)
            {
                // Must initialize the colors in all cases, otherwise you get 
                // random color junk on the screen.
                for (; linepos < length; linepos++)
                    attrs[linepos] = (uint)TokenColor.Text;
            }
            return 0;
        }

        /// <summary>
        ///  This method will "lex" the entire SQL buffer into an array
        ///  of TokenInfo structs. It compares the buffer lines to a cached
        ///  copy and only re-lexes if there are changes
        /// </summary>
        private void LexBuffer()
        {
            int lineCount;
            buffer.GetLineCount(out lineCount);

            StringBuilder sb = new StringBuilder();
            string[] lines = new string[lineCount];

            for (int line = 0; line < lineCount; line++)
            {
                int length;
                buffer.GetLengthOfLine(line, out length);

                buffer.GetLineText(line, 0, line, length, out lines[line]);
                lines[line] = lines[line].Replace(@"\r\n", " ").Replace(@"\n", " ");
            }

            // now reformat the buffer to have new lines where VS wants them to be
            foreach (string line in lines)
                sb.AppendLine(line);

            string sqlText = sb.ToString();
            if (sqlText == savedSqlText) return;
            savedSqlText = sqlText;

            // things have changed so we need to create an array
            // of TokenInfo arrays to hold our color info
            tokenList = new List<TokenInfo>[lineCount];
            for (int i = 0; i < lineCount; i++)
                tokenList[i] = new List<TokenInfo>();

            tokenizer.Text = sqlText;
            tokenizer.ReturnComments = true;

            string token = tokenizer.NextToken();

            while (token != null)
            {
                int startLine = tokenizer.StartLine;
                int endLine = tokenizer.StopLine;

                for (int line = startLine; line <= endLine; line++)
                {
                    TokenInfo ti = Configuration.GetTokenInfo(token, tokenizer);
                    ti.StartIndex = line == startLine ? tokenizer.StartIndex : 0;
                    ti.EndIndex = line == endLine ? tokenizer.StopIndex : lines[line].Length;
                    tokenList[line].Add(ti);
                }

                token = tokenizer.NextToken();
            }
        }
    }
}
