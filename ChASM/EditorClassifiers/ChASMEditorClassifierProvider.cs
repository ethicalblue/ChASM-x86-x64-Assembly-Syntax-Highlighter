/*
 * 
 * © https://learn.microsoft.com/en-us/visualstudio/extensibility/creating-an-extension-with-an-editor-item-template?view=vs-2022
 * 
 */

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace ChASM.EditorClassifiers
{
    [Export(typeof(IClassifierProvider))]
    [ContentType("masm")]
    internal class ChASMEditorClassifierProvider : IClassifierProvider
    {

#pragma warning disable 649

        [Import]
        private IClassificationTypeRegistryService classificationRegistry;

#pragma warning restore 649

        #region IClassifierProvider

        /// <summary>
        /// Gets a classifier for the given text buffer.
        /// </summary>
        /// <param name="buffer">The <see cref="ITextBuffer"/> to classify.</param>
        /// <returns>A classifier for the text buffer, or null if the provider cannot do so in its current state.</returns>
        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            return buffer.Properties.GetOrCreateSingletonProperty<ChASMEditorClassifier>(creator: () => new ChASMEditorClassifier(this.classificationRegistry));
        }

        #endregion
    }
}
