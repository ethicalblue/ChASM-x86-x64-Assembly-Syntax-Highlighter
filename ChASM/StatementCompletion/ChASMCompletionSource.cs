/*
 * 
 * © https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-displaying-statement-completion?view=vs-2022&tabs=csharp
 * 
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Language.Intellisense;
using Microsoft.VisualStudio.Package;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Operations;
using static Microsoft.VisualStudio.Shell.ThreadedWaitDialogHelper;

namespace ChASM
{
    internal class ChASMCompletionSource : ICompletionSource
    {
        private ChASMCompletionSourceProvider m_sourceProvider;
        private ITextBuffer m_textBuffer;

        public ChASMCompletionSource(ChASMCompletionSourceProvider sourceProvider, ITextBuffer textBuffer)
        {
            m_sourceProvider = sourceProvider;
            m_textBuffer = textBuffer;
        }

        void ICompletionSource.AugmentCompletionSession(ICompletionSession session, IList<Microsoft.VisualStudio.Language.Intellisense.CompletionSet> completionSets)
        {
            completionSets.Add(new Microsoft.VisualStudio.Language.Intellisense.CompletionSet(
                "Registers",    //the non-localized title of the tab
                "Registers",    //the display title of the tab
                FindTokenSpanAtPosition(session.GetTriggerPoint(m_textBuffer),
            session),
                Globalz.Registers.Items,
                null));

            completionSets.Add(new Microsoft.VisualStudio.Language.Intellisense.CompletionSet(
                "Mnemonics",    //the non-localized title of the tab
                "Mnemonics",    //the display title of the tab
                FindTokenSpanAtPosition(session.GetTriggerPoint(m_textBuffer),
                    session),
                Globalz.Mnemonics.ItemsForCodeCompletion,
                null));

            completionSets.Add(new Microsoft.VisualStudio.Language.Intellisense.CompletionSet(
                "WinAPIs",    //the non-localized title of the tab
                "WinAPIs",    //the display title of the tab
                FindTokenSpanAtPosition(session.GetTriggerPoint(m_textBuffer),
                    session),
                Globalz.WinAPIs.ItemsForCodeCompletion,
                null));

            var extra = new Microsoft.VisualStudio.Language.Intellisense.CompletionSet(
                "Extra",    //the non-localized title of the tab
                "Extra",    //the display title of the tab
                FindTokenSpanAtPosition(session.GetTriggerPoint(m_textBuffer),
                    session),
                new HashSet<Completion>()
                {
                    new Completion("sizeof", "sizeof", "sizeof", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("extrn", "extrn", "extrn", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("proc", "proc", "proc", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("endp", "endp", "endp", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("includelib", "includelib", "includelib", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("include", "include", "include", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion(".const", ".const", ".const", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion(".data", ".data", ".data", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion(".code", ".code", ".code", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("equ", "equ", "equ", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("textequ", "textequ", "textequ", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("db", "db", "db", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("dw", "dw", "dw", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("dd", "dd", "dd", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("dq", "dq", "dq", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("byte", "byte", "byte", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("sbyte", "sbyte", "sbyte", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("word", "word", "word", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("sword", "sword", "sword", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("dword", "dword", "dword", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("sdword", "sdword", "sdword", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("qword", "qword", "qword", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("sqword", "sqword", "sqword", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("fword", "fword", "fword", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("df", "df", "df", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("tbyte", "tbyte", "tbyte", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("dt", "dt", "dt", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("real4", "real4", "real4", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("real8", "real8", "real8", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("real10", "real10", "real10", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("macro", "macro", "macro", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("endm", "real10", "real10", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("public", "public", "public", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("struct", "struct", "struct", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("union", "union", "union", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("record", "record", "record", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("typedef", "typedef", "typedef", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null),
                    new Completion("dup", "dup", "dup", CodeCompletion.GetItemIcon(CompletionItemType.Misc), null)
                },
            null);

            completionSets.Add(extra);

            completionSets.Add(new Microsoft.VisualStudio.Language.Intellisense.CompletionSet(
                "Code Samples",    //the non-localized title of the tab
                "Code Samples",    //the display title of the tab
                FindTokenSpanAtPosition(session.GetTriggerPoint(m_textBuffer),
                    session),
                Globalz.Specialz.CodeSamples,
                null));
        }

        private ITrackingSpan FindTokenSpanAtPosition(ITrackingPoint point, ICompletionSession session)
        {
            SnapshotPoint currentPoint = (session.TextView.Caret.Position.BufferPosition) - 1;
            ITextStructureNavigator navigator = m_sourceProvider.NavigatorService.GetTextStructureNavigator(m_textBuffer);
            TextExtent extent = navigator.GetExtentOfWord(currentPoint);
            return currentPoint.Snapshot.CreateTrackingSpan(extent.Span, SpanTrackingMode.EdgeInclusive);
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
