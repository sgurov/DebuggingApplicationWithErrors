using System;
using System.Collections.Generic;
using System.Linq;

namespace DebuggingApplication
{
    public class ButtonClickEventArgs : EventArgs
    {

        public ButtonClickEventArgs(){ }

        // Fields...
        private Button _Button;
        private bool _Handled;

        public bool Handled
        {
            get { return _Handled; }
            set
            {
                _Handled = value;
            }
        }


        public Button Button
        {
            get { return _Button; }
            internal set { _Button = value; }
        }
    }
}
