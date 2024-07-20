/*
 * 
 * Based on:
 * © https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-displaying-signature-help?view=vs-2022&tabs=csharp
 * 
 */

using ChASM.Globalz;
using Microsoft.VisualStudio;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.OLE.Interop;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Operations;
using Microsoft.VisualStudio.TextManager.Interop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ChASM.SignatureHelp
{
    internal sealed class TestSignatureHelpCommandHandler : IOleCommandTarget
    {
        IOleCommandTarget m_nextCommandHandler;
        ITextView m_textView;
        ISignatureHelpBroker m_broker;
        ISignatureHelpSession m_session;
        ITextStructureNavigator m_navigator;

        bool signatureTriggered = false;
        int backspaces = 0;

        internal TestSignatureHelpCommandHandler(IVsTextView textViewAdapter, ITextView textView, ITextStructureNavigator nav, ISignatureHelpBroker broker)
        {
            this.m_textView = textView;
            this.m_broker = broker;
            this.m_navigator = nav;

            //add this to the filter chain
            textViewAdapter.AddCommandFilter(this, out m_nextCommandHandler);
        }

        public int Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            char typedChar = char.MinValue;

            if (pguidCmdGroup == VSConstants.VSStd2K && nCmdID == (uint)VSConstants.VSStd2KCmdID.TYPECHAR)
            {
                typedChar = (char)(ushort)Marshal.GetObjectForNativeVariant(pvaIn);
                if (typedChar.Equals(' ') && signatureTriggered == false)
                {
                    //move the point back so it's in the preceding word
                    SnapshotPoint point = m_textView.Caret.Position.BufferPosition - 1;
                    TextExtent extent = m_navigator.GetExtentOfWord(point);
                    string word = extent.Span.GetText().Trim();

                    if (Signatures.ItemsForCodeCompletion.Any(i => i.StartsWith(word)))
                    {
                        m_session = m_broker.TriggerSignatureHelp(m_textView);
                        signatureTriggered = true;
                    }
                }
            }

            if (nCmdID == (uint)VSConstants.VSStd2KCmdID.RETURN)
            {
                if (m_session != null)
                {
                    signatureTriggered = false;
                    m_session.Dismiss();
                    m_session = null;
                }
            }

            if (nCmdID == (uint)VSConstants.VSStd2KCmdID.BACKSPACE)
            {
                backspaces++;

                if (backspaces >= 2 && m_session != null)
                {
                    signatureTriggered = false;
                    m_session.Dismiss();
                    m_session = null;
                    backspaces = 0;
                }
            }

            return m_nextCommandHandler.Exec(ref pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);
        }

        public int QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
        {
            Microsoft.VisualStudio.Shell.ThreadHelper.ThrowIfNotOnUIThread();
            return m_nextCommandHandler.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText);
        }
    }
}
