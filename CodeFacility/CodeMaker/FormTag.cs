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
    public partial class FormTag : FormWin
    {
        ITagType menuDal = new TagType();
        IList<TagTypeInfo> menuList; //TreeView目录所有数据
        TagTypeInfo ParentInfo; //父目录实体
        Boolean nodekey; //TreeView目录递归搜索 nodekey=true 执行 nodekey=false 停止
        int index = 0; //TreeView目录递归搜索序号
        int nodeid = 0; //TreeView目录递归搜索序号暂存
        int nodeMax1 = 0; //TreeView最后节点
        int nodeMax2 = 0; //TreeView当前节点
        DALFactory.CodeMaker.ITag dal = new AccessDal.CodeMaker.Tag();

        public FormTag()
        {
            InitializeComponent();
        }

        private void FormTag_Load(object sender, EventArgs e)
        {
            lb_ID.Text = "";
            lb_ParentTitle.Text = "根目录";
            QueryMenu();
            QueryData();
            InitOption();
        }

        private void InitOption()
        {
            panel1.BackColor = BaseConfigure.ColorTheme;
            rtb_Content.BackColor = BaseConfigure.ColorTheme;
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
                tb_Title.Text = dgv_Data.Rows[rowindex].Cells["Title"].Value.ToString();
                tb_Remark.Text = dgv_Data.Rows[rowindex].Cells["Remark"].Value.ToString();
                rtb_Content.Text = dgv_Data.Rows[rowindex].Cells["Content"].Value.ToString();
            }
            else if (columnindex == 1)  //删除
            {
                if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (dal.Tag_Del(Convert.ToInt32(id)) == 1)
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
            TagInfo info = new TagInfo();
            info.Title = tb_Title.Text.Trim();
            info.Remark = tb_Remark.Text.Trim();
            info.Content = rtb_Content.Text;

            if (ParentInfo == null && lb_ID.Text == "")
            {
                MessageBox.Show("请选择目录。");
                return;
            }
            if (info.Title == "")
            {
                MessageBox.Show("请输入名称。");
                return;
            }
            if (info.Content == "")
            {
                MessageBox.Show("请输入内容。");
                return;
            }

            int rst = -1;
            if (lb_ID.Text == "")
            {
                info.ParentID = ParentInfo.ID;
                rst = dal.Tag_Add(info);
            }
            else
            {
                info.ID = int.Parse(lb_ID.Text);
                rst = dal.Tag_Edit(info);
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
            tb_Title.Text = "";
            tb_Remark.Text = "";
            rtb_Content.Text = "";
        }
        /// <summary>
        /// 查询数据并显示
        /// </summary>
        private void QueryData()
        {
            IList<TagInfo> datalist = dal.TagGetList();
            if (ParentInfo == null)
            {
                dgv_Data.DataSource = datalist;
            }
            else
            {
                IList<TagInfo> ilist = new List<TagInfo>();
                GetDataInfo(datalist, ilist, ParentInfo);
                dgv_Data.DataSource = ilist;
            }
        }

        /// <summary>
        /// 递归获取数据实体
        /// </summary>
        /// <param name="datalist">数据列表</param>
        /// <param name="ilist">返回数据列表</param>
        /// <param name="ninfo">实体</param>
        private void GetDataInfo(IList<TagInfo> datalist, IList<TagInfo> ilist, TagTypeInfo ninfo)
        {
            var dlist = from dl in datalist
                        where dl.ParentID == ninfo.ID
                        select dl;
            foreach (TagInfo dinfo in dlist)
            {
                ilist.Add(dinfo);
            }
            var list = from tl in menuList
                       where tl.ParentID == ninfo.ID
                       select tl;
            foreach (TagTypeInfo info in list)
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
                    TagTypeInfo info = tv_left.SelectedNode.Tag as TagTypeInfo;
                    if (info != null)
                    {
                        ParentInfo = info;
                        lb_ParentTitle.Text = info.Title;
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
            menuList = menuDal.TagTypeGetList();

            TreeNode tn = new TreeNode("根目录");
            TagTypeInfo info = new TagTypeInfo();
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
        private void GetNode(TreeNode tn, TagTypeInfo tinfo)
        {
            var list = from tl in menuList
                       where tl.ParentID == tinfo.ID
                       select tl;
            foreach (TagTypeInfo info in list)
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
        private void GetMenuInfo(IList<TagTypeInfo> ilist, TagTypeInfo ninfo)
        {
            ilist.Add(ninfo);
            var list = from tl in menuList
                       where tl.ParentID == ninfo.ID
                       select tl;
            foreach (TagTypeInfo info in list)
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
            if (chb_Rows.Checked)
            {
                rtb_Content.WordWrap = true;
            }
            else
            {
                rtb_Content.WordWrap = false;
            }
        }

    }
}
