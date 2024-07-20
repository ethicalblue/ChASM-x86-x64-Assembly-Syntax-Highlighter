/*
 * 
 * © https://learn.microsoft.com/en-us/visualstudio/extensibility/creating-an-extension-with-an-editor-item-template?view=vs-2022
 * 
 */

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using System;
using System.Collections.Generic;

namespace ChASM.EditorClassifiers
{
    internal class ChASMEditorClassifier : IClassifier
    {
        private readonly IClassificationType classificationType;

        internal ChASMEditorClassifier(IClassificationTypeRegistryService registry)
        {
            this.classificationType = registry.GetClassificationType("ChASMEditorClassifier");
        }

        #region IClassifier

#pragma warning disable 67

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

#pragma warning restore 67

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            var result = new List<ClassificationSpan>()
            {
                new ClassificationSpan(new SnapshotSpan(span.Snapshot, new Span(span.Start, span.Length)), this.classificationType)
            };

            return result;
        }

        #endregion
    }
}
