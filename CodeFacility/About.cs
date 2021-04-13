using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CodeFacility
{
    public partial class About : Form
    {
        public About()
        {
            InitializeComponent();
        }
        private void About_Load(object sender, EventArgs e)
        {
            StreamReader fileStream = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "CodeMaker\\Remark.txt", Encoding.Default);
            txtRemark.Text = fileStream.ReadToEnd();
            txtRemark.SelectionStart = txtRemark.Text.Length;
            txtRemark.SelectionLength = 0;
            txtRemark.ScrollToCaret();
            fileStream.Close();
        }

    }
}
