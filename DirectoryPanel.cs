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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ComponentModel;

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
        Icon iconUp = Properties.Resources.IconUP;
        Icon iconUnknown = Properties.Resources.IconUnknown;
        Icon iconFolder;

        volatile List<DirectoryList> directoryListVolatile = new List<DirectoryList>();
        int selectionIndex;
        int topIndex = 0;
        int step = 500;
        bool treadRun = false;
        string dateTimeFormat = "dd.MM.yyyy HH:mm:ss";
        string rootDir;
        string currentDir;
        string currentFolder;

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
            buttonFavoriteCatalogs.Click += ButtonFavoriteCatalogs_Click;
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
            listViewDirectory.TopItemChanged += ListViewDirectory_TopItemChanged;

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

            iconFolder = GetIcon(@"C:\windows\");
        }

        

        private void ButtonFavoriteCatalogs_Click(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
            //SetIcon();
            Thread setIconTread = new Thread(SetIcon);
            setIconTread.Start();

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

            SHFILEINFO shinfo = new SHFILEINFO();

            IntPtr hIconSmall = Win32.SHGetFileInfo(filePatch, 0, ref shinfo, (uint)Marshal.SizeOf(shinfo), Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON);

            if (hIconSmall == IntPtr.Zero)
            {
                icon = iconUnknown;

                //throw (new System.IO.FileNotFoundException());

            }
            else
            {
                //Console.WriteLine(filePatch);

                icon = (Icon)Icon.FromHandle(shinfo.hIcon).Clone();
                //Необработанное исключение типа "System.ArgumentException" в System.Drawing.dll

            }

            Win32.DestroyIcon(new HandleRef(this, shinfo.hIcon));
            Win32.DestroyIcon(new HandleRef(this, shinfo.iIcon));

            //if (Win32.DestroyIcon(new HandleRef(this, shinfo.hIcon))) Console.WriteLine("Del Handle hIcon");
            //if (Win32.DestroyIcon(new HandleRef(this, shinfo.iIcon))) Console.WriteLine("Del Handle iIcon");

            return (icon);
        }

        // Open selected item ListView
        private void OpenSelected(int index)
        {
            if (listViewDirectory.Items[index].Tag.ToString().Contains(("Directory")))
            {
                if (listViewDirectory.Items[index].Tag.ToString() == "DirectoryUp")
                {
                    string backFolder = this.currentFolder;
                    GetFoldersFiles(listViewDirectory.Items[index].Name.ToString() + "\\");
                    int itemIndex = listViewDirectory.FindItemWithText("[" + backFolder + "]").Index;

                    // select
                    listViewDirectory.Items[itemIndex].Selected = true;
                    listViewDirectory.Items[itemIndex].Focused = true;
                    listViewDirectory.Items[itemIndex].EnsureVisible();
                }
                else
                {
                    GetFoldersFiles(listViewDirectory.Items[index].Name.ToString() + "\\");

                    // select
                    listViewDirectory.Items[0].Selected = true;
                    listViewDirectory.Items[0].Focused = true;
                }              
            }
            else
            {
                MessageBox.Show(listViewDirectory.Items[index].Name.ToString() + " " +listViewDirectory.Items[index].Tag.ToString());
            }         
        }

        public void GetFoldersFiles(string directory)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            List<DirectoryList> directoryList = new List<DirectoryList>();
            DirectoryInfo thisDirectory = new DirectoryInfo(directory);

            currentFolder = thisDirectory.Name.ToString();

            // if root
            rootDir = thisDirectory.Root.ToString();
            currentDir = thisDirectory.FullName.ToString();

            if (rootDir != currentDir)
            {
                string dirFolder = Directory.GetParent(Directory.GetParent(directory).ToString()).ToString();
                DateTime dateFolder = Directory.GetCreationTime(dirFolder);

                directoryList.Add(new DirectoryList()
                {
                    tag = "up",
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
                        tag = "folder",
                        directory = folder.FullName,
                        name = "[" + folder.Name + "]",
                        type = "",
                        size = "<папка>",
                        date = Directory.GetCreationTime(folder.FullName).ToString(dateTimeFormat),
                        atrributes = folder.Attributes
                    });
                }
                
                // Get files
                FileInfo[] files = thisDirectory.GetFiles();
                foreach (FileInfo file in files)
                {
                    string ext = "";
                    try
                    {
                        if (file.Extension.Length > 0)
                        {
                            ext = file.Extension.Split('.')[1];
                        }                           
                    }
                    catch { }

                    directoryList.Add(new DirectoryList()
                    {
                        tag = "file",
                        directory = file.FullName,
                        name = Path.GetFileNameWithoutExtension(file.FullName),
                        type = " " + ext,
                        size = NumberFormat.DigitNumber(file.Length),
                        date = File.GetCreationTime(file.FullName).ToString(dateTimeFormat),
                        atrributes = file.Attributes
                    });
                }
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return;
            }

            ImageList iconList = new ImageList();
            iconList.ColorDepth = ColorDepth.Depth32Bit;
            iconList.Images.Add(iconUp);
            iconList.Images.Add(iconFolder);
            iconList.Images.Add(iconUnknown);

            listViewDirectory.SmallImageList = iconList;
            listViewDirectory.Items.Clear();

            listViewDirectory.BeginUpdate();

            foreach (DirectoryList lineDirectoryList in directoryList)
            {
                string[] item = { lineDirectoryList.name, lineDirectoryList.type, lineDirectoryList.size, lineDirectoryList.date };
                ListViewItem listItem = new ListViewItem(item);
                listItem.Name = lineDirectoryList.directory;

                switch ( lineDirectoryList.tag )
                {
                    case "up":
                        listItem.ImageIndex = 0;
                        listItem.Tag = "DirectoryUp";
                        break;
                    case "folder":
                        listItem.ImageIndex = 1;
                        listItem.Tag = lineDirectoryList.atrributes.ToString();
                        break;
                    default:
                        listItem.ImageIndex = 2;
                        listItem.Tag = lineDirectoryList.atrributes.ToString();
                        break;
                }
                listViewDirectory.Items.Add(listItem);
            }

            listViewDirectory.EndUpdate();

            // Set current dir label
            //labelDir.Text = currentDir;

            // Tread GetIcons
            directoryListVolatile = directoryList;
            //directoryList.Clear();

            //SetIcon();
            Thread setIconTread = new Thread(SetIcon);
            setIconTread.Start();
            //SetIcon();

            // Tread DriveInfo
            Thread dirveInfoThread = new Thread(GetDriveInfo);
            dirveInfoThread.Start();

            // Tread InfoFolder
            Thread folderInfoThread = new Thread(GetFolderInfo);
            folderInfoThread.Start();

            stopWatch.Stop();
            TimeSpan ts = stopWatch.Elapsed;
            labelDir.Text = ts.Milliseconds.ToString(); 
        }

        // if TopItemChanged during PAINT
        private void ListViewDirectory_TopItemChanged(object sender, EventArgs e)
        {
            //SetIcon
            if (((listViewDirectory.TopItem.Index > topIndex + step - 50) || (listViewDirectory.TopItem.Index < topIndex - step + 50)) && treadRun == false)
            {
                topIndex = listViewDirectory.TopItem.Index;
                //MessageBox.Show(topIndex.ToString());
                treadRun = true;
                Thread setIconTread = new Thread(SetIcon);
                setIconTread.Start();
            }
        }

        // Add icon
        public void AddIconListViewDirectory(ImageList value, int index)
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                try
                {
                    listViewDirectory.SmallImageList = value;
                }
                catch { }

                for (int count = 0; count < listViewDirectory.SmallImageList.Images.Count; count++)
                {
                    listViewDirectory.Items[count + index].ImageIndex = count;
                }
                treadRun = false;
            });         
        }

        private void SetIcon()
        {
            this.Invoke((MethodInvoker)delegate ()
            {
                ImageList iconList = new ImageList();
                iconList.ColorDepth = ColorDepth.Depth32Bit;

                int topItem = listViewDirectory.TopItem.Index;
                int countItem = listViewDirectory.Items.Count;
                int stepItem = step + topItem;
                if (stepItem > countItem) stepItem = countItem;

                topItem = topItem - step;
                if (topItem < 0) topItem = 0;

                for (int count = topItem; count < stepItem; count++)
                {
                    string patch = listViewDirectory.Items[count].Name.ToString();

                    if (listViewDirectory.Items[count].Tag.ToString() == "DirectoryUp")
                    {
                        iconList.Images.Add(iconUp);
                    }
                    else
                    {
                        Icon icon = GetIcon(patch);
                        iconList.Images.Add(icon);
                        Win32.DestroyIcon(new HandleRef(this, icon.Handle));
                    }
                }
                AddIconListViewDirectory(iconList, topItem);
            });    
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
            listViewDirectory.Select();
            listViewDirectory.Items[selectionIndex].Selected = true;
        }

        // Key Tab press
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
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
                    if (listViewDirectory.Items[0].Tag.ToString().Contains(("DirectoryUp")))
                    {
                        OpenSelected(0);
                    }
                    break;
            
                default:                   
                    baseResult = base.ProcessCmdKey(ref msg, keyData);
                    break;
            }
            
            return baseResult;   
        }
    }
}
