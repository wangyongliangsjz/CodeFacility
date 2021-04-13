namespace CodeFacility.CodeMaker
{
    partial class FormDbLink
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tb_ID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_DbAbbreviation = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_Port = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_Clear = new System.Windows.Forms.Button();
            this.tb_DataSource = new System.Windows.Forms.TextBox();
            this.lb_ServerName = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.btn_Link = new System.Windows.Forms.Button();
            this.cob_DbType = new System.Windows.Forms.ComboBox();
            this.lb_DbType = new System.Windows.Forms.Label();
            this.tb_Password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_UserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_DbName = new System.Windows.Forms.TextBox();
            this.lb_DbName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgv_DbLink = new System.Windows.Forms.DataGridView();
            this.编辑 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.删除 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DbName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DbAbbreviation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UserName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DbTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DataSource = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Port = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DbType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PassWord = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txt_Charset = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DbLink)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txt_Charset);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.tb_ID);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.tb_DbAbbreviation);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tb_Port);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.tb_Clear);
            this.panel1.Controls.Add(this.tb_DataSource);
            this.panel1.Controls.Add(this.lb_ServerName);
            this.panel1.Controls.Add(this.btn_Save);
            this.panel1.Controls.Add(this.btn_Link);
            this.panel1.Controls.Add(this.cob_DbType);
            this.panel1.Controls.Add(this.lb_DbType);
            this.panel1.Controls.Add(this.tb_Password);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tb_UserName);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tb_DbName);
            this.panel1.Controls.Add(this.lb_DbName);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(680, 161);
            this.panel1.TabIndex = 0;
            // 
            // tb_ID
            // 
            this.tb_ID.Location = new System.Drawing.Point(580, 60);
            this.tb_ID.Name = "tb_ID";
            this.tb_ID.ReadOnly = true;
            this.tb_ID.Size = new System.Drawing.Size(81, 21);
            this.tb_ID.TabIndex = 18;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(522, 63);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 12);
            this.label5.TabIndex = 17;
            this.label5.Text = "编  号";
            // 
            // tb_DbAbbreviation
            // 
            this.tb_DbAbbreviation.Location = new System.Drawing.Point(438, 4);
            this.tb_DbAbbreviation.Name = "tb_DbAbbreviation";
            this.tb_DbAbbreviation.Size = new System.Drawing.Size(223, 21);
            this.tb_DbAbbreviation.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(368, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "数据库简称";
            // 
            // tb_Port
            // 
            this.tb_Port.Location = new System.Drawing.Point(313, 4);
            this.tb_Port.Name = "tb_Port";
            this.tb_Port.Size = new System.Drawing.Size(48, 21);
            this.tb_Port.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(277, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "端口";
            // 
            // tb_Clear
            // 
            this.tb_Clear.Location = new System.Drawing.Point(380, 120);
            this.tb_Clear.Name = "tb_Clear";
            this.tb_Clear.Size = new System.Drawing.Size(75, 23);
            this.tb_Clear.TabIndex = 12;
            this.tb_Clear.Text = "清  除";
            this.tb_Clear.UseVisualStyleBackColor = true;
            this.tb_Clear.Click += new System.EventHandler(this.tb_Clear_Click);
            // 
            // tb_DataSource
            // 
            this.tb_DataSource.Location = new System.Drawing.Point(103, 4);
            this.tb_DataSource.Name = "tb_DataSource";
            this.tb_DataSource.Size = new System.Drawing.Size(150, 21);
            this.tb_DataSource.TabIndex = 11;
            // 
            // lb_ServerName
            // 
            this.lb_ServerName.AutoSize = true;
            this.lb_ServerName.Location = new System.Drawing.Point(13, 9);
            this.lb_ServerName.Name = "lb_ServerName";
            this.lb_ServerName.Size = new System.Drawing.Size(89, 12);
            this.lb_ServerName.TabIndex = 10;
            this.lb_ServerName.Text = "服务器名称或IP";
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(256, 120);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 9;
            this.btn_Save.Text = "确  定";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // btn_Link
            // 
            this.btn_Link.Location = new System.Drawing.Point(129, 120);
            this.btn_Link.Name = "btn_Link";
            this.btn_Link.Size = new System.Drawing.Size(75, 23);
            this.btn_Link.TabIndex = 8;
            this.btn_Link.Text = "测试连接";
            this.btn_Link.UseVisualStyleBackColor = true;
            this.btn_Link.Click += new System.EventHandler(this.btn_Link_Click);
            // 
            // cob_DbType
            // 
            this.cob_DbType.FormattingEnabled = true;
            this.cob_DbType.Location = new System.Drawing.Point(490, 31);
            this.cob_DbType.Name = "cob_DbType";
            this.cob_DbType.Size = new System.Drawing.Size(171, 20);
            this.cob_DbType.TabIndex = 7;
            // 
            // lb_DbType
            // 
            this.lb_DbType.AutoSize = true;
            this.lb_DbType.Location = new System.Drawing.Point(419, 34);
            this.lb_DbType.Name = "lb_DbType";
            this.lb_DbType.Size = new System.Drawing.Size(65, 12);
            this.lb_DbType.TabIndex = 6;
            this.lb_DbType.Text = "数据库类型";
            // 
            // tb_Password
            // 
            this.tb_Password.Location = new System.Drawing.Point(348, 60);
            this.tb_Password.Name = "tb_Password";
            this.tb_Password.PasswordChar = '*';
            this.tb_Password.Size = new System.Drawing.Size(150, 21);
            this.tb_Password.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(277, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "密  码";
            // 
            // tb_UserName
            // 
            this.tb_UserName.Location = new System.Drawing.Point(103, 60);
            this.tb_UserName.Name = "tb_UserName";
            this.tb_UserName.Size = new System.Drawing.Size(150, 21);
            this.tb_UserName.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "用户名";
            // 
            // tb_DbName
            // 
            this.tb_DbName.Location = new System.Drawing.Point(103, 31);
            this.tb_DbName.Name = "tb_DbName";
            this.tb_DbName.Size = new System.Drawing.Size(303, 21);
            this.tb_DbName.TabIndex = 1;
            // 
            // lb_DbName
            // 
            this.lb_DbName.AutoSize = true;
            this.lb_DbName.Location = new System.Drawing.Point(13, 34);
            this.lb_DbName.Name = "lb_DbName";
            this.lb_DbName.Size = new System.Drawing.Size(65, 12);
            this.lb_DbName.TabIndex = 0;
            this.lb_DbName.Text = "数据库名称";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv_DbLink);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 161);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(680, 280);
            this.panel2.TabIndex = 1;
            // 
            // dgv_DbLink
            // 
            this.dgv_DbLink.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_DbLink.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.编辑,
            this.删除,
            this.ID,
            this.DbName,
            this.DbAbbreviation,
            this.UserName,
            this.DbTypeName,
            this.DataSource,
            this.Port,
            this.CreateTime,
            this.DbType,
            this.PassWord});
            this.dgv_DbLink.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_DbLink.Location = new System.Drawing.Point(0, 0);
            this.dgv_DbLink.Name = "dgv_DbLink";
            this.dgv_DbLink.RowTemplate.Height = 23;
            this.dgv_DbLink.Size = new System.Drawing.Size(680, 280);
            this.dgv_DbLink.TabIndex = 0;
            this.dgv_DbLink.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_DbLink_CellMouseDoubleClick);
            // 
            // 编辑
            // 
            dataGridViewCellStyle1.NullValue = "编辑";
            this.编辑.DefaultCellStyle = dataGridViewCellStyle1;
            this.编辑.HeaderText = "编辑";
            this.编辑.Name = "编辑";
            this.编辑.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.编辑.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.编辑.Width = 60;
            // 
            // 删除
            // 
            dataGridViewCellStyle2.NullValue = "删除";
            this.删除.DefaultCellStyle = dataGridViewCellStyle2;
            this.删除.HeaderText = "删除";
            this.删除.Name = "删除";
            this.删除.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.删除.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.删除.Width = 60;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "序号";
            this.ID.Name = "ID";
            // 
            // DbName
            // 
            this.DbName.DataPropertyName = "DbName";
            this.DbName.HeaderText = "数据库名称";
            this.DbName.Name = "DbName";
            // 
            // DbAbbreviation
            // 
            this.DbAbbreviation.DataPropertyName = "DbAbbreviation";
            this.DbAbbreviation.HeaderText = "数据库简称";
            this.DbAbbreviation.Name = "DbAbbreviation";
            // 
            // UserName
            // 
            this.UserName.DataPropertyName = "UserName";
            this.UserName.HeaderText = "登录名称";
            this.UserName.Name = "UserName";
            // 
            // DbTypeName
            // 
            this.DbTypeName.DataPropertyName = "DbTypeName";
            this.DbTypeName.HeaderText = "类型";
            this.DbTypeName.Name = "DbTypeName";
            // 
            // DataSource
            // 
            this.DataSource.DataPropertyName = "DataSource";
            this.DataSource.HeaderText = "地址";
            this.DataSource.Name = "DataSource";
            // 
            // Port
            // 
            this.Port.DataPropertyName = "Port";
            this.Port.HeaderText = "端口";
            this.Port.Name = "Port";
            // 
            // CreateTime
            // 
            this.CreateTime.DataPropertyName = "CreateTime";
            this.CreateTime.HeaderText = "添加时间";
            this.CreateTime.Name = "CreateTime";
            // 
            // DbType
            // 
            this.DbType.DataPropertyName = "DbType";
            this.DbType.HeaderText = "DbType";
            this.DbType.Name = "DbType";
            this.DbType.Visible = false;
            // 
            // PassWord
            // 
            this.PassWord.DataPropertyName = "PassWord";
            this.PassWord.HeaderText = "PassWord";
            this.PassWord.Name = "PassWord";
            this.PassWord.Visible = false;
            // 
            // txt_Charset
            // 
            this.txt_Charset.Location = new System.Drawing.Point(130, 89);
            this.txt_Charset.Name = "txt_Charset";
            this.txt_Charset.Size = new System.Drawing.Size(150, 21);
            this.txt_Charset.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 19;
            this.label6.Text = "字符集(utf8/gdk)";
            // 
            // FormDbLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 441);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "FormDbLink";
            this.TabText = "数据库连接";
            this.Text = "数据库连接";
            this.Load += new System.EventHandler(this.FormDbLink_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_DbLink)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Button btn_Link;
        private System.Windows.Forms.ComboBox cob_DbType;
        private System.Windows.Forms.Label lb_DbType;
        private System.Windows.Forms.TextBox tb_Password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tb_UserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_DbName;
        private System.Windows.Forms.Label lb_DbName;
        private System.Windows.Forms.Label lb_ServerName;
        private System.Windows.Forms.TextBox tb_DataSource;
        private System.Windows.Forms.Button tb_Clear;
        private System.Windows.Forms.TextBox tb_Port;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_DbAbbreviation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgv_DbLink;
        private System.Windows.Forms.TextBox tb_ID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridViewLinkColumn 编辑;
        private System.Windows.Forms.DataGridViewLinkColumn 删除;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DbName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DbAbbreviation;
        private System.Windows.Forms.DataGridViewTextBoxColumn UserName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DbTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DataSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn Port;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn DbType;
        private System.Windows.Forms.DataGridViewTextBoxColumn PassWord;
        private System.Windows.Forms.TextBox txt_Charset;
        private System.Windows.Forms.Label label6;
    }
}