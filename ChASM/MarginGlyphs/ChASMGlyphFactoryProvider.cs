/*
 * 
 * © https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-creating-a-margin-glyph?view=vs-2022&tabs=csharp
 * 
 */

using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace ChASM
{
    [Export(typeof(IGlyphFactoryProvider))]
    [Name("ChASMGlyph")]
    [Order(After = "VsTextMarker")]
    [ContentType("masm")]
    [TagType(typeof(ChASMTag))]
    internal sealed class ChASMGlyphFactoryProvider : IGlyphFactoryProvider
    {
        public IGlyphFactory GetGlyphFactory(IWpfTextView view, IWpfTextViewMargin margin)
        {
            return new ChASMGlyphFactory();
        }
    }
}
