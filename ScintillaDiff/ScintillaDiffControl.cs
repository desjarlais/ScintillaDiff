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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;
using ScintillaNET;
using static ScintillaDiff.ScintillaDiffStyles;

namespace ScintillaDiff
{
    /// <summary>
    /// A control for comparing two text files using <see cref="Scintilla"/> controls.
    /// Implements the <see cref="System.Windows.Forms.UserControl" />
    /// </summary>
    /// <seealso cref="System.Windows.Forms.UserControl" />
    public partial class ScintillaDiffControl : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScintillaDiffControl"/> class.
        /// </summary>
        public ScintillaDiffControl()
        {
            InitializeComponent();

            // this tag values are for line number length counting..
            scintillaOne.Tag = -1;
            scintillaTwo.Tag = -1;

            SetSymbolMasks();

            InitScintillaMargins();

            SetSymbols();
        }


        #region PrivateEvents
        // if the control size has changed, set the splitter to the middle..
        private void ScintillaDiffer_SizeChanged(object sender, EventArgs e)
        {
            RecalculateSize();
        }

        #region https://github.com/jacobslusser/ScintillaNET/wiki/Displaying-Line-Numbers

        private void Scintilla_TextChanged(object sender, EventArgs e)
        {
            Scintilla scintilla = (Scintilla) sender;

            int maxLineNumberCharLengthFromTag = (int) scintilla.Tag;

            // Did the number of characters in the line number display change?
            // i.e. nnn VS nn, or nnnn VS nn, etc...
            var maxLineNumberCharLength = scintilla.Lines.Count.ToString().Length;
            if (maxLineNumberCharLength == maxLineNumberCharLengthFromTag)
                return;

            // Calculate the width required to display the last line number
            // and include some padding for good measure.
            const int padding = 2;
            scintilla.Margins[0].Width =
                scintilla.TextWidth(Style.LineNumber, new string('9', maxLineNumberCharLength + 1)) + padding;
            scintilla.Tag = maxLineNumberCharLength;
        }
        #endregion
        #endregion

        #region PrivateFields
        private string textLeft = string.Empty;
        private string textRight = string.Empty;
        private Bitmap imageRowAdded = Properties.Resources.plus;
        private Bitmap imageRowDeleted = Properties.Resources.minus;
        private Bitmap imageRowOk = Properties.Resources.ok;
        private Bitmap imageRowDiff = Properties.Resources.diff;
        private int imageRowAddedScintillaIndex = 28;
        private int imageRowDeletedScintillaIndex = 29;
        private int imageRowOkScintillaIndex = 30;
        private int imageRowDiffScintillaIndex = 31;
        private int markColorIndexRemovedOrAdded = 30;
        private int markColorIndexModifiedBackground = 31;
        private bool useRowOkSign;
        private DiffStyle diffStyle = DiffStyle.DiffList;
        private Color diffColorDeleted = Color.FromArgb(0xFF, 0XFF, 0XB2, 0XB2);
        private Color diffColorAdded = Color.FromArgb(0xFF, 0X87, 0XFF, 0X87);
        private Color diffColorChangeBackground = Color.FromArgb(0xFF, 0XFC, 0XFF, 0X8C);
        private int diffIndex;
        private readonly StringBuilder builderLeft = new StringBuilder();
        private readonly StringBuilder builderRight = new StringBuilder();

        #endregion

        #region PublicProperties
        /// <summary>
        /// Gets the left <see cref="Scintilla"/> control.
        /// </summary>
        [Browsable(false)]
        // ReSharper disable once ConvertToAutoPropertyWhenPossible
        // ReSharper disable once UnusedMember.Global
        public Scintilla LeftScintilla => scintillaOne;

        /// <summary>
        /// Gets the value indicating whether a navigation to the previous difference is possible. (<seealso cref="Previous"/>).
        /// </summary>
        [Browsable(false)]
        public bool CanGoPrevious => diffIndex > 0 && DiffLocations.Count > 0;

        /// <summary>
        /// Gets the value indicating whether a navigation to the next difference is possible. (<seealso cref="Next"/>).
        /// </summary>
        [Browsable(false)]
        public bool CanGoNext => diffIndex + 1 < DiffLocations.Count;

        /// <summary>
        /// Gets the right <see cref="Scintilla"/> control.
        /// </summary>
        [Browsable(false)]
        // ReSharper disable once ConvertToAutoPropertyWhenPossible
        // ReSharper disable once UnusedMember.Global
        public Scintilla RightScintilla => scintillaTwo;

        /// <summary>
        /// Gets the difference locations found by the <see cref="Differ"/> class.
        /// </summary>
        [Browsable(false)]
        public List<int> DiffLocations { get; internal set; } = new List<int>();

        /// <summary>
        /// Gets a value indicating whether the two compared texts differs from each-other.
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public bool IsMatch => textLeft.Equals(textRight);
        #endregion

        #region PublicEvents        
        /// <summary>
        /// A delegate for the <see cref="ExternalStyleNeeded"/> event.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="StyleRefreshEventArgs"/> instance containing the event data.</param>
        public delegate void OnExternalStyleNeeded(object sender, StyleRefreshEventArgs e);

        /// <summary>
        /// Occurs when external styling is needed to keep the document style up to date
        /// (i.e. a class property change has caused the diff to update thus clearing the document's style).
        /// </summary>
        public event OnExternalStyleNeeded ExternalStyleNeeded;
        #endregion

        #region PublicVisualProperties
        /// <summary>
        /// Gets or sets the index for the style for a mark color used by the <see cref="Scintilla"/> control to indicate a addition or a deletion difference.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value must be between 0 and 31.</exception>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the index for the style index for a mark color used by the Scintilla control to indicate a addition or a deletion difference.")]
        public int MarkColorIndexModifiedBackground
        {
            get => markColorIndexModifiedBackground;

            set
            {
                if (value != markColorIndexModifiedBackground)
                {
                    if (value < 0 || value > 31)
                    {
                        // ReSharper disable once LocalizableElement
                        throw new ArgumentOutOfRangeException(nameof(value), "The value must be between 0 and 31.");
                    }

                    markColorIndexModifiedBackground = value;
                    DiffTexts();
                }
            }
        }

        /// <summary>
        /// Gets or sets the index for the style for a background color color used by the <see cref="Scintilla"/> control to indicate a change in file line.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value must be between 0 and 31.</exception>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the index for the style for a background color color used by the Scintilla control to indicate a change in file line.")]
        public int MarkColorIndexRemovedOrAdded
        {
            get => markColorIndexRemovedOrAdded;

            set
            {
                if (value != markColorIndexRemovedOrAdded)
                {
                    if (value < 0 || value > 31)
                    {
                        // ReSharper disable once LocalizableElement
                        throw new ArgumentOutOfRangeException(nameof(value), "The value must be between 0 and 31.");
                    }

                    markColorIndexRemovedOrAdded = value;
                    DiffTexts();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to mark unchanged lines with an ok sign.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets a value indicating whether to mark unchanged lines with an ok sign.")]
        public bool UseRowOkSign
        {
            get => useRowOkSign;

            set
            {
                if (useRowOkSign != value)
                {
                    useRowOkSign = value;
                    DiffTexts();
                }
            }
        }

        /// <summary>
        /// Gets or sets a diff style of the control.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets a diff style of the control.")]
        public DiffStyle DiffStyle
        {
            get => diffStyle;

            set
            {
                if (diffStyle != value)
                {
                    diffStyle = value;

                    // don't synchronize the scroll bars with list view..
                    if (diffStyle == DiffStyle.DiffList)
                    {
                        scintillaOne.ScrollSync = null;
                        scintillaTwo.ScrollSync = null;
                    }
                    else
                    {
                        // synchronize the scroll bars with side-by-side view..
                        scintillaOne.ScrollSync = scintillaTwo;
                        scintillaTwo.ScrollSync = scintillaOne;
                    }

                    DiffTexts();
                }
            }
        }

        /// <summary>
        /// Gets or sets the indicator for the diff that a row was added.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the indicator for the diff that a row was added.")]
        public Bitmap ImageRowAdded
        {
            get => imageRowAdded;
            set
            {
                if (value != imageRowAdded && value != null)
                {
                    imageRowAdded = value;
                    SetSymbols();
                }
            }
        }

        /// <summary>
        /// Gets or sets the index for the <see cref="ImageRowAdded"/> used by the <see cref="Scintilla"/> control.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value must be between 0 and 31.</exception>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the index for the ImageRowAdded used by the Scintilla control.")]
        public int ImageRowAddedScintillaIndex
        {
            get => imageRowAddedScintillaIndex;

            set
            {
                if (value != imageRowAddedScintillaIndex)
                {
                    if (value < 0 || value > 31)
                    {
                        // ReSharper disable once LocalizableElement
                        throw new ArgumentOutOfRangeException(nameof(value), "The value must be between 0 and 31.");
                    }

                    imageRowAddedScintillaIndex = value;
                    SetSymbolMasks();
                }
            }
        }

        /// <summary>
        /// Gets or sets the indicator for the diff that a row was deleted.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the indicator for the diff that a row was deleted.")]
        public Bitmap ImageRowDeleted
        {
            get => imageRowDeleted;
            set
            {
                if (value != imageRowDeleted && value != null)
                {
                    imageRowDeleted = value;
                    SetSymbols();
                }
            }
        }

        /// <summary>
        /// Gets or sets the index for the <see cref="ImageRowDeleted"/> used by the <see cref="Scintilla"/> control.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value must be between 0 and 31.</exception>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the index for the ImageRowDeleted used by the Scintilla control.")]
        public int ImageRowDeletedScintillaIndex
        {
            get => imageRowDeletedScintillaIndex;

            set
            {
                if (value != imageRowDeletedScintillaIndex)
                {
                    if (value < 0 || value > 31)
                    {
                        // ReSharper disable once LocalizableElement
                        throw new ArgumentOutOfRangeException(nameof(value), "The value must be between 0 and 31.");
                    }

                    imageRowDeletedScintillaIndex = value;
                    SetSymbolMasks();
                }
            }
        }

        /// <summary>
        /// Gets or sets the indicator for the diff that a row hasn't changed.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the indicator for the diff that a row hasn't changed.")]
        public Bitmap ImageRowOk
        {
            get => imageRowOk;
            set
            {
                if (value != imageRowOk && value != null)
                {
                    imageRowOk = value;
                    SetSymbols();
                }
            }
        }

        /// <summary>
        /// Gets or sets the index for the <see cref="ImageRowOk"/> used by the <see cref="Scintilla"/> control.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value must be between 0 and 31.</exception>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the index for the ImageRowOk used by the Scintilla control.")]
        public int ImageRowOkScintillaIndex
        {
            get => imageRowOkScintillaIndex;

            set
            {
                if (value != imageRowOkScintillaIndex)
                {
                    if (value < 0 || value > 31)
                    {
                        // ReSharper disable once LocalizableElement
                        throw new ArgumentOutOfRangeException(nameof(value), "The value must be between 0 and 31.");
                    }

                    imageRowOkScintillaIndex = value;
                    SetSymbolMasks();
                }
            }
        }

        /// <summary>
        /// Gets or sets the indicator for the diff that two rows have some differences.
        /// </summary>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the indicator for the diff that two rows have some differences.")]
        public Bitmap ImageRowDiff
        {
            get => imageRowDiff;
            set
            {
                if (value != imageRowDiff && value != null)
                {
                    imageRowDiff = value;
                    SetSymbols();
                }
            }
        }

        /// <summary>
        /// Gets or sets the index for the <see cref="ImageRowDiff"/> used by the <see cref="Scintilla"/> control.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">The value must be between 0 and 31.</exception>
        [Browsable(true)]
        [Category("Appearance")]
        [Description("Gets or sets the index for the ImageRowDiff used by the Scintilla control.")]
        public int ImageRowDiffScintillaIndex
        {
            get => imageRowDiffScintillaIndex;

            set
            {
                if (value != imageRowDiffScintillaIndex)
                {
                    if (value < 0 || value > 31)
                    {
                        // ReSharper disable once LocalizableElement
                        throw new ArgumentOutOfRangeException(nameof(value), "The value must be between 0 and 31.");
                    }

                    imageRowDiffScintillaIndex = value;
                    SetSymbolMasks();
                }
            }
        }

        /// <summary>
        /// Gets or sets the text on the left <see cref="Scintilla"/> control.
        /// </summary>
        [Browsable(true)]
        [Category("Diff")]
        [Description("Gets or sets the text on the left Scintilla control.")]
        public string TextLeft
        {
            get => textLeft;
            set
            {
                textLeft = value;
                DiffTexts();
            }
        }

        /// <summary>
        /// Gets or sets the text on the right <see cref="Scintilla"/> control.
        /// </summary>
        [Browsable(true)]
        [Category("Diff")]
        [Description("Gets or sets the text on the right Scintilla control.")]
        public string TextRight
        {
            get => textRight;
            set
            {
                textRight = value;
                DiffTexts();
            }
        }

        /// <summary>
        /// Gets or sets the text deleted color for the <see cref="Scintilla"/> control.
        /// </summary>
        [Browsable(true)]
        [Category("Diff")]
        [Description("Gets or sets the text deleted color for the Scintilla control.")]
        public Color DiffColorDeleted
        {
            get => diffColorDeleted;
            set
            {
                if (value != diffColorDeleted)
                {
                    diffColorDeleted = value;
                    DiffTexts();
                }
            }
        }

        /// <summary>
        /// Gets or sets the text inserted color for the <see cref="Scintilla"/> control.
        /// </summary>
        [Browsable(true)]
        [Category("Diff")]
        [Description("Gets or sets the text inserted color for the Scintilla control.")]
        public Color DiffColorAdded
        {
            get => diffColorAdded;
            set
            {
                if (value != diffColorAdded)
                {
                    diffColorAdded = value;
                    DiffTexts();
                }
            }
        }

        /// <summary>
        /// Gets or sets the background color for a changed text row for the <see cref="Scintilla"/> control.
        /// </summary>
        [Browsable(true)]
        [Category("Diff")]
        [Description("Gets or sets the background color for a changed text row for the Scintilla control.")]
        public Color DiffColorChangeBackground
        {
            get => diffColorChangeBackground;
            set
            {
                if (value != diffColorChangeBackground)
                {
                    diffColorChangeBackground = value;
                    DiffTexts();
                }
            }
        }
        #endregion

        #region PrivateProperties

        #endregion

        #region PrivateMethods
        /// <summary>
        /// Re-calculates the split container's (<see cref="SplitContainer"/>) splitter position.
        /// </summary>
        private void RecalculateSize()
        {
            int size = scMain.ClientSize.Width - scMain.SplitterWidth;
			if (size >= 0)
            scMain.SplitterDistance = size / 2;
        }

        /// <summary>
        /// Sets the margin styles of both the <see cref="Scintilla"/> controls.
        /// </summary>
        private void InitScintillaMargins()
        {
            scintillaOne.Margins[0].Type = MarginType.Number;
            scintillaTwo.Margins[0].Type = MarginType.Number;

            scintillaOne.Margins[1].Width = 25;
            scintillaOne.Margins[1].Type = MarginType.Symbol;

            scintillaTwo.Margins[1].Width = 25;
            scintillaTwo.Margins[1].Type = MarginType.Symbol;
        }

        /// <summary>
        /// Sets the bit masks to the second margin symbols.
        /// </summary>
        private void SetSymbolMasks()
        {
            // the Scintilla does like this bit masks or "bitmaps"..
            scintillaOne.Margins[1].Mask =
                GetScintillaSymbolIndex(imageRowAddedScintillaIndex) | 
                GetScintillaSymbolIndex(imageRowDeletedScintillaIndex) |
                GetScintillaSymbolIndex(imageRowDiffScintillaIndex) |
                GetScintillaSymbolIndex(imageRowOkScintillaIndex);
            scintillaTwo.Margins[1].Mask = scintillaOne.Margins[1].Mask;
        }

        /// <summary>
        /// Shifts value of one with the given amount to the left.
        /// </summary>
        /// <param name="amount">The amount of how much to shift number one to the left.</param>
        /// <returns>An unsigned integer containing the value shifted to the left.</returns>
        uint GetScintillaSymbolIndex(int amount)
        {
            return (uint) 1 << amount;
        }

        /// <summary>
        /// Sets the symbol images for both the left and right <see cref="Scintilla"/> controls.
        /// </summary>
        private void SetSymbols()
        {
            // the plus-symbol..
            scintillaOne.Markers[imageRowAddedScintillaIndex].Symbol = MarkerSymbol.RgbaImage;
            scintillaOne.Markers[imageRowAddedScintillaIndex].DefineRgbaImage(imageRowAdded);

            // the minus-symbol..
            scintillaOne.Markers[imageRowDeletedScintillaIndex].Symbol = MarkerSymbol.RgbaImage;
            scintillaOne.Markers[imageRowDeletedScintillaIndex].DefineRgbaImage(imageRowDeleted);

            // the plus-symbol..
            scintillaTwo.Markers[imageRowAddedScintillaIndex].Symbol = MarkerSymbol.RgbaImage;
            scintillaTwo.Markers[imageRowAddedScintillaIndex].DefineRgbaImage(imageRowAdded);

            // the minus-symbol..
            scintillaTwo.Markers[imageRowDeletedScintillaIndex].Symbol = MarkerSymbol.RgbaImage;
            scintillaTwo.Markers[imageRowDeletedScintillaIndex].DefineRgbaImage(imageRowDeleted);

            // the row ok symbol..
            scintillaOne.Markers[imageRowOkScintillaIndex].Symbol = MarkerSymbol.RgbaImage;
            scintillaOne.Markers[imageRowOkScintillaIndex].DefineRgbaImage(imageRowOk);

            // the row ok symbol..
            scintillaTwo.Markers[imageRowOkScintillaIndex].Symbol = MarkerSymbol.RgbaImage;
            scintillaTwo.Markers[imageRowOkScintillaIndex].DefineRgbaImage(imageRowOk);

            // the row diff symbol..
            scintillaOne.Markers[imageRowDiffScintillaIndex].Symbol = MarkerSymbol.RgbaImage;
            scintillaOne.Markers[imageRowDiffScintillaIndex].DefineRgbaImage(imageRowDiff);

            // the row diff symbol..
            scintillaTwo.Markers[imageRowDiffScintillaIndex].Symbol = MarkerSymbol.RgbaImage;
            scintillaTwo.Markers[imageRowDiffScintillaIndex].DefineRgbaImage(imageRowDiff);
        }

        /// <summary>
        /// Appends a row to the left <see cref="Scintilla"/> control.
        /// </summary>
        /// <param name="rowText">The text to append to the left <see cref="Scintilla"/> document row.</param>
        private void AppendRowAdded(string rowText)
        {
            builderLeft.AppendLine(rowText);
//            scintillaOne.Text += rowText + Environment.NewLine;
        }

        /// <summary>
        /// Sets a row added marker based on the given <paramref name="index"/> row amount.
        /// </summary>
        /// <param name="index">The index of the row to set the marker to.</param>
        private void AppendRowAddedMarker(int index)
        {
            scintillaOne.Lines[index].MarkerAdd(imageRowAddedScintillaIndex);
        }

        /// <summary>
        /// Sets a row added marker based on the given <paramref name="index"/> row amount to either the left side or the right side <see cref="Scintilla"/> document.
        /// </summary>
        /// <param name="index">The index of the row to set the marker to.</param>
        /// <param name="left">A value indicating whether to append the marker to the left or to the right side <see cref="Scintilla"/> document.</param>
        private void AppendRowAddedMarker(int index, bool left)
        {
            if (left)
            {
                scintillaOne.Lines[index].MarkerAdd(imageRowAddedScintillaIndex);
            }
            else
            {
                scintillaTwo.Lines[index].MarkerAdd(imageRowAddedScintillaIndex);
            }
        }

        /// <summary>
        /// Appends a row to the left <see cref="Scintilla"/> control.
        /// </summary>
        /// <param name="rowText">The text to append to the right <see cref="Scintilla"/> document row.</param>
        private void AppendRowDeleted(string rowText)
        {
            builderLeft.AppendLine(rowText);
            //scintillaOne.Text += rowText + Environment.NewLine;
        }

        /// <summary>
        /// Sets a row deleted marker based on the given <paramref name="index"/> row amount.
        /// </summary>
        /// <param name="index">The index of the row to set the marker to.</param>
        private void AppendRowDeletedMarker(int index)
        {
            scintillaOne.Lines[index].MarkerAdd(imageRowDeletedScintillaIndex);
        }

        /// <summary>
        /// Sets a row deleted marker based on the given <paramref name="index"/> row amount to either the left side or the right side <see cref="Scintilla"/> document.
        /// </summary>
        /// <param name="index">The index of the row to set the marker to.</param>
        /// <param name="left">A value indicating whether to append the marker to the left or to the right side <see cref="Scintilla"/> document.</param>
        private void AppendRowDeletedMarker(int index, bool left)
        {
            if (left)
            {
                scintillaOne.Lines[index].MarkerAdd(imageRowDeletedScintillaIndex);
            }
            else
            {
                scintillaTwo.Lines[index].MarkerAdd(imageRowDeletedScintillaIndex);
            }
        }

        /// <summary>
        /// Sets a row ok marker based on the given <paramref name="index"/> row amount.
        /// </summary>
        /// <param name="index">The index of the row to set the marker to.</param>
        private void AppendRowOkMarker(int index)
        {
            if (!UseRowOkSign)
            {
                return;
            }
            scintillaOne.Lines[index].MarkerAdd(imageRowOkScintillaIndex);
        }

        /// <summary>
        /// Sets a row ok marker based on the given <paramref name="index"/> row amount to either the left side or the right side <see cref="Scintilla"/> document.
        /// </summary>
        /// <param name="index">The index of the row to set the marker to.</param>
        /// <param name="left">A value indicating whether to append the marker to the left or to the right side <see cref="Scintilla"/> document.</param>
        private void AppendRowOkMarker(int index, bool left)
        {
            if (!UseRowOkSign)
            {
                return;
            }

            if (left)
            {
                scintillaOne.Lines[index].MarkerAdd(imageRowOkScintillaIndex);
            }
            else
            {
                scintillaTwo.Lines[index].MarkerAdd(imageRowOkScintillaIndex);
            }
        }

        /// <summary>
        /// Sets a row differ marker based on the given <paramref name="index"/> row amount.
        /// </summary>
        /// <param name="index">The index of the row to set the marker to.</param>
        private void AppendRowDiffMarker(int index)
        {
            scintillaOne.Lines[index].MarkerAdd(imageRowDiffScintillaIndex);
        }

        /// <summary>
        /// Sets a row differ marker based on the given <paramref name="index"/> row amount to either the left side or the right side <see cref="Scintilla"/> document.
        /// </summary>
        /// <param name="index">The index of the row to set the marker to.</param>
        /// <param name="left">A value indicating whether to append the marker to the left or to the right side <see cref="Scintilla"/> document.</param>
        private void AppendRowDiffMarker(int index, bool left)
        {
            if (left)
            {
                scintillaOne.Lines[index].MarkerAdd(imageRowDiffScintillaIndex);
            }
            else
            {
                scintillaTwo.Lines[index].MarkerAdd(imageRowDiffScintillaIndex);
            }
        }

        /// <summary>
        /// Appends the same row to the left and right <see cref="Scintilla"/> controls.
        /// </summary>
        /// <param name="rowText">The text to append to the right and to the left <see cref="Scintilla"/> document rows.</param>
        private void AppendRow(string rowText)
        {
            builderLeft.AppendLine(rowText);
            builderRight.AppendLine(rowText);
            //scintillaOne.Text += rowText + Environment.NewLine;
            //scintillaTwo.Text += rowText + Environment.NewLine;
        }

        /// <summary>
        /// Appends different rows to the left and right side <see cref="Scintilla"/> controls.
        /// </summary>
        /// <param name="rowTextLeft">The text to append to the the left <see cref="Scintilla"/> document rows.</param>
        /// <param name="rowTextRight">The text to append to the right <see cref="Scintilla"/> document rows.</param>
        private void AppendRow(string rowTextLeft, string rowTextRight)
        {
            builderLeft.AppendLine(rowTextLeft);
            builderRight.AppendLine(rowTextRight);
//            scintillaOne.Text += rowTextLeft + Environment.NewLine;
//            scintillaTwo.Text += rowTextRight + Environment.NewLine;
        }

        /// <summary>
        /// Compares to contents of the two texts if both are assigned in a list style view.
        /// </summary>
        private void DiffTextsList()
        {
            // collapse the right panel of the split control..
            scMain.Panel2Collapsed = true;

            // validate that there is text to compare..
            if (TextLeft != string.Empty && TextRight != string.Empty)
            {
                // clear the two Scintilla control contents..
                scintillaOne.Text = string.Empty;
                scintillaTwo.Text = string.Empty;
                
                // clear the two StringBuilder instance contents..
                builderLeft.Clear();
                builderRight.Clear();

                // create a diff for a list style text comparison..
                var diffBuilder = new InlineDiffBuilder(new Differ());

                // compare the two texts..
                var diff = diffBuilder.BuildDiffModel(TextLeft, TextRight);

                // output the diff data to the left side Scintilla control;
                // first the rows so the style can be appended afterwards..
                foreach (var line in diff.Lines)
                {
                    switch (line.Type)
                    {
                        case ChangeType.Inserted:
                            AppendRowAdded(line.Text);
                            break;
                        case ChangeType.Deleted:
                            AppendRowDeleted(line.Text);
                            break;
                        case ChangeType.Unchanged:
                            AppendRow(line.Text);
                            break;
                    }
                }

                scintillaOne.Text = builderLeft.ToString();

                // set a variable for the line index..
                int lineIndex = 0;

                // set the style for the lines now that the Scintilla document's
                // contents have been set..
                foreach (var line in diff.Lines)
                {
                    switch (line.Type)
                    {
                        case ChangeType.Inserted:
                            // save the line location..
                            SaveLineLocation(lineIndex);
                            AppendRowAddedMarker(lineIndex); 
                            break;
                        case ChangeType.Deleted:
                            // save the line location..
                            SaveLineLocation(lineIndex);
                            AppendRowDeletedMarker(lineIndex);
                            break;
                        case ChangeType.Unchanged:
                            AppendRowOkMarker(lineIndex);
                            break;
                        case ChangeType.Modified:
                            // save the line location..
                            SaveLineLocation(lineIndex);
                            AppendRowDiffMarker(lineIndex);
                            break;
                    }

                    lineIndex++;
                }
                
                // reset the index of the next difference..
                diffIndex = -1;

                // raise the ExternalStyleNeeded event if it's subscribed..
                ExternalStyleNeeded?.Invoke(this, new StyleRefreshEventArgs {Scintilla = LeftScintilla});

                // sort the jump locations..
                DiffLocations.Sort();
            }
        }

        /// <summary>
        /// Saves the line location to the <see cref="DiffLocations"/> property.
        /// </summary>
        /// <param name="lineIndex">The index of the row which location to save.</param>
        private void SaveLineLocation(int lineIndex)
        {
            if (DiffLocations.Contains(lineIndex))
            {
                return;
            }

            DiffLocations.Add(lineIndex);
        }

        /// <summary>
        /// Compares to contents of the two texts if both are assigned in a side by side style view.
        /// </summary>
        private void DiffTextsSideBySide()
        {
            // un-collapse the right panel of the split control..
            scMain.Panel2Collapsed = false;

            // recalculate the position of the split control's
            // splitter..
            RecalculateSize();

            // validate that there is text to compare..
            if (TextLeft != string.Empty && TextRight != string.Empty)
            {
                // clear the two Scintilla control contents..
                scintillaOne.Text = string.Empty;
                scintillaTwo.Text = string.Empty;

                // clear the two StringBuilder instance contents..
                builderLeft.Clear();
                builderRight.Clear();
                 
                // create a diff for a side by side text comparison..
                var diffBuilder = new SideBySideDiffBuilder(new Differ());

                // compare the two texts..
                var diff = diffBuilder.BuildDiffModel(TextLeft, TextRight);

                // output the diff data to the left and to the right side Scintilla controls;
                // first the rows so the style can be appended afterwards..
                for (int i = 0; i < diff.OldText.Lines.Count; i++)
                {
                    AppendRow(diff.OldText.Lines[i].Text, diff.NewText.Lines[i].Text);
                }

                scintillaOne.Text = builderLeft.ToString();
                scintillaTwo.Text = builderRight.ToString();

                // clear the list of difference locations..
                DiffLocations.Clear();

                // loop through the meta-data of the diff result and set the styling
                // for the Scintilla controls accordingly..
                for (int i = 0; i < diff.OldText.Lines.Count; i++)
                {
                    switch (diff.OldText.Lines[i].Type)
                    {
                        case ChangeType.Inserted:
                            AppendRowAddedMarker(i, true);
                            // save the line location..
                            SaveLineLocation(i);
                            break;
                        case ChangeType.Deleted:
                            AppendRowDeletedMarker(i, true);
                            // save the line location..
                            SaveLineLocation(i);
                            break;
                        case ChangeType.Unchanged:
                            AppendRowOkMarker(i, true);
                            break;
                        case ChangeType.Modified:
                            // save the line location..
                            SaveLineLocation(i);
                            AppendRowDiffMarker(i, false);
                            MarkLineWithColor(i, DiffColorChangeBackground, true);
                            HandleDiffSubPieces(diff.OldText.Lines[i].SubPieces, i, true);
                            break;
                    }

                    switch (diff.NewText.Lines[i].Type)
                    {
                        case ChangeType.Inserted:
                            AppendRowAddedMarker(i, false);
                            // save the line location..
                            SaveLineLocation(i);
                            break;
                        case ChangeType.Deleted:
                            AppendRowDeletedMarker(i, false);
                            // save the line location..
                            SaveLineLocation(i);
                            break;
                        case ChangeType.Unchanged:
                            AppendRowOkMarker(i, false);
                            break;
                        case ChangeType.Modified:
                            // save the line location..
                            SaveLineLocation(i);
                            AppendRowDiffMarker(i, true);
                            MarkLineWithColor(i, DiffColorChangeBackground, false);
                            HandleDiffSubPieces(diff.NewText.Lines[i].SubPieces, i, false);
                            break;
                    }
                }

                // reset the index of the next difference..
                diffIndex = -1;

                // raise the ExternalStyleNeeded event if it's subscribed for both of the Scintilla controls..
                ExternalStyleNeeded?.Invoke(this, new StyleRefreshEventArgs {Scintilla = LeftScintilla});
                ExternalStyleNeeded?.Invoke(this, new StyleRefreshEventArgs {Scintilla = RightScintilla});

                // sort the jump locations..
                DiffLocations.Sort();
            }
        }

        /// <summary>
        /// Marks a given position of a row with a given background color.
        /// </summary>
        /// <param name="lineIndex">The index of the line to mark with the <see cref="Scintilla"/> control.</param>
        /// <param name="subPosition">The position in the line to mark with the given color.</param>
        /// <param name="left">A value indicating whether to use the left or the right side <see cref="Scintilla"/> control.</param>
        /// <param name="diffPiece">A <see cref="DiffPiece"/> class instance to get the length of the text.</param>
        /// <param name="color">A <see cref="Color"/> to use with the marking.</param>
        private void MarkWithBackgroundColor(int lineIndex, int subPosition, bool left, DiffPiece diffPiece, Color color)
        {
            if (diffPiece.Position == null)
            {
                return;
            }

            int start = left
                ? scintillaOne.Lines[lineIndex].Position + subPosition
                : scintillaTwo.Lines[lineIndex].Position + subPosition;

            Highlight.HighlightRange(left ? scintillaOne : scintillaTwo, MarkColorIndexModifiedBackground, start,
                diffPiece.Text.Length, color);
        }

        /// <summary>
        /// Marks a line of a <see cref="Scintilla"/> control with a given <paramref name="color"/>.
        /// </summary>
        /// <param name="lineIndex">The index of the line to mark with the <see cref="Scintilla"/> control.</param>
        /// <param name="color">A <see cref="Color"/> to use with the marking.</param>
        /// <param name="left">A value indicating whether to use the left or the right side <see cref="Scintilla"/> control.</param>
        private void MarkLineWithColor(int lineIndex, Color color, bool left)
        {
            int start = left
                ? scintillaOne.Lines[lineIndex].Position
                : scintillaTwo.Lines[lineIndex].Position;

            int length = left
                ? scintillaOne.Lines[lineIndex].Length
                : scintillaTwo.Lines[lineIndex].Length;

            Highlight.HighlightRange(left ? scintillaOne : scintillaTwo, MarkColorIndexRemovedOrAdded, start, length,
                color);
        }

        /// <summary>
        /// Handles the difference sub-pieces in a side-by-side comparison to set a color for a word.
        /// </summary>
        /// <param name="subPieces">A list of <see cref="DiffPiece"/> class instances to be marked.</param>
        /// <param name="lineIndex">The index of the line to mark with the <see cref="Scintilla"/> control.</param>
        /// <param name="left">A value indicating whether to use the left or the right side <see cref="Scintilla"/> control.</param>
        private void HandleDiffSubPieces(List<DiffPiece> subPieces, int lineIndex, bool left)
        {
            int calcPosition = 0;
            foreach (var subPiece in subPieces)
            {
                switch (subPiece.Type)
                {
                    case ChangeType.Deleted:
                        MarkWithBackgroundColor(lineIndex, calcPosition, left, subPiece,
                            DiffColorDeleted);
                        break;

                    case ChangeType.Inserted:
                        MarkWithBackgroundColor(lineIndex, calcPosition, left, subPiece,
                            DiffColorAdded);
                        break;
                }

                if (subPiece.Position != null)
                {
                    calcPosition += subPiece.Text.Length;
                }
            }
        }

        /// <summary>
        /// Jumps the view into a given line index.
        /// </summary>
        /// <param name="lineIndex">Index of the line to jump the view to.</param>
        /// <param name="backwards">if set to <c>true</c> the call was made from the <see cref="Previous"/> method.</param>
        /// <returns><c>true</c> if the line index was valid and the view was scrolled, <c>false</c> otherwise.</returns>
        private bool JumpToLine(int lineIndex, bool backwards)
        {
            if (lineIndex < 0 || lineIndex >= DiffLocations.Count)
            {
                return false;
            }
            
            if (diffStyle == DiffStyle.DiffList)
            {
                int linePos1 = scintillaOne.Lines[DiffLocations[lineIndex]].Position;
                scintillaOne.GotoPosition(linePos1);
                scintillaOne.ScrollCaret();
            }
            else if (diffStyle == DiffStyle.DiffSideBySide)
            {
                int linePos1 = scintillaOne.Lines[DiffLocations[lineIndex]].Position;
                scintillaOne.GotoPosition(linePos1);

                int linePos2 = scintillaTwo.Lines[DiffLocations[lineIndex]].Position;
                scintillaTwo.GotoPosition(linePos2);

                scintillaOne.ScrollCaret();
                scintillaTwo.ScrollCaret();
            }

            if (backwards && lineIndex == -1 || 
                !backwards && lineIndex + 1 >= DiffLocations.Count)
            {
                return false;
            }

            return true;
        }
        #endregion

        #region PublicMethods        
        /// <summary>
        /// Compares to contents of the two texts based on the <see cref="DiffStyle"/> property value.
        /// </summary>
        public void DiffTexts()
        {
            if (diffStyle == DiffStyle.DiffList)
            {
                DiffTextsList();
            }
            else
            {
                DiffTextsSideBySide();
            }
        }

        /// <summary>
        /// Swaps the texts to compare.
        /// </summary>
        public void SwapDiff()
        {
            string temp = textLeft;
            textLeft = textRight;
            textRight = temp;
            DiffTexts();
        }

        /// <summary>
        /// Jumps to the next difference within the diff view.
        /// </summary>
        /// <returns><c>true</c> if the navigation to the next position was possible, <c>false</c> otherwise.</returns>
        public bool Next()
        {
            if (diffIndex >= DiffLocations.Count)
            {
                return false;
            }

            diffIndex++;
            return JumpToLine(diffIndex, false);
        }

        /// <summary>
        /// Jumps to the previous difference within the diff view.
        /// </summary>
        /// <returns><c>true</c> if the navigation to the previous position was possible, <c>false</c> otherwise.</returns>
        public bool Previous()
        {
            if (diffIndex < 0)
            {
                return false;
            }

            diffIndex--;

            return JumpToLine(diffIndex, true);
        }
        #endregion
    }
}
