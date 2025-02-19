﻿using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PintoNS.Controls
{
    public class TextBoxWithPlaceholderSupport : TextBox
    {
        private bool isFocused = false;
        protected string orgText = "";
        protected string placeholderText = "";
        protected Color textForeColor = Color.Black;
        protected Color placeholderTextForeColor = Color.DimGray;
        public string PlaceholderText { get { return placeholderText; } set { placeholderText = value; } }
        public Color TextForeColor { get { return textForeColor; } set { textForeColor = value; } }
        public Color PlaceholderTextForeColor { get { return placeholderTextForeColor; } set { placeholderTextForeColor = value; } }
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public event EventHandler TextChanged2;

        public void ChangeTextDisplayed() 
        {
            if (isFocused)
            {
                Text = orgText;
                ForeColor = textForeColor;
            }
            else 
            {
                orgText = Text;

                if (string.IsNullOrEmpty(orgText))
                {
                    Text = placeholderText;
                    ForeColor = placeholderTextForeColor;
                }
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            orgText = Text;
            ChangeTextDisplayed();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            isFocused = true;
            ChangeTextDisplayed();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            isFocused = false;
            ChangeTextDisplayed();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (isFocused)
                TextChanged2.Invoke(this, e);
            base.OnTextChanged(e);
        }
    }
}
