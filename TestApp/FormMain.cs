#region License
/*
MIT License

Copyright(c) 2020 Petteri Kautonen

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
*/
#endregion

using System;
using System.IO;
using System.Windows.Forms;
using VPKSoft.VersionCheck.Forms;
using static ScintillaDiff.ScintillaDiffStyles;

namespace TestApp
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void MnuOpenFile_Click(object sender, EventArgs e)
        {
            if (odAnyFile.ShowDialog() == DialogResult.OK)
            {
                if (sender.Equals(mnuOpenFileOne))
                {
                    scintillaDiffControl.TextLeft = File.ReadAllText(odAnyFile.FileName);
                }
                else if (sender.Equals(mnuOpenFileTwo))
                {
                    scintillaDiffControl.TextRight = File.ReadAllText(odAnyFile.FileName);
                }
            }

            btJumpBackwards.Enabled = scintillaDiffControl.CanGoPrevious;
            btJumpForwards.Enabled = scintillaDiffControl.CanGoNext;
        }

        private void BtJumpBackwards_Click(object sender, EventArgs e)
        {
            scintillaDiffControl.Previous();
            btJumpBackwards.Enabled = scintillaDiffControl.CanGoPrevious;
            btJumpForwards.Enabled = scintillaDiffControl.CanGoNext;
        }

        private void BtJumpForwards_Click(object sender, EventArgs e)
        {
            scintillaDiffControl.Next();
            btJumpBackwards.Enabled = scintillaDiffControl.CanGoPrevious;
            btJumpForwards.Enabled = scintillaDiffControl.CanGoNext;
        }

        private void BtSingleView_Click(object sender, EventArgs e)
        {
            var button = (ToolStripButton) sender;
            scintillaDiffControl.DiffStyle = button.Checked ? DiffStyle.DiffList : DiffStyle.DiffSideBySide;
        }

        private void MnuAbout_Click(object sender, EventArgs e)
        {
            // ReSharper disable once ObjectCreationAsStatement
            new FormAbout(this, "MIT", "https://raw.githubusercontent.com/VPKSoft/ScriptNotepad/master/LICENSE", "N/A");
        }
    }
}
