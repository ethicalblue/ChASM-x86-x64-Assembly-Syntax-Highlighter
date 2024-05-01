/*
 * 
 * © https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-displaying-statement-completion?view=vs-2022&tabs=csharp
 * 
 */

using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace ChASM
{
    [Export(typeof(ICompletionSourceProvider))]
    [ContentType("masm")]
    [Name("token completion")]
    internal class ChASMCompletionSourceProvider : ICompletionSourceProvider
    {
        [Import]
        internal ITextStructureNavigatorSelectorService NavigatorService { get; set; }

        public ICompletionSource TryCreateCompletionSource(ITextBuffer textBuffer)
        {
            return new ChASMCompletionSource(this, textBuffer);
        }
    }
}
