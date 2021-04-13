namespace CodeFacility.CodeMaker
{
    partial class FormOption
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_FontSize = new System.Windows.Forms.TextBox();
            this.cb_ColorTheme = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "颜色主题";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(57, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "编辑器字体";
            // 
            // tb_FontSize
            // 
            this.tb_FontSize.Location = new System.Drawing.Point(135, 65);
            this.tb_FontSize.Name = "tb_FontSize";
            this.tb_FontSize.Size = new System.Drawing.Size(121, 21);
            this.tb_FontSize.TabIndex = 3;
            // 
            // cb_ColorTheme
            // 
            this.cb_ColorTheme.BackColor = System.Drawing.SystemColors.Window;
            this.cb_ColorTheme.FormattingEnabled = true;
            this.cb_ColorTheme.Items.AddRange(new object[] {
            "默认",
            "浅色",
            "深色"});
            this.cb_ColorTheme.Location = new System.Drawing.Point(135, 35);
            this.cb_ColorTheme.Name = "cb_ColorTheme";
            this.cb_ColorTheme.Size = new System.Drawing.Size(121, 20);
            this.cb_ColorTheme.TabIndex = 4;
            // 
            // FormOption
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(417, 303);
            this.Controls.Add(this.cb_ColorTheme);
            this.Controls.Add(this.tb_FontSize);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormOption";
            this.Text = "选项";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_FontSize;
        private System.Windows.Forms.ComboBox cb_ColorTheme;
    }
}