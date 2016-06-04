using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Commander
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        //PanelButtonsDisk panelButtonDisk = new PanelButtonsDisk();
        DirectoryPanel leftDirectoryPanel = new DirectoryPanel();
        DirectoryPanel rightDirectoryPanel = new DirectoryPanel();

        Button[] leftDiskButtons = new Button[255];

        private void FormMain_Load(object sender, EventArgs e)
        {
            GreateButtonDrive();
          
            leftDirectoryPanel.Dock = DockStyle.Fill;
            leftDirectoryPanel.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Panel1.Controls.Add(leftDirectoryPanel);
            leftDirectoryPanel.GetFoldersFiles(@"C:\");
         
            rightDirectoryPanel.Dock = DockStyle.Fill;
            rightDirectoryPanel.BorderStyle = BorderStyle.FixedSingle;
            splitContainer1.Panel2.Controls.Add(rightDirectoryPanel);
            rightDirectoryPanel.GetFoldersFiles(@"D:\");
        }

        public void GreateButtonDrive()
        {
            DriveInfo[] allDrive = DriveInfo.GetDrives();
            //Button[] leftDiskButtons = new Button[255];

            int countButton = 0;

            foreach (DriveInfo localDrive in allDrive)
            {
                //Button leftDiskButton = new Button();
                leftDiskButtons[countButton] = new Button();

                int leftDiskButtonWidth = 39;
                //leftDiskButton.Name = localDrive.Name;
                leftDiskButtons[countButton].Location = new Point(1 + leftDiskButtonWidth * countButton, 4);
                leftDiskButtons[countButton].Size = new Size(leftDiskButtonWidth, 21);
                leftDiskButtons[countButton].Text = localDrive.Name.Split(':')[0].ToLower();
                leftDiskButtons[countButton].Click += leftDiskButtonsClick;
                

                IntPtr iconSmall;
                SHFILEINFO shinfo = new SHFILEINFO();
                iconSmall = Win32.SHGetFileInfo(localDrive.Name, 0, ref shinfo, /*(uint)Marshal.SizeOf(shinfo)*/ 80, Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON);
                leftDiskButtons[countButton].Image = Icon.FromHandle(shinfo.hIcon).ToBitmap();
                leftDiskButtons[countButton].ImageAlign = ContentAlignment.MiddleLeft;
                leftDiskButtons[countButton].TextAlign = ContentAlignment.MiddleRight;

                panelDiskButton.Controls.Add(leftDiskButtons[countButton]);

                countButton++;
            }
        }

        private void leftDiskButtonsClick(object sender, EventArgs e)
        {
            MessageBox.Show(sender.ToString());
            //throw new NotImplementedException();
        }
    }
}
