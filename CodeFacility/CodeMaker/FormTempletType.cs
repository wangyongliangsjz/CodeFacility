using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DALFactory.CodeMaker;
using AccessDal.CodeMaker;
using Model.CodeMaker;

namespace CodeFacility.CodeMaker
{
    public partial class FormTempletType : FormWin
    {
        ITempletType menuDal = new TempletType();
        IList<TempletTypeInfo> menuList; //TreeView目录所有数据
        TempletTypeInfo ParentInfo; //父目录实体
        Boolean nodekey; //TreeView目录递归搜索 nodekey=true 执行 nodekey=false 停止
        int index = 0; //TreeView目录递归搜索序号
        int nodeid = 0; //TreeView目录递归搜索序号暂存
        int nodeMax1 = 0; //TreeView最后节点
        int nodeMax2 = 0; //TreeView当前节点

        public FormTempletType()
        {
            InitializeComponent();            
        }

        private void FormTempletType_Load(object sender, EventArgs e)
        {
            lb_ID.Text = "";
            lb_ParentTitle.Text = "根目录";
            QueryData();
            QueryFileType();
            InitOption();
        }

        private void InitOption()
        {
            panel1.BackColor = BaseConfigure.ColorTheme;
            tv_left.BackColor = BaseConfigure.ColorTheme;
            dgv_Data.RowsDefaultCellStyle.BackColor = BaseConfigure.ColorTheme;
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
                lb_ID.Text = id.ToString();
                tb_Code.Text = dgv_Data.Rows[rowindex].Cells["Code"].Value.ToString();
                tb_Code.Enabled = false;
                tb_Title.Text = dgv_Data.Rows[rowindex].Cells["Title"].Value.ToString();
                tb_Remark.Text = dgv_Data.Rows[rowindex].Cells["Remark"].Value.ToString();
                string FileTypeID = dgv_Data.Rows[rowindex].Cells["FileTypeID"].Value.ToString();

                if (FileTypeID =="")
                {
                    cb_FileType.SelectedValue = 0;
                }
                else
                {
                    cb_FileType.SelectedValue = int.Parse(FileTypeID);
                }
            }
            else if (columnindex == 1)  //删除
            {
                if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (menuDal.TempletTypeByParentIDGetList(Convert.ToInt32(id)).Count > 0)
                    {
                        MessageBox.Show("请删除子目录。");
                        return;
                    }
                    ITemplet tlDal = new Templet();
                    if (tlDal.TempletByParentIDGetList(Convert.ToInt32(id)).Count > 0)
                    {
                        MessageBox.Show("请删除子目录模板。");
                        return;
                    }
                    if (menuDal.TempletType_Del(Convert.ToInt32(id)) == 1)
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
            try
            {


                TempletTypeInfo info = new TempletTypeInfo();
                info.Code = tb_Code.Text.Trim();
                info.Title = tb_Title.Text.Trim();
                info.Remark = tb_Remark.Text.Trim();
                info.Alias = "";


                //if (info.Code == "")
                //{
                //    MessageBox.Show("请输入编码。");
                //    return;
                //}
                if (info.Title == "")
                {
                    MessageBox.Show("请输入名称。");
                    return;
                }

                int rst = -1;
                if (lb_ID.Text == "")
                {
                    if (ParentInfo == null)
                    {
                        //if (info.Code.Length != 3)
                        //{
                        //    MessageBox.Show("请输入3位长度编码。");
                        //    return;
                        //}

                        info.ParentID = 0;

                    }
                    else
                    {
                        //if (info.Code.Length - ParentInfo.Code.Length != 3)
                        //{
                        //    MessageBox.Show("请再输入3位长度编码。");
                        //    return;
                        //}
                        info.ParentID = ParentInfo.ID;
                    }
                    info.Code = GetMaxCode(info.ParentID);
                    rst = menuDal.TempletType_Add(info);
                }
                else
                {
                    info.ID = int.Parse(lb_ID.Text);
                    rst = menuDal.TempletType_Edit(info);
                }

                QueryData();

                if (rst == 1)
                    MessageBox.Show("保存成功。");
                else
                    MessageBox.Show("保存失败。");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        }
        /// <summary>
        /// 查询数据并显示
        /// </summary>
        private void QueryData()
        {
            QueryMenu();
            if (ParentInfo == null)
            {
                dgv_Data.DataSource = menuList;
            }
            else
            {
                IList<TempletTypeInfo> ilist = new List<TempletTypeInfo>();
                GetInfo(ilist, ParentInfo);
                dgv_Data.DataSource = ilist;
            }
        }
        /// <summary>
        /// 查询显示文件类型
        /// </summary>
        private void QueryFileType()
        {
            IFileType ftDal = new FileType();
            IList<FileTypeInfo> ftlist = ftDal.FileTypeGetList();
            IList<FileTypeInfo> ilist=new List<FileTypeInfo>();

            FileTypeInfo pinfo=new FileTypeInfo();
            pinfo.ID=0;
            GetFileTypeInfo(ftlist, ilist, pinfo);

            cb_FileType.Items.Clear();
            cb_FileType.DataSource = ilist;
            cb_FileType.DisplayMember = "Title";
            cb_FileType.ValueMember = "ID";
            //加一个空行
            //ListItem litem=new ListItem("","0");
            //cb_FileType.Items.Add(litem);
            cb_FileType.SelectedValue = 0;

        }

        /// <summary>
        /// 递归获取实体
        /// </summary>
        /// <param name="ilist">节点</param>
        /// <param name="ninfo">实体</param>
        private void GetFileTypeInfo(IList<FileTypeInfo> ftlist,IList<FileTypeInfo> ilist, FileTypeInfo ninfo)
        {
            if (ninfo.ID != 0)
            {
                string prefix = "";
                for (int i = 1; i < ninfo.Code.Length / 3; i++)
                {
                    prefix = prefix + "-";
                }
                ninfo.Title = prefix + ninfo.Title+"("+ninfo.Alias+")";
                ilist.Add(ninfo);
            }
            var list = from tl in ftlist
                       where tl.ParentID == ninfo.ID
                       select tl;
            foreach (FileTypeInfo info in list)
            {
                GetFileTypeInfo(ftlist,ilist, info);
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
            if(nodeMax1==nodeMax2)
            {
                nodeid=0;
                MessageBox.Show("搜索结束。");
            }
        }
        private void ClearToolStripBtn_Click(object sender, EventArgs e)
        {
            QueryToolStripTb.Text = "";
            nodeid = 0;
            QueryMenu();
            cb_FileType.SelectedValue = 0;

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
                        tb_Code.Text = info.Code;
                        if (info.FileTypeID != 0)
                        {
                            cb_FileType.SelectedValue = info.FileTypeID;
                        }
                        else
                        {
                            cb_FileType.SelectedValue = 0;
                        }
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
        private void GetInfo(IList<TempletTypeInfo> ilist, TempletTypeInfo ninfo)
        {
            if (ninfo.ID != 0)
            {
                ilist.Add(ninfo);
            }
            var list = from tl in menuList
                       where tl.ParentID == ninfo.ID
                       select tl;
            foreach (TempletTypeInfo info in list)
            {
                GetInfo(ilist, info);
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

        private void btn_FileType_Click(object sender, EventArgs e)
        {
            if (ParentInfo == null)
            {
                MessageBox.Show("请选择一级目录。");
                return;
            }
            if (ParentInfo.ParentID!=0)
            {
                MessageBox.Show("请选择一级目录。");
                return;
            }
            if (cb_FileType.SelectedValue.ToString() == "0")
            {
                MessageBox.Show("请选择文件夹名称。");
                return;
            }
            int FileTypeID = int.Parse(cb_FileType.SelectedValue.ToString());
            if (menuDal.TempletTypeByFileTypeID_Edit(FileTypeID, ParentInfo.ID) == 1)
                MessageBox.Show("保存成功。");
            else
                MessageBox.Show("保存失败。");

        }

        /// <summary>
        /// 获取最大code
        /// </summary>
        /// <param name="code"></param>
        private string GetMaxCode(int id)
        {
            var maxcode = string.Empty;
            var model = menuDal.TempletTypeGetInfo(id);
            var length = model == null || model.Code == null ? 0 : model.Code.Length;
            var list = menuDal.TempletTypeByParentIDGetList(id);
            if(list!=null && list.Count>0)
            {
                var listcode = list.Select(t => new { Code = t.Code.Substring(length, t.Code.Length - length) }).ToList();
                var maxid= listcode.Select(t => new { id = int.Parse(t.Code) }).Max(t=>t.id)+1;
                maxcode = maxid.ToString().PadLeft(3, '0');
            }
            else
            {
                maxcode = "001"; 
            }
            maxcode= model == null || model.Code == null ? maxcode : model.Code + maxcode;
            return maxcode;
        }


    }
}
