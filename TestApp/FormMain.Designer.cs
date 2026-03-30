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
            msMain = new System.Windows.Forms.MenuStrip();
            mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            mnuOpenFileOne = new System.Windows.Forms.ToolStripMenuItem();
            mnuOpenFileTwo = new System.Windows.Forms.ToolStripMenuItem();
            odAnyFile = new System.Windows.Forms.OpenFileDialog();
            tsMain = new System.Windows.Forms.ToolStrip();
            btJumpBackwards = new System.Windows.Forms.ToolStripButton();
            btJumpForwards = new System.Windows.Forms.ToolStripButton();
            btSingleView = new System.Windows.Forms.ToolStripButton();
            scintillaDiffControl = new ScintillaDiff.ScintillaDiffControl();
            msMain.SuspendLayout();
            tsMain.SuspendLayout();
            SuspendLayout();
            // 
            // msMain
            // 
            msMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { mnuFile });
            msMain.Location = new System.Drawing.Point(0, 0);
            msMain.Name = "msMain";
            msMain.Padding = new System.Windows.Forms.Padding(10, 4, 0, 4);
            msMain.Size = new System.Drawing.Size(1333, 37);
            msMain.TabIndex = 1;
            msMain.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { mnuOpenFileOne, mnuOpenFileTwo });
            mnuFile.Name = "mnuFile";
            mnuFile.Size = new System.Drawing.Size(54, 29);
            mnuFile.Text = "File";
            // 
            // mnuOpenFileOne
            // 
            mnuOpenFileOne.Name = "mnuOpenFileOne";
            mnuOpenFileOne.Size = new System.Drawing.Size(221, 34);
            mnuOpenFileOne.Text = "Open file one";
            mnuOpenFileOne.Click += MnuOpenFile_Click;
            // 
            // mnuOpenFileTwo
            // 
            mnuOpenFileTwo.Name = "mnuOpenFileTwo";
            mnuOpenFileTwo.Size = new System.Drawing.Size(221, 34);
            mnuOpenFileTwo.Text = "Open file two";
            mnuOpenFileTwo.Click += MnuOpenFile_Click;
            // 
            // odAnyFile
            // 
            odAnyFile.Filter = "All files|*.*";
            // 
            // tsMain
            // 
            tsMain.ImageScalingSize = new System.Drawing.Size(24, 24);
            tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { btJumpBackwards, btJumpForwards, btSingleView });
            tsMain.Location = new System.Drawing.Point(0, 37);
            tsMain.Name = "tsMain";
            tsMain.Padding = new System.Windows.Forms.Padding(0, 0, 3, 0);
            tsMain.Size = new System.Drawing.Size(1333, 34);
            tsMain.TabIndex = 2;
            tsMain.Text = "toolStrip1";
            // 
            // btJumpBackwards
            // 
            btJumpBackwards.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            btJumpBackwards.Image = (System.Drawing.Image)resources.GetObject("btJumpBackwards.Image");
            btJumpBackwards.ImageTransparentColor = System.Drawing.Color.Magenta;
            btJumpBackwards.Name = "btJumpBackwards";
            btJumpBackwards.Size = new System.Drawing.Size(40, 29);
            btJumpBackwards.Text = "<<";
            btJumpBackwards.Click += BtJumpBackwards_Click;
            // 
            // btJumpForwards
            // 
            btJumpForwards.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            btJumpForwards.Image = (System.Drawing.Image)resources.GetObject("btJumpForwards.Image");
            btJumpForwards.ImageTransparentColor = System.Drawing.Color.Magenta;
            btJumpForwards.Name = "btJumpForwards";
            btJumpForwards.Size = new System.Drawing.Size(40, 29);
            btJumpForwards.Text = ">>";
            btJumpForwards.Click += BtJumpForwards_Click;
            // 
            // btSingleView
            // 
            btSingleView.CheckOnClick = true;
            btSingleView.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            btSingleView.Image = (System.Drawing.Image)resources.GetObject("btSingleView.Image");
            btSingleView.ImageTransparentColor = System.Drawing.Color.Magenta;
            btSingleView.Name = "btSingleView";
            btSingleView.Size = new System.Drawing.Size(104, 29);
            btSingleView.Text = "Single view";
            btSingleView.Click += BtSingleView_Click;
            // 
            // scintillaDiffControl
            // 
            scintillaDiffControl.AddedCharacterSymbol = '+';
            scintillaDiffControl.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            scintillaDiffControl.CharacterComparison = true;
            scintillaDiffControl.CharacterComparisonMarkAddRemove = true;
            scintillaDiffControl.DiffColorAdded = System.Drawing.Color.FromArgb(192, 255, 192);
            scintillaDiffControl.DiffColorChangeBackground = System.Drawing.Color.FromArgb(252, 255, 140);
            scintillaDiffControl.DiffColorCharAdded = System.Drawing.Color.Lime;
            scintillaDiffControl.DiffColorCharDeleted = System.Drawing.Color.FromArgb(225, 125, 125);
            scintillaDiffControl.DiffColorDeleted = System.Drawing.Color.FromArgb(255, 178, 178);
            scintillaDiffControl.DiffStyle = ScintillaDiff.ScintillaDiffStyles.DiffStyle.DiffSideBySide;
            scintillaDiffControl.ImageRowAdded = (System.Drawing.Bitmap)resources.GetObject("scintillaDiffControl.ImageRowAdded");
            scintillaDiffControl.ImageRowAddedScintillaIndex = 28;
            scintillaDiffControl.ImageRowDeleted = (System.Drawing.Bitmap)resources.GetObject("scintillaDiffControl.ImageRowDeleted");
            scintillaDiffControl.ImageRowDeletedScintillaIndex = 29;
            scintillaDiffControl.ImageRowDiff = (System.Drawing.Bitmap)resources.GetObject("scintillaDiffControl.ImageRowDiff");
            scintillaDiffControl.ImageRowDiffScintillaIndex = 31;
            scintillaDiffControl.ImageRowOk = (System.Drawing.Bitmap)resources.GetObject("scintillaDiffControl.ImageRowOk");
            scintillaDiffControl.ImageRowOkScintillaIndex = 30;
            scintillaDiffControl.IsEntireLineHighlighted = false;
            scintillaDiffControl.Location = new System.Drawing.Point(20, 100);
            scintillaDiffControl.Margin = new System.Windows.Forms.Padding(8, 12, 8, 12);
            scintillaDiffControl.MarkColorIndexModifiedBackground = 31;
            scintillaDiffControl.MarkColorIndexRemovedOrAdded = 30;
            scintillaDiffControl.Name = "scintillaDiffControl";
            scintillaDiffControl.RemovedCharacterSymbol = '-';
            scintillaDiffControl.Size = new System.Drawing.Size(1293, 742);
            scintillaDiffControl.TabIndex = 0;
            scintillaDiffControl.TextLeft = "";
            scintillaDiffControl.TextRight = "";
            scintillaDiffControl.UseRowOkSign = false;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1333, 865);
            Controls.Add(tsMain);
            Controls.Add(scintillaDiffControl);
            Controls.Add(msMain);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = msMain;
            Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            Name = "FormMain";
            Text = "Test application for the ScintillaDiff library    © VPKSoft 2020";
            msMain.ResumeLayout(false);
            msMain.PerformLayout();
            tsMain.ResumeLayout(false);
            tsMain.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

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
    }
}

