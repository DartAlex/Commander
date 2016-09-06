using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Globalization;

namespace Commander
{
    class DirectoryPanel : Panel
    {
        ComboBox comboBoxDrive = new ComboBox();       
        Label labelFreeSpace = new Label();
        Label labelDir = new Label();
        Label labelInfoFolder = new Label();
        Button buttonToRoot = new Button();
        Button buttonToParent = new Button();
        Button buttonFavoriteCatalogs = new Button();
        Button buttonHistory = new Button();
        Panel panelSpaceDisk = new Panel();
        Panel panelDir = new Panel();
        Panel panelInfoFolder = new Panel();
        CustomListView listViewDirectory = new CustomListView();     
        //List<DirectoryList> directoryList = new List<DirectoryList>();
        //List<DirectoryList> tempDirectoryList = new List<DirectoryList>();
        Icon iconUp = Properties.Resources.IconUP;
        Icon IconUnknown = Properties.Resources.IconUnknown;   

        int selectionIndex;
        string dateTimeFormat = "dd.MM.yyyy HH:mm:ss";
        string rootDir;
        string currentDir;

        protected override void CreateHandle()
        {
            this.Dock = DockStyle.Fill;
            this.BorderStyle = BorderStyle.FixedSingle;

            CreateInterface();
            base.CreateHandle();
        }

        public void CreateInterface()
        {
            comboBoxDrive.Location = new Point(2, 2);
            comboBoxDrive.Size = new Size(42, 21);
            comboBoxDrive.Text = "[-c-]";
            comboBoxDrive.TabIndex = 2;
            this.Controls.Add(comboBoxDrive);

            buttonToRoot.Location = new Point(this.Width - 43, 1);
            buttonToRoot.Size = new Size(21, 23);
            buttonToRoot.Anchor = AnchorStyles.None;
            buttonToRoot.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            buttonToRoot.Text = "/";
            buttonToRoot.TabIndex = 3;
            this.Controls.Add(buttonToRoot);

            buttonToParent.Location = new Point(this.Width - 22, 1);
            buttonToParent.Size = new Size(21, 23);
            buttonToParent.Anchor = AnchorStyles.None;
            buttonToParent.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            buttonToParent.Text = "..";
            buttonToParent.TabIndex = 4;
            this.Controls.Add(buttonToParent);

            panelSpaceDisk.Location = new Point(46, 2);
            panelSpaceDisk.Size = new Size(this.Width - 90, 21);
            panelSpaceDisk.Anchor = AnchorStyles.None;
            panelSpaceDisk.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            panelSpaceDisk.BorderStyle = BorderStyle.FixedSingle;
            panelSpaceDisk.TabIndex = 5;
            this.Controls.Add(panelSpaceDisk);

            labelFreeSpace.Location = new Point(0, 3);
            labelFreeSpace.AutoSize = true;
            labelFreeSpace.Text = "Free space disck";
            labelFreeSpace.TabIndex = 6;
            this.panelSpaceDisk.Controls.Add(labelFreeSpace);

            buttonFavoriteCatalogs.Location = new Point(this.Width - 43, 24);
            buttonFavoriteCatalogs.Size = new Size(21, 18);
            buttonFavoriteCatalogs.Anchor = AnchorStyles.None;
            buttonFavoriteCatalogs.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            buttonFavoriteCatalogs.Text = "*"; //1F7C9 u1F7CA
            buttonFavoriteCatalogs.TabIndex = 7;
            this.Controls.Add(buttonFavoriteCatalogs);

            buttonHistory.Location = new Point(this.Width - 22, 24);
            buttonHistory.Size = new Size(21, 18);
            buttonHistory.Anchor = AnchorStyles.None;
            buttonHistory.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            buttonHistory.Text = "H";
            buttonHistory.TabIndex = 8;
            this.Controls.Add(buttonHistory);

            panelDir.Location = new Point(2, 25);
            panelDir.Size = new Size(this.Width - 46, 16);
            panelDir.Anchor = AnchorStyles.None;
            panelDir.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            panelDir.BorderStyle = BorderStyle.FixedSingle;
            panelDir.TabIndex = 9;
            this.Controls.Add(panelDir);

            labelDir.Location = new Point(0, 0);
            labelDir.AutoSize = true;
            labelDir.Text = "DirDirectory";
            labelDir.TabIndex = 10;
            this.panelDir.Controls.Add(labelDir);

            listViewDirectory.Location = new Point(2, 42);
            listViewDirectory.Size = new Size(this.Width - 4, this.Height - 65);                                

            listViewDirectory.ItemSelectionChanged += ListViewDirectory_ItemSelectionChanged;
            listViewDirectory.MouseDoubleClick += ListViewDirectory_MouseDoubleClick;

            listViewDirectory.Columns.Add("Имя", 200, HorizontalAlignment.Left);
            listViewDirectory.Columns.Add("Тип", 50, HorizontalAlignment.Left);
            listViewDirectory.Columns.Add("Размер", 100, HorizontalAlignment.Right);
            listViewDirectory.Columns.Add("Дата", 120, HorizontalAlignment.Left);

            listViewDirectory.TabIndex = 1;
            this.Controls.Add(listViewDirectory);          

            panelInfoFolder.Location = new Point(2, this.Height - 22);
            panelInfoFolder.Size = new Size(this.Width - 4, 21);
            panelInfoFolder.Anchor = AnchorStyles.None;
            panelInfoFolder.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            panelInfoFolder.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(panelInfoFolder);

            labelInfoFolder.Location = new Point(0, 3);
            labelInfoFolder.AutoSize = true;
            labelInfoFolder.Text = "Info folder";
            
            this.panelInfoFolder.Controls.Add(labelInfoFolder);
        }

        private void ListViewDirectory_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            //throw new NotImplementedException();
            selectionIndex = e.Item.Index;
        }

        private void ListViewDirectory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            OpenSelected(selectionIndex);                   
        } 

        private Icon GetIcon(string filePatch)
        {
            Icon icon;
            IntPtr iconSmall;
            SHFILEINFO shinfo = new SHFILEINFO();
            iconSmall = Win32.SHGetFileInfo(filePatch, 0, ref shinfo, /*(uint)Marshal.SizeOf(shinfo)*/ 80, Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON);
            if (iconSmall == IntPtr.Zero)
            {
                icon = IconUnknown;
                //throw (new System.IO.FileNotFoundException());               
            }
            else
            {
                icon = Icon.FromHandle(shinfo.hIcon);
            }
            return (icon);
        }

        // Open selected item ListView
        private void OpenSelected(int index)
        {
            /*if (!directoryList[selectionIndex].atrributes.ToString().Contains("Directory"))
            {
                if (directoryList[selectionIndex].type != "")
                {
                    MessageBox.Show("file " + directoryList[selectionIndex].name + "." + directoryList[selectionIndex].type + Environment.NewLine + directoryList[selectionIndex].atrributes.ToString());
                }
                else
                {
                    MessageBox.Show("file " + directoryList[selectionIndex].name + Environment.NewLine + directoryList[selectionIndex].atrributes.ToString());
                }              
            }
            else
            {
                GetFoldersFiles(directoryList[selectionIndex].directory + "\\");              
            }*/

            if (listViewDirectory.Items[index].Tag.ToString().Contains(("Directory")))
            {
                GetFoldersFiles(listViewDirectory.Items[index].Name.ToString() + "\\");
            }
            else
            {
                MessageBox.Show(listViewDirectory.Items[index].Name.ToString() + " " +listViewDirectory.Items[index].Tag.ToString());
            }         
        }

        public void GetFoldersFiles(string directory)
        {

            List<DirectoryList> directoryList = new List<DirectoryList>();
            DirectoryInfo thisDirectory = new DirectoryInfo(directory);

            // if root
            rootDir = thisDirectory.Root.ToString();
            currentDir = thisDirectory.FullName.ToString();

            if (rootDir != currentDir)
            {
                string dirFolder = Directory.GetParent(Directory.GetParent(directory).ToString()).ToString();
                DateTime dateFolder = Directory.GetCreationTime(dirFolder);

                directoryList.Add(new DirectoryList()
                {
                    icon = iconUp,
                    directory = dirFolder,
                    name = "[..]",
                    type = "",
                    size = "",
                    atrributes = thisDirectory.Attributes
                });
            }

            try
            {
                // Get folders
                DirectoryInfo[] folders = thisDirectory.GetDirectories();
                foreach (DirectoryInfo folder in folders)
                {
                    directoryList.Add(new DirectoryList()
                    {
                        icon = IconUnknown,
                        directory = folder.FullName,
                        name = "[" + folder.Name + "]",
                        type = "",
                        size = "<папка>",
                        //date = Directory.GetCreationTime(dirFolder),
                        atrributes = folder.Attributes
                    });
                }

                // Get folders
                FileInfo[] files = thisDirectory.GetFiles();
                foreach (FileInfo file in files)
                {
                    directoryList.Add(new DirectoryList()
                    {
                        icon = IconUnknown,
                        directory = file.FullName,
                        name = file.Name,
                        type = file.Extension,
                        size = NumberFormat.DigitNumber(file.Length),
                        date = File.GetCreationTime(file.FullName).ToString(),
                        atrributes = file.Attributes
                    });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return;
            }

            listViewDirectory.Items.Clear();

            foreach (DirectoryList lineDirectoryList in directoryList)
            {
                string[] item = { lineDirectoryList.name, lineDirectoryList.type, lineDirectoryList.size, lineDirectoryList.date };
                ListViewItem listItem = new ListViewItem(item);
                listItem.Name = lineDirectoryList.directory;
                listItem.Tag = lineDirectoryList.atrributes;
                listViewDirectory.Items.Add(listItem);
            }

            SetIcon(directoryList);

            // select
            listViewDirectory.Items[0].Selected = true;
            listViewDirectory.Items[0].Focused = true;
        }

        /*public void GetFoldersFiles(string directory)
        {
            tempDirectoryList.Clear();
            
            DirectoryInfo thisDirectory = new DirectoryInfo(directory);

            // if root
            rootDir = thisDirectory.Root.ToString();
            currentDir = thisDirectory.FullName.ToString();         

            if (rootDir != currentDir)
            {
                string dirFolder = Directory.GetParent(Directory.GetParent(directory).ToString()).ToString();
                DateTime dateFolder = Directory.GetCreationTime(dirFolder);

                tempDirectoryList.Add(new DirectoryList()
                {
                    icon = iconUp,
                    directory = dirFolder,
                    name = "[..]",
                    type = "",
                    size = "",
                    date = dateFolder,
                    atrributes = thisDirectory.Attributes
                });
            }
            
            try
            {
                // Get folders
                DirectoryInfo[] folders = thisDirectory.GetDirectories();
                foreach (DirectoryInfo folder in folders)
                {
                    string dirFolder = folder.FullName;
                    tempDirectoryList.Add(new DirectoryList()
                    {
                        icon = GetIcon(dirFolder),
                        directory = dirFolder,
                        name = "[" + folder.Name + "]",
                        type = "",
                        size = "<папка>",
                        date = Directory.GetCreationTime(dirFolder),
                        atrributes = folder.Attributes
                    });
                }
                // Get folders
                FileInfo[] files = thisDirectory.GetFiles();
                foreach (FileInfo file in files)
                {
                    string dirFile = file.FullName;
                    string typeFile = Path.GetExtension(dirFile);
                    try
                    {
                        typeFile = typeFile.Substring(1);
                    }
                    catch { }
                    tempDirectoryList.Add(new DirectoryList()
                    {
                        icon = GetIcon(dirFile),
                        directory = dirFile,
                        name = Path.GetFileNameWithoutExtension(dirFile),
                        type = typeFile,
                        //size = file.Length.ToString(),
                        size = NumberFormat.DigitNumber(file.Length),
                        date = File.GetCreationTime(dirFile),
                        atrributes = file.Attributes
                    });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return;
            }

            // Save current directory
            FormMain.EventSend.SendCurrrentDirectory(rootDir, currentDir);

            // Set current dir label
            labelDir.Text = currentDir;

            // Adding in ListView
            directoryList.Clear();
            directoryList = new List<DirectoryList>(tempDirectoryList);
            listViewDirectory.Items.Clear();

            foreach (DirectoryList lineDirectoryList in directoryList)
            {
                string[] item = { lineDirectoryList.name, lineDirectoryList.type, lineDirectoryList.size, lineDirectoryList.date.ToString(dateTimeFormat) };
                ListViewItem listItem = new ListViewItem(item);
                listViewDirectory.Items.Add(listItem);
            }

            // select
            listViewDirectory.Items[0].Selected = true;
            listViewDirectory.Items[0].Focused = true;

            // Tread add icon
            Thread iconFileThread = new Thread(SetIcon);
            iconFileThread.Start();

            // Tread DriveInfo
            Thread dirveInfoThread = new Thread(GetDriveInfo);
            dirveInfoThread.Start();

            // Tread InfoFolder
            Thread folderInfoThread = new Thread(GetFolderInfo);
            folderInfoThread.Start();
        }*/

        // Add icon
        public void AddIconListViewDirectory(ImageList value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<ImageList>(AddIconListViewDirectory), new object[] { value });
                return;
            }
            listViewDirectory.SmallImageList = value;
            for (int count = 0; count < listViewDirectory.Items.Count; count++)
            {
                listViewDirectory.Items[count].ImageIndex = count;
            }
        }
        // Tread
        private void SetIcon(List<DirectoryList> directoryList)
        {
            ImageList iconList = new ImageList();
            iconList.ColorDepth = ColorDepth.Depth32Bit;
            foreach (DirectoryList lineDirectoryList in directoryList)
            {
                iconList.Images.Add(lineDirectoryList.icon);
            }
            AddIconListViewDirectory(iconList);
        }

        // Drive info to label
        public void DriveInfoToLabel(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(DriveInfoToLabel), new object[] { value });
                return;
            }
            labelFreeSpace.Text = value;
        }
        // Tread
        private void GetDriveInfo()
        {
            DriveInfo driveInfo = new DriveInfo(rootDir);

            string driveName = driveInfo.VolumeLabel;
            if (driveName.Length == 0)
            {
                driveName = "-нет-";
            }
            string driveFreeSpace = NumberFormat.DigitNumber(driveInfo.TotalFreeSpace);
            string driveTotalSpace = NumberFormat.DigitNumber(driveInfo.TotalSize);
            DriveInfoToLabel("["+ driveName+"] " + driveFreeSpace + "  из " + driveTotalSpace);
        }

        // Folder info to label
        private void FileInfoToLabel(string value)
        {
            if (InvokeRequired)
            {
                this.Invoke(new Action<string>(FileInfoToLabel), new object[] { value });
                return;
            }
            labelInfoFolder.Text = value;
        }
        // Thread
        private void GetFolderInfo()
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(currentDir);

            long sizeSelectedItem = 0;
            long sizeAllItem = DirSize(directoryInfo);

            int countSelectedFiles = 0;
            int countAllFiles = directoryInfo.GetFiles().Length;

            int countSelectedFolders = 0;
            int countAllFolders = directoryInfo.GetDirectories().Length;

            FileInfoToLabel(NumberFormat.DigitNumber(sizeSelectedItem) + " из " + NumberFormat.DigitNumber(sizeAllItem) + "; файлов: " + countSelectedFiles + " из " + countAllFiles + "; папок: " + countSelectedFolders + " из " + countAllFolders);
        }

        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            try
            {
                FileInfo[] fis = d.GetFiles();
                foreach (FileInfo fi in fis)
                {
                    size += fi.Length;
                }
            }
            catch { }
            
            return size;
        }

        // Focus
        public void SetFocus()
        {           
            //listViewDirectory.Select();
            //listViewDirectory.Items[selectionIndex].Selected = true;
        }

        // Key Tab press
        /*protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            bool baseResult = true;

            switch (keyData)
            {
                case Keys.Tab:
                    if (this.Tag.ToString() == "left")
                    {
                        FormMain.EventSend.RightPanelFocus();
                    }
                    else
                    {
                        FormMain.EventSend.LeftPanelFocus();
                    }
                    break;
  
                case Keys.Enter:
                    OpenSelected(selectionIndex);
                    break;

                case Keys.Back:
                    //OpenSelected(0);
                    MessageBox.Show(directoryList[selectionIndex].directory);
                    break;
            
                default:                   
                    baseResult = base.ProcessCmdKey(ref msg, keyData);
                    break;
            }
            
            return baseResult;   
        }*/
    }
}
