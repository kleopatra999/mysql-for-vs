﻿// Copyright © 2015, Oracle and/or its affiliates. All rights reserved.
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

namespace MySql.Data.VisualStudio.Editors
{
  partial class ExecutionPlanView
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.txtExecPlan = new System.Windows.Forms.TextBox();
      this.dgvExecPlan = new System.Windows.Forms.DataGridView();
      ((System.ComponentModel.ISupportInitialize)(this.dgvExecPlan)).BeginInit();
      this.SuspendLayout();
      // 
      // txtExecPlan
      // 
      this.txtExecPlan.Dock = System.Windows.Forms.DockStyle.Fill;
      this.txtExecPlan.Location = new System.Drawing.Point(0, 0);
      this.txtExecPlan.Multiline = true;
      this.txtExecPlan.Name = "txtExecPlan";
      this.txtExecPlan.ReadOnly = true;
      this.txtExecPlan.ScrollBars = System.Windows.Forms.ScrollBars.Both;
      this.txtExecPlan.Size = new System.Drawing.Size(600, 500);
      this.txtExecPlan.TabIndex = 0;
      // 
      // dgvExecPlan
      // 
      this.dgvExecPlan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgvExecPlan.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dgvExecPlan.Location = new System.Drawing.Point(0, 0);
      this.dgvExecPlan.Name = "dgvExecPlan";
      this.dgvExecPlan.Size = new System.Drawing.Size(600, 500);
      this.dgvExecPlan.TabIndex = 1;
      // 
      // ExecutionPlanView
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.txtExecPlan);
      this.Controls.Add(this.dgvExecPlan);
      this.Name = "ExecutionPlanView";
      this.Size = new System.Drawing.Size(600, 500);
      ((System.ComponentModel.ISupportInitialize)(this.dgvExecPlan)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtExecPlan;
    private System.Windows.Forms.DataGridView dgvExecPlan;

  }
}
