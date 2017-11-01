namespace SnakeWindowsFormsApplication
{
    partial class GameForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.menuDropDown = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameOption = new System.Windows.Forms.ToolStripMenuItem();
            this.loadGameOption = new System.Windows.Forms.ToolStripMenuItem();
            this.saveGameOption = new System.Windows.Forms.ToolStripMenuItem();
            this.exitOption = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsDropDown = new System.Windows.Forms.ToolStripMenuItem();
            this.smallGameTableOption = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumGameTableOption = new System.Windows.Forms.ToolStripMenuItem();
            this.bigGameTableOption = new System.Windows.Forms.ToolStripMenuItem();
            this.gameTableBox = new System.Windows.Forms.GroupBox();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.GameScoreLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.GameScoreTextLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.progressLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip2.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip2
            // 
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDropDown,
            this.settingsDropDown});
            this.menuStrip2.Location = new System.Drawing.Point(0, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(383, 24);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // menuDropDown
            // 
            this.menuDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameOption,
            this.loadGameOption,
            this.saveGameOption,
            this.exitOption});
            this.menuDropDown.Name = "menuDropDown";
            this.menuDropDown.Size = new System.Drawing.Size(50, 20);
            this.menuDropDown.Text = "Menü";
            // 
            // newGameOption
            // 
            this.newGameOption.Name = "newGameOption";
            this.newGameOption.Size = new System.Drawing.Size(151, 22);
            this.newGameOption.Text = "Új játék";
            this.newGameOption.Click += new System.EventHandler(this.newGameOption_Click);
            // 
            // loadGameOption
            // 
            this.loadGameOption.Name = "loadGameOption";
            this.loadGameOption.Size = new System.Drawing.Size(151, 22);
            this.loadGameOption.Text = "Játék betöltése";
            this.loadGameOption.Click += new System.EventHandler(this.loadGameOption_Click);
            // 
            // saveGameOption
            // 
            this.saveGameOption.Name = "saveGameOption";
            this.saveGameOption.Size = new System.Drawing.Size(151, 22);
            this.saveGameOption.Text = "Játék mentése";
            this.saveGameOption.Click += new System.EventHandler(this.saveGameOption_Click);
            // 
            // exitOption
            // 
            this.exitOption.Name = "exitOption";
            this.exitOption.Size = new System.Drawing.Size(151, 22);
            this.exitOption.Text = "KIlépés";
            this.exitOption.Click += new System.EventHandler(this.exitOption_Click);
            // 
            // settingsDropDown
            // 
            this.settingsDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.smallGameTableOption,
            this.mediumGameTableOption,
            this.bigGameTableOption});
            this.settingsDropDown.Name = "settingsDropDown";
            this.settingsDropDown.Size = new System.Drawing.Size(75, 20);
            this.settingsDropDown.Text = "Beállítások";
            // 
            // smallGameTableOption
            // 
            this.smallGameTableOption.Name = "smallGameTableOption";
            this.smallGameTableOption.Size = new System.Drawing.Size(148, 22);
            this.smallGameTableOption.Text = "Kis pálya";
            this.smallGameTableOption.Click += new System.EventHandler(this.smallGameTableOption_Click);
            // 
            // mediumGameTableOption
            // 
            this.mediumGameTableOption.Name = "mediumGameTableOption";
            this.mediumGameTableOption.Size = new System.Drawing.Size(148, 22);
            this.mediumGameTableOption.Text = "Közepes pálya";
            this.mediumGameTableOption.Click += new System.EventHandler(this.mediumGameTableOption_Click);
            // 
            // bigGameTableOption
            // 
            this.bigGameTableOption.Name = "bigGameTableOption";
            this.bigGameTableOption.Size = new System.Drawing.Size(148, 22);
            this.bigGameTableOption.Text = "Nagy pálya";
            this.bigGameTableOption.Click += new System.EventHandler(this.bigGameTableOption_Click);
            // 
            // gameTableBox
            // 
            this.gameTableBox.Location = new System.Drawing.Point(12, 40);
            this.gameTableBox.Name = "gameTableBox";
            this.gameTableBox.Size = new System.Drawing.Size(200, 100);
            this.gameTableBox.TabIndex = 2;
            this.gameTableBox.TabStop = false;
            this.gameTableBox.Text = "Játék tábla";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Snake tábla (*.stl)|*.stl";
            this.saveFileDialog.Title = "Snake játék mentése";
            // 
            // openFileDialog
            // 
        
            this.openFileDialog.Filter = "Snake tábla (*.stl)|*.stl";
            this.openFileDialog.Title = "Snake játék betöltése";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GameScoreLabel,
            this.GameScoreTextLabel,
            this.progressLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 256);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(383, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // GameScoreLabel
            // 
            this.GameScoreLabel.Name = "GameScoreLabel";
            this.GameScoreLabel.Size = new System.Drawing.Size(48, 17);
            this.GameScoreLabel.Text = "Pontok:";
            // 
            // GameScoreTextLabel
            // 
            this.GameScoreTextLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.GameScoreTextLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.GameScoreTextLabel.Name = "GameScoreTextLabel";
            this.GameScoreTextLabel.Size = new System.Drawing.Size(0, 17);
            this.GameScoreTextLabel.Text = "0";
            // 
            // progressLabel
            // 
            this.progressLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.progressLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(54, 17);
            this.progressLabel.Text = "Esemény";
            // 
            // GameForm
            // 
            this.ClientSize = new System.Drawing.Size(383, 278);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gameTableBox);
            this.Controls.Add(this.menuStrip2);
            this.MainMenuStrip = this.menuStrip2;
            this.Name = "GameForm";
            this.Text = "Snake";
            this.Load += new System.EventHandler(this.GameForm_Load);
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem _menu;
        private System.Windows.Forms.ToolStripMenuItem újJátékToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem játékMentéseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem játékBetöltéseToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem kilépésToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem _map;
        private System.Windows.Forms.ToolStripMenuItem kicsiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem közepesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nagyToolStripMenuItem;
        private System.Windows.Forms.Label _score;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem menuDropDown;
        private System.Windows.Forms.ToolStripMenuItem newGameOption;
        private System.Windows.Forms.ToolStripMenuItem loadGameOption;
        private System.Windows.Forms.ToolStripMenuItem saveGameOption;
        private System.Windows.Forms.ToolStripMenuItem exitOption;
        private System.Windows.Forms.ToolStripMenuItem settingsDropDown;
        private System.Windows.Forms.ToolStripMenuItem smallGameTableOption;
        private System.Windows.Forms.ToolStripMenuItem mediumGameTableOption;
        private System.Windows.Forms.ToolStripMenuItem bigGameTableOption;
        private System.Windows.Forms.GroupBox gameTableBox;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel GameScoreLabel;
        private System.Windows.Forms.ToolStripStatusLabel GameScoreTextLabel;
        private System.Windows.Forms.ToolStripStatusLabel progressLabel;
    }
}

