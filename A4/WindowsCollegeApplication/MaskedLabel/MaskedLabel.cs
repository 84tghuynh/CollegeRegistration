//=============================================================================
// System  : EWSoftware MaskedLabel Control
// File    : MaskedLabel.cs
// Author  : Eric Woodruff  (Eric@EWoodruff.us)
// Updated : 06/26/2006
// Note    : Copyright 2006, Eric Woodruff, All rights reserved
// Compiler: Microsoft Visual C#
//
// This file contains a derived label control that can format its text using
// the same mask characters as the MaskedTextBox control.
//
// This code may be used in compiled form in any way you desire.  This
// file may be redistributed unmodified by any means PROVIDING it is not
// sold for profit without the author's written consent, and providing
// that this notice and the author's name and all copyright notices
// remain intact.
//
// This code is provided "as is" with no warranty either express or
// implied.  The author accepts no liability for any damage or loss of
// business that this product may cause.
//
// Version     Date     Who  Comments
// ============================================================================
// 1.0.0.0  05/22/2006  EFW  Created the code
//=============================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace EWSoftware.MaskedLabelControl
{
    /// <summary>
    /// This is a derived label control that can format its text using the same
    /// mask characters as the <see cref="MaskedTextBox" /> control.
    /// </summary>
    [Description("A label control that can format its text using the same " +
        "mask characters as the MaskedTextBox control")]
    public class MaskedLabel : Label
    {
        //=====================================================================
        // Private data members

        MaskedTextProvider provider;
        MaskedTextResultHint hint;

        int hintPos;

        string unmaskedText;

        //=====================================================================

        /// <summary>
        /// Constructor
        /// </summary>
        /// <remarks>There is no mask applied by default</remarks>
        public MaskedLabel()
        {
            provider = new MaskedTextProvider("<>", CultureInfo.CurrentCulture);
        }

        #region Properties
        //=====================================================================
        // Properties

        /// <summary>
        /// This returns a clone of the masked text provider currently being
        /// used by the masked label control.
        /// </summary>
        [Browsable(false)]
        public MaskedTextProvider MaskedTextProvider
        {
            get { return (MaskedTextProvider)provider.Clone(); }
        }

        /// <summary>
        /// This returns the result hint for the last assignment to the
        /// <see cref="Text" /> property.
        /// </summary>
        /// <remarks>If the assigned text could not be properly formatted,
        /// this will contain a hint as to why not.  Positive values
        /// indicate success.  Negative values indicate failure.</remarks>
        [Browsable(false)]
        public MaskedTextResultHint ResultHint
        {
            get { return hint; }
        }

        /// <summary>
        /// This returns the result hint position for the last assignment to
        /// the <see cref="Text" /> property.
        /// </summary>
        /// <remarks>If the assigned text could not be properly formatted,
        /// this will contain the position of the first failure.</remarks>
        [Browsable(false)]
        public int HintPosition
        {
            get { return hintPos; }
        }

        /// <summary>
        /// This read-only property returns the unmasked copy of the text
        /// </summary>
        [Browsable(false)]
        public string UnmaskedText
        {
            get { return unmaskedText; }
        }

        /// <summary>
        /// This is used to set or get the label text.
        /// </summary>
        /// <remarks>When set, the text is formatted according to the current
        /// masked text provider settings.  If the mask is empty, the text is
        /// set as-is.  When retrieved, the text is returned in its formatted
        /// state.  Use <see cref="UnmaskedText" /> to get the text without
        /// the mask applied.</remarks>
        [Description("The text to display in the label")]
        public override string Text
        {
            get { return base.Text; }
            set
            {
                unmaskedText = value;

                // If there is no mask or no text, pass it through unchanged
                if(provider.Mask != "<>" && value != null)
                {
                    provider.Set(value, out hintPos, out hint);

                    // Positive hint results are successful
                    if(hint > 0)
                    {
                        base.Text = provider.ToString();
                        return;
                    }
                }

                base.Text = unmaskedText;
            }
        }

        /// <summary>
        /// This is used to set or get the culture information associated with
        /// the masked label.
        /// </summary>
        /// <exception cref="ArgumentNullException">This is thrown if the
        /// culture value is null</exception>
        [Category("Appearance"),
          Description("Gets or sets the culture information associated with the masked label")]
        public CultureInfo Culture
        {
            get { return provider.Culture; }
            set
            {
                if(value == null)
                    throw new ArgumentNullException("value");

                if(!provider.Culture.Equals(value))
                {
                    // Recreate the provider with the new culture
                    MaskedTextProvider newProvider = new MaskedTextProvider(
                        provider.Mask, value);
                    newProvider.IncludePrompt = provider.IncludePrompt;
                    newProvider.PromptChar = provider.PromptChar;

                    provider = newProvider;
                    this.Text = unmaskedText;
                }
            }
        }

        /// <summary>
        /// This is used to set or get the mask for the label text
        /// </summary>
        [Category("Appearance"), DefaultValue(""),
          Description("Gets or sets the mask to use for the label text")]
        public string Mask
        {
            get
            {
                if(provider.Mask == "<>")
                    return String.Empty;

                return provider.Mask;
            }
            set
            {
                if(value == null || value.Length == 0)
                    value = "<>";

                if(provider.Mask != value)
                {
                    // Recreate the provider with the new mask
                    MaskedTextProvider newProvider = new MaskedTextProvider(
                        value, provider.Culture);
                    newProvider.IncludePrompt = provider.IncludePrompt;
                    newProvider.PromptChar = provider.PromptChar;

                    provider = newProvider;
                    this.Text = unmaskedText;
                }
            }
        }

        /// <summary>
        /// This is used to set or get the prompt character for display
        /// in the label text.
        /// </summary>
        /// <value>The default is an underscore (_).</value>
        [Category("Appearance"), DefaultValue('_'),
          Description("Gets or sets the prompt character for display in the label text")]
        public char PromptChar
        {
            get { return provider.PromptChar; }
            set
            {
                provider.PromptChar = value;
                this.Text = unmaskedText;
            }
        }

        /// <summary>
        /// This is used to set or get whether or not prompt characters are
        /// also displayed in the label text.
        /// </summary>
        /// <value>By default, prompt characters are not shown.</value>
        [Category("Appearance"), DefaultValue(false),
          Description("Gets or sets whether or not prompt characters are displayed in the label text")]
        public bool IncludePrompt
        {
            get { return provider.IncludePrompt; }
            set
            {
                provider.IncludePrompt = value;
                this.Text = unmaskedText;
            }
        }
        #endregion

        #region Private designer methods
        //=====================================================================
        // Private designer methods

        // <summary>
        // This is used to determine whether or not to serialize the culture
        // </summary>
        // <returns></returns>
        private bool ShouldSerializeCulture()
        {
            return !CultureInfo.CurrentCulture.Equals(this.Culture);
        }
        #endregion

        //=====================================================================
        // Static methods

        /// <summary>
        /// Format the specified text using the specified mask
        /// </summary>
        /// <param name="mask">The mask to use</param>
        /// <param name="text">The text to format</param>
        /// <returns>The formatted text string</returns>
        /// <overloads>There are four overloads for this method.</overloads>
        public static string Format(string mask, string text)
        {
            MaskedTextResultHint hint;
            int pos;

            return MaskedLabel.Format(mask, text, '\x0', null,
                out hint, out pos);
        }

        /// <summary>
        /// Format the specified text using the specified mask and prompt
        /// character.
        /// </summary>
        /// <param name="mask">The mask to use.</param>
        /// <param name="text">The text to format.</param>
        /// <param name="promptChar">The prompt character to use for missing
        /// characters.  If a null character ('\x0') is specified, prompt
        /// characters are omitted.</param>
        /// <returns>The formatted text string.</returns>
        public static string Format(string mask, string text, char promptChar)
        {
            MaskedTextResultHint hint;
            int pos;

            return MaskedLabel.Format(mask, text, promptChar, null,
                out hint, out pos);
        }

        /// <summary>
        /// Format the specified text using the specified mask, prompt
        /// character, and culture information.
        /// </summary>
        /// <param name="mask">The mask to use.</param>
        /// <param name="text">The text to format.</param>
        /// <param name="promptChar">The prompt character to use for missing
        /// characters.  If a null character ('\x0') is specified, prompt
        /// characters are omitted.</param>
        /// <param name="culture">The culture information to use.  If null,
        /// the current culture is used.</param>
        /// <returns>The formatted text string.</returns>
        public static string Format(string mask, string text, char promptChar,
          CultureInfo culture)
        {
            MaskedTextResultHint hint;
            int pos;

            return MaskedLabel.Format(mask, text, promptChar, culture,
                out hint, out pos);
        }

        /// <summary>
        /// Format the specified text using the specified mask, prompt
        /// character, and culture information and return the result
        /// values.
        /// </summary>
        /// <param name="mask">The mask to use.</param>
        /// <param name="text">The text to format.</param>
        /// <param name="promptChar">The prompt character to use for missing
        /// characters.  If a null character ('\x0') is specified, prompt
        /// characters are omitted.</param>
        /// <param name="culture">The culture information to use.  If null,
        /// the current culture is used.</param>
        /// <param name="hint">The result of formatting the text.</param>
        /// <param name="hintPosition">The position related to the result
        /// hint.</param>
        /// <returns>The formatted text string.</returns>
        public static string Format(string mask, string text, char promptChar,
          CultureInfo culture, out MaskedTextResultHint hint,
          out int hintPosition)
        {
            if(text == null)
                text = String.Empty;

            if(culture == null)
                culture = CultureInfo.CurrentCulture;

            MaskedTextProvider provider = new MaskedTextProvider(mask, culture);

            // Set the prompt character options
            if(promptChar != '\x0')
            {
                provider.PromptChar = promptChar;
                provider.IncludePrompt = true;
            }

            // Format and return the string
            provider.Set(text, out hintPosition, out hint);

            // Positive hint results are successful
            if(hint > 0)
                return provider.ToString();

            // Return the text as-is if it didn't fit the mask
            return text;
        }
    }
}
