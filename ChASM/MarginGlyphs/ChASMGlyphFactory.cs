/*
 * 
 * © https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-creating-a-margin-glyph?view=vs-2022&tabs=csharp
 * 
 */

using System.Windows;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Formatting;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Interop;

namespace ChASM
{
    internal class ChASMGlyphFactory : IGlyphFactory
    {
        const double m_glyphSize = 16.0;

        public ChASMGlyphFactory()
        {

        }

        public UIElement GenerateGlyph(IWpfTextViewLine line, IGlyphTag tag)
        {
            if (tag == null || !(tag is ChASMTag))
            {
                return null;
            }

            BitmapSource bitmap = Imaging.CreateBitmapSourceFromHBitmap(
                           Properties.Resources.blue_squares1.GetHbitmap(),
                           IntPtr.Zero,
                           Int32Rect.Empty,
                           BitmapSizeOptions.FromEmptyOptions());

            System.Windows.Controls.Image image = new System.Windows.Controls.Image
            {
                Height = m_glyphSize,
                Width = m_glyphSize,
                Source = bitmap
            };

            return image;
        }
    }
}
