using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Drawing;

namespace DebuggingApplication
{
 
    public class Button
    {

        public Button(string text, ButtonKind kind, ButtonBorderStyle style)
        {
            _ButtonBorderStyle = style;
            _Kind = kind;
            _Text = text;
            _Visible = true;
            _Painter = CreatePainter();
            _ViewInfo = CreateViewInfo();
        }


        // Fields...
        private bool _Visible;
        private ButtonKind _Kind;
        private string _Text;
        private ButtonViewInfo _ViewInfo;
        private ButtonPainter _Painter;
        private ButtonBorderStyle _ButtonBorderStyle;


        public ButtonPainter ButtonPainter
        {
            get { return _Painter; }
        }

        public string Text
        {
            get { return _Text; }
            set
            {
                if (_Text != value)
                    _Text = value;
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public ButtonViewInfo ButtonViewInfo
        {
            get { return _ViewInfo; }
        }

        public ButtonBorderStyle ButtonBorderStyle
        {
            get { return _ButtonBorderStyle; }
            set { _ButtonBorderStyle = value; }
        }

        public ButtonKind Kind
        {
            get { return _Kind; }
            set { _Kind = value; }
        }


        public bool Visible
        {
            get { return _Visible; }
            set
            {
                _Visible = value;
            }
        }
        

        protected virtual ButtonPainter CreatePainter()
        {
            return new ButtonPainter();
        }

        protected virtual ButtonViewInfo CreateViewInfo()
        {
            return new ButtonViewInfo(this);
        }

        protected internal void CalcViewInfo(Graphics graphics, Rectangle headerTextBounds)
        {
            ButtonViewInfo.CalcViewInfo(graphics, headerTextBounds);
        }
    }
}