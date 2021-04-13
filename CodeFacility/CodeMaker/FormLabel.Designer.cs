namespace CodeFacility.CodeMaker
{
    partial class FormLabel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormLabel));
            this.dgv_Data = new System.Windows.Forms.DataGridView();
            this.编辑 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.删除 = new System.Windows.Forms.DataGridViewLinkColumn();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParentTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remark = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ParentID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Content = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel3 = new System.Windows.Forms.Panel();
            this.rtb_Content = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tv_left = new System.Windows.Forms.TreeView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chb_Rows = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.lb_ID = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.lb_ParentTitle = new System.Windows.Forms.Label();
            this.tb_Remark = new System.Windows.Forms.TextBox();
            this.tb_Title = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.QueryToolStripTb = new System.Windows.Forms.ToolStripTextBox();
            this.QueryToolStripBtn = new System.Windows.Forms.ToolStripButton();
            this.ClearToolStripBtn = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Data)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.Title,
            this.ParentTitle,
            this.Remark,
            this.ParentID,
            this.Content});
            this.dgv_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_Data.Location = new System.Drawing.Point(0, 0);
            this.dgv_Data.Name = "dgv_Data";
            this.dgv_Data.ReadOnly = true;
            this.dgv_Data.RowTemplate.Height = 23;
            this.dgv_Data.Size = new System.Drawing.Size(496, 231);
            this.dgv_Data.TabIndex = 0;
            this.dgv_Data.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_Data_CellMouseDoubleClick);
            // 
            // 编辑
            // 
            dataGridViewCellStyle1.NullValue = "编辑";
            this.编辑.DefaultCellStyle = dataGridViewCellStyle1;
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
            dataGridViewCellStyle2.NullValue = "删除";
            this.删除.DefaultCellStyle = dataGridViewCellStyle2;
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
            // Title
            // 
            this.Title.DataPropertyName = "Title";
            this.Title.HeaderText = "名称";
            this.Title.Name = "Title";
            this.Title.ReadOnly = true;
            // 
            // ParentTitle
            // 
            this.ParentTitle.DataPropertyName = "ParentTitle";
            this.ParentTitle.HeaderText = "类型";
            this.ParentTitle.Name = "ParentTitle";
            this.ParentTitle.ReadOnly = true;
            // 
            // Remark
            // 
            this.Remark.DataPropertyName = "Remark";
            this.Remark.HeaderText = "备注";
            this.Remark.Name = "Remark";
            this.Remark.ReadOnly = true;
            // 
            // ParentID
            // 
            this.ParentID.DataPropertyName = "ParentID";
            this.ParentID.HeaderText = "ParentID";
            this.ParentID.Name = "ParentID";
            this.ParentID.ReadOnly = true;
            this.ParentID.Visible = false;
            // 
            // Content
            // 
            this.Content.DataPropertyName = "Content";
            this.Content.HeaderText = "Content";
            this.Content.Name = "Content";
            this.Content.ReadOnly = true;
            this.Content.Visible = false;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rtb_Content);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 110);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(496, 132);
            this.panel3.TabIndex = 2;
            // 
            // rtb_Content
            // 
            this.rtb_Content.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_Content.Location = new System.Drawing.Point(0, 0);
            this.rtb_Content.Name = "rtb_Content";
            this.rtb_Content.Size = new System.Drawing.Size(496, 132);
            this.rtb_Content.TabIndex = 0;
            this.rtb_Content.Text = "";
            this.rtb_Content.WordWrap = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.dgv_Data);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 242);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(496, 231);
            this.panel2.TabIndex = 1;
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
            // panel1
            // 
            this.panel1.Controls.Add(this.chb_Rows);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btn_Clear);
            this.panel1.Controls.Add(this.lb_ID);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.btn_Save);
            this.panel1.Controls.Add(this.lb_ParentTitle);
            this.panel1.Controls.Add(this.tb_Remark);
            this.panel1.Controls.Add(this.tb_Title);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(496, 110);
            this.panel1.TabIndex = 0;
            // 
            // chb_Rows
            // 
            this.chb_Rows.AutoSize = true;
            this.chb_Rows.Location = new System.Drawing.Point(372, 85);
            this.chb_Rows.Name = "chb_Rows";
            this.chb_Rows.Size = new System.Drawing.Size(48, 16);
            this.chb_Rows.TabIndex = 25;
            this.chb_Rows.Text = "换行";
            this.chb_Rows.UseVisualStyleBackColor = true;
            this.chb_Rows.CheckedChanged += new System.EventHandler(this.chb_Rows_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "内容";
            // 
            // btn_Clear
            // 
            this.btn_Clear.Location = new System.Drawing.Point(266, 81);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(50, 23);
            this.btn_Clear.TabIndex = 12;
            this.btn_Clear.Text = "清除";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // lb_ID
            // 
            this.lb_ID.AutoSize = true;
            this.lb_ID.Location = new System.Drawing.Point(213, 9);
            this.lb_ID.Name = "lb_ID";
            this.lb_ID.Size = new System.Drawing.Size(23, 12);
            this.lb_ID.TabIndex = 11;
            this.lb_ID.Text = "...";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(178, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "序号";
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(168, 81);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(50, 23);
            this.btn_Save.TabIndex = 7;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
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
            // tb_Remark
            // 
            this.tb_Remark.Location = new System.Drawing.Point(79, 55);
            this.tb_Remark.Name = "tb_Remark";
            this.tb_Remark.Size = new System.Drawing.Size(383, 21);
            this.tb_Remark.TabIndex = 6;
            // 
            // tb_Title
            // 
            this.tb_Title.Location = new System.Drawing.Point(79, 30);
            this.tb_Title.Name = "tb_Title";
            this.tb_Title.Size = new System.Drawing.Size(237, 21);
            this.tb_Title.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(31, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 3;
            this.label4.Text = "备注";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(31, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "名称";
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
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(672, 473);
            this.splitContainer1.SplitterDistance = 172;
            this.splitContainer1.TabIndex = 2;
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
            // QueryToolStripTb
            // 
            this.QueryToolStripTb.Name = "QueryToolStripTb";
            this.QueryToolStripTb.Size = new System.Drawing.Size(80, 33);
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
            // FormLabel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 473);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormLabel";
            this.TabText = "模板标签";
            this.Text = "模板标签";
            this.Load += new System.EventHandler(this.FormLabel_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Data)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv_Data;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RichTextBox rtb_Content;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TreeView tv_left;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Label lb_ID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Label lb_ParentTitle;
        private System.Windows.Forms.TextBox tb_Remark;
        private System.Windows.Forms.TextBox tb_Title;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox QueryToolStripTb;
        private System.Windows.Forms.ToolStripButton QueryToolStripBtn;
        private System.Windows.Forms.ToolStripButton ClearToolStripBtn;
        private System.Windows.Forms.DataGridViewLinkColumn 编辑;
        private System.Windows.Forms.DataGridViewLinkColumn 删除;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParentTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn Remark;
        private System.Windows.Forms.DataGridViewTextBoxColumn ParentID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Content;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chb_Rows;
    }
}