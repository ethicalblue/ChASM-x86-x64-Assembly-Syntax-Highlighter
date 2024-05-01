/*
 * 
 * © https://learn.microsoft.com/en-us/visualstudio/extensibility/creating-an-extension-with-an-editor-item-template?view=vs-2022
 * 
 */

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace ChASM.EditorClassifiers
{
    internal static class ChASMEditorClassifierClassificationDefinition
    {

#pragma warning disable 169

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("ChASMEditorClassifier")]
        private static ClassificationTypeDefinition typeDefinition;

#pragma warning restore 169

    }
}
