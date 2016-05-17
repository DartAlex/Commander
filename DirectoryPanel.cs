using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Commander
{
    class DirectoryPanel : Panel
    {
        ComboBox comboBoxDrive = new ComboBox();
        Label labelFreeSpace = new Label();
        Button buttonToRoot = new Button();
        Button buttonToParent = new Button();

        public void CreateInterface()
        {
            comboBoxDrive.Location = new Point(2, 2);
            comboBoxDrive.Size = new Size(42, 21);
            comboBoxDrive.Text = "[-c-]";
            this.Controls.Add(comboBoxDrive);

            labelFreeSpace.Location = new Point(44, 6);
            labelFreeSpace.AutoSize = true;
            labelFreeSpace.Text = "Free space disck";
            this.Controls.Add(labelFreeSpace);

            buttonToParent.Location = new Point(this.Width - 23, 2);
        }
    }
}
