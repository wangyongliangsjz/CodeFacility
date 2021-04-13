namespace CodeFacility.CodeMaker
{
    partial class FormCreater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCreater));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tv_left = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.QueryToolStripTb = new System.Windows.Forms.ToolStripTextBox();
            this.QueryToolStripBtn = new System.Windows.Forms.ToolStripButton();
            this.ClearToolStripBtn = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textEditorControl = new ICSharpCode.TextEditor.TextEditorControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.btn_rightDown = new System.Windows.Forms.Button();
            this.btn_rightUp = new System.Windows.Forms.Button();
            this.btn_toLeftAll = new System.Windows.Forms.Button();
            this.btn_toLeft = new System.Windows.Forms.Button();
            this.btn_toRight = new System.Windows.Forms.Button();
            this.btn_toRightAll = new System.Windows.Forms.Button();
            this.dgv_left = new System.Windows.Forms.DataGridView();
            this.select = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ltName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ltDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_right = new System.Windows.Forms.DataGridView();
            this.rSelect = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.rName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rDescription = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rPrimaryKey = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rIsIdentity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rNullable = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rFieldType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chb_Rows = new System.Windows.Forms.CheckBox();
            this.btn_Copy = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lb_Templet = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lb_DbMessage = new System.Windows.Forms.Label();
            this.btn_Create = new System.Windows.Forms.Button();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_left)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_right)).BeginInit();
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
            this.splitContainer1.Panel1.Controls.Add(this.tv_left);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(672, 473);
            this.splitContainer1.SplitterDistance = 156;
            this.splitContainer1.TabIndex = 2;
            // 
            // tv_left
            // 
            this.tv_left.BackColor = System.Drawing.SystemColors.Window;
            this.tv_left.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_left.Location = new System.Drawing.Point(0, 34);
            this.tv_left.Name = "tv_left";
            this.tv_left.Size = new System.Drawing.Size(156, 439);
            this.tv_left.TabIndex = 1;
            this.tv_left.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_left_NodeMouseClick);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.QueryToolStripTb,
            this.QueryToolStripBtn,
            this.ClearToolStripBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(156, 34);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // QueryToolStripTb
            // 
            this.QueryToolStripTb.Name = "QueryToolStripTb";
            this.QueryToolStripTb.Size = new System.Drawing.Size(80, 34);
            // 
            // QueryToolStripBtn
            // 
            this.QueryToolStripBtn.Image = ((System.Drawing.Image)(resources.GetObject("QueryToolStripBtn.Image")));
            this.QueryToolStripBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.QueryToolStripBtn.Name = "QueryToolStripBtn";
            this.QueryToolStripBtn.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.QueryToolStripBtn.Size = new System.Drawing.Size(52, 31);
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
            this.ClearToolStripBtn.Size = new System.Drawing.Size(52, 31);
            this.ClearToolStripBtn.Text = "刷新";
            this.ClearToolStripBtn.ToolTipText = "刷新";
            this.ClearToolStripBtn.Click += new System.EventHandler(this.ClearToolStripBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 95);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(512, 378);
            this.panel2.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(512, 378);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.textEditorControl);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(504, 352);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "代码查看";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // textEditorControl
            // 
            this.textEditorControl.BackColor = System.Drawing.Color.Transparent;
            this.textEditorControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.textEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditorControl.IsReadOnly = false;
            this.textEditorControl.Location = new System.Drawing.Point(3, 3);
            this.textEditorControl.Name = "textEditorControl";
            this.textEditorControl.Size = new System.Drawing.Size(498, 346);
            this.textEditorControl.TabIndex = 5;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.tableLayoutPanel1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(504, 352);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "生成设置";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40F));
            this.tableLayoutPanel1.Controls.Add(this.panel6, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgv_left, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dgv_right, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(498, 346);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.Transparent;
            this.panel6.Controls.Add(this.btn_rightDown);
            this.panel6.Controls.Add(this.btn_rightUp);
            this.panel6.Controls.Add(this.btn_toLeftAll);
            this.panel6.Controls.Add(this.btn_toLeft);
            this.panel6.Controls.Add(this.btn_toRight);
            this.panel6.Controls.Add(this.btn_toRightAll);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(202, 3);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(93, 340);
            this.panel6.TabIndex = 0;
            // 
            // btn_rightDown
            // 
            this.btn_rightDown.Location = new System.Drawing.Point(8, 243);
            this.btn_rightDown.Name = "btn_rightDown";
            this.btn_rightDown.Size = new System.Drawing.Size(75, 23);
            this.btn_rightDown.TabIndex = 11;
            this.btn_rightDown.Text = "∨";
            this.btn_rightDown.UseVisualStyleBackColor = true;
            this.btn_rightDown.Click += new System.EventHandler(this.btn_rightDown_Click);
            // 
            // btn_rightUp
            // 
            this.btn_rightUp.Location = new System.Drawing.Point(8, 214);
            this.btn_rightUp.Name = "btn_rightUp";
            this.btn_rightUp.Size = new System.Drawing.Size(75, 23);
            this.btn_rightUp.TabIndex = 10;
            this.btn_rightUp.Text = "∧";
            this.btn_rightUp.UseVisualStyleBackColor = true;
            this.btn_rightUp.Click += new System.EventHandler(this.btn_rightUp_Click);
            // 
            // btn_toLeftAll
            // 
            this.btn_toLeftAll.Location = new System.Drawing.Point(8, 162);
            this.btn_toLeftAll.Name = "btn_toLeftAll";
            this.btn_toLeftAll.Size = new System.Drawing.Size(75, 23);
            this.btn_toLeftAll.TabIndex = 9;
            this.btn_toLeftAll.Text = "<<";
            this.btn_toLeftAll.UseVisualStyleBackColor = true;
            this.btn_toLeftAll.Click += new System.EventHandler(this.btn_toLeftAll_Click);
            // 
            // btn_toLeft
            // 
            this.btn_toLeft.Location = new System.Drawing.Point(8, 133);
            this.btn_toLeft.Name = "btn_toLeft";
            this.btn_toLeft.Size = new System.Drawing.Size(75, 23);
            this.btn_toLeft.TabIndex = 8;
            this.btn_toLeft.Text = "<";
            this.btn_toLeft.UseVisualStyleBackColor = true;
            this.btn_toLeft.Click += new System.EventHandler(this.btn_toLeft_Click);
            // 
            // btn_toRight
            // 
            this.btn_toRight.Location = new System.Drawing.Point(8, 104);
            this.btn_toRight.Name = "btn_toRight";
            this.btn_toRight.Size = new System.Drawing.Size(75, 23);
            this.btn_toRight.TabIndex = 7;
            this.btn_toRight.Text = ">";
            this.btn_toRight.UseVisualStyleBackColor = true;
            this.btn_toRight.Click += new System.EventHandler(this.btn_toRight_Click);
            // 
            // btn_toRightAll
            // 
            this.btn_toRightAll.Location = new System.Drawing.Point(8, 75);
            this.btn_toRightAll.Name = "btn_toRightAll";
            this.btn_toRightAll.Size = new System.Drawing.Size(75, 23);
            this.btn_toRightAll.TabIndex = 6;
            this.btn_toRightAll.Text = ">>";
            this.btn_toRightAll.UseVisualStyleBackColor = true;
            this.btn_toRightAll.Click += new System.EventHandler(this.btn_toRightAll_Click);
            // 
            // dgv_left
            // 
            this.dgv_left.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_left.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.select,
            this.ltName,
            this.ltDescription});
            this.dgv_left.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_left.Location = new System.Drawing.Point(3, 3);
            this.dgv_left.Name = "dgv_left";
            this.dgv_left.RowTemplate.Height = 23;
            this.dgv_left.Size = new System.Drawing.Size(193, 340);
            this.dgv_left.TabIndex = 1;
            // 
            // select
            // 
            this.select.HeaderText = "选择";
            this.select.Name = "select";
            this.select.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.select.Width = 50;
            // 
            // ltName
            // 
            this.ltName.DataPropertyName = "Name";
            this.ltName.HeaderText = "字段";
            this.ltName.Name = "ltName";
            // 
            // ltDescription
            // 
            this.ltDescription.DataPropertyName = "Description";
            this.ltDescription.HeaderText = "说明";
            this.ltDescription.Name = "ltDescription";
            // 
            // dgv_right
            // 
            this.dgv_right.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_right.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.rSelect,
            this.rName,
            this.rDescription,
            this.rPrimaryKey,
            this.rIsIdentity,
            this.rNullable,
            this.rFieldType});
            this.dgv_right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgv_right.Location = new System.Drawing.Point(301, 3);
            this.dgv_right.Name = "dgv_right";
            this.dgv_right.RowTemplate.Height = 23;
            this.dgv_right.Size = new System.Drawing.Size(194, 340);
            this.dgv_right.TabIndex = 2;
            // 
            // rSelect
            // 
            this.rSelect.HeaderText = "选择";
            this.rSelect.Name = "rSelect";
            this.rSelect.Width = 50;
            // 
            // rName
            // 
            this.rName.DataPropertyName = "Name";
            this.rName.HeaderText = "字段";
            this.rName.Name = "rName";
            this.rName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.rName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // rDescription
            // 
            this.rDescription.DataPropertyName = "Description";
            this.rDescription.HeaderText = "说明";
            this.rDescription.Name = "rDescription";
            this.rDescription.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.rDescription.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // rPrimaryKey
            // 
            this.rPrimaryKey.HeaderText = "PrimaryKey";
            this.rPrimaryKey.Name = "rPrimaryKey";
            // 
            // rIsIdentity
            // 
            this.rIsIdentity.HeaderText = "IsIdentity";
            this.rIsIdentity.Name = "rIsIdentity";
            // 
            // rNullable
            // 
            this.rNullable.HeaderText = "Nullable";
            this.rNullable.Name = "rNullable";
            // 
            // rFieldType
            // 
            this.rFieldType.HeaderText = "FieldType";
            this.rFieldType.Name = "rFieldType";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chb_Rows);
            this.panel1.Controls.Add(this.btn_Copy);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.lb_Templet);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lb_DbMessage);
            this.panel1.Controls.Add(this.btn_Create);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(512, 95);
            this.panel1.TabIndex = 0;
            // 
            // chb_Rows
            // 
            this.chb_Rows.AutoSize = true;
            this.chb_Rows.Location = new System.Drawing.Point(261, 56);
            this.chb_Rows.Name = "chb_Rows";
            this.chb_Rows.Size = new System.Drawing.Size(48, 16);
            this.chb_Rows.TabIndex = 24;
            this.chb_Rows.Text = "换行";
            this.chb_Rows.UseVisualStyleBackColor = true;
            this.chb_Rows.CheckedChanged += new System.EventHandler(this.chb_Rows_CheckedChanged);
            // 
            // btn_Copy
            // 
            this.btn_Copy.Location = new System.Drawing.Point(420, 52);
            this.btn_Copy.Name = "btn_Copy";
            this.btn_Copy.Size = new System.Drawing.Size(50, 23);
            this.btn_Copy.TabIndex = 23;
            this.btn_Copy.Text = "复制";
            this.btn_Copy.UseVisualStyleBackColor = true;
            this.btn_Copy.Click += new System.EventHandler(this.btn_Copy_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 22;
            this.label1.Text = "代码内容";
            // 
            // lb_Templet
            // 
            this.lb_Templet.AutoSize = true;
            this.lb_Templet.Location = new System.Drawing.Point(69, 33);
            this.lb_Templet.Name = "lb_Templet";
            this.lb_Templet.Size = new System.Drawing.Size(23, 12);
            this.lb_Templet.TabIndex = 21;
            this.lb_Templet.Text = "...";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(39, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 20;
            this.label3.Text = "模板";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "提示";
            // 
            // lb_DbMessage
            // 
            this.lb_DbMessage.AutoSize = true;
            this.lb_DbMessage.Location = new System.Drawing.Point(69, 9);
            this.lb_DbMessage.Name = "lb_DbMessage";
            this.lb_DbMessage.Size = new System.Drawing.Size(23, 12);
            this.lb_DbMessage.TabIndex = 18;
            this.lb_DbMessage.Text = "...";
            // 
            // btn_Create
            // 
            this.btn_Create.Location = new System.Drawing.Point(352, 52);
            this.btn_Create.Name = "btn_Create";
            this.btn_Create.Size = new System.Drawing.Size(50, 23);
            this.btn_Create.TabIndex = 7;
            this.btn_Create.Text = "生成";
            this.btn_Create.UseVisualStyleBackColor = true;
            this.btn_Create.Click += new System.EventHandler(this.btn_Create_Click);
            // 
            // FormCreater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 473);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormCreater";
            this.TabText = "表单代码生成器";
            this.Text = "表单代码生成器";
            this.Load += new System.EventHandler(this.FormCreater_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgv_left)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_right)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_Create;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStripButton ClearToolStripBtn;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tv_left;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox QueryToolStripTb;
        private System.Windows.Forms.ToolStripButton QueryToolStripBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lb_DbMessage;
        private System.Windows.Forms.Label lb_Templet;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Copy;
        private System.Windows.Forms.CheckBox chb_Rows;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ICSharpCode.TextEditor.TextEditorControl textEditorControl;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button btn_rightDown;
        private System.Windows.Forms.Button btn_rightUp;
        private System.Windows.Forms.Button btn_toLeftAll;
        private System.Windows.Forms.Button btn_toLeft;
        private System.Windows.Forms.Button btn_toRight;
        private System.Windows.Forms.Button btn_toRightAll;
        private System.Windows.Forms.DataGridView dgv_left;
        private System.Windows.Forms.DataGridView dgv_right;
        private System.Windows.Forms.DataGridViewCheckBoxColumn select;
        private System.Windows.Forms.DataGridViewTextBoxColumn ltName;
        private System.Windows.Forms.DataGridViewTextBoxColumn ltDescription;
        private System.Windows.Forms.DataGridViewCheckBoxColumn rSelect;
        private System.Windows.Forms.DataGridViewTextBoxColumn rName;
        private System.Windows.Forms.DataGridViewTextBoxColumn rDescription;
        private System.Windows.Forms.DataGridViewTextBoxColumn rPrimaryKey;
        private System.Windows.Forms.DataGridViewTextBoxColumn rIsIdentity;
        private System.Windows.Forms.DataGridViewTextBoxColumn rNullable;
        private System.Windows.Forms.DataGridViewTextBoxColumn rFieldType;
    }
}