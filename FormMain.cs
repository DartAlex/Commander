using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Commander
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            DirectoryPanel leftDirectoryPanel = new DirectoryPanel();
            leftDirectoryPanel.Dock = DockStyle.Fill;
            leftDirectoryPanel.BorderStyle = BorderStyle.FixedSingle;
            leftDirectoryPanel.CreateInterface();
            splitContainer1.Panel1.Controls.Add(leftDirectoryPanel);

            DirectoryPanel rightDirectoryPanel = new DirectoryPanel();
            rightDirectoryPanel.Dock = DockStyle.Fill;
            rightDirectoryPanel.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Panel2.Controls.Add(rightDirectoryPanel);
        }
    }
}
