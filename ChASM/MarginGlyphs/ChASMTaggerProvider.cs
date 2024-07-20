/*
 * 
 * © https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-creating-a-margin-glyph?view=vs-2022&tabs=csharp
 * 
 */

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System;
using System.ComponentModel.Composition;

namespace ChASM
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("masm")]
    [TagType(typeof(ChASMTag))]
    class ChASMTaggerProvider : ITaggerProvider
    {
        [Import]
        internal IClassifierAggregatorService AggregatorService = null;

        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            if (buffer == null)
            {
                throw new ArgumentNullException("buffer");
            }

            return new ChASMTagger(AggregatorService.GetClassifier(buffer)) as ITagger<T>;
        }
    }
}
