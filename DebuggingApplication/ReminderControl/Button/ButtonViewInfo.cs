using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace DebuggingApplication
{
    
    public class ButtonViewInfo
    {
        public ButtonViewInfo(Button button)
        {
            _Owner = button;
            _DefaultFont = new Font("Arial", 12);
        }

     
        // Fields...
        private State _ButtonState;
        private Font _DefaultFont;
        private Rectangle _BorderBounds;
        private Rectangle _TextBounds;
        private Rectangle _Bounds;
        private Button _Owner;

        public Font DefaultFont
        {
            get { return _DefaultFont; }
        }

        protected virtual Size MinButtonSize
        {
            get { return new Size(15, 15); }
        }


        public Button Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }

        public Rectangle Bounds
        {
            get { return _Bounds; }
            set { _Bounds = value; }
        }


        public Rectangle TextBounds
        {
            get { return _TextBounds; }
            set
            {
                _TextBounds = value;
            }
        }

        public State ButtonState
        {
            get { return _ButtonState; }
            set { _ButtonState = value; }
        }

        public virtual Padding ButtonPaddings
        {
            get { return new Padding(5, 3, 5, 3); }
        }

        protected internal void CalcViewInfo(Graphics graphics, Rectangle bounds)
        {
            Size textSize = TextRenderer.MeasureText(Owner.Text, DefaultFont);
            Size buttonSize = new Size(Math.Max(textSize.Width, MinButtonSize.Width), Math.Max(textSize.Height, MinButtonSize.Height));
            Bounds = new Rectangle(bounds.Right - ButtonPaddings.Right - buttonSize.Width,
                         bounds.Top + ButtonPaddings.Top,
                         buttonSize.Width,
                         buttonSize.Height);
            TextBounds = new Rectangle(Bounds.X + Bounds.Width / 2 - textSize.Width / 2, 
                             Bounds.Y + Bounds.Height / 2 - textSize.Height / 2, 
                             textSize.Width, 
                             textSize.Height);
        }

        public void OffsetBounds(Point offset, ButtonViewInfo buttonViewInfo, Rectangle buttonRect, Rectangle textRect)
        {
            buttonRect.Offset(offset.X, offset.Y);
            textRect.Offset(offset.X, offset.Y);

            buttonViewInfo.Bounds = buttonRect;
            buttonViewInfo.TextBounds = textRect;
        }

        public void RestoreBounds(Point offset, ButtonViewInfo buttonViewInfo, Rectangle buttonRect, Rectangle textRect)
        {
            buttonRect.Offset(-offset.X, -offset.Y);
            textRect.Offset(-offset.X, -offset.Y);
            buttonViewInfo.Bounds = buttonRect;
            buttonViewInfo.TextBounds = textRect;
        }

        public ButtonViewInfo Clone()
        {
            ButtonViewInfo viewInfo = new ButtonViewInfo(Owner);
            viewInfo.Bounds = Bounds;
            viewInfo.TextBounds = TextBounds;
            return viewInfo;
        }
    }
}
