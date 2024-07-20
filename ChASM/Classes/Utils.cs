/*
 * 
 * © 2022 by ethical.blue Magazine
 * 
 */

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChASM
{
    internal class Utilz
    {
        internal static TextBlock CreateTextBlock(TextBlockType textBlockType, string text)
        {
            if(textBlockType == TextBlockType.GeneralPurposeRegister)
            {
                return new TextBlock
                {
                    Foreground = new SolidColorBrush(new Color() { R = 0x0D, G = 0xCA, B = 0xF0, A = 0xFF }),
                    Text = text
                };
            }
            else if(textBlockType == TextBlockType.SpecialWord)
            {
                return new TextBlock
                {
                    Foreground = new SolidColorBrush(new Color() { R = 0xDD, G = 0xEE, B = 0xFF, A = 0xFF }),
                    Background = new SolidColorBrush(new Color() { R = 0x1E, G = 0x1E, B = 0x1E, A = 0xFF }),
                    Text = text
                };
            }
            else if(textBlockType == TextBlockType.TextString)
            {
                return new TextBlock
                {
                    Foreground = new SolidColorBrush(new Color() { R = 0x00, G = 0xAA, B = 0xFF, A = 0xFF }),
                    Background = new SolidColorBrush(new Color() { R = 0x1E, G = 0x1E, B = 0x1E, A = 0xFF }),
                    Text = text,
                    FontStyle = FontStyles.Normal
                };
            }
            else if(textBlockType == TextBlockType.Comment)
            {
                return new TextBlock
                {
                    Foreground = new SolidColorBrush(new Color() { R = 0xAA, G = 0xAA, B = 0xAA, A = 0xFF }),
                    Background = new SolidColorBrush(new Color() { R = 0x1E, G = 0x1E, B = 0x1E, A = 0xFF }),
                    Text = text,
                    FontStyle = FontStyles.Normal
                };
            }
            else
            {
                return new TextBlock
                {
                    Foreground = new SolidColorBrush(new Color() { R = 0x00, G = 0xAA, B = 0xFF, A = 0xFF }),
                    Background = new SolidColorBrush(new Color() { R = 0x1E, G = 0x1E, B = 0x1E, A = 0xFF }),
                    Text = text,
                    FontStyle = FontStyles.Normal
                };
            }
        }
    }

    internal enum TextBlockType
    {
        GeneralPurposeRegister,
        SpecialWord,
        TextString,
        Comment
    }
}
