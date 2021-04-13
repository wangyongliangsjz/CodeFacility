namespace CodeFacility.CodeMaker
{
    partial class FormQueryConstrue
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormQueryConstrue));
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.RunToolScriptBtn = new System.Windows.Forms.ToolStripButton();
            this.StopToolStripBtn = new System.Windows.Forms.ToolStripButton();
            this.miEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.miEditFind = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditReplace = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditFindNext = new System.Windows.Forms.ToolStripMenuItem();
            this.miEditFindPrev = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.miToggleBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.miGoToNextBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.miGoToPrevBookmark = new System.Windows.Forms.ToolStripMenuItem();
            this.miOption = new System.Windows.Forms.ToolStripMenuItem();
            this.miSplitWindow = new System.Windows.Forms.ToolStripMenuItem();
            this.miShowSpacesTabs = new System.Windows.Forms.ToolStripMenuItem();
            this.miShowEOLMarkers = new System.Windows.Forms.ToolStripMenuItem();
            this.miShowInvalidLines = new System.Windows.Forms.ToolStripMenuItem();
            this.miShowLineNumbers = new System.Windows.Forms.ToolStripMenuItem();
            this.miHLCurRow = new System.Windows.Forms.ToolStripMenuItem();
            this.miBracketMatchingStyle = new System.Windows.Forms.ToolStripMenuItem();
            this.miEnableVirtualSpace = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.miConvertTabsToSpaces = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetTabSize = new System.Windows.Forms.ToolStripMenuItem();
            this.miSetFont = new System.Windows.Forms.ToolStripMenuItem();
            this.DBToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.SaveToolStripBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripComboBox_TargetDbType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripBtn_Insert = new System.Windows.Forms.ToolStripButton();
            this.textEditorControl = new ICSharpCode.TextEditor.TextEditorControl();
            this.fontDialog = new System.Windows.Forms.FontDialog();
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
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
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
            this.tabControl1.Size = new System.Drawing.Size(895, 189);
            this.tabControl1.TabIndex = 2;
            this.tabControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tabControl1_KeyUp);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.DGV1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(887, 163);
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
            this.DGV1.Size = new System.Drawing.Size(881, 157);
            this.DGV1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.DGV2);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(887, 163);
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
            this.DGV2.Size = new System.Drawing.Size(881, 157);
            this.DGV2.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.DGV3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(887, 163);
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
            this.DGV3.Size = new System.Drawing.Size(881, 157);
            this.DGV3.TabIndex = 2;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.DGV4);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(887, 163);
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
            this.DGV4.Size = new System.Drawing.Size(881, 157);
            this.DGV4.TabIndex = 2;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.DGV5);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(887, 163);
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
            this.DGV5.Size = new System.Drawing.Size(881, 157);
            this.DGV5.TabIndex = 2;
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.rtb_Message);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(887, 163);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "消息";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // rtb_Message
            // 
            this.rtb_Message.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb_Message.Location = new System.Drawing.Point(0, 0);
            this.rtb_Message.Name = "rtb_Message";
            this.rtb_Message.Size = new System.Drawing.Size(887, 163);
            this.rtb_Message.TabIndex = 0;
            this.rtb_Message.Text = "";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tableLayoutPanel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer1.Size = new System.Drawing.Size(895, 498);
            this.splitContainer1.SplitterDistance = 305;
            this.splitContainer1.TabIndex = 4;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.toolStrip1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.textEditorControl, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.4918F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 89.50819F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(895, 305);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.RunToolScriptBtn,
            this.StopToolStripBtn,
            this.miEdit,
            this.miOption,
            this.DBToolStripComboBox,
            this.SaveToolStripBtn,
            this.toolStripLabel1,
            this.toolStripComboBox_TargetDbType,
            this.toolStripBtn_Insert});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(895, 31);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // RunToolScriptBtn
            // 
            this.RunToolScriptBtn.Image = ((System.Drawing.Image)(resources.GetObject("RunToolScriptBtn.Image")));
            this.RunToolScriptBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.RunToolScriptBtn.Name = "RunToolScriptBtn";
            this.RunToolScriptBtn.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.RunToolScriptBtn.Size = new System.Drawing.Size(52, 28);
            this.RunToolScriptBtn.Text = "执行";
            this.RunToolScriptBtn.Click += new System.EventHandler(this.RunToolScriptBtn_Click);
            // 
            // StopToolStripBtn
            // 
            this.StopToolStripBtn.Image = ((System.Drawing.Image)(resources.GetObject("StopToolStripBtn.Image")));
            this.StopToolStripBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.StopToolStripBtn.Name = "StopToolStripBtn";
            this.StopToolStripBtn.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.StopToolStripBtn.Size = new System.Drawing.Size(52, 28);
            this.StopToolStripBtn.Text = "停止";
            this.StopToolStripBtn.Click += new System.EventHandler(this.StopToolStripBtn_Click);
            // 
            // miEdit
            // 
            this.miEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEditCut,
            this.miEditCopy,
            this.miEditPaste,
            this.miEditDelete,
            this.toolStripMenuItem2,
            this.miEditFind,
            this.miEditReplace,
            this.miEditFindNext,
            this.miEditFindPrev,
            this.toolStripMenuItem3,
            this.miToggleBookmark,
            this.miGoToNextBookmark,
            this.miGoToPrevBookmark});
            this.miEdit.Name = "miEdit";
            this.miEdit.Size = new System.Drawing.Size(59, 31);
            this.miEdit.Text = "编辑(&E)";
            // 
            // miEditCut
            // 
            this.miEditCut.Image = ((System.Drawing.Image)(resources.GetObject("miEditCut.Image")));
            this.miEditCut.Name = "miEditCut";
            this.miEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.miEditCut.Size = new System.Drawing.Size(206, 22);
            this.miEditCut.Text = "剪切(&X)";
            this.miEditCut.Click += new System.EventHandler(this.miEditCut_Click);
            // 
            // miEditCopy
            // 
            this.miEditCopy.Image = ((System.Drawing.Image)(resources.GetObject("miEditCopy.Image")));
            this.miEditCopy.Name = "miEditCopy";
            this.miEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.miEditCopy.Size = new System.Drawing.Size(206, 22);
            this.miEditCopy.Text = "复制(&C)";
            this.miEditCopy.Click += new System.EventHandler(this.miEditCopy_Click);
            // 
            // miEditPaste
            // 
            this.miEditPaste.Image = ((System.Drawing.Image)(resources.GetObject("miEditPaste.Image")));
            this.miEditPaste.Name = "miEditPaste";
            this.miEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.miEditPaste.Size = new System.Drawing.Size(206, 22);
            this.miEditPaste.Text = "粘贴(&V)";
            this.miEditPaste.Click += new System.EventHandler(this.miEditPaste_Click);
            // 
            // miEditDelete
            // 
            this.miEditDelete.Image = ((System.Drawing.Image)(resources.GetObject("miEditDelete.Image")));
            this.miEditDelete.Name = "miEditDelete";
            this.miEditDelete.Size = new System.Drawing.Size(206, 22);
            this.miEditDelete.Text = "删除(&D)";
            this.miEditDelete.Click += new System.EventHandler(this.miEditDelete_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(203, 6);
            // 
            // miEditFind
            // 
            this.miEditFind.Image = ((System.Drawing.Image)(resources.GetObject("miEditFind.Image")));
            this.miEditFind.Name = "miEditFind";
            this.miEditFind.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.miEditFind.Size = new System.Drawing.Size(206, 22);
            this.miEditFind.Text = "查找(&F)...";
            this.miEditFind.Click += new System.EventHandler(this.miEditFind_Click);
            // 
            // miEditReplace
            // 
            this.miEditReplace.Name = "miEditReplace";
            this.miEditReplace.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            this.miEditReplace.Size = new System.Drawing.Size(206, 22);
            this.miEditReplace.Text = "替换(&R)...";
            this.miEditReplace.Click += new System.EventHandler(this.miEditReplace_Click);
            // 
            // miEditFindNext
            // 
            this.miEditFindNext.Image = ((System.Drawing.Image)(resources.GetObject("miEditFindNext.Image")));
            this.miEditFindNext.Name = "miEditFindNext";
            this.miEditFindNext.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.miEditFindNext.Size = new System.Drawing.Size(206, 22);
            this.miEditFindNext.Text = "查找下一个(&N)";
            this.miEditFindNext.Click += new System.EventHandler(this.miEditFindNext_Click);
            // 
            // miEditFindPrev
            // 
            this.miEditFindPrev.Name = "miEditFindPrev";
            this.miEditFindPrev.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F3)));
            this.miEditFindPrev.Size = new System.Drawing.Size(206, 22);
            this.miEditFindPrev.Text = "查找上一个(&P)";
            this.miEditFindPrev.Click += new System.EventHandler(this.miEditFindPrev_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(203, 6);
            // 
            // miToggleBookmark
            // 
            this.miToggleBookmark.Name = "miToggleBookmark";
            this.miToggleBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F2)));
            this.miToggleBookmark.Size = new System.Drawing.Size(206, 22);
            this.miToggleBookmark.Text = "设置/取消书签";
            this.miToggleBookmark.Click += new System.EventHandler(this.miToggleBookmark_Click);
            // 
            // miGoToNextBookmark
            // 
            this.miGoToNextBookmark.Name = "miGoToNextBookmark";
            this.miGoToNextBookmark.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.miGoToNextBookmark.Size = new System.Drawing.Size(206, 22);
            this.miGoToNextBookmark.Text = "转到下一书签";
            this.miGoToNextBookmark.Click += new System.EventHandler(this.miGoToNextBookmark_Click);
            // 
            // miGoToPrevBookmark
            // 
            this.miGoToPrevBookmark.Name = "miGoToPrevBookmark";
            this.miGoToPrevBookmark.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F2)));
            this.miGoToPrevBookmark.Size = new System.Drawing.Size(206, 22);
            this.miGoToPrevBookmark.Text = "转到前一书签";
            this.miGoToPrevBookmark.Click += new System.EventHandler(this.miGoToPrevBookmark_Click);
            // 
            // miOption
            // 
            this.miOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miSplitWindow,
            this.miShowSpacesTabs,
            this.miShowEOLMarkers,
            this.miShowInvalidLines,
            this.miShowLineNumbers,
            this.miHLCurRow,
            this.miBracketMatchingStyle,
            this.miEnableVirtualSpace,
            this.toolStripMenuItem4,
            this.miConvertTabsToSpaces,
            this.miSetTabSize,
            this.miSetFont});
            this.miOption.Name = "miOption";
            this.miOption.Size = new System.Drawing.Size(62, 31);
            this.miOption.Text = "选项(&O)";
            // 
            // miSplitWindow
            // 
            this.miSplitWindow.Image = ((System.Drawing.Image)(resources.GetObject("miSplitWindow.Image")));
            this.miSplitWindow.Name = "miSplitWindow";
            this.miSplitWindow.Size = new System.Drawing.Size(248, 22);
            this.miSplitWindow.Text = "拆分窗口(&W)";
            this.miSplitWindow.Click += new System.EventHandler(this.miSplitWindow_Click);
            // 
            // miShowSpacesTabs
            // 
            this.miShowSpacesTabs.Name = "miShowSpacesTabs";
            this.miShowSpacesTabs.Size = new System.Drawing.Size(248, 22);
            this.miShowSpacesTabs.Text = "显示空格和制表符(&S)";
            this.miShowSpacesTabs.Click += new System.EventHandler(this.miShowSpacesTabs_Click);
            // 
            // miShowEOLMarkers
            // 
            this.miShowEOLMarkers.Name = "miShowEOLMarkers";
            this.miShowEOLMarkers.Size = new System.Drawing.Size(248, 22);
            this.miShowEOLMarkers.Text = "显示换行标记(&E)";
            this.miShowEOLMarkers.Click += new System.EventHandler(this.miShowEOLMarkers_Click);
            // 
            // miShowInvalidLines
            // 
            this.miShowInvalidLines.Name = "miShowInvalidLines";
            this.miShowInvalidLines.Size = new System.Drawing.Size(248, 22);
            this.miShowInvalidLines.Text = "显示无效行标记(&I)";
            this.miShowInvalidLines.Click += new System.EventHandler(this.miShowInvalidLines_Click);
            // 
            // miShowLineNumbers
            // 
            this.miShowLineNumbers.Name = "miShowLineNumbers";
            this.miShowLineNumbers.Size = new System.Drawing.Size(248, 22);
            this.miShowLineNumbers.Text = "显示行号(&L)";
            this.miShowLineNumbers.Click += new System.EventHandler(this.miShowLineNumbers_Click);
            // 
            // miHLCurRow
            // 
            this.miHLCurRow.Image = ((System.Drawing.Image)(resources.GetObject("miHLCurRow.Image")));
            this.miHLCurRow.Name = "miHLCurRow";
            this.miHLCurRow.Size = new System.Drawing.Size(248, 22);
            this.miHLCurRow.Text = "高亮当前行(&H)";
            this.miHLCurRow.Click += new System.EventHandler(this.miHLCurRow_Click);
            // 
            // miBracketMatchingStyle
            // 
            this.miBracketMatchingStyle.Name = "miBracketMatchingStyle";
            this.miBracketMatchingStyle.Size = new System.Drawing.Size(248, 22);
            this.miBracketMatchingStyle.Text = "高亮匹配括号当光标在其后时(&A)";
            this.miBracketMatchingStyle.Visible = false;
            this.miBracketMatchingStyle.Click += new System.EventHandler(this.miBracketMatchingStyle_Click);
            // 
            // miEnableVirtualSpace
            // 
            this.miEnableVirtualSpace.Name = "miEnableVirtualSpace";
            this.miEnableVirtualSpace.Size = new System.Drawing.Size(248, 22);
            this.miEnableVirtualSpace.Text = "启用虚空格(&V)";
            this.miEnableVirtualSpace.Click += new System.EventHandler(this.miEnableVirtualSpace_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(245, 6);
            // 
            // miConvertTabsToSpaces
            // 
            this.miConvertTabsToSpaces.Name = "miConvertTabsToSpaces";
            this.miConvertTabsToSpaces.Size = new System.Drawing.Size(248, 22);
            this.miConvertTabsToSpaces.Text = "制表符转换为空格(&C)";
            this.miConvertTabsToSpaces.Click += new System.EventHandler(this.miConvertTabsToSpaces_Click);
            // 
            // miSetTabSize
            // 
            this.miSetTabSize.Name = "miSetTabSize";
            this.miSetTabSize.Size = new System.Drawing.Size(248, 22);
            this.miSetTabSize.Text = "设置制表符大小(&T)";
            this.miSetTabSize.Click += new System.EventHandler(this.miSetTabSize_Click);
            // 
            // miSetFont
            // 
            this.miSetFont.Image = ((System.Drawing.Image)(resources.GetObject("miSetFont.Image")));
            this.miSetFont.Name = "miSetFont";
            this.miSetFont.Size = new System.Drawing.Size(248, 22);
            this.miSetFont.Text = "字体(&F)";
            this.miSetFont.Click += new System.EventHandler(this.miSetFont_Click);
            // 
            // DBToolStripComboBox
            // 
            this.DBToolStripComboBox.Name = "DBToolStripComboBox";
            this.DBToolStripComboBox.Size = new System.Drawing.Size(230, 31);
            // 
            // SaveToolStripBtn
            // 
            this.SaveToolStripBtn.Image = ((System.Drawing.Image)(resources.GetObject("SaveToolStripBtn.Image")));
            this.SaveToolStripBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SaveToolStripBtn.Name = "SaveToolStripBtn";
            this.SaveToolStripBtn.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.SaveToolStripBtn.Size = new System.Drawing.Size(52, 28);
            this.SaveToolStripBtn.Text = "导出";
            this.SaveToolStripBtn.Click += new System.EventHandler(this.SaveToolStripBtn_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(92, 28);
            this.toolStripLabel1.Text = "目标数据库类型";
            // 
            // toolStripComboBox_TargetDbType
            // 
            this.toolStripComboBox_TargetDbType.Items.AddRange(new object[] {
            "SQLServer",
            "Oracle",
            "MySql"});
            this.toolStripComboBox_TargetDbType.Name = "toolStripComboBox_TargetDbType";
            this.toolStripComboBox_TargetDbType.Size = new System.Drawing.Size(100, 31);
            // 
            // toolStripBtn_Insert
            // 
            this.toolStripBtn_Insert.Image = ((System.Drawing.Image)(resources.GetObject("toolStripBtn_Insert.Image")));
            this.toolStripBtn_Insert.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripBtn_Insert.Name = "toolStripBtn_Insert";
            this.toolStripBtn_Insert.Padding = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.toolStripBtn_Insert.Size = new System.Drawing.Size(85, 28);
            this.toolStripBtn_Insert.Text = "复制Insert";
            this.toolStripBtn_Insert.Click += new System.EventHandler(this.toolStripBtn_Insert_Click);
            // 
            // textEditorControl
            // 
            this.textEditorControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.textEditorControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textEditorControl.IsReadOnly = false;
            this.textEditorControl.Location = new System.Drawing.Point(3, 34);
            this.textEditorControl.Name = "textEditorControl";
            this.textEditorControl.Size = new System.Drawing.Size(889, 268);
            this.textEditorControl.TabIndex = 3;
            this.textEditorControl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textEditorControl_KeyUp);
            // 
            // FormQueryConstrue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 498);
            this.Controls.Add(this.splitContainer1);
            this.Name = "FormQueryConstrue";
            this.TabText = "查询分析器";
            this.Text = "查询分析器";
            this.Load += new System.EventHandler(this.FormQueryConstrue_Load);
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
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView DGV1;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.DataGridView DGV2;
        private System.Windows.Forms.DataGridView DGV3;
        private System.Windows.Forms.DataGridView DGV4;
        private System.Windows.Forms.DataGridView DGV5;
        private System.Windows.Forms.RichTextBox rtb_Message;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton RunToolScriptBtn;
        private System.Windows.Forms.ToolStripButton StopToolStripBtn;
        private System.Windows.Forms.ToolStripMenuItem miEdit;
        private System.Windows.Forms.ToolStripMenuItem miEditCut;
        private System.Windows.Forms.ToolStripMenuItem miEditCopy;
        private System.Windows.Forms.ToolStripMenuItem miEditPaste;
        private System.Windows.Forms.ToolStripMenuItem miEditDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem miEditFind;
        private System.Windows.Forms.ToolStripMenuItem miEditReplace;
        private System.Windows.Forms.ToolStripMenuItem miEditFindNext;
        private System.Windows.Forms.ToolStripMenuItem miEditFindPrev;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem miToggleBookmark;
        private System.Windows.Forms.ToolStripMenuItem miGoToNextBookmark;
        private System.Windows.Forms.ToolStripMenuItem miGoToPrevBookmark;
        private System.Windows.Forms.ToolStripMenuItem miOption;
        private System.Windows.Forms.ToolStripMenuItem miSplitWindow;
        private System.Windows.Forms.ToolStripMenuItem miShowSpacesTabs;
        private System.Windows.Forms.ToolStripMenuItem miShowEOLMarkers;
        private System.Windows.Forms.ToolStripMenuItem miShowInvalidLines;
        private System.Windows.Forms.ToolStripMenuItem miShowLineNumbers;
        private System.Windows.Forms.ToolStripMenuItem miHLCurRow;
        private System.Windows.Forms.ToolStripMenuItem miBracketMatchingStyle;
        private System.Windows.Forms.ToolStripMenuItem miEnableVirtualSpace;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem miConvertTabsToSpaces;
        private System.Windows.Forms.ToolStripMenuItem miSetTabSize;
        private System.Windows.Forms.ToolStripMenuItem miSetFont;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.ToolStripComboBox DBToolStripComboBox;
        private ICSharpCode.TextEditor.TextEditorControl textEditorControl;
        private System.Windows.Forms.ToolStripButton SaveToolStripBtn;
        private System.Windows.Forms.ToolStripButton toolStripBtn_Insert;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox_TargetDbType;

    }
}