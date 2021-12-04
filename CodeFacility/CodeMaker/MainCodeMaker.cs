using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

using Model.CodeMaker;

namespace CodeFacility.CodeMaker
{
    public partial class MainCodeMaker : Form
    {
        #region 加载和load操作

        DbDataInfo dinfo;
        public MainCodeMaker()
        {
            InitializeComponent();
            //toolStripStatusLabel3.Text += FormUI.UserName;
        }


        private void MainCodeMaker_Load(object sender, EventArgs e)
        {
            #region 时间加载
            string sdate = DateTime.Now.ToLongDateString();
            toolStripStatusLabel1.Text += sdate;
            timer1.Enabled = false;
            #endregion

            ToolForm tForm = new ToolForm();
            tForm.Show(dockPanel);
            tForm.DockTo(dockPanel, DockStyle.Left);
            tForm.DockState = DockState.DockLeftAutoHide;

            //订阅了信息发送事件，即接受参数值
            //Common.MidModule.EventSend += new Common.DataDlg(MidModule_EventSend);

            InitOption();
        }

        private void InitOption()
        {

            menuStrip1.BackColor = BaseConfigure.ColorTheme;
            toolStrip1.BackColor = BaseConfigure.ColorTheme;
            statusStrip1.BackColor = BaseConfigure.ColorTheme;
            this.BackColor = BaseConfigure.ColorTheme;
        }

        //接受参数事件
        //private void MidModule_EventSend(object sender, object data, object e)
        //{
        //    if (sender != null)
        //    {
        //        Form fr = sender as Form;
        //        if (fr.Text == "对象资源管理器")
        //        {
        //            dinfo = data as DbDataInfo;
        //            if (e == null)
        //                return;
        //            Model.EventInfo einfo = e as Model.EventInfo;
        //        }
        //    }
        //}


        private void timer1_Tick(object sender, EventArgs e)
        {
            string sdate = DateTime.Now.ToLongDateString();
            toolStripStatusLabel1.Text = "时间:" + sdate;
        }

        #endregion

        #region 关于窗口的相关操作
        /// <summary>
        /// 关闭当前窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
                ActiveMdiChild.Close();
            else if (dockPanel.ActiveDocument != null)
                dockPanel.ActiveDocument.DockHandler.Close();
        }
        /// <summary>
        /// 关闭其他窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void COtherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                Form activeMdi = ActiveMdiChild;
                foreach (Form form in MdiChildren)
                {
                    if (form != activeMdi)
                        form.Close();
                }
            }
            else
            {
                foreach (IDockContent document in dockPanel.DocumentsToArray())
                {
                    if (!document.DockHandler.IsActivated)
                        document.DockHandler.Close();
                }
            }
        }
        /// <summary>
        /// 关闭所有窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    form.Close();
            }
            else
            {
                IDockContent[] documents = dockPanel.DocumentsToArray();
                foreach (IDockContent content in documents)
                    content.DockHandler.Close();
            }
        }
        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// 在dockPanel中查找已经打开的窗口
        /// </summary>
        /// <param name="text">传入的窗口标题</param>
        /// <returns>返回的窗口</returns>
        private IDockContent FindDocument(string text)
        {
            if (dockPanel.DocumentStyle == DocumentStyle.SystemMdi)
            {
                foreach (Form form in MdiChildren)
                    if (form.Text == text)
                        return form as IDockContent;

                return null;
            }
            else
            {
                foreach (IDockContent content in dockPanel.Documents)
                    if (content.DockHandler.TabText == text)
                        return content;

                return null;
            }
        }
        /// <summary>
        /// 查找当前打开的活动子窗口
        /// </summary>
        /// <param name="childFrmName">活动子窗口的标题</param>
        /// <returns></returns>
        public bool checkChildFrmExist(string childFrmName)
        {
            foreach (IDockContent childFrm in dockPanel.Contents)
            {
                if (childFrm.DockHandler.TabText == childFrmName)
                {
                    if (!childFrm.DockHandler.IsActivated)
                    {
                        childFrm.DockHandler.DockTo(dockPanel, DockStyle.Left);
                    }
                    return true;
                }
            }
            return false;
        }

        //private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}

        #endregion

        private void HToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About f = new About();
            f.ShowDialog();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            About f = new About();
            f.ShowDialog();
        }

        private void DataExport_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        #region 用户管理
        private void AddUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (!FormUI.UserType)
            //{
            //    FormUI.Message("当前登陆用户不是管理员，不允许新增用户，请联系管理员进行新增。");
            //    return;
            //}
            //UserAdd f = new UserAdd();
            //f.ShowDialog();
        }

        private void EditUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //UserPwdEdit f = new UserPwdEdit();
            //f.ShowDialog();
        }




        private void DelUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (FormUI.UserType)
            //{
            //    FormUI.Message("当前登陆用户为管理员，不允许删除自己，\n请先将管理员权限给其他用户再进行删除操作。");
            //    return;
            //}
            //if (FormUI.Confirm("确认删除自己吗？删除后程序将退出并且此用户名不能再次登陆。"))
            //{
            //    E_Users model = new E_Users();
            //    model.UserName = FormUI.UserName;
            //    E_UsersBLL bll = new E_UsersBLL();
            //    bll.DelUser(model);

            //    FormUI.Message("删除成功，程序退出！");
            //    Close();
            //    lg.Close();
            //}
        }

        private void InfoUToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindDocument("用户查询") == null)
            {
                //UserInfo sm = new UserInfo();
                //sm.Show(dockPanel);
            }
            else
            {
                Form f = FindDocument("用户查询") as Form;
                f.Focus();
            }
        }

        #endregion

        #region 视图
        private void ResourceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!checkChildFrmExist("对象资源管理器"))
            {
                ToolForm tf = new ToolForm();
                tf.Show(dockPanel);
                tf.DockTo(dockPanel, DockStyle.Left);
            }
        }
        #endregion

        #region 功能

        private void FileTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindDocument("设置文件类型") == null)
            {
                FormFileType sm = new FormFileType();
                sm.Show(dockPanel);
            }
            else
            {
                Form f = FindDocument("设置文件类型") as Form;
                f.Focus();
            }
        }

        private void TempletTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindDocument("设置表单类型") == null)
            {
                FormTempletType sm = new FormTempletType();
                sm.Show(dockPanel);
            }
            else
            {
                Form f = FindDocument("设置表单类型") as Form;
                f.Focus();
            }
        }

        private void TempletToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindDocument("表单模板") == null)
            {
                FormTemplet sm = new FormTemplet();
                sm.Show(dockPanel);
            }
            else
            {
                Form f = FindDocument("表单模板") as Form;
                f.Focus();
            }
        }

        private void LableTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindDocument("设置模板标签类型") == null)
            {
                FormLabelType sm = new FormLabelType();
                sm.Show(dockPanel);
            }
            else
            {
                Form f = FindDocument("设置模板标签类型") as Form;
                f.Focus();
            }
        }

        private void LableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindDocument("设置模板标签") == null)
            {
                FormLabel sm = new FormLabel();
                sm.Show(dockPanel);
            }
            else
            {
                Form f = FindDocument("设置模板标签") as Form;
                f.Focus();
            }
        }

        private void TagTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindDocument("设置搜索标签类型") == null)
            {
                FormTagType sm = new FormTagType();
                sm.Show(dockPanel);
            }
            else
            {
                Form f = FindDocument("设置搜索标签类型") as Form;
                f.Focus();
            }
        }

        private void TagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindDocument("设置搜索标签") == null)
            {
                FormTag sm = new FormTag();
                sm.Show(dockPanel);
            }
            else
            {
                Form f = FindDocument("设置搜索标签") as Form;
                f.Focus();
            }
        }

        private void CreaterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindDocument("表单代码生成器") == null)
            {
                FormCreater sm = new FormCreater();
                sm.Show(dockPanel);
            }
            else
            {
                Form f = FindDocument("表单代码生成器") as Form;
                f.Focus();
            }
        }

        private void MultiTableQueryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (FindDocument("表结构查看") == null)
            //{
                FormMultiTableQuery sm = new FormMultiTableQuery();
                sm.Show(dockPanel);
            //}
            //else
            //{
            //    Form f = FindDocument("表结构查看") as Form;
            //    f.Focus();
            //}
        }

        private void DbLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindDocument("数据库连接") == null)
            {
                FormDbLink sm = new FormDbLink();
                sm.Show(dockPanel);
            }
            else
            {
                Form f = FindDocument("数据库连接") as Form;
                f.Focus();
            }
        }

        private void QueryConstrueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //新建查询
            FormQueryConstrue sm = new FormQueryConstrue();
            sm.Show(dockPanel);
        }

        private void ImportDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindDocument("数据导入") == null)
            {
                FormImportData sm = new FormImportData();
                sm.Show(dockPanel);
            }
            else
            {
                Form f = FindDocument("数据导入") as Form;
                f.Focus();
            }
        }

        private void CopyDataToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (FindDocument("数据复制") == null)
            {
                FormCopyData sm = new FormCopyData();
                sm.Show(dockPanel);
            }
            else
            {
                Form f = FindDocument("数据复制") as Form;
                f.Focus();
            }
        }

        private void OptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOption f = new FormOption();
            f.ShowDialog();
        }

        private void DataTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindDocument("数据传输") == null)
            {
                FormDataTransfer sm = new FormDataTransfer();
                sm.Show(dockPanel);
            }
            else
            {
                Form f = FindDocument("数据传输") as Form;
                f.Focus();
            }
        }

        private void TableStructureExportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FindDocument("表结构导出") == null)
            {
                FormTableStructure sm = new FormTableStructure();
                sm.Show(dockPanel);
            }
            else
            {
                Form f = FindDocument("表结构导出") as Form;
                f.Focus();
            }
        }
        #endregion

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
        }

    }
}
