/*
 * 
 * © https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-displaying-statement-completion?view=vs-2022&tabs=csharp
 * 
 */

using System;
using System.ComponentModel.Composition;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;

namespace ChASM
{
    [Export(typeof(IVsTextViewCreationListener))]
    [Name("token completion handler")]
    [ContentType("masm")]
    [TextViewRole(PredefinedTextViewRoles.Editable)]
    internal class ChASMCompletionCommandHandlerProvider : IVsTextViewCreationListener
    {
        [Import]
        internal IVsEditorAdaptersFactoryService AdapterService = null;
        [Import]
        internal ICompletionBroker CompletionBroker { get; set; }
        [Import]
        internal SVsServiceProvider ServiceProvider { get; set; }

        public void VsTextViewCreated(IVsTextView textViewAdapter)
        {
            ITextView textView = AdapterService.GetWpfTextView(textViewAdapter);
            if (textView == null)
                return;

            Func<ChASMCompletionCommandHandler> createCommandHandler = delegate () { return new ChASMCompletionCommandHandler(textViewAdapter, textView, this); };
            textView.Properties.GetOrCreateSingletonProperty(createCommandHandler);
        }
    }
}
