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
    public partial class FormLabelType : FormWin
    {
        ILabelType menuDal = new LabelType();
        IList<LabelTypeInfo> menuList; //TreeView目录所有数据
        LabelTypeInfo ParentInfo; //父目录实体
        Boolean nodekey; //TreeView目录递归搜索 nodekey=true 执行 nodekey=false 停止
        int index = 0; //TreeView目录递归搜索序号
        int nodeid = 0; //TreeView目录递归搜索序号暂存
        int nodeMax1 = 0; //TreeView最后节点
        int nodeMax2 = 0; //TreeView当前节点

        public FormLabelType()
        {
            InitializeComponent();
        }

        private void FormLabelType_Load(object sender, EventArgs e)
        {
            lb_ID.Text = "";
            lb_ParentTitle.Text = "根目录";            
            QueryData();
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
                tb_Title.Text = dgv_Data.Rows[rowindex].Cells["Title"].Value.ToString();
                tb_Remark.Text = dgv_Data.Rows[rowindex].Cells["Remark"].Value.ToString();

            }
            else if (columnindex == 1)  //删除
            {
                if (MessageBox.Show("确认删除？", "此删除不可恢复", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    if (menuDal.LabelTypeByParentIDGetList(Convert.ToInt32(id)).Count > 0)
                    {
                        MessageBox.Show("请删除子目录。");
                        return;
                    }
                    DALFactory.CodeMaker.ILabel tlDal = new AccessDal.CodeMaker.Label();
                    if (tlDal.LabelByParentIDGetList(Convert.ToInt32(id)).Count > 0)
                    {
                        MessageBox.Show("请删除子目录模板标签。");
                        return;
                    }
                    if (menuDal.LabelType_Del(Convert.ToInt32(id)) == 1)
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
            LabelTypeInfo info = new LabelTypeInfo();
            info.Code = tb_Code.Text.Trim();
            info.Title = tb_Title.Text.Trim();
            info.Remark = tb_Remark.Text.Trim();

            if (info.Code == "")
            {
                MessageBox.Show("请输入编码。");
                return;
            }
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
                    if (info.Code.Length != 3)
                    {
                        MessageBox.Show("请输入3位长度编码。");
                        return;
                    }
                    info.ParentID = 0;
                }
                else
                {
                    if (info.Code.Length - ParentInfo.Code.Length != 3)
                    {
                        MessageBox.Show("请再输入3位长度编码。");
                        return;
                    }
                    info.ParentID = ParentInfo.ID;
                }
                rst = menuDal.LabelType_Add(info);
            }
            else
            {
                info.ID = int.Parse(lb_ID.Text);
                rst = menuDal.LabelType_Edit(info);
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
                IList<LabelTypeInfo> ilist = new List<LabelTypeInfo>();
                GetInfo(ilist, ParentInfo);
                dgv_Data.DataSource = ilist;
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
                    LabelTypeInfo info = tv_left.SelectedNode.Tag as LabelTypeInfo;
                    if (info != null)
                    {
                        ParentInfo = info;
                        lb_ParentTitle.Text = info.Title;
                        tb_Code.Text = info.Code;
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
            menuList = menuDal.LabelTypeGetList();
            TreeNode tn = new TreeNode("根目录");
            LabelTypeInfo info = new LabelTypeInfo();
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
        private void GetNode(TreeNode tn, LabelTypeInfo tinfo)
        {
            var list = from tl in menuList
                       where tl.ParentID == tinfo.ID
                       select tl;
            foreach (LabelTypeInfo info in list)
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
        private void GetInfo(IList<LabelTypeInfo> ilist, LabelTypeInfo ninfo)
        {
            ilist.Add(ninfo);
            var list = from tl in menuList
                       where tl.ParentID == ninfo.ID
                       select tl;
            foreach (LabelTypeInfo info in list)
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
    }
}
