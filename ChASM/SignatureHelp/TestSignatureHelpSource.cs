/*
 * 
 * Based on:
 * © https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-displaying-signature-help?view=vs-2022&tabs=csharp
 * 
 */

using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Text;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChASM.Globalz;
using System.Security.Cryptography;

namespace ChASM.SignatureHelp
{
    internal class TestSignatureHelpSource : ISignatureHelpSource
    {
        private ITextBuffer m_textBuffer;

        public TestSignatureHelpSource(ITextBuffer textBuffer)
        {
            m_textBuffer = textBuffer;
        }

        public void AugmentSignatureHelpSession(ISignatureHelpSession session, IList<ISignature> signatures)
        {
            ITextSnapshot snapshot = m_textBuffer.CurrentSnapshot;
            int position = session.GetTriggerPoint(m_textBuffer).GetPosition(snapshot);

            ITrackingSpan applicableToSpan = m_textBuffer.CurrentSnapshot.CreateTrackingSpan(
             new Span(position, 0), SpanTrackingMode.EdgeInclusive, 0);

            string mnemonic = snapshot.GetLineFromPosition(position).GetText().Trim();

            foreach(var signature in Signatures.ItemsForCodeCompletion)
            {
                var splitted = signature.Split(' ').ElementAt(0);

                if (splitted.Equals(mnemonic))
                {
                    var desc = Mnemonics.ItemsForCodeCompletion.FirstOrDefault(m => m.InsertionText.Equals(mnemonic))?.Description;

                    signatures.Add(CreateSignature(m_textBuffer, signature, desc, applicableToSpan));
                }
            }
        }

        private TestSignature CreateSignature(ITextBuffer textBuffer, string methodSig, string methodDoc, ITrackingSpan span)
        {
            TestSignature sig = new TestSignature(textBuffer, methodSig, methodDoc, null);
            textBuffer.Changed += new EventHandler<TextContentChangedEventArgs>(sig.OnSubjectBufferChanged);

            //find the parameters in the method signature - expect: mnemonic operand_1, operand_2
            string[] pars = methodSig.Replace(",", "").Split(new char[] { ' ' });
            List<IParameter> paramList = new List<IParameter>();

            int locusSearchStart = 0;
            for (int i = 1; i < pars.Length; i++)
            {
                string param = pars[i].Trim();

                if (string.IsNullOrEmpty(param))
                    continue;

                //find where this parameter is located in the method signature
                int locusStart = methodSig.IndexOf(param, locusSearchStart);
                if (locusStart >= 0)
                {
                    Span locus = new Span(locusStart, param.Length);
                    locusSearchStart = locusStart + param.Length;
                    paramList.Add(new TestParameter(param, locus, param, sig));
                }
            }

            sig.Parameters = new ReadOnlyCollection<IParameter>(paramList);
            sig.ApplicableToSpan = span;
            sig.ComputeCurrentParameter();
            return sig;
        }

        public ISignature GetBestMatch(ISignatureHelpSession session)
        {
            if (session.Signatures.Count > 0)
            {
                ITrackingSpan applicableToSpan = session.Signatures[0].ApplicableToSpan;
                string text = applicableToSpan.GetText(applicableToSpan.TextBuffer.CurrentSnapshot);

                return session.Signatures.FirstOrDefault(s => s.Content.Trim().Equals(text.Trim()));
            }
            return null;
        }

        private bool m_isDisposed;
        public void Dispose()
        {
            if (!m_isDisposed)
            {
                GC.SuppressFinalize(this);
                m_isDisposed = true;
            }
        }
    }
}
