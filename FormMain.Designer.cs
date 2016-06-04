namespace Commander
{
    partial class FormMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.panelControlButton = new System.Windows.Forms.Panel();
            this.tableLayoutPanelCommand = new System.Windows.Forms.TableLayoutPanel();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanelButton = new System.Windows.Forms.TableLayoutPanel();
            this.buttonF3View = new System.Windows.Forms.Button();
            this.buttonF4Edit = new System.Windows.Forms.Button();
            this.buttonF5Copy = new System.Windows.Forms.Button();
            this.buttonF6Move = new System.Windows.Forms.Button();
            this.buttonF7Directory = new System.Windows.Forms.Button();
            this.buttonF8Delete = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.panelDiskButton = new System.Windows.Forms.Panel();
            this.menuStrip.SuspendLayout();
            this.panelControlButton.SuspendLayout();
            this.tableLayoutPanelCommand.SuspendLayout();
            this.tableLayoutPanelButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(624, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // toolStrip
            // 
            this.toolStrip.Location = new System.Drawing.Point(0, 24);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(624, 25);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip";
            // 
            // panelControlButton
            // 
            this.panelControlButton.Controls.Add(this.tableLayoutPanelCommand);
            this.panelControlButton.Controls.Add(this.tableLayoutPanelButton);
            this.panelControlButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControlButton.Location = new System.Drawing.Point(0, 392);
            this.panelControlButton.Name = "panelControlButton";
            this.panelControlButton.Size = new System.Drawing.Size(624, 49);
            this.panelControlButton.TabIndex = 5;
            // 
            // tableLayoutPanelCommand
            // 
            this.tableLayoutPanelCommand.ColumnCount = 2;
            this.tableLayoutPanelCommand.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanelCommand.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70F));
            this.tableLayoutPanelCommand.Controls.Add(this.comboBox1, 1, 0);
            this.tableLayoutPanelCommand.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanelCommand.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelCommand.Name = "tableLayoutPanelCommand";
            this.tableLayoutPanelCommand.RowCount = 1;
            this.tableLayoutPanelCommand.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelCommand.Size = new System.Drawing.Size(624, 26);
            this.tableLayoutPanelCommand.TabIndex = 1;
            // 
            // comboBox1
            // 
            this.comboBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(190, 3);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(431, 21);
            this.comboBox1.TabIndex = 0;
            // 
            // tableLayoutPanelButton
            // 
            this.tableLayoutPanelButton.ColumnCount = 7;
            this.tableLayoutPanelButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanelButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanelButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanelButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanelButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanelButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanelButton.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 14.28571F));
            this.tableLayoutPanelButton.Controls.Add(this.buttonF3View, 0, 0);
            this.tableLayoutPanelButton.Controls.Add(this.buttonF4Edit, 1, 0);
            this.tableLayoutPanelButton.Controls.Add(this.buttonF5Copy, 2, 0);
            this.tableLayoutPanelButton.Controls.Add(this.buttonF6Move, 3, 0);
            this.tableLayoutPanelButton.Controls.Add(this.buttonF7Directory, 4, 0);
            this.tableLayoutPanelButton.Controls.Add(this.buttonF8Delete, 5, 0);
            this.tableLayoutPanelButton.Controls.Add(this.buttonExit, 6, 0);
            this.tableLayoutPanelButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanelButton.Location = new System.Drawing.Point(0, 26);
            this.tableLayoutPanelButton.Name = "tableLayoutPanelButton";
            this.tableLayoutPanelButton.RowCount = 1;
            this.tableLayoutPanelButton.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelButton.Size = new System.Drawing.Size(624, 23);
            this.tableLayoutPanelButton.TabIndex = 0;
            // 
            // buttonF3View
            // 
            this.buttonF3View.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonF3View.Location = new System.Drawing.Point(1, 1);
            this.buttonF3View.Margin = new System.Windows.Forms.Padding(1);
            this.buttonF3View.Name = "buttonF3View";
            this.buttonF3View.Size = new System.Drawing.Size(87, 21);
            this.buttonF3View.TabIndex = 0;
            this.buttonF3View.Text = "F3 Просмотр";
            this.buttonF3View.UseVisualStyleBackColor = true;
            // 
            // buttonF4Edit
            // 
            this.buttonF4Edit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonF4Edit.Location = new System.Drawing.Point(90, 1);
            this.buttonF4Edit.Margin = new System.Windows.Forms.Padding(1);
            this.buttonF4Edit.Name = "buttonF4Edit";
            this.buttonF4Edit.Size = new System.Drawing.Size(87, 21);
            this.buttonF4Edit.TabIndex = 1;
            this.buttonF4Edit.Text = "F4 Правка";
            this.buttonF4Edit.UseVisualStyleBackColor = true;
            // 
            // buttonF5Copy
            // 
            this.buttonF5Copy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonF5Copy.Location = new System.Drawing.Point(179, 1);
            this.buttonF5Copy.Margin = new System.Windows.Forms.Padding(1);
            this.buttonF5Copy.Name = "buttonF5Copy";
            this.buttonF5Copy.Size = new System.Drawing.Size(87, 21);
            this.buttonF5Copy.TabIndex = 2;
            this.buttonF5Copy.Text = "F5 Копировать";
            this.buttonF5Copy.UseVisualStyleBackColor = true;
            // 
            // buttonF6Move
            // 
            this.buttonF6Move.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonF6Move.Location = new System.Drawing.Point(268, 1);
            this.buttonF6Move.Margin = new System.Windows.Forms.Padding(1);
            this.buttonF6Move.Name = "buttonF6Move";
            this.buttonF6Move.Size = new System.Drawing.Size(87, 21);
            this.buttonF6Move.TabIndex = 3;
            this.buttonF6Move.Text = "F6 Переместить";
            this.buttonF6Move.UseVisualStyleBackColor = true;
            // 
            // buttonF7Directory
            // 
            this.buttonF7Directory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonF7Directory.Location = new System.Drawing.Point(357, 1);
            this.buttonF7Directory.Margin = new System.Windows.Forms.Padding(1);
            this.buttonF7Directory.Name = "buttonF7Directory";
            this.buttonF7Directory.Size = new System.Drawing.Size(87, 21);
            this.buttonF7Directory.TabIndex = 4;
            this.buttonF7Directory.Text = "F7 Каталог";
            this.buttonF7Directory.UseVisualStyleBackColor = true;
            // 
            // buttonF8Delete
            // 
            this.buttonF8Delete.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonF8Delete.Location = new System.Drawing.Point(446, 1);
            this.buttonF8Delete.Margin = new System.Windows.Forms.Padding(1);
            this.buttonF8Delete.Name = "buttonF8Delete";
            this.buttonF8Delete.Size = new System.Drawing.Size(87, 21);
            this.buttonF8Delete.TabIndex = 5;
            this.buttonF8Delete.Text = "F8 Удаление";
            this.buttonF8Delete.UseVisualStyleBackColor = true;
            // 
            // buttonExit
            // 
            this.buttonExit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.buttonExit.Location = new System.Drawing.Point(535, 1);
            this.buttonExit.Margin = new System.Windows.Forms.Padding(1);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(88, 21);
            this.buttonExit.TabIndex = 6;
            this.buttonExit.Text = "Alt+F4 Выход";
            this.buttonExit.UseVisualStyleBackColor = true;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 79);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Size = new System.Drawing.Size(624, 313);
            this.splitContainer.SplitterDistance = 311;
            this.splitContainer.TabIndex = 6;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // panelDiskButton
            // 
            this.panelDiskButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDiskButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelDiskButton.Location = new System.Drawing.Point(0, 49);
            this.panelDiskButton.Name = "panelDiskButton";
            this.panelDiskButton.Size = new System.Drawing.Size(624, 30);
            this.panelDiskButton.TabIndex = 2;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 441);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.panelControlButton);
            this.Controls.Add(this.panelDiskButton);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.panelControlButton.ResumeLayout(false);
            this.tableLayoutPanelCommand.ResumeLayout(false);
            this.tableLayoutPanelButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.Panel panelControlButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelButton;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.Button buttonF3View;
        private System.Windows.Forms.Button buttonF4Edit;
        private System.Windows.Forms.Button buttonF5Copy;
        private System.Windows.Forms.Button buttonF6Move;
        private System.Windows.Forms.Button buttonF7Directory;
        private System.Windows.Forms.Button buttonF8Delete;
        private System.Windows.Forms.Button buttonExit;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelCommand;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Panel panelDiskButton;
    }
}

