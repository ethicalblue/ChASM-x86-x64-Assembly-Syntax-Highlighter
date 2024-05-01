/*
 * 
 * © 2022 by ethical.blue Magazine
 * Based on https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-highlighting-text?view=vs-2022&tabs=csharp
 * 
 */

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChASM.TextAdornments
{
    /// <summary>
    /// Quick and dirty parser here.
    /// </summary>
    internal sealed class ChASMTextAdornment
    {
        private readonly IAdornmentLayer layer;
        private readonly IWpfTextView view;

#pragma warning disable 169
        private readonly Brush brush;
        private readonly Pen pen;
#pragma warning restore 169

        public ChASMTextAdornment(IWpfTextView view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }

            this.layer = view.GetAdornmentLayer("ChASMTextAdornment");

            this.view = view;
            this.view.LayoutChanged += this.OnLayoutChanged;
        }

        internal void OnLayoutChanged(object sender, TextViewLayoutChangedEventArgs e)
        {
            foreach (ITextViewLine line in e.NewOrReformattedLines)
            {
                this.CreateVisuals(line);
            }
        }

        private void AddAdornmentToLayer(ref IWpfTextViewLineCollection textViewLines,
            TextBlock textBlock, int charIndex, int charIndexEnd)
        {
            SnapshotSpan spanText = new SnapshotSpan(this.view.TextSnapshot, Span.FromBounds(charIndex, charIndexEnd));

            Geometry geometry = textViewLines.GetMarkerGeometry(spanText);
            if (geometry != null)
            {
                textBlock.FontSize += 1.0;
                textBlock.FontFamily = new FontFamily("Cascadia Code");

                Canvas.SetLeft(textBlock, geometry.Bounds.Left);
                Canvas.SetTop(textBlock, geometry.Bounds.Top);
                Panel.SetZIndex(textBlock, 8);

                layer.AddAdornment(AdornmentPositioningBehavior.TextRelative, spanText, null, textBlock, null);
            }
        }

        private void QuoteOrApostropheDetected(ref IWpfTextViewLineCollection textViewLines,
            ref ITextViewLine line, ref int charIndex)
        {
            string currentText = string.Empty;
            int charIndexEnd = 0;

            for (int i = charIndex; i < line.End; i++)
            {
                currentText += view.TextSnapshot[i];
                charIndexEnd = i;
            }

            var textBlock = Utilz.CreateTextBlock(TextBlockType.TextString, currentText);

            if (charIndex == charIndexEnd && charIndexEnd < line.End)
                charIndexEnd++;

            AddAdornmentToLayer(ref textViewLines, textBlock, charIndex, charIndexEnd);
        }

        private void SemicolonDetected(ref IWpfTextViewLineCollection textViewLines,
            ref ITextViewLine line, ref int charIndex)
        {
            string currentComment = string.Empty;

            int charIndexEnd = 0;

            for (int i = charIndex; i < line.End; i++)
            {
                currentComment += view.TextSnapshot[i];
                charIndexEnd = i;
            }

            var textBlock = Utilz.CreateTextBlock(TextBlockType.Comment, currentComment);

            if (charIndex == charIndexEnd && charIndexEnd < line.End)
                charIndexEnd++;

            AddAdornmentToLayer(ref textViewLines, textBlock, charIndex, charIndexEnd);
        }

        private void GeneralPurposeRegisterDetected(ref IWpfTextViewLineCollection textViewLines,
            ref ITextViewLine line, int startIndexReg, string currentRegister)
        {
            if (startIndexReg > 0 && char.IsLetterOrDigit(view.TextSnapshot[startIndexReg - 1]))
                return;

            if (startIndexReg + currentRegister.Length < line.End &&
                char.IsLetterOrDigit(view.TextSnapshot[startIndexReg + currentRegister.Length]))
                return;

            var textBlock = Utilz.CreateTextBlock(TextBlockType.GeneralPurposeRegister, currentRegister);

            AddAdornmentToLayer(ref textViewLines, textBlock, startIndexReg, startIndexReg + currentRegister.Length);
        }

        private void SpecialWordDetected(ref IWpfTextViewLineCollection textViewLines,
            ref ITextViewLine line, int startIndexSpec, string currentSpecial)
        {
            if (startIndexSpec > 0 && char.IsLetterOrDigit(view.TextSnapshot[startIndexSpec - 1]))
                return;

            if (startIndexSpec + currentSpecial.Length < line.End &&
                char.IsLetterOrDigit(view.TextSnapshot[startIndexSpec + currentSpecial.Length]))
                return;

            var textBlock = Utilz.CreateTextBlock(TextBlockType.SpecialWord, currentSpecial);

            AddAdornmentToLayer(ref textViewLines, textBlock, startIndexSpec, startIndexSpec + currentSpecial.Length);
        }

        private void ColorizeGeneralPurposeRegistersAndSpecials(string currentCharacter,
            ref IWpfTextViewLineCollection textViewLines, ref ITextViewLine line,
            ref string currentRegister, ref string currentSpecial,
            ref int startIndexReg, ref int currentIndexReg,
            ref int startIndexSpec, ref int currentIndexSpec,
            ref int charIndex)
        {
            currentRegister += currentCharacter;
            currentSpecial += currentCharacter;

            if (currentRegister.Length == 1)
                startIndexReg = charIndex;

            if (currentSpecial.Length == 1)
                startIndexSpec = charIndex;

            currentIndexReg = currentIndexSpec = charIndex;

            string register = currentRegister;
            string special = currentSpecial;

            if (Globalz.Registers.x64_Registers.Any(a => a.StartsWith(register)) == false &&
                Globalz.Specialz.Items.Any(a => a.StartsWith(special)) == false)
            {
                currentRegister = currentSpecial = string.Empty;
                currentIndexReg = currentIndexSpec = charIndex;
            }
            else
            {
                if (Globalz.Registers.x64_Registers.Contains(currentRegister))
                {
                    GeneralPurposeRegisterDetected(ref textViewLines, ref line, startIndexReg, currentRegister);
                }

                if (Globalz.Specialz.Items.Contains(currentSpecial))
                {
                    SpecialWordDetected(ref textViewLines, ref line, startIndexSpec, currentSpecial);
                }
            }
        }

        private void CreateVisuals(ITextViewLine line)
        {
            IWpfTextViewLineCollection textViewLines = this.view.TextViewLines;

            string currentRegister = string.Empty;
            int startIndexReg = 0;
            int currentIndexReg = 0;

            string currentSpecial = string.Empty;
            int startIndexSpec = 0;
            int currentIndexSpec = 0;

            // Loop through each character
            for (int charIndex = line.Start; charIndex < line.End; charIndex++)
            {
                string currentCharacter = view.TextSnapshot[charIndex].ToString();

                if (currentCharacter == "\'" || currentCharacter == "\"")
                {
                    QuoteOrApostropheDetected(ref textViewLines, ref line, ref charIndex);
                    continue;
                }

                if (currentCharacter == ";")
                {
                    SemicolonDetected(ref textViewLines, ref line, ref charIndex);
                    continue;
                }

                ColorizeGeneralPurposeRegistersAndSpecials(currentCharacter, ref textViewLines, ref line,
                    ref currentRegister, ref currentSpecial, ref startIndexReg, ref currentIndexReg,
                    ref startIndexSpec, ref currentIndexSpec, ref charIndex);
            }
        }
    }
}