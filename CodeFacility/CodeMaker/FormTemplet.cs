using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;

using DALFactory.CodeMaker;
using AccessDal.CodeMaker;
using Model.CodeMaker;

namespace CodeFacility.CodeMaker
{
    public partial class FormTemplet : FormWin
    {
        ITempletType menuDal = new TempletType();
        IList<TempletTypeInfo> menuList; //TreeView目录所有数据
        TempletTypeInfo ParentInfo; //父目录实体
        Boolean nodekey; //TreeView目录递归搜索 nodekey=true 执行 nodekey=false 停止
        int index = 0; //TreeView目录递归搜索序号
        int nodeid = 0; //TreeView目录递归搜索序号暂存
        int nodeMax1 = 0; //TreeView最后节点
        int nodeMax2 = 0; //TreeView当前节点
        ITemplet dal = new Templet();

        public FormTemplet()
        {
            InitializeComponent();
        }

        private void FormTemplet_Load(object sender, EventArgs e)
        {
            lb_ID.Text = "";
            lb_ParentTitle.Text = "根目录";
            ini();
            QueryMenu();
            QueryData();
            GetPostfix();
            InitOption();
        }

        private void InitOption()
        {
            tv_left.BackColor = BaseConfigure.ColorTheme;
            panel1.BackColor = BaseConfigure.ColorTheme;
            textEditorControl.BackColor = BaseConfigure.ColorTheme;
            textEditorControl.Font = new System.Drawing.Font(BaseConfigure.FontTypeface, BaseConfigure.FontSize, FontStyle.Regular);
        }


        private void ini()
        {
            textEditorControl.ShowEOLMarkers = false;
            textEditorControl.ShowHRuler = false;
            textEditorControl.ShowInvalidLines = false;
            textEditorControl.ShowLineNumbers = true;
            textEditorControl.ShowMatchingBracket = true;
            textEditorControl.ShowSpaces = false;
            textEditorControl.ShowTabs = false;
            textEditorControl.ShowVRuler = true;
            textEditorControl.AllowCaretBeyondEOL = false;
            textEditorControl.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("XML");
            textEditorControl.Encoding = Encoding.GetEncoding("GB2312");
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void dgv_Data_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int rowindex = e.RowIndex;
            int columnindex = e.ColumnIndex;
            string id = dgv_Data.Rows[rowindex].Cells["ID"].Value.ToString();
            if (columnindex == 0) //编辑
            {
                lb_ID.Text = id;
                tb_Code.Text = dgv_Data.Rows[rowindex].Cells["Code"].Value.ToString();
                tb_Title.Text = dgv_Data.Rows[rowindex].Cells["Title"].Value.ToString();
                tb_Remark.Text = dgv_Data.Rows[rowindex].Cells["Remark"].Value.ToString();
                cb_Postfix.Text = dgv_Data.Rows[rowindex].Cells["Postfix"].Value.ToString();

                IUpLoadFile ufDal= new AccessDal.CodeMaker.UpLoadFile();
                UpLoadFileInfo upfInfo = ufDal.UpLoadFileGetInfo("Cm_Templet", int.Parse(id));
                if (upfInfo != null)
                {
                    string fpath = AppDomain.CurrentDomain.BaseDirectory + upfInfo.FilePath + "\\" + upfInfo.UniqueName;
                    string Content = Common.FileHandle.ReadFile(fpath);
                    textEditorControl.Text = Content;
                }
            }
            else if (columnindex == 1)  //删除
            {
                if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    IUpLoadFile ufDal = new AccessDal.CodeMaker.UpLoadFile();
                    UpLoadFileInfo upfInfo = new UpLoadFileInfo();
                    upfInfo = ufDal.UpLoadFileGetInfo("Cm_Templet",int.Parse(id));
                    if (upfInfo != null && upfInfo.FileID != 0)
                    {
                        string fpath = AppDomain.CurrentDomain.BaseDirectory + upfInfo.FilePath + "\\" + upfInfo.UniqueName;
                        Common.FileHandle.DeleteFile(fpath);
                        if (ufDal.UpLoadFile_Del(upfInfo.FileID) != 1)
                        {
                            MessageBox.Show("删除失败。");
                            return;
                        }
                    }

                    if (dal.Templet_Del(int.Parse(id)) == 1)
                    {
                        QueryData();
                        MessageBox.Show("删除成功。");
                    }
                    else
                        MessageBox.Show("删除失败。");
                }
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearInput();
        }

        #region 数据相关方法
        /// <summary>
        /// 保存数据
        /// </summary>
        private void SaveData()
        {
            TempletInfo info = new TempletInfo();
            info.Code = tb_Code.Text.Trim();
            info.Title = tb_Title.Text.Trim();
            info.Content = "";
            info.Remark = tb_Remark.Text.Trim();
            info.Postfix = cb_Postfix.Text;
            info.Path = "";
            string Content = textEditorControl.Text;

            if (ParentInfo == null && lb_ID.Text =="")
            {
                MessageBox.Show("请选择目录。");
                return;
            }
            if (info.Title == "")
            {
                MessageBox.Show("请输入名称。");
                return;
            }
            if (Content == "")
            {
                MessageBox.Show("请输入内容。");
                return;
            }
            if (info.Postfix == "")
            {
                MessageBox.Show("请选择后缀名称。");
                return;
            }

            int rst = -1;
            int TempletID = 0;
            if (lb_ID.Text == "")
            {
                
                info.ParentID = ParentInfo.ID;
                rst = dal.Templet_Add(info,out TempletID);
            }
            else
            {
                info.ID = int.Parse(lb_ID.Text);
                TempletID = info.ID;
                rst = dal.Templet_Edit(info);
            }

            if (rst == 1)
            {
                IUpLoadFile ufDal = new AccessDal.CodeMaker.UpLoadFile();
                UpLoadFileInfo upfInfo = new UpLoadFileInfo();

                upfInfo = ufDal.UpLoadFileGetInfo("Cm_Templet", TempletID);
                if (upfInfo != null && upfInfo.FileID != 0)
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + upfInfo.FilePath + "\\" + upfInfo.UniqueName;
                    Common.FileHandle.DeleteFile(path);
                    if (ufDal.UpLoadFile_Del(upfInfo.FileID) != 1)
                    {
                        MessageBox.Show("操作失败。");
                        return;
                    }
                }

                TempletInfo tinfo = dal.TempletGetInfo(TempletID);
                TempletTypeInfo tltinfo = menuDal.TempletTypeGetInfo(tinfo.ParentID);
                string fpath = CodeFile.GetFilePath(tltinfo);
                string filepath = AppDomain.CurrentDomain.BaseDirectory + fpath;

                rst = Common.FileHandle.WirteCreateFile(Content, filepath + "\\" + info.Title + "." + info.Postfix);
                if (rst == 1)
                {
                    upfInfo.TableName = "Cm_Templet";
                    upfInfo.RootField = "";
                    upfInfo.RootId = "";
                    upfInfo.ParentField = "";
                    upfInfo.ParentId = TempletID.ToString();
                    upfInfo.FilePath = fpath;
                    upfInfo.Remark = "";
                    upfInfo.FileSize = 0;
                    upfInfo.FileName = info.Title;
                    upfInfo.ExpandName = info.Postfix;
                    upfInfo.UniqueName = info.Title + "." + info.Postfix;
                    upfInfo.FileTypeId = tltinfo.ID;
                    upfInfo.AttachmentId = tltinfo.Code;
                    rst = ufDal.UpLoadFile_Add(upfInfo);
                }
            }

            QueryData();

            if (rst == 1)
                MessageBox.Show("保存成功。");
            else
                MessageBox.Show("保存失败。");
        }

        /// <summary>
        /// 清除录入数据
        /// </summary>
        private void ClearInput()
        {
            lb_ID.Text = "";
            tb_Code.Text = "";
            tb_Title.Text = "";
            tb_Remark.Text = "";
            textEditorControl.Text = "";
        }
        /// <summary>
        /// 查询数据并显示
        /// </summary>
        private void QueryData()
        {
            IList<TempletInfo> datalist = dal.TempletGetList();
            if (ParentInfo == null)
            {
                dgv_Data.DataSource = datalist;
            }
            else
            {
                IList<TempletInfo> ilist = new List<TempletInfo>();
                GetDataInfo(datalist,ilist, ParentInfo);
                dgv_Data.DataSource = ilist;
            }
        }

        private void GetPostfix()
        {
            IPostfix pdal = new AccessDal.CodeMaker.Postfix();
            IList<PostfixInfo> ilist = pdal.PostfixGetList();
            cb_Postfix.DataSource = ilist;
            cb_Postfix.DisplayMember = "Postfix";
            cb_Postfix.ValueMember = "ID";
            cb_Postfix.SelectedValue = 0;
        }
        
        /// <summary>
        /// 递归获取数据实体
        /// </summary>
        /// <param name="datalist">数据列表</param>
        /// <param name="ilist">返回数据列表</param>
        /// <param name="ninfo">实体</param>
        private void GetDataInfo(IList<TempletInfo> datalist, IList<TempletInfo> ilist, TempletTypeInfo ninfo)
        {
            var dlist = from dl in datalist
                       where dl.ParentID == ninfo.ID
                       select dl;
            foreach (TempletInfo dinfo in dlist)
            {
                ilist.Add(dinfo);
            }
            var list = from tl in menuList
                       where tl.ParentID == ninfo.ID
                       select tl;
            foreach (TempletTypeInfo info in list)
            {
                GetDataInfo(datalist, ilist, info);
            }
        }
        #endregion

        #region 菜单Treeview相关事件
        private void QueryToolStripBtn_Click(object sender, EventArgs e)
        {
            tv_left.Focus();
            index = 0;
            nodekey = true;
            nodeMax2 = 0;
            string title = QueryToolStripTb.Text;
            TreeNodeCollection tnc = tv_left.Nodes;
            QueryNode(tnc, title);
            if (nodeMax1 == nodeMax2 && nodeMax1 != 0 && nodeMax2 != 0)
            {
                nodeid = 0;
                MessageBox.Show("搜索结束。");
            }
        }
        private void ClearToolStripBtn_Click(object sender, EventArgs e)
        {
            QueryToolStripTb.Text = "";
            nodeid = 0;
            QueryMenu();
        }
        private void tv_left_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            tv_left.SelectedNode = e.Node;
            if (tv_left.SelectedNode != null)
            {
                ClearInput();
                if (tv_left.SelectedNode.Text == "根目录")
                {
                    ParentInfo = null;
                    lb_ParentTitle.Text = "根目录";
                }
                else
                {
                    TempletTypeInfo info = tv_left.SelectedNode.Tag as TempletTypeInfo;
                    if (info != null)
                    {
                        ParentInfo = info;
                        lb_ParentTitle.Text = info.Title;
                        lb_ParentCode.Text = info.Code;
                    }
                }
                QueryData();
            }
        }
        #endregion

        #region 菜单Treeview相关方法
        /// <summary>
        /// 显示菜单Treeview
        /// </summary>
        private void QueryMenu()
        {
            nodeid = 0;
            menuList = menuDal.TempletTypeGetList();
            
            TreeNode tn = new TreeNode("根目录");
            TempletTypeInfo info = new TempletTypeInfo();
            info.ID = 0;
            GetNode(tn, info);
            tn.ExpandAll();
            tv_left.Nodes.Clear();
            tv_left.Nodes.Add(tn);

            nodeMax1 = 0;
            TreeNodeCollection tnc = tv_left.Nodes;
            GetNodeNameMax(tnc);
        }

        /// <summary>
        /// 递归获取目录节点
        /// </summary>
        /// <param name="tn">节点</param>
        /// <param name="tinfo">实体</param>
        private void GetNode(TreeNode tn, TempletTypeInfo tinfo)
        {
            var list = from tl in menuList
                       where tl.ParentID == tinfo.ID
                       select tl;
            foreach (TempletTypeInfo info in list)
            {
                TreeNode tnode = new TreeNode(info.Title);
                GetNode(tnode, info);
                tnode.Tag = info;
                tn.Nodes.Add(tnode);
            }
        }

        /// <summary>
        /// 递归获取实体
        /// </summary>
        /// <param name="ilist">节点</param>
        /// <param name="ninfo">实体</param>
        private void GetMenuInfo(IList<TempletTypeInfo> ilist, TempletTypeInfo ninfo)
        {
            ilist.Add(ninfo);
            var list = from tl in menuList
                       where tl.ParentID == ninfo.ID
                       select tl;
            foreach (TempletTypeInfo info in list)
            {
                GetMenuInfo(ilist, info);
            }
        }

        /// <summary>
        /// 菜单节点数量Treeview
        /// </summary>
        /// <param name="tnc">节点控件</param>
        /// <param name="title">节点标题</param> 
        private void GetNodeNameMax(TreeNodeCollection tnc)
        {
            foreach (TreeNode tn in tnc)
            {
                nodeMax1++;
                if (tn.Nodes.Count > 0)
                {
                    GetNodeNameMax(tn.Nodes);
                }
            }
        }

        /// <summary>
        /// 搜索菜单Treeview
        /// </summary>
        /// <param name="tnc">节点控件</param>
        /// <param name="title">节点标题</param> 
        private void QueryNode(TreeNodeCollection tnc, string title)
        {
            foreach (TreeNode tn in tnc)
            {
                nodeMax2++;
                if (tn.Text.IndexOf(title) > -1)
                {
                    index++;
                    if (index > nodeid)
                    {
                        tv_left.SelectedNode = tn;
                        nodeid = index;
                        nodekey = false;
                        return;
                    }
                }
                if (tn.Nodes.Count > 0 && nodekey)
                {
                    QueryNode(tn.Nodes, title);
                }
            }
        }
        #endregion

        private void chb_Rows_CheckedChanged(object sender, EventArgs e)
        {
            //if (chb_Rows.Checked)
            //{
            //    rtb_Content.WordWrap = true;
            //}
            //else
            //{
            //    rtb_Content.WordWrap = false;
            //}
        }

      

    }
}
