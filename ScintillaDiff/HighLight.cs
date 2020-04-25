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

using System.Drawing;
using ScintillaNET;

namespace ScintillaDiff
{
    /// <summary>
    /// A helper class to highlight words with a given color.
    /// </summary>
    public class Highlight
    {
		/// <summary>
		/// Highlights a given range with a given color and given alpha values of the <see cref="Scintilla"/> control.
		/// </summary>
		/// <param name="scintilla">The scintilla of which words to highlight.</param>
		/// <param name="num">The indicator number for the <paramref name="scintilla"/>.Indicators 0-7 could be in use by a lexer so use a higher value.</param>
		/// <param name="start">The starting position of the highlight area.</param>
		/// <param name="length">The length of the highlight area.</param>
		/// <param name="color">The color to use for the highlight.</param>
		/// <param name="alpha">The transparency value of the indicator.</param>
		/// <param name="outlineAlpha">The transparency value of the indicator outline.</param>
		/// <note>(C): https://github.com/jacobslusser/ScintillaNET/wiki/Find-and-Highlight-Words</note>
		public static void HighlightRange(Scintilla scintilla, int num, int start, int length, Color color, byte alpha = 255, byte outlineAlpha = 255)
        {
            // Remove all uses of our indicator
            scintilla.IndicatorCurrent = num;

            // Update indicator appearance
            scintilla.Indicators[num].Style = IndicatorStyle.StraightBox;
            scintilla.Indicators[num].Under = true;
            scintilla.Indicators[num].ForeColor = color;
            scintilla.Indicators[num].OutlineAlpha = alpha;
            scintilla.Indicators[num].Alpha = outlineAlpha;

            // Mark the search position with the current indicator..
            scintilla.IndicatorFillRange(start, length);
        }

		/// <summary>
		/// Highlights an entire line
		/// </summary>
		/// <param name="scintilla">The scintilla of which contains the line to highlight.</param>
		/// <param name="lineIndex">The index of the line to be highlighted within the scintilla.</param>
		/// <param name="changeType">The type of change has been made, indicating which colour to highlight the line with.</param>
		public static void HighlightLine(Scintilla scintilla, int lineIndex, int changeType)
		{
			int start = scintilla.Lines[lineIndex].Position;
			int length = scintilla.Lines[lineIndex].Length;

			scintilla.StartStyling(start);
			scintilla.SetStyling(length, changeType);
		}

        /// <summary>
        /// Clears the given style form a <see cref="Scintilla"/>.
        /// </summary>
        /// <param name="scintilla">The scintilla to clear the style from.</param>
        /// <param name="num">The number of the style to clear.</param>
        // ReSharper disable once UnusedMember.Global
        public static void ClearStyle(Scintilla scintilla, int num)
        {
            scintilla.IndicatorCurrent = num;
            scintilla.IndicatorClearRange(0, scintilla.TextLength);
        }
    }
}
