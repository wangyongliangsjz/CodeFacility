namespace CodeFacility.CodeMaker
{
    partial class FormImportData
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tv_table = new System.Windows.Forms.TreeView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dgv1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgv2 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgv3 = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.dgv4 = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dgv5 = new System.Windows.Forms.DataGridView();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.dgv6 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBoxDB = new System.Windows.Forms.ComboBox();
            this.tb_AddTableName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.ckb_IsAddTable = new System.Windows.Forms.CheckBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btn_clear = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_field = new System.Windows.Forms.TextBox();
            this.tb_file = new System.Windows.Forms.TextBox();
            this.btb_scan = new System.Windows.Forms.Button();
            this.btn_import = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_DbMessage = new System.Windows.Forms.Label();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv3)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv4)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv5)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv6)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tv_table);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(877, 473);
            this.splitContainer1.SplitterDistance = 238;
            this.splitContainer1.TabIndex = 0;
            // 
            // tv_table
            // 
            this.tv_table.CheckBoxes = true;
            this.tv_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_table.Location = new System.Drawing.Point(0, 0);
            this.tv_table.Name = "tv_table";
            this.tv_table.Size = new System.Drawing.Size(238, 473);
            this.tv_table.TabIndex = 0;
            this.tv_table.BeforeCheck += new System.Windows.Forms.TreeViewCancelEventHandler(this.tv_table_BeforeCheck);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 155);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(635, 318);
            this.panel2.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(635, 318);
            this.tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.dgv1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(627, 292);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Sheet1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // dgv1
            // 
            this.dgv1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv1.Location = new System.Drawing.Point(3, 3);
            this.dgv1.Name = "dgv1";
            this.dgv1.RowTemplate.Height = 23;
            this.dgv1.Size = new System.Drawing.Size(621, 286);
            this.dgv1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgv2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(627, 292);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sheet2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgv2
            // 
            this.dgv2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv2.Location = new System.Drawing.Point(3, 3);
            this.dgv2.Name = "dgv2";
            this.dgv2.RowTemplate.Height = 23;
            this.dgv2.Size = new System.Drawing.Size(621, 286);
            this.dgv2.TabIndex = 1;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgv3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(627, 292);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Sheet3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgv3
            // 
            this.dgv3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv3.Location = new System.Drawing.Point(3, 3);
            this.dgv3.Name = "dgv3";
            this.dgv3.RowTemplate.Height = 23;
            this.dgv3.Size = new System.Drawing.Size(621, 286);
            this.dgv3.TabIndex = 2;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgv4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(627, 292);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Sheet4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // dgv4
            // 
            this.dgv4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv4.Location = new System.Drawing.Point(3, 3);
            this.dgv4.Name = "dgv4";
            this.dgv4.RowTemplate.Height = 23;
            this.dgv4.Size = new System.Drawing.Size(621, 286);
            this.dgv4.TabIndex = 2;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dgv5);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(627, 292);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Sheet5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dgv5
            // 
            this.dgv5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv5.Location = new System.Drawing.Point(3, 3);
            this.dgv5.Name = "dgv5";
            this.dgv5.RowTemplate.Height = 23;
            this.dgv5.Size = new System.Drawing.Size(621, 286);
            this.dgv5.TabIndex = 2;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.dgv6);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(627, 292);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Sheet6";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // dgv6
            // 
            this.dgv6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv6.Location = new System.Drawing.Point(3, 3);
            this.dgv6.Name = "dgv6";
            this.dgv6.RowTemplate.Height = 23;
            this.dgv6.Size = new System.Drawing.Size(621, 286);
            this.dgv6.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.comboBoxDB);
            this.panel1.Controls.Add(this.tb_AddTableName);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.ckb_IsAddTable);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.btn_clear);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tb_field);
            this.panel1.Controls.Add(this.tb_file);
            this.panel1.Controls.Add(this.btb_scan);
            this.panel1.Controls.Add(this.btn_import);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lb_DbMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(635, 155);
            this.panel1.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(288, 122);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 36;
            this.label5.Text = "数据库：";
            // 
            // comboBoxDB
            // 
            this.comboBoxDB.FormattingEnabled = true;
            this.comboBoxDB.Location = new System.Drawing.Point(342, 119);
            this.comboBoxDB.Name = "comboBoxDB";
            this.comboBoxDB.Size = new System.Drawing.Size(259, 20);
            this.comboBoxDB.TabIndex = 35;
            // 
            // tb_AddTableName
            // 
            this.tb_AddTableName.Location = new System.Drawing.Point(117, 118);
            this.tb_AddTableName.Name = "tb_AddTableName";
            this.tb_AddTableName.Size = new System.Drawing.Size(166, 21);
            this.tb_AddTableName.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(82, 122);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 33;
            this.label4.Text = "表名";
            // 
            // ckb_IsAddTable
            // 
            this.ckb_IsAddTable.AutoSize = true;
            this.ckb_IsAddTable.Location = new System.Drawing.Point(16, 121);
            this.ckb_IsAddTable.Name = "ckb_IsAddTable";
            this.ckb_IsAddTable.Size = new System.Drawing.Size(60, 16);
            this.ckb_IsAddTable.TabIndex = 32;
            this.ckb_IsAddTable.Text = "新建表";
            this.ckb_IsAddTable.UseVisualStyleBackColor = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(479, 26);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(61, 23);
            this.btnRefresh.TabIndex = 31;
            this.btnRefresh.Text = "刷  新";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(413, 57);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(60, 23);
            this.btn_clear.TabIndex = 30;
            this.btn_clear.Text = "清  除";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 29;
            this.label3.Text = "导入字段";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 28;
            this.label1.Text = "导入文件";
            // 
            // tb_field
            // 
            this.tb_field.Location = new System.Drawing.Point(73, 54);
            this.tb_field.Multiline = true;
            this.tb_field.Name = "tb_field";
            this.tb_field.Size = new System.Drawing.Size(333, 55);
            this.tb_field.TabIndex = 27;
            // 
            // tb_file
            // 
            this.tb_file.Location = new System.Drawing.Point(73, 27);
            this.tb_file.Name = "tb_file";
            this.tb_file.Size = new System.Drawing.Size(333, 21);
            this.tb_file.TabIndex = 26;
            // 
            // btb_scan
            // 
            this.btb_scan.Location = new System.Drawing.Point(412, 26);
            this.btb_scan.Name = "btb_scan";
            this.btb_scan.Size = new System.Drawing.Size(61, 23);
            this.btb_scan.TabIndex = 25;
            this.btb_scan.Text = "浏  览";
            this.btb_scan.UseVisualStyleBackColor = true;
            this.btb_scan.Click += new System.EventHandler(this.btb_scan_Click);
            // 
            // btn_import
            // 
            this.btn_import.Location = new System.Drawing.Point(412, 86);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(61, 23);
            this.btn_import.TabIndex = 24;
            this.btn_import.Text = "导  入";
            this.btn_import.UseVisualStyleBackColor = true;
            this.btn_import.Click += new System.EventHandler(this.btn_import_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 23;
            this.label2.Text = "提示";
            // 
            // lb_DbMessage
            // 
            this.lb_DbMessage.AutoSize = true;
            this.lb_DbMessage.Location = new System.Drawing.Point(42, 9);
            this.lb_DbMessage.Name = "lb_DbMessage";
            this.lb_DbMessage.Size = new System.Drawing.Size(23, 12);
            this.lb_DbMessage.TabIndex = 22;
            this.lb_DbMessage.Text = "...";
            // 
            // FormImportData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(877, 473);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormImportData";
            this.TabText = "数据导入";
            this.Text = "数据导入";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormImportData_FormClosed);
            this.Load += new System.EventHandler(this.FormImportData_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv3)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv4)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv5)).EndInit();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv6)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tv_table;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgv1;
        private System.Windows.Forms.Button btn_import;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_DbMessage;
        private System.Windows.Forms.TextBox tb_file;
        private System.Windows.Forms.Button btb_scan;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_field;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgv2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.DataGridView dgv3;
        private System.Windows.Forms.DataGridView dgv4;
        private System.Windows.Forms.DataGridView dgv5;
        private System.Windows.Forms.DataGridView dgv6;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.CheckBox ckb_IsAddTable;
        private System.Windows.Forms.TextBox tb_AddTableName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBoxDB;
    }
}