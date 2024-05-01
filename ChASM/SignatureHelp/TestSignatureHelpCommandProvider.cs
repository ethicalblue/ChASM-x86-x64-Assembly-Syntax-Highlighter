/*
 * 
 * © https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-displaying-signature-help?view=vs-2022&tabs=csharp
 * 
 */

using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChASM.SignatureHelp
{
    [Export(typeof(IVsTextViewCreationListener))]
    [Name("Signature Help controller")]
    [TextViewRole(PredefinedTextViewRoles.Editable)]
    [ContentType("masm")]
    internal class TestSignatureHelpCommandProvider : IVsTextViewCreationListener
    {
        [Import]
        internal IVsEditorAdaptersFactoryService AdapterService = null;

        [Import]
        internal ITextStructureNavigatorSelectorService NavigatorService { get; set; }

        [Import]
        internal ISignatureHelpBroker SignatureHelpBroker = null;

        public void VsTextViewCreated(IVsTextView textViewAdapter)
        {
            ITextView textView = AdapterService.GetWpfTextView(textViewAdapter);
            if (textView == null)
                return;

            textView.Properties.GetOrCreateSingletonProperty(
                 () => new TestSignatureHelpCommandHandler(textViewAdapter,
                    textView,
                    NavigatorService.GetTextStructureNavigator(textView.TextBuffer),
                    SignatureHelpBroker));
        }
    }
}
