using ScintillaNET;

namespace ScintillaDiff
{
    partial class ScintillaDiffControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.scintillaOne = new ScintillaDiff.ScrollSyncScintilla();
            this.scintillaTwo = new ScintillaDiff.ScrollSyncScintilla();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.scintillaOne);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.scintillaTwo);
            this.scMain.Size = new System.Drawing.Size(918, 492);
            this.scMain.SplitterDistance = 434;
            this.scMain.TabIndex = 0;
            // 
            // scintillaOne
            // 
            this.scintillaOne.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaOne.Location = new System.Drawing.Point(0, 0);
            this.scintillaOne.Name = "scintillaOne";
            this.scintillaOne.ScrollSync = this.scintillaTwo;
            this.scintillaOne.Size = new System.Drawing.Size(434, 492);
            this.scintillaOne.TabIndex = 0;
            this.scintillaOne.TextChanged += new System.EventHandler(this.Scintilla_TextChanged);
            // 
            // scintillaTwo
            // 
            this.scintillaTwo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scintillaTwo.Location = new System.Drawing.Point(0, 0);
            this.scintillaTwo.Name = "scintillaTwo";
            this.scintillaTwo.ScrollSync = this.scintillaOne;
            this.scintillaTwo.Size = new System.Drawing.Size(480, 492);
            this.scintillaTwo.TabIndex = 0;
            this.scintillaTwo.TextChanged += new System.EventHandler(this.Scintilla_TextChanged);
            // 
            // ScintillaDiffControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scMain);
            this.Name = "ScintillaDiffControl";
            this.Size = new System.Drawing.Size(918, 492);
            this.SizeChanged += new System.EventHandler(this.ScintillaDiffer_SizeChanged);
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMain;
        private ScrollSyncScintilla scintillaOne;
        private ScrollSyncScintilla scintillaTwo;
    }
}
