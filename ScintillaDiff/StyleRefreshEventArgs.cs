using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScintillaNET;

namespace ScintillaDiff
{
    /// <summary>
    /// A class used by the <see cref="ScintillaDiffControl"/> when external styling update is needed.
    /// Implements the <see cref="System.EventArgs" />
    /// </summary>
    /// <seealso cref="System.EventArgs" />
    public class StyleRefreshEventArgs: EventArgs
    {
        /// <summary>
        /// Gets the scintilla requesting for external style update.
        /// </summary>
        public Scintilla Scintilla { get; internal set; }
    }
}
