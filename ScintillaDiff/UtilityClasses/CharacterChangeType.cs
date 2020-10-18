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

using ScintillaDiff.Enumerations;

namespace ScintillaDiff.UtilityClasses
{
    /// <summary>
    /// A class to hold character level change values.
    /// </summary>
    public class CharacterChangeType
    {
        /// <summary>
        /// Gets or sets the index of the line.
        /// </summary>
        /// <value>The index of the line.</value>
        public int LineIndex { get; set; }

        /// <summary>
        /// Gets or sets the position in the <see cref="LineIndex"/>.
        /// </summary>
        /// <value>The position in the <see cref="LineIndex"/>.</value>
        public int Position { get; set; }

        /// <summary>
        /// Gets or sets the length of the character change area.
        /// </summary>
        /// <value>The length of the character change area.</value>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the type of the change within the character area.
        /// </summary>
        /// <value>The type of the change within the character area.</value>
        public CharChangedType ChangeType { get; set; }
    }
}
