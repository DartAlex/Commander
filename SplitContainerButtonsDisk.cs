using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Commander
{
    class SplitContainerButtonsDisk: SplitContainer
    {
        ButtonDrive[] leftDiskButtons = new ButtonDrive[255];
        ButtonDrive[] RightDiskButtons = new ButtonDrive[255];

        protected override void CreateHandle()
        {
            base.Dock = DockStyle.Fill;
            CreateInterface();
            base.CreateHandle();
        }

        public void CreateInterface()
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

                int DiskButtonWidth = 36;

                leftDiskButtons[countButton].Directory = localDrive.Name;
                leftDiskButtons[countButton].Location = new Point(1 + DiskButtonWidth * countButton + 2 * countButton, 4);
                leftDiskButtons[countButton].Size = new Size(DiskButtonWidth, 21);
                leftDiskButtons[countButton].Text = localDrive.Name.Split(':')[0].ToLower();
                leftDiskButtons[countButton].Click += LeftSplitContainerButtonsDisk_Click;

                leftDiskButtons[countButton].Image = Icon.FromHandle(shinfo.hIcon).ToBitmap();
                leftDiskButtons[countButton].ImageAlign = ContentAlignment.MiddleLeft;
                leftDiskButtons[countButton].TextAlign = ContentAlignment.MiddleRight;

                this.Panel1.Controls.Add(leftDiskButtons[countButton]);

                RightDiskButtons[countButton].Directory = localDrive.Name;
                RightDiskButtons[countButton].Location = new Point(DiskButtonWidth * countButton + 2 * countButton, 4);
                RightDiskButtons[countButton].Size = new Size(DiskButtonWidth, 21);
                RightDiskButtons[countButton].Text = localDrive.Name.Split(':')[0].ToLower();
                RightDiskButtons[countButton].Click += RightSplitContainerButtonsDisk_Click;

                RightDiskButtons[countButton].Image = Icon.FromHandle(shinfo.hIcon).ToBitmap();
                RightDiskButtons[countButton].ImageAlign = ContentAlignment.MiddleLeft;
                RightDiskButtons[countButton].TextAlign = ContentAlignment.MiddleRight;

                this.Panel2.Controls.Add(RightDiskButtons[countButton]);

                // Count++

                countButton++;
            }
        }

        private void RightSplitContainerButtonsDisk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            string dir = (((ButtonDrive)sender).Directory.ToString());
            FormMain.EventSend.SendToRightPanel(dir);
        }

        private void LeftSplitContainerButtonsDisk_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            string dir = (((ButtonDrive)sender).Directory.ToString());
            FormMain.EventSend.SendToLeftPanel(dir);
        }
    }
}
