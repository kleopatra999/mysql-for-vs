﻿// Copyright © 2015, 2017, Oracle and/or its affiliates. All rights reserved.
//
// MySQL for Visual Studio is licensed under the terms of the GPLv2
// <http://www.gnu.org/licenses/old-licenses/gpl-2.0.html>, like most
// MySQL Connectors. There are special exceptions to the terms and
// conditions of the GPLv2 as it is applied to this software, see the
// FLOSS License Exception
// <http://www.mysql.com/about/legal/licensing/foss-exception.html>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published
// by the Free Software Foundation; version 2 of the License.
//
// This program is distributed in the hope that it will be useful, but
// WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
// or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License
// for more details.
//
// You should have received a copy of the GNU General Public License along
// with this program; if not, write to the Free Software Foundation, Inc.,
// 51 Franklin St, Fifth Floor, Boston, MA 02110-1301  USA

using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MySqlX;
using MySqlX.Shell;

namespace MySql.Data.VisualStudio.Editors
{
  /// <summary>
  /// User control that will be used to display JSON like documents in a grid view.
  /// </summary>
  public partial class GridViewResult : UserControl
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="GridViewResult"/> class.
    /// </summary>
    public GridViewResult()
    {
      InitializeComponent();
    }

    /// <summary>
    /// Load data to the tree view
    /// </summary>
    /// <param name="data">Data to load</param>
    public void SetData(List<Dictionary<string, object>> data)
    {
      GenerateItem(data);
      SetDataGridStyle();
      Utils.SanitizeBlobs(ref gvData);
    }

    /// <summary>
    /// Create tree nodes from the data received to add it to the tree view and display the information
    /// </summary>
    /// <param name="rows">Data to generate the tree nodes</param>
    private void GenerateItem(List<Dictionary<string, object>> rows)
    {
      var dt = new DataTable();
      var colNames = new List<string>();
      rows.ForEach(item => colNames.AddRange(item.Keys.Where(name => !colNames.Contains(name))));
      colNames.ForEach(colName => dt.Columns.Add(colName));
      foreach (var dicItem in rows)
      {
        var dr = dt.NewRow();
        foreach (var column in colNames)
        {
          object value;
          dicItem.TryGetValue(column, out value);
          object formattedValue = Utils.GetFormattedValue(value);

          if (formattedValue != null && formattedValue.GetType().Name.Contains("Dictionary"))
          {
            dr[column] = string.Format("{0} Fields", (formattedValue as Dictionary<string, object>).Count());
          }
          else
          {
            dr[column] = formattedValue;
          }
        }

        dt.Rows.Add(dr);
      }
      gvData.DataSource = dt;
    }

    /// <summary>
    /// Apply style to the grid used to show the data
    /// </summary>
    public void SetDataGridStyle()
    {
      gvData.ColumnHeadersDefaultCellStyle = Utils.GetHeaderStyle();
      gvData.RowsDefaultCellStyle = Utils.GetRowStyle();
      gvData.AlternatingRowsDefaultCellStyle = Utils.GetAlternateRowStyle();
      gvData.BorderStyle = BorderStyle.None;
    }
  }
}
