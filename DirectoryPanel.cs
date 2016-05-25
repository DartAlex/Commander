using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

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
        //ListView listViewDirectory = new ListView();
        List<DirectoryList> directoryList = new List<DirectoryList>();
        int selectionIndex;

        public void CreateInterface()
        {
            comboBoxDrive.Location = new Point(2, 2);
            comboBoxDrive.Size = new Size(42, 21);
            comboBoxDrive.Text = "[-c-]";
            this.Controls.Add(comboBoxDrive);

            buttonToRoot.Location = new Point(this.Width - 43, 1);
            buttonToRoot.Size = new Size(21, 23);
            buttonToRoot.Anchor = AnchorStyles.None;
            buttonToRoot.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            buttonToRoot.Text = "/";
            this.Controls.Add(buttonToRoot);

            buttonToParent.Location = new Point(this.Width - 22, 1);
            buttonToParent.Size = new Size(21, 23);
            buttonToParent.Anchor = AnchorStyles.None;
            buttonToParent.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            buttonToParent.Text = "..";
            this.Controls.Add(buttonToParent);

            panelSpaceDisk.Location = new Point(46, 2);
            panelSpaceDisk.Size = new Size(this.Width - 90, 21);
            panelSpaceDisk.Anchor = AnchorStyles.None;
            panelSpaceDisk.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            panelSpaceDisk.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(panelSpaceDisk);

            labelFreeSpace.Location = new Point(0, 3);
            labelFreeSpace.AutoSize = true;
            labelFreeSpace.Text = "Free space disck";
            this.panelSpaceDisk.Controls.Add(labelFreeSpace);

            buttonFavoriteCatalogs.Location = new Point(this.Width - 43, 24);
            buttonFavoriteCatalogs.Size = new Size(21, 18);
            buttonFavoriteCatalogs.Anchor = AnchorStyles.None;
            buttonFavoriteCatalogs.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            //buttonHistory.Font = new Font("Arial Unicode MS", 8.5f);
            buttonFavoriteCatalogs.Text = "*"; //1F7C9 u1F7CA
            this.Controls.Add(buttonFavoriteCatalogs);

            buttonHistory.Location = new Point(this.Width - 22, 24);
            buttonHistory.Size = new Size(21, 18);
            buttonHistory.Anchor = AnchorStyles.None;
            buttonHistory.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
            buttonHistory.Text = "H";
            this.Controls.Add(buttonHistory);

            panelDir.Location = new Point(2, 25);
            panelDir.Size = new Size(this.Width - 46, 16);
            panelDir.Anchor = AnchorStyles.None;
            panelDir.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
            panelDir.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(panelDir);

            labelDir.Location = new Point(0, 0);
            labelDir.AutoSize = true;
            labelDir.Text = "DirDirectory";
            this.panelDir.Controls.Add(labelDir);

            listViewDirectory.Location = new Point(2, 42);
            listViewDirectory.Size = new Size(this.Width - 4, this.Height - 65);
            listViewDirectory.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            listViewDirectory.View = View.Details;
            listViewDirectory.FullRowSelect = true;
            //listViewDirectory.OwnerDraw = true;

            listViewDirectory.ItemSelectionChanged += ListViewDirectory_ItemSelectionChanged;
            listViewDirectory.MouseDoubleClick += ListViewDirectory_MouseDoubleClick;

            listViewDirectory.Columns.Add("Имя", 200, HorizontalAlignment.Left);
            listViewDirectory.Columns.Add("Тип", -2, HorizontalAlignment.Left);
            listViewDirectory.Columns.Add("Размер", 70, HorizontalAlignment.Left);
            listViewDirectory.Columns.Add("Дата", 120, HorizontalAlignment.Left);        

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
            //MessageBox.Show(directoryList[e.Item.Index].name);
            selectionIndex = e.Item.Index;
        }

        private void ListViewDirectory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            OpenSelected(selectionIndex);                   
        }

        // Open selected item ListView
        private void OpenSelected(int Index)
        {
            if (directoryList[selectionIndex].isFile == true)
            {
                if (directoryList[selectionIndex].type != "")
                {
                    MessageBox.Show("file " + directoryList[selectionIndex].name + "." + directoryList[selectionIndex].type);
                }
                else
                {
                    MessageBox.Show("file " + directoryList[selectionIndex].name);
                }              
            }
            else
            {
                GetFiles(directoryList[selectionIndex].directory + "\\");
            }
        }

        public void GetFiles(string directory)
        {
            IntPtr iconSmall;
            SHFILEINFO shinfo = new SHFILEINFO();
            
            directoryList.Clear();
            listViewDirectory.Items.Clear();           

            DirectoryInfo thisDirectory = new DirectoryInfo(directory);

            // if root
            string rootDir = thisDirectory.Root.ToString();
            string currentDir = thisDirectory.FullName.ToString();

            labelDir.Text = currentDir;

            if (rootDir != currentDir)
            {              
                string dirFolder = Directory.GetParent(Directory.GetParent(directory).ToString()).ToString();
                iconSmall = Win32.SHGetFileInfo(dirFolder, 0, ref shinfo, /*(uint)Marshal.SizeOf(shinfo)*/ 80, Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON);
                Icon iconFolder = Icon.FromHandle(shinfo.hIcon);
                DateTime dateFolder = Directory.GetCreationTime(dirFolder);

                directoryList.Add(new DirectoryList() { isFile = false, icon = iconFolder, directory = dirFolder, name = "[..]", type = "", size = "", date = dateFolder });
            }

            // Get folders
            try
            {               
                DirectoryInfo[] folders = thisDirectory.GetDirectories();
                foreach (DirectoryInfo folder in folders)
                {
                    iconSmall = Win32.SHGetFileInfo(folder.FullName, 0, ref shinfo, /*(uint)Marshal.SizeOf(shinfo)*/ 80, Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON);
                    Icon iconFolder = Icon.FromHandle(shinfo.hIcon);
                    string dirFolder = folder.FullName;
                    string nameFolder = "[" + folder.Name + "]";
                    string typeFolder = "";
                    string sizeFolder = "<папка>";
                    DateTime dateFolder = Directory.GetCreationTime(dirFolder);

                    directoryList.Add(new DirectoryList() { isFile = false, icon = iconFolder, directory = dirFolder, name = nameFolder, type = typeFolder, size = sizeFolder, date = dateFolder });
                }               
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            // Get files
            try
            {
                FileInfo[] files = thisDirectory.GetFiles();
                foreach (FileInfo file in files)
                {
                    iconSmall = Win32.SHGetFileInfo(file.FullName, 0, ref shinfo, /*(uint)Marshal.SizeOf(shinfo)*/ 80, Win32.SHGFI_ICON | Win32.SHGFI_SMALLICON);
                    Icon iconFile = Icon.FromHandle(shinfo.hIcon);
                    string dirFile = file.FullName;
                    string nameFile = Path.GetFileNameWithoutExtension(dirFile);
                    string typeFile = Path.GetExtension(dirFile);
                    try
                    {
                        typeFile = typeFile.Substring(1);
                    }
                    catch { }
                    string sizeFile = file.Length.ToString();
                    DateTime dateFile = File.GetCreationTime(dirFile);

                    directoryList.Add(new DirectoryList() { isFile = true, icon = iconFile, directory = dirFile, name = nameFile, type = typeFile, size = sizeFile, date = dateFile });
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }

            // Adding in ListView
            ImageList iconList = new ImageList();
            iconList.ColorDepth = ColorDepth.Depth32Bit;
            listViewDirectory.SmallImageList = iconList;
            listViewDirectory.LargeImageList = iconList;
            int count = 0;          

            foreach (DirectoryList lineDirectoryList in directoryList)
            {
                string[] item = { lineDirectoryList.name, lineDirectoryList.type, lineDirectoryList.size, lineDirectoryList.date.ToString("dd.MM.yyyy HH.mm.ss") };
                ListViewItem listItem = new ListViewItem(item);
                listItem.ImageIndex = count;
                iconList.Images.Add(lineDirectoryList.icon);
                listViewDirectory.Items.Add(listItem);
                count++;
            }
        }
    }
}
