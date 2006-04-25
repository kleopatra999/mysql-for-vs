using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Resources;
using System.Reflection;

namespace MySql.VSTools
{
    internal partial class TableEditor : BaseEditor
    {
        private TableNode table;
        private ImageList listImages;
        private Font wingDingFont;
        private ArrayList columns;

        public TableEditor()
        {
            InitializeComponent();

            if (DesignMode) return;
        }

        public TableEditor(TableNode table) : base(table)
        {
            InitializeComponent();

            if (DesignMode) return;
            base.Init();

            this.table = table;
            tableName.Text = table.Caption;
            tableSchema.Text = table.Schema;
            tableType.SelectedItem = table.TypeName;
            tableType.SelectedText = table.TypeName;
            dataDirectory.Text = table.DataDirectory;
            indexDirectory.Text = table.IndexDirectory;
            rowFormat.SelectedItem = table.RowFormat;
            useChecksum.Checked = table.UseChecksum;
            avgRowLen.Text = AsString(table.AverageRowLength);
            minRows.Text = AsString(table.MinimumRowCount);
            maxRows.Text = AsString(table.MaximumRowCount);

            // setup the image list that will handle the 'key' image
            listImages = new ImageList();
            ResourceManager rm = new ResourceManager("MySql.VSTools.VSPackage",
                Assembly.GetExecutingAssembly());
            listImages.ImageSize = new Size(9, 11);
            listImages.TransparentColor = Color.Transparent;
            listImages.Images.Add((Image)rm.GetObject("key"));
            columnList.SmallImageList = listImages;

            wingDingFont = new Font("Wingdings", 9F, FontStyle.Regular);
            PopulateGrid();
        }

        private string AsString(object val)
        {
            if (val.Equals(0))
                return String.Empty;
            return val.ToString();
        }

        private void PopulateGrid()
        {
            columnList.Items.Clear();

            columns = table.GetColumns();
            foreach (ColumnNode node in columns)
            {
                ListViewItem item = columnList.Items.Add(node.Caption);
                if (node.IsPrimary)
                    item.ImageIndex = 0;
                item.UseItemStyleForSubItems = false;
                item.SubItems.Add(node.Typename);
                item.SubItems.Add(node.CharacterMaxLength == -1 ? String.Empty :
                    node.CharacterMaxLength.ToString());
                ListViewItem.ListViewSubItem sub = item.SubItems.Add(
                    node.AllowNulls ? "u" : "");
                sub.ForeColor = Color.DarkBlue;
                sub.Font = wingDingFont;
                sub = item.SubItems.Add(node.IsBinary ? "u" : "");
                sub.ForeColor = Color.DarkBlue;
                sub.Font = wingDingFont;
                sub = item.SubItems.Add(node.IsZeroFill ? "u" : "");
                sub.Font = wingDingFont;
                sub.ForeColor = Color.DarkBlue;
                sub = item.SubItems.Add(node.CharacterSet);
                sub = item.SubItems.Add(node.Collation);
            }
        }

        private void columnList_SizeChanged(object sender, EventArgs e)
        {

        }

        void columnList_DoubleClick(object sender, System.EventArgs e)
        {
            if (columnList.SelectedIndices.Count == 0) return;
            int selIndex = columnList.SelectedIndices[0];
            ListViewItem selItem = columnList.Items[selIndex];
            ColumnNode selNode = (ColumnNode)columns[selIndex];
            EditColumnDialog dlg = new EditColumnDialog(selNode);

            // if the user canceled the operation, then return
            if (DialogResult.Cancel == dlg.ShowDialog())
                return;

            // if the user didn't change anything, then just return
            if (!selNode.IsDirty) return;

            // update the list item
            selItem.Text = selNode.Caption;
            if (selNode.IsPrimary)
                selItem.ImageIndex = 0;
            selItem.SubItems[1].Text = selNode.Typename;
            selItem.SubItems[2].Text = selNode.CharacterMaxLength == -1 ? String.Empty :
                selNode.CharacterMaxLength.ToString();
            selItem.SubItems[3].Text = selNode.AllowNulls ? "u" : "";
            selItem.SubItems[4].Text = selNode.IsBinary ? "u" : "";
            selItem.SubItems[5].Text = selNode.IsZeroFill ? "u" : "";
            selItem.SubItems[6].Text = selNode.CharacterSet;
            selItem.SubItems[7].Text = selNode.Collation;
        }

        private void common_changed(object sender, EventArgs e)
        {

        }



    }
}