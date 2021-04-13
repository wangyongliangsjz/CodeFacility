namespace CodeFacility.CodeMaker
{
    partial class FormCopyData
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_import = new System.Windows.Forms.Button();
            this.btn_Link = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_DataSource = new System.Windows.Forms.TextBox();
            this.lb_ServerName = new System.Windows.Forms.Label();
            this.tb_Password = new System.Windows.Forms.TextBox();
            this.tb_UserName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_DbName = new System.Windows.Forms.TextBox();
            this.lb_DbName = new System.Windows.Forms.Label();
            this.btn_permission = new System.Windows.Forms.Button();
            this.btb_forbid = new System.Windows.Forms.Button();
            this.dgv6 = new System.Windows.Forms.DataGridView();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.dgv5 = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.dgv4 = new System.Windows.Forms.DataGridView();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Link2 = new System.Windows.Forms.Button();
            this.tb_Password2 = new System.Windows.Forms.TextBox();
            this.tb_UserName2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.tb_DataSource2 = new System.Windows.Forms.TextBox();
            this.tb_DbName2 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tv_table = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btn_clear = new System.Windows.Forms.Button();
            this.btn_clear2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv6)).BeginInit();
            this.tabPage6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv5)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv4)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_import);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(508, 114);
            this.panel1.TabIndex = 0;
            // 
            // btn_import
            // 
            this.btn_import.Location = new System.Drawing.Point(435, 35);
            this.btn_import.Name = "btn_import";
            this.btn_import.Size = new System.Drawing.Size(61, 23);
            this.btn_import.TabIndex = 24;
            this.btn_import.Text = "复  制";
            this.btn_import.UseVisualStyleBackColor = true;
            // 
            // btn_Link
            // 
            this.btn_Link.Location = new System.Drawing.Point(200, 73);
            this.btn_Link.Name = "btn_Link";
            this.btn_Link.Size = new System.Drawing.Size(61, 23);
            this.btn_Link.TabIndex = 39;
            this.btn_Link.Text = "测试连接";
            this.btn_Link.UseVisualStyleBackColor = true;
            this.btn_Link.Click += new System.EventHandler(this.btn_Link_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(343, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 38;
            this.label3.Text = "密  码";
            // 
            // tb_DataSource
            // 
            this.tb_DataSource.Location = new System.Drawing.Point(102, 16);
            this.tb_DataSource.Name = "tb_DataSource";
            this.tb_DataSource.Size = new System.Drawing.Size(372, 21);
            this.tb_DataSource.TabIndex = 37;
            // 
            // lb_ServerName
            // 
            this.lb_ServerName.AutoSize = true;
            this.lb_ServerName.Location = new System.Drawing.Point(5, 21);
            this.lb_ServerName.Name = "lb_ServerName";
            this.lb_ServerName.Size = new System.Drawing.Size(89, 12);
            this.lb_ServerName.TabIndex = 36;
            this.lb_ServerName.Text = "服务器名称或IP";
            // 
            // tb_Password
            // 
            this.tb_Password.Location = new System.Drawing.Point(396, 41);
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.PasswordChar = '*';
            this.tb_Password.Size = new System.Drawing.Size(78, 21);
            this.tb_Password.TabIndex = 35;
            // 
            // tb_UserName
            // 
            this.tb_UserName.Location = new System.Drawing.Point(245, 41);
            this.tb_UserName.Name = "tb_UserName";
            this.tb_UserName.Size = new System.Drawing.Size(89, 21);
            this.tb_UserName.TabIndex = 34;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(198, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 33;
            this.label1.Text = "用户名";
            // 
            // tb_DbName
            // 
            this.tb_DbName.Location = new System.Drawing.Point(75, 41);
            this.tb_DbName.Name = "tb_DbName";
            this.tb_DbName.Size = new System.Drawing.Size(117, 21);
            this.tb_DbName.TabIndex = 32;
            // 
            // lb_DbName
            // 
            this.lb_DbName.AutoSize = true;
            this.lb_DbName.Location = new System.Drawing.Point(4, 44);
            this.lb_DbName.Name = "lb_DbName";
            this.lb_DbName.Size = new System.Drawing.Size(65, 12);
            this.lb_DbName.TabIndex = 31;
            this.lb_DbName.Text = "数据库名称";
            // 
            // btn_permission
            // 
            this.btn_permission.Location = new System.Drawing.Point(345, 79);
            this.btn_permission.Name = "btn_permission";
            this.btn_permission.Size = new System.Drawing.Size(92, 23);
            this.btn_permission.TabIndex = 30;
            this.btn_permission.Text = "启用约束/自增";
            this.btn_permission.UseVisualStyleBackColor = true;
            this.btn_permission.Click += new System.EventHandler(this.btn_permission_Click);
            // 
            // btb_forbid
            // 
            this.btb_forbid.Location = new System.Drawing.Point(242, 79);
            this.btb_forbid.Name = "btb_forbid";
            this.btb_forbid.Size = new System.Drawing.Size(92, 23);
            this.btb_forbid.TabIndex = 25;
            this.btb_forbid.Text = "禁用约束/自增";
            this.btb_forbid.UseVisualStyleBackColor = true;
            this.btb_forbid.Click += new System.EventHandler(this.btb_forbid_Click);
            // 
            // dgv6
            // 
            this.dgv6.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv6.Location = new System.Drawing.Point(3, 3);
            this.dgv6.Name = "dgv6";
            this.dgv6.RowTemplate.Height = 23;
            this.dgv6.Size = new System.Drawing.Size(494, 327);
            this.dgv6.TabIndex = 2;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.dgv6);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage6.Size = new System.Drawing.Size(500, 333);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "Sheet6";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // dgv5
            // 
            this.dgv5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv5.Location = new System.Drawing.Point(3, 3);
            this.dgv5.Name = "dgv5";
            this.dgv5.RowTemplate.Height = 23;
            this.dgv5.Size = new System.Drawing.Size(494, 327);
            this.dgv5.TabIndex = 2;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.dgv5);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(500, 333);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Sheet5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // dgv4
            // 
            this.dgv4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv4.Location = new System.Drawing.Point(3, 3);
            this.dgv4.Name = "dgv4";
            this.dgv4.RowTemplate.Height = 23;
            this.dgv4.Size = new System.Drawing.Size(494, 327);
            this.dgv4.TabIndex = 2;
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
            this.tabControl1.Size = new System.Drawing.Size(508, 359);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Tag = "";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(500, 333);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Sheet1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_clear2);
            this.groupBox2.Controls.Add(this.btn_permission);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.btb_forbid);
            this.groupBox2.Controls.Add(this.btn_Link2);
            this.groupBox2.Controls.Add(this.tb_Password2);
            this.groupBox2.Controls.Add(this.tb_UserName2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.tb_DataSource2);
            this.groupBox2.Controls.Add(this.tb_DbName2);
            this.groupBox2.Location = new System.Drawing.Point(10, 128);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(484, 111);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "目标数据库";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 12);
            this.label2.TabIndex = 36;
            this.label2.Text = "服务器名称或IP";
            // 
            // btn_Link2
            // 
            this.btn_Link2.Location = new System.Drawing.Point(75, 79);
            this.btn_Link2.Name = "btn_Link2";
            this.btn_Link2.Size = new System.Drawing.Size(61, 23);
            this.btn_Link2.TabIndex = 39;
            this.btn_Link2.Text = "测试连接";
            this.btn_Link2.UseVisualStyleBackColor = true;
            this.btn_Link2.Click += new System.EventHandler(this.btn_Link2_Click);
            // 
            // tb_Password2
            // 
            this.tb_Password2.Location = new System.Drawing.Point(396, 41);
            this.tb_Password2.Name = "tb_Password2";
            this.tb_Password2.PasswordChar = '*';
            this.tb_Password2.Size = new System.Drawing.Size(78, 21);
            this.tb_Password2.TabIndex = 35;
            // 
            // tb_UserName2
            // 
            this.tb_UserName2.Location = new System.Drawing.Point(245, 41);
            this.tb_UserName2.Name = "tb_UserName2";
            this.tb_UserName2.Size = new System.Drawing.Size(89, 21);
            this.tb_UserName2.TabIndex = 34;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 31;
            this.label4.Text = "数据库名称";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(198, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 33;
            this.label5.Text = "用户名";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(343, 46);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 38;
            this.label6.Text = "密  码";
            // 
            // tb_DataSource2
            // 
            this.tb_DataSource2.Location = new System.Drawing.Point(102, 16);
            this.tb_DataSource2.Name = "tb_DataSource2";
            this.tb_DataSource2.Size = new System.Drawing.Size(372, 21);
            this.tb_DataSource2.TabIndex = 37;
            // 
            // tb_DbName2
            // 
            this.tb_DbName2.Location = new System.Drawing.Point(75, 41);
            this.tb_DbName2.Name = "tb_DbName2";
            this.tb_DbName2.Size = new System.Drawing.Size(117, 21);
            this.tb_DbName2.TabIndex = 32;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_clear);
            this.groupBox1.Controls.Add(this.lb_ServerName);
            this.groupBox1.Controls.Add(this.btn_Link);
            this.groupBox1.Controls.Add(this.tb_Password);
            this.groupBox1.Controls.Add(this.tb_UserName);
            this.groupBox1.Controls.Add(this.lb_DbName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_DataSource);
            this.groupBox1.Controls.Add(this.tb_DbName);
            this.groupBox1.Location = new System.Drawing.Point(10, 20);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(484, 102);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "源数据库";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.listBox1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(500, 333);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Sheet2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(8, 6);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(486, 292);
            this.listBox1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.textBox5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(500, 333);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Sheet3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(6, 6);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(488, 324);
            this.textBox5.TabIndex = 0;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.dgv4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(500, 333);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Sheet4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 114);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(508, 359);
            this.panel2.TabIndex = 1;
            // 
            // tv_table
            // 
            this.tv_table.CheckBoxes = true;
            this.tv_table.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_table.Location = new System.Drawing.Point(0, 0);
            this.tv_table.Name = "tv_table";
            this.tv_table.Size = new System.Drawing.Size(160, 473);
            this.tv_table.TabIndex = 0;
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
            this.splitContainer1.Size = new System.Drawing.Size(672, 473);
            this.splitContainer1.SplitterDistance = 160;
            this.splitContainer1.TabIndex = 1;
            // 
            // btn_clear
            // 
            this.btn_clear.Location = new System.Drawing.Point(280, 73);
            this.btn_clear.Name = "btn_clear";
            this.btn_clear.Size = new System.Drawing.Size(60, 23);
            this.btn_clear.TabIndex = 40;
            this.btn_clear.Text = "清  除";
            this.btn_clear.UseVisualStyleBackColor = true;
            this.btn_clear.Click += new System.EventHandler(this.btn_clear_Click);
            // 
            // btn_clear2
            // 
            this.btn_clear2.Location = new System.Drawing.Point(159, 79);
            this.btn_clear2.Name = "btn_clear2";
            this.btn_clear2.Size = new System.Drawing.Size(60, 23);
            this.btn_clear2.TabIndex = 41;
            this.btn_clear2.Text = "清  除";
            this.btn_clear2.UseVisualStyleBackColor = true;
            this.btn_clear2.Click += new System.EventHandler(this.btn_clear2_Click);
            // 
            // FormCopyData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 473);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormCopyData";
            this.TabText = "FormCopyDb";
            this.Text = "FormCopyDb";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv6)).EndInit();
            this.tabPage6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv5)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv4)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_permission;
        private System.Windows.Forms.Button btb_forbid;
        private System.Windows.Forms.Button btn_import;
        private System.Windows.Forms.DataGridView dgv6;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.DataGridView dgv5;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridView dgv4;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView tv_table;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox tb_DataSource;
        private System.Windows.Forms.Label lb_ServerName;
        private System.Windows.Forms.TextBox tb_Password;
        private System.Windows.Forms.TextBox tb_UserName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_DbName;
        private System.Windows.Forms.Label lb_DbName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Link;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Link2;
        private System.Windows.Forms.TextBox tb_Password2;
        private System.Windows.Forms.TextBox tb_UserName2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tb_DataSource2;
        private System.Windows.Forms.TextBox tb_DbName2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button btn_clear;
        private System.Windows.Forms.Button btn_clear2;
    }
}