using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.Drawing;

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
        ListView listViewDirectory = new ListView();
        

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

            listViewDirectory.Columns.Add("Имя", -2, HorizontalAlignment.Left);
            listViewDirectory.Columns.Add("Тип", -2, HorizontalAlignment.Left);
            listViewDirectory.Columns.Add("Размер", -2, HorizontalAlignment.Left);
            listViewDirectory.Columns.Add("Дата", -2, HorizontalAlignment.Left);

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
    }
}
