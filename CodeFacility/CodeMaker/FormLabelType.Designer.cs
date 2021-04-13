namespace CodeFacility.CodeMaker
{
    partial class FormLabelType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLabelType));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lb_ID = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.lb_ParentTitle = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.tb_Remark = new System.Windows.Forms.TextBox();
            this.tb_Title = new System.Windows.Forms.TextBox();
            this.tb_Code = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.ParentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tv_left = new System.Windows.Forms.TreeView();
            this.EditTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QueryToolStripBtn = new System.Windows.Forms.ToolStripButton();
            this.ClearToolStripBtn = new System.Windows.Forms.ToolStripButton();
            this.QueryToolStripTb = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgv_Data = new System.Windows.Forms.DataGridView();
            this.编辑 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.删除 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Code = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CreateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Data)).BeginInit();
            this.SuspendLayout();
            // 
            // lb_ID
            // 
            this.lb_ID.AutoSize = true;
            this.lb_ID.Location = new System.Drawing.Point(254, 9);
            this.lb_ID.Name = "lb_ID";
            this.lb_ID.Size = new System.Drawing.Size(23, 12);
            this.lb_ID.TabIndex = 11;
            this.lb_ID.Text = "...";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btn_Clear);
            this.panel1.Controls.Add(this.lb_ID);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.lb_ParentTitle);
            this.panel1.Controls.Add(this.btn_Save);
            this.panel1.Controls.Add(this.tb_Remark);
            this.panel1.Controls.Add(this.tb_Title);
            this.panel1.Controls.Add(this.tb_Code);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(496, 120);
            this.panel1.TabIndex = 0;
            // 
            // btn_Clear
            // 
            this.btn_Clear.Location = new System.Drawing.Point(273, 86);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(50, 23);
            this.btn_Clear.TabIndex = 12;
            this.btn_Clear.Text = "清除";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(219, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "序号";
            // 
            // lb_ParentTitle
            // 
            this.lb_ParentTitle.AutoSize = true;
            this.lb_ParentTitle.Location = new System.Drawing.Point(77, 9);
            this.lb_ParentTitle.Name = "lb_ParentTitle";
            this.lb_ParentTitle.Size = new System.Drawing.Size(23, 12);
            this.lb_ParentTitle.TabIndex = 9;
            this.lb_ParentTitle.Text = "...";
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(133, 86);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(50, 23);
            this.btn_Save.TabIndex = 7;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // tb_Remark
            // 
            this.tb_Remark.Location = new System.Drawing.Point(79, 59);
            this.tb_Remark.Name = "tb_Remark";
            this.tb_Remark.Size = new System.Drawing.Size(318, 21);
            this.tb_Remark.TabIndex = 6;
            // 
            // tb_Title
            // 
            this.tb_Title.Location = new System.Drawing.Point(256, 30);
            this.tb_Title.Name = "tb_Title";
            this.tb_Title.Size = new System.Drawing.Size(141, 21);
            this.tb_Title.TabIndex = 5;
            // 
            // tb_Code
            // 
            this.tb_Code.Location = new System.Drawing.Point(79, 30);
            this.tb_Code.Name = "tb_Code";
            this.tb_Code.Size = new System.Drawing.Size(126, 21);
            this.tb_Code.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "备注";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(217, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "名称";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(25, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = " 编码";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "目录";
            // 
            // ParentID
            // 
            this.ParentID.DataPropertyName = "ParentID";
            this.ParentID.HeaderText = "ParentID";
            this.ParentID.Name = "ParentID";
            this.ParentID.ReadOnly = true;
            this.ParentID.Visible = false;
            // 
            // Remark
            // 
            this.Remark.DataPropertyName = "Remark";
            this.Remark.HeaderText = "备注";
            this.Remark.Name = "Remark";
            this.Remark.ReadOnly = true;
            // 
            // tv_left
            // 
            this.tv_left.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_left.Location = new System.Drawing.Point(0, 33);
            this.tv_left.Name = "tv_left";
            this.tv_left.Size = new System.Drawing.Size(172, 440);
            this.tv_left.TabIndex = 1;
            this.tv_left.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_left_NodeMouseClick);
            // 
            // EditTime
            // 
            this.EditTime.DataPropertyName = "EditTime";
            this.EditTime.HeaderText = "修改日期";
            this.EditTime.Name = "EditTime";
            this.EditTime.ReadOnly = true;
            // 
            // QueryToolStripBtn
            // 
            this.QueryToolStripBtn.Image = ((System.Drawing.Image)(resources.GetObject("QueryToolStripBtn.Image")));
            this.QueryToolStripBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.QueryToolStripBtn.Name = "QueryToolStripBtn";
            this.QueryToolStripBtn.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.QueryToolStripBtn.Size = new System.Drawing.Size(49, 30);
            this.QueryToolStripBtn.Text = "搜索";
            this.QueryToolStripBtn.ToolTipText = "搜索";
            this.QueryToolStripBtn.Click += new System.EventHandler(this.QueryToolStripBtn_Click);
            // 
            // ClearToolStripBtn
            // 
            this.ClearToolStripBtn.Image = ((System.Drawing.Image)(resources.GetObject("ClearToolStripBtn.Image")));
            this.ClearToolStripBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ClearToolStripBtn.Name = "ClearToolStripBtn";
            this.ClearToolStripBtn.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.ClearToolStripBtn.Size = new System.Drawing.Size(49, 30);
            this.ClearToolStripBtn.Text = "刷新";
            this.ClearToolStripBtn.ToolTipText = "刷新";
            this.ClearToolStripBtn.Click += new System.EventHandler(this.ClearToolStripBtn_Click);
            // 
            // QueryToolStripTb
            // 
            this.QueryToolStripTb.Name = "QueryToolStripTb";
            this.QueryToolStripTb.Size = new System.Drawing.Size(80, 33);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.QueryToolStripTb,
            this.QueryToolStripBtn,
            this.ClearToolStripBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(172, 33);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tv_left);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(672, 473);
            this.splitContainer1.SplitterDistance = 172;
            this.splitContainer1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv_Data);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 120);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(496, 353);
            this.panel2.TabIndex = 1;
            // 
            // dgv_Data
            // 
            this.dgv_Data.AllowUserToAddRows = false;
            this.dgv_Data.AllowUserToDeleteRows = false;
            this.dgv_Data.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Data.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.编辑,
            this.删除,
            this.ID,
            this.Code,
            this.Title,
            this.CreateTime,
            this.EditTime,
            this.Remark,
            this.ParentID});
            this.dgv_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Data.Location = new System.Drawing.Point(0, 0);
            this.dgv_Data.Name = "dgv_Data";
            this.dgv_Data.ReadOnly = true;
            this.dgv_Data.RowTemplate.Height = 23;
            this.dgv_Data.Size = new System.Drawing.Size(496, 353);
            this.dgv_Data.TabIndex = 0;
            this.dgv_Data.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_Data_CellMouseDoubleClick);
            // 
            // 编辑
            // 
            dataGridViewCellStyle3.NullValue = "编辑";
            this.编辑.DefaultCellStyle = dataGridViewCellStyle3;
            this.编辑.HeaderText = "编辑";
            this.编辑.Name = "编辑";
            this.编辑.ReadOnly = true;
            this.编辑.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.编辑.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.编辑.Text = "";
            this.编辑.Width = 60;
            // 
            // 删除
            // 
            dataGridViewCellStyle4.NullValue = "删除";
            this.删除.DefaultCellStyle = dataGridViewCellStyle4;
            this.删除.HeaderText = "删除";
            this.删除.Name = "删除";
            this.删除.ReadOnly = true;
            this.删除.Width = 60;
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.HeaderText = "序号";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 60;
            // 
            // Code
            // 
            this.Code.DataPropertyName = "Code";
            this.Code.HeaderText = "编码";
            this.Code.Name = "Code";
            this.Code.ReadOnly = true;
            // 
            // Title
            // 
            this.Title.DataPropertyName = "Title";
            this.Title.HeaderText = "名称";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            // 
            // CreateTime
            // 
            this.CreateTime.DataPropertyName = "CreateTime";
            this.CreateTime.HeaderText = "创建日期";
            this.CreateTime.Name = "CreateTime";
            this.CreateTime.ReadOnly = true;
            // 
            // FormLabelType
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 473);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormLabelType";
            this.TabText = "模板标签类型";
            this.Text = "模板标签类型";
            this.Load += new System.EventHandler(this.FormLabelType_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Data)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lb_ID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lb_ParentTitle;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.TextBox tb_Remark;
        private System.Windows.Forms.TextBox tb_Title;
        private System.Windows.Forms.TextBox tb_Code;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark;
        private System.Windows.Forms.TreeView tv_left;
        private System.Windows.Forms.DataGridViewTextBoxColumn EditTime;
        private System.Windows.Forms.ToolStripButton QueryToolStripBtn;
        private System.Windows.Forms.ToolStripButton ClearToolStripBtn;
        private System.Windows.Forms.ToolStripTextBox QueryToolStripTb;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgv_Data;
        private System.Windows.Forms.DataGridViewLinkColumn 编辑;
        private System.Windows.Forms.DataGridViewLinkColumn 删除;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Code;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn CreateTime;

    }
}