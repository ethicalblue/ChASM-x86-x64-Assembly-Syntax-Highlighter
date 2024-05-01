/*
 * 
 * © https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-displaying-signature-help?view=vs-2022&tabs=csharp
 * 
 */

using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChASM.SignatureHelp
{
    [Export(typeof(ISignatureHelpSourceProvider))]
    [Name("Signature Help source")]
    [Order(Before = "default")]
    [ContentType("masm")]
    internal class TestSignatureHelpSourceProvider : ISignatureHelpSourceProvider
    {
        public ISignatureHelpSource TryCreateSignatureHelpSource(ITextBuffer textBuffer)
        {
            return new TestSignatureHelpSource(textBuffer);
        }
    }
}
