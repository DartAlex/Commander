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

        DirectoryPanel leftDirectoryPanel = new DirectoryPanel();
        DirectoryPanel rightDirectoryPanel = new DirectoryPanel();

        ButtonDrive[] leftDiskButtons = new ButtonDrive[255];
        ButtonDrive[] RightDiskButtons = new ButtonDrive[255];

        private void FormMain_Load(object sender, EventArgs e)
        {           
            splitContainerButtonsDisk.IsSplitterFixed = true;
         
            СreateButtonDrive();
          
            leftDirectoryPanel.Dock = DockStyle.Fill;
            leftDirectoryPanel.BorderStyle = BorderStyle.FixedSingle;
            splitContainer.Panel1.Controls.Add(leftDirectoryPanel);
            leftDirectoryPanel.GetFoldersFiles(@"C:\");
         
            rightDirectoryPanel.Dock = DockStyle.Fill;
            rightDirectoryPanel.BorderStyle = BorderStyle.FixedSingle;
            splitContainer.Panel2.Controls.Add(rightDirectoryPanel);
            rightDirectoryPanel.GetFoldersFiles(@"D:\");
        }

        public void СreateButtonDrive()
        {
            DriveInfo[] allDrive = DriveInfo.GetDrives();

            int countButton = 0;

            foreach (DriveInfo localDrive in allDrive)
            {
                // Buttons
                IntPtr iconSmall;
                SHFILEINFO shinfo = new SHFILEINFO();
                iconSmall = Win32.SHGetFileInfo(localDrive.Name, 0, ref shinfo, /*(uint)Marshal.SizeOf(shinfo)*/ 80, Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON);

                leftDiskButtons[countButton] = new ButtonDrive();
                RightDiskButtons[countButton] = new ButtonDrive();

                int leftDiskButtonWidth = 36;

                leftDiskButtons[countButton].Directory = localDrive.Name;
                leftDiskButtons[countButton].Numer = countButton; 
                leftDiskButtons[countButton].Location = new Point(1 + leftDiskButtonWidth * countButton + 2* countButton, 4);
                leftDiskButtons[countButton].Size = new Size(leftDiskButtonWidth, 21);
                leftDiskButtons[countButton].Text = localDrive.Name.Split(':')[0].ToLower();
                leftDiskButtons[countButton].Click += leftDiskButtonsClick;               
                
                leftDiskButtons[countButton].Image = Icon.FromHandle(shinfo.hIcon).ToBitmap();
                leftDiskButtons[countButton].ImageAlign = ContentAlignment.MiddleLeft;
                leftDiskButtons[countButton].TextAlign = ContentAlignment.MiddleRight;

                splitContainerButtonsDisk.Panel1.Controls.Add(leftDiskButtons[countButton]);

                RightDiskButtons[countButton].Directory = localDrive.Name;
                RightDiskButtons[countButton].Numer = countButton;
                RightDiskButtons[countButton].Location = new Point(leftDiskButtonWidth * countButton + 2 * countButton, 4);
                RightDiskButtons[countButton].Size = new Size(leftDiskButtonWidth, 21);
                RightDiskButtons[countButton].Text = localDrive.Name.Split(':')[0].ToLower();
                RightDiskButtons[countButton].Click += RightDiskButtonsClick;

                RightDiskButtons[countButton].Image = Icon.FromHandle(shinfo.hIcon).ToBitmap();
                RightDiskButtons[countButton].ImageAlign = ContentAlignment.MiddleLeft;
                RightDiskButtons[countButton].TextAlign = ContentAlignment.MiddleRight;

                splitContainerButtonsDisk.Panel2.Controls.Add(RightDiskButtons[countButton]);

                // Count++

                countButton++;
            }
        }

        private void RightDiskButtonsClick(object sender, EventArgs e)
        {
            string dir = (((ButtonDrive)sender).Directory.ToString());
            rightDirectoryPanel.GetFoldersFiles(dir);
        }

        private void leftDiskButtonsClick(object sender, EventArgs e)
        {
            string dir = (((ButtonDrive)sender).Directory.ToString());
            leftDirectoryPanel.GetFoldersFiles(dir);
        }
    }
}
