using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
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

        List<CurrentDirectory> listCurentDirectory = new List<CurrentDirectory>();

        // Сохранить текущую директорию диска
        public void SendCurrrentDirectory(string root, string currentDirectory)
        {
            int index = listCurentDirectory.IndexOf(listCurentDirectory.Where(d => d.Root == root).FirstOrDefault());
            if (index == -1)
            {
                listCurentDirectory.Add(new CurrentDirectory { Root = root, Directory = currentDirectory });
            }
            else
            {
                listCurentDirectory[index].Directory = currentDirectory;
            }
            
        }
        // Переход на диск левая панель
        public void SendToLeftPanel(string value)
        {
            int index = listCurentDirectory.IndexOf(listCurentDirectory.Where(d => d.Root == value).FirstOrDefault());
            if (index == -1)
            {
                leftDirectoryPanel.GetFoldersFiles(value);
            }
            else
            {
                leftDirectoryPanel.GetFoldersFiles(listCurentDirectory[index].Directory);
            }

            LeftPanelFocus();
            
        }

        // Переход на диск правая панель
        public void SendToRightPanel(string value)
        {
            int index = listCurentDirectory.IndexOf(listCurentDirectory.Where(d => d.Root == value).FirstOrDefault());
            if (index == -1)
            {
                rightDirectoryPanel.GetFoldersFiles(value);
            }
            else
            {
                rightDirectoryPanel.GetFoldersFiles(listCurentDirectory[index].Directory);
            }

            RightPanelFocus();
        }

        public void RightPanelFocus()
        {
            rightDirectoryPanel.SetFocus();
        }

        public void LeftPanelFocus()
        {
            leftDirectoryPanel.SetFocus();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {                    
            panelDiskButton.Controls.Add(buttonContainer);
            buttonContainer.SplitterDistance = 310;
            buttonContainer.IsSplitterFixed = true;
          
            splitContainer.Panel1.Controls.Add(leftDirectoryPanel);
            leftDirectoryPanel.Tag = "left";
            leftDirectoryPanel.GetFoldersFiles(@"C:\");

            splitContainer.Panel2.Controls.Add(rightDirectoryPanel);
            rightDirectoryPanel.Tag = "right";
            rightDirectoryPanel.GetFoldersFiles(@"D:\");

            LeftPanelFocus();
        }

        // Exit
        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
