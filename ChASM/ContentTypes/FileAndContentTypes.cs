/*
 * 
 * © https://learn.microsoft.com/en-us/visualstudio/extensibility/creating-an-extension-with-an-editor-item-template?view=vs-2022
 * 
 */

using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace ChASM
{
    internal static class FileAndContentTypeDefinitions
    {
        [Export]
        [Name("masm")]
        [BaseDefinition("text")]
        internal static ContentTypeDefinition hidingContentTypeDefinition = null;

        [Export]
        [FileExtension(".asm")]
        [ContentType("masm")]
        internal static FileExtensionToContentTypeDefinition hiddenFileExtensionDefinition = null;

        [Export]
        [FileExtension(".inc")]
        [ContentType("masm")]
        internal static FileExtensionToContentTypeDefinition hiddenFileExtensionDefinition2 = null;
    }
}