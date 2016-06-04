using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace Commander
{
    public partial class FormMain : Form
    {
        public static FormMain EventSend;

        public FormMain()
        {
            InitializeComponent();
            EventSend = this;
        }
      
        SplitContainerButtonsDisk buttonContainer = new SplitContainerButtonsDisk();

        DirectoryPanel leftDirectoryPanel = new DirectoryPanel();
        DirectoryPanel rightDirectoryPanel = new DirectoryPanel();

        public void SendToLeftPanel(string value)
        {
            leftDirectoryPanel.GetFoldersFiles(value);
        }

        public void SendToRightPanel(string value)
        {
            leftDirectoryPanel.GetFoldersFiles(value);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {                    
            panelDiskButton.Controls.Add(buttonContainer);
            buttonContainer.SplitterDistance = 310;
            buttonContainer.IsSplitterFixed = true;

            leftDirectoryPanel.Dock = DockStyle.Fill;
            leftDirectoryPanel.BorderStyle = BorderStyle.FixedSingle;
            splitContainer.Panel1.Controls.Add(leftDirectoryPanel);
            leftDirectoryPanel.GetFoldersFiles(@"C:\");
         
            rightDirectoryPanel.Dock = DockStyle.Fill;
            rightDirectoryPanel.BorderStyle = BorderStyle.FixedSingle;
            splitContainer.Panel2.Controls.Add(rightDirectoryPanel);
            rightDirectoryPanel.GetFoldersFiles(@"D:\");
        }
    }
}
