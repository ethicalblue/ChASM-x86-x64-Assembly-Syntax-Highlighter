/*
 * 
 * © https://learn.microsoft.com/en-us/visualstudio/extensibility/creating-an-extension-with-an-editor-item-template?view=vs-2022
 * 
 */

using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;
using System.Windows.Media;
using System.Windows;

namespace ChASM.EditorClassifiers
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "ChASMEditorClassifier")]
    [Name("ChASMEditorClassifier")]
    [UserVisible(true)] // This should be set to true
    [Order(Before = Priority.Default)]
    internal sealed class ChASMEditorClassifierFormat : ClassificationFormatDefinition
    {
        public ChASMEditorClassifierFormat()
        {
            this.DisplayName = "ChASMEditorClassifier";
            this.ForegroundColor = new Color() { R = 0x00, G = 0xAA, B = 0xFF, A = 0xFF };
            this.FontTypeface = new Typeface(new FontFamily("Cascadia Code"),
                                                 FontStyles.Normal,
                                                 FontWeights.Normal,
                                                 FontStretches.Normal,
                                                 new FontFamily("Cascadia Code"));
        }
    }
}
