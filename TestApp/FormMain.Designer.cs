namespace TestApp
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenFileOne = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenFileTwo = new System.Windows.Forms.ToolStripMenuItem();
            this.odAnyFile = new System.Windows.Forms.OpenFileDialog();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.btJumpBackwards = new System.Windows.Forms.ToolStripButton();
            this.btJumpForwards = new System.Windows.Forms.ToolStripButton();
            this.btSingleView = new System.Windows.Forms.ToolStripButton();
            this.scintillaDiffControl = new ScintillaDiff.ScintillaDiffControl();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuAbout});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(800, 24);
            this.msMain.TabIndex = 1;
            this.msMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuOpenFileOne,
            this.mnuOpenFileTwo});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "File";
            // 
            // mnuOpenFileOne
            // 
            this.mnuOpenFileOne.Name = "mnuOpenFileOne";
            this.mnuOpenFileOne.Size = new System.Drawing.Size(145, 22);
            this.mnuOpenFileOne.Text = "Open file one";
            this.mnuOpenFileOne.Click += new System.EventHandler(this.MnuOpenFile_Click);
            // 
            // mnuOpenFileTwo
            // 
            this.mnuOpenFileTwo.Name = "mnuOpenFileTwo";
            this.mnuOpenFileTwo.Size = new System.Drawing.Size(145, 22);
            this.mnuOpenFileTwo.Text = "Open file two";
            this.mnuOpenFileTwo.Click += new System.EventHandler(this.MnuOpenFile_Click);
            // 
            // odAnyFile
            // 
            this.odAnyFile.Filter = "All files|*.*";
            // 
            // tsMain
            // 
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btJumpBackwards,
            this.btJumpForwards,
            this.btSingleView});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(800, 25);
            this.tsMain.TabIndex = 2;
            this.tsMain.Text = "toolStrip1";
            // 
            // btJumpBackwards
            // 
            this.btJumpBackwards.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btJumpBackwards.Image = ((System.Drawing.Image)(resources.GetObject("btJumpBackwards.Image")));
            this.btJumpBackwards.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btJumpBackwards.Name = "btJumpBackwards";
            this.btJumpBackwards.Size = new System.Drawing.Size(27, 22);
            this.btJumpBackwards.Text = "<<";
            this.btJumpBackwards.Click += new System.EventHandler(this.BtJumpBackwards_Click);
            // 
            // btJumpForwards
            // 
            this.btJumpForwards.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btJumpForwards.Image = ((System.Drawing.Image)(resources.GetObject("btJumpForwards.Image")));
            this.btJumpForwards.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btJumpForwards.Name = "btJumpForwards";
            this.btJumpForwards.Size = new System.Drawing.Size(27, 22);
            this.btJumpForwards.Text = ">>";
            this.btJumpForwards.Click += new System.EventHandler(this.BtJumpForwards_Click);
            // 
            // btSingleView
            // 
            this.btSingleView.CheckOnClick = true;
            this.btSingleView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btSingleView.Image = ((System.Drawing.Image)(resources.GetObject("btSingleView.Image")));
            this.btSingleView.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btSingleView.Name = "btSingleView";
            this.btSingleView.Size = new System.Drawing.Size(70, 22);
            this.btSingleView.Text = "Single view";
            this.btSingleView.Click += new System.EventHandler(this.BtSingleView_Click);
            // 
            // scintillaDiffControl
            // 
            this.scintillaDiffControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scintillaDiffControl.DiffColorAdded = System.Drawing.Color.FromArgb(((int)(((byte)(135)))), ((int)(((byte)(255)))), ((int)(((byte)(135)))));
            this.scintillaDiffControl.DiffColorChangeBackground = System.Drawing.Color.FromArgb(((int)(((byte)(252)))), ((int)(((byte)(255)))), ((int)(((byte)(140)))));
            this.scintillaDiffControl.DiffColorDeleted = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(178)))), ((int)(((byte)(178)))));
            this.scintillaDiffControl.DiffStyle = ScintillaDiff.ScintillaDiffStyles.DiffStyle.DiffSideBySide;
            this.scintillaDiffControl.ImageRowAdded = ((System.Drawing.Bitmap)(resources.GetObject("scintillaDiffControl.ImageRowAdded")));
            this.scintillaDiffControl.ImageRowAddedScintillaIndex = 28;
            this.scintillaDiffControl.ImageRowDeleted = ((System.Drawing.Bitmap)(resources.GetObject("scintillaDiffControl.ImageRowDeleted")));
            this.scintillaDiffControl.ImageRowDeletedScintillaIndex = 29;
            this.scintillaDiffControl.ImageRowDiff = ((System.Drawing.Bitmap)(resources.GetObject("scintillaDiffControl.ImageRowDiff")));
            this.scintillaDiffControl.ImageRowDiffScintillaIndex = 31;
            this.scintillaDiffControl.ImageRowOk = ((System.Drawing.Bitmap)(resources.GetObject("scintillaDiffControl.ImageRowOk")));
            this.scintillaDiffControl.ImageRowOkScintillaIndex = 30;
            this.scintillaDiffControl.Location = new System.Drawing.Point(12, 52);
            this.scintillaDiffControl.MarkColorIndexModifiedBackground = 31;
            this.scintillaDiffControl.MarkColorIndexRemovedOrAdded = 30;
            this.scintillaDiffControl.Name = "scintillaDiffControl";
            this.scintillaDiffControl.Size = new System.Drawing.Size(776, 386);
            this.scintillaDiffControl.TabIndex = 0;
            this.scintillaDiffControl.TextLeft = "";
            this.scintillaDiffControl.TextRight = "";
            this.scintillaDiffControl.UseRowOkSign = false;
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(52, 20);
            this.mnuAbout.Text = "About";
            this.mnuAbout.Click += new System.EventHandler(this.MnuAbout_Click);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.scintillaDiffControl);
            this.Controls.Add(this.msMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "FormMain";
            this.Text = "Test application for the ScintillaDiff library    © VPKSoft 2019";
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ScintillaDiff.ScintillaDiffControl scintillaDiffControl;
        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenFileOne;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenFileTwo;
        private System.Windows.Forms.OpenFileDialog odAnyFile;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton btJumpBackwards;
        private System.Windows.Forms.ToolStripButton btJumpForwards;
        private System.Windows.Forms.ToolStripButton btSingleView;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
    }
}

