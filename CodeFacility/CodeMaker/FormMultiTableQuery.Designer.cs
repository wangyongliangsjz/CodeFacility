namespace CodeFacility.CodeMaker
{
    partial class FormMultiTableQuery
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
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxTable = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxDB = new System.Windows.Forms.ComboBox();
            this.btn_Query = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_DbMessage = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.DGV1 = new System.Windows.Forms.DataGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.DGV2 = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.DGV3 = new System.Windows.Forms.DataGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.DGV4 = new System.Windows.Forms.DataGridView();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.DGV5 = new System.Windows.Forms.DataGridView();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.rtb_Message = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV2)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV3)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV4)).BeginInit();
            this.tabPage5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGV5)).BeginInit();
            this.tabPage6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBoxTable);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.comboBoxDB);
            this.panel1.Controls.Add(this.btn_Query);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lb_DbMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(806, 58);
            this.panel1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(305, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 27;
            this.label3.Text = "表：";
            // 
            // comboBoxTable
            // 
            this.comboBoxTable.FormattingEnabled = true;
            this.comboBoxTable.Location = new System.Drawing.Point(334, 6);
            this.comboBoxTable.Name = "comboBoxTable";
            this.comboBoxTable.Size = new System.Drawing.Size(196, 20);
            this.comboBoxTable.TabIndex = 26;
            this.comboBoxTable.SelectedIndexChanged += new System.EventHandler(this.comboBoxTable_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 25;
            this.label1.Text = "数据库：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(546, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(60, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "清  除";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxDB
            // 
            this.comboBoxDB.FormattingEnabled = true;
            this.comboBoxDB.Location = new System.Drawing.Point(66, 6);
            this.comboBoxDB.Name = "comboBoxDB";
            this.comboBoxDB.Size = new System.Drawing.Size(233, 20);
            this.comboBoxDB.TabIndex = 23;
            this.comboBoxDB.SelectedIndexChanged += new System.EventHandler(this.comboBoxDB_SelectedIndexChanged);
            // 
            // btn_Query
            // 
            this.btn_Query.Location = new System.Drawing.Point(612, 4);
            this.btn_Query.Name = "btn_Query";
            this.btn_Query.Size = new System.Drawing.Size(60, 23);
            this.btn_Query.TabIndex = 22;
            this.btn_Query.Text = "查  询";
            this.btn_Query.UseVisualStyleBackColor = true;
            this.btn_Query.Click += new System.EventHandler(this.btn_Query_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 21;
            this.label2.Text = "提示：";
            // 
            // lb_DbMessage
            // 
            this.lb_DbMessage.AutoSize = true;
            this.lb_DbMessage.Location = new System.Drawing.Point(58, 30);
            this.lb_DbMessage.Name = "lb_DbMessage";
            this.lb_DbMessage.Size = new System.Drawing.Size(23, 12);
            this.lb_DbMessage.TabIndex = 20;
            this.lb_DbMessage.Text = "...";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 58);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(806, 456);
            this.panel2.TabIndex = 2;
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
            this.tabControl1.Size = new System.Drawing.Size(806, 456);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DGV1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(798, 430);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "结果1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // DGV1
            // 
            this.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV1.Location = new System.Drawing.Point(3, 3);
            this.DGV1.Name = "DGV1";
            this.DGV1.RowTemplate.Height = 23;
            this.DGV1.Size = new System.Drawing.Size(792, 424);
            this.DGV1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DGV2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(798, 430);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "结果2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DGV2
            // 
            this.DGV2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV2.Location = new System.Drawing.Point(3, 3);
            this.DGV2.Name = "DGV2";
            this.DGV2.RowTemplate.Height = 23;
            this.DGV2.Size = new System.Drawing.Size(792, 424);
            this.DGV2.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.DGV3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(798, 430);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "结果3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // DGV3
            // 
            this.DGV3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV3.Location = new System.Drawing.Point(3, 3);
            this.DGV3.Name = "DGV3";
            this.DGV3.RowTemplate.Height = 23;
            this.DGV3.Size = new System.Drawing.Size(792, 424);
            this.DGV3.TabIndex = 2;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.DGV4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(798, 430);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "结果4";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // DGV4
            // 
            this.DGV4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV4.Location = new System.Drawing.Point(3, 3);
            this.DGV4.Name = "DGV4";
            this.DGV4.RowTemplate.Height = 23;
            this.DGV4.Size = new System.Drawing.Size(792, 424);
            this.DGV4.TabIndex = 2;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.DGV5);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(798, 430);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "结果5";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // DGV5
            // 
            this.DGV5.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DGV5.Location = new System.Drawing.Point(3, 3);
            this.DGV5.Name = "DGV5";
            this.DGV5.RowTemplate.Height = 23;
            this.DGV5.Size = new System.Drawing.Size(792, 424);
            this.DGV5.TabIndex = 2;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.rtb_Message);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(798, 430);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "消息";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // rtb_Message
            // 
            this.rtb_Message.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_Message.Location = new System.Drawing.Point(0, 0);
            this.rtb_Message.Name = "rtb_Message";
            this.rtb_Message.Size = new System.Drawing.Size(798, 430);
            this.rtb_Message.TabIndex = 0;
            this.rtb_Message.Text = "";
            // 
            // FormMultiTableQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(806, 514);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormMultiTableQuery";
            this.TabText = "表结构查看";
            this.Text = "表结构查看";
            this.Load += new System.EventHandler(this.FormMultiTableQuery_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV1)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV2)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV3)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV4)).EndInit();
            this.tabPage5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGV5)).EndInit();
            this.tabPage6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_DbMessage;
        private System.Windows.Forms.Button btn_Query;
        private System.Windows.Forms.ComboBox comboBoxDB;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.DataGridView DGV1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView DGV2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView DGV3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.DataGridView DGV4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.DataGridView DGV5;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.RichTextBox rtb_Message;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox comboBoxTable;
        private System.Windows.Forms.Label label1;
    }
}