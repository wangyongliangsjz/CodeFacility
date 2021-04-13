namespace CodeFacility.CodeMaker
{
    partial class ToolForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ToolForm));
            this.tv_Db = new System.Windows.Forms.TreeView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.LinkToolScriptBtn = new System.Windows.Forms.ToolStripButton();
            this.NotLinkToolStripBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.DelLinkToolStripBtn = new System.Windows.Forms.ToolStripButton();
            this.RefurbishToolStripBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tv_Db
            // 
            this.tv_Db.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tv_Db.Location = new System.Drawing.Point(0, 34);
            this.tv_Db.Name = "tv_Db";
            this.tv_Db.Size = new System.Drawing.Size(292, 439);
            this.tv_Db.TabIndex = 0;
            this.tv_Db.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_Db_NodeMouseClick);
            this.tv_Db.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tv_Db_KeyUp);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LinkToolScriptBtn,
            this.NotLinkToolStripBtn,
            this.toolStripButton1,
            this.DelLinkToolStripBtn,
            this.RefurbishToolStripBtn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(292, 34);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // LinkToolScriptBtn
            // 
            this.LinkToolScriptBtn.Image = ((System.Drawing.Image)(resources.GetObject("LinkToolScriptBtn.Image")));
            this.LinkToolScriptBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.LinkToolScriptBtn.Name = "LinkToolScriptBtn";
            this.LinkToolScriptBtn.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.LinkToolScriptBtn.Size = new System.Drawing.Size(52, 31);
            this.LinkToolScriptBtn.Text = "连接";
            this.LinkToolScriptBtn.Click += new System.EventHandler(this.LinkToolScriptBtn_Click);
            // 
            // NotLinkToolStripBtn
            // 
            this.NotLinkToolStripBtn.Image = ((System.Drawing.Image)(resources.GetObject("NotLinkToolStripBtn.Image")));
            this.NotLinkToolStripBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.NotLinkToolStripBtn.Name = "NotLinkToolStripBtn";
            this.NotLinkToolStripBtn.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.NotLinkToolStripBtn.Size = new System.Drawing.Size(52, 31);
            this.NotLinkToolStripBtn.Text = "断开";
            this.NotLinkToolStripBtn.Click += new System.EventHandler(this.LinkToolScriptBtn_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.toolStripButton1.Size = new System.Drawing.Size(64, 31);
            this.toolStripButton1.Text = "重连接";
            this.toolStripButton1.Click += new System.EventHandler(this.LinkToolScriptBtn_Click);
            // 
            // DelLinkToolStripBtn
            // 
            this.DelLinkToolStripBtn.Image = ((System.Drawing.Image)(resources.GetObject("DelLinkToolStripBtn.Image")));
            this.DelLinkToolStripBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DelLinkToolStripBtn.Name = "DelLinkToolStripBtn";
            this.DelLinkToolStripBtn.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.DelLinkToolStripBtn.Size = new System.Drawing.Size(52, 31);
            this.DelLinkToolStripBtn.Text = "删除";
            this.DelLinkToolStripBtn.ToolTipText = "注销";
            this.DelLinkToolStripBtn.Click += new System.EventHandler(this.LinkToolScriptBtn_Click);
            // 
            // RefurbishToolStripBtn
            // 
            this.RefurbishToolStripBtn.Image = ((System.Drawing.Image)(resources.GetObject("RefurbishToolStripBtn.Image")));
            this.RefurbishToolStripBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RefurbishToolStripBtn.Name = "RefurbishToolStripBtn";
            this.RefurbishToolStripBtn.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.RefurbishToolStripBtn.Size = new System.Drawing.Size(52, 31);
            this.RefurbishToolStripBtn.Text = "刷新";
            this.RefurbishToolStripBtn.ToolTipText = "注销";
            this.RefurbishToolStripBtn.Click += new System.EventHandler(this.LinkToolScriptBtn_Click);
            // 
            // ToolForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 473);
            this.Controls.Add(this.tv_Db);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ToolForm";
            this.TabText = "对象资源管理器";
            this.Text = "对象资源管理器";
            this.Load += new System.EventHandler(this.ToolForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView tv_Db;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton LinkToolScriptBtn;
        private System.Windows.Forms.ToolStripButton NotLinkToolStripBtn;
        private System.Windows.Forms.ToolStripButton DelLinkToolStripBtn;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton RefurbishToolStripBtn;


    }
}