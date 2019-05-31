using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;

namespace ScintillaDiff
{
    /// <summary>
    /// An inherited class from the <see cref="Scintilla"/> which can synchronize vertical scroll bar position with another <see cref="Scintilla"/> control.
    /// Implements the <see cref="ScintillaNET.Scintilla" />
    /// </summary>
    /// <seealso cref="ScintillaNET.Scintilla" />
    public class ScrollSyncScintilla: Scintilla
    {
        // ReSharper disable once CommentTypo
        /// <summary>
        /// The WM_VSCROLL message is sent to a window when a scroll event occurs in the window's standard vertical scroll bar.
        /// This message is also sent to the owner of a vertical scroll bar control when a scroll event occurs in the control.
        /// <remarks>
        /// https://docs.microsoft.com/en-us/windows/desktop/controls/wm-vscroll
        /// </remarks>
        /// </summary>
        const int WM_VSCROLL = 0x115;

        /// <summary>
        /// Processes Windows messages.
        /// </summary>
        /// <param name="m">The Windows Message to process.</param>
        protected override void WndProc(ref Message m)
        {
            // ReSharper disable once CommentTypo
            // capture the WM_VSCROLL message..
            if (m.Msg == WM_VSCROLL && ScrollSync != null)
            {
                // get the first visible line of this control..
                int scrollTop = FirstVisibleLine;

                // set the value to the another Scintilla control..
                ScrollSync.FirstVisibleLine = scrollTop;
            }
            base.WndProc(ref m);
        }

        /// <summary>
        /// Gets or sets the <see cref="Scintilla"/> to synchronize the vertical scroll bar with.
        /// </summary>
        [Browsable(true)]
        [Category("Miscellaneous")]
        [Description("Gets or sets the Scintilla to synchronize the vertical scroll bar with.")]
        public Scintilla ScrollSync { get; set; }
    }
}
