/*
 * 
 * © https://learn.microsoft.com/en-us/visualstudio/extensibility/walkthrough-highlighting-text?view=vs-2022&tabs=csharp
 * 
 */

using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace ChASM.TextAdornments
{
    [Export(typeof(IWpfTextViewCreationListener))]
    [ContentType("masm")]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal sealed class ChASMTextAdornmentTextViewCreationListener : IWpfTextViewCreationListener
    {
#pragma warning disable 649, 169

        [Export(typeof(AdornmentLayerDefinition))]
        [Name("ChASMTextAdornment")]
        [Order(After = PredefinedAdornmentLayers.Text, Before = PredefinedAdornmentLayers.Selection)]
        private AdornmentLayerDefinition editorAdornmentLayer;

#pragma warning restore 649, 169

        #region IWpfTextViewCreationListener

        /// <summary>
        /// Called when a text view having matching roles is created over a text data model having a matching content type.
        /// Instantiates a ChASMTextAdornment manager when the textView is created.
        /// </summary>
        /// <param name="textView">The <see cref="IWpfTextView"/> upon which the adornment should be placed</param>
        public void TextViewCreated(IWpfTextView textView)
        {
            // The adornment will listen to any event that changes the layout (text changes, scrolling, etc)
            new ChASMTextAdornment(textView);
        }

        #endregion
    }
}
