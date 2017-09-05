using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace DebuggingApplication
{
    public class ButtonPainter
    {
        public ButtonPainter() { }
      
        public virtual void Draw(System.Drawing.Graphics graphics, ButtonViewInfo viewInfo)
        {
            DrawButton(graphics, viewInfo);
            DrawGlyph(graphics, viewInfo);
            DrawBorder(graphics, viewInfo);
        }

        protected virtual void DrawButton(System.Drawing.Graphics graphics, ButtonViewInfo viewInfo)
        {
            Brush brush = GetButtonBrush(viewInfo);
            graphics.FillRectangle(brush, viewInfo.Bounds);
        }

        protected virtual void DrawBorder(System.Drawing.Graphics graphics, ButtonViewInfo viewInfo)
        {
            if (viewInfo.Owner.ButtonBorderStyle == ButtonBorderStyle.Normal)
            {
                Pen pen = GetBorderPen(viewInfo);
                graphics.DrawRectangle(pen, viewInfo.Bounds);
            }
        }

        protected virtual void DrawGlyph(System.Drawing.Graphics graphics, ButtonViewInfo viewInfo)
        {
            Brush brush = GetGlyphBrush(viewInfo);
            graphics.DrawString(viewInfo.Owner.Text, viewInfo.DefaultFont, brush, viewInfo.TextBounds);
        }

        private Pen GetBorderPen(ButtonViewInfo viewInfo)
        {
            Pen pen = Pens.White;
            if (viewInfo.ButtonState == State.Hot)
                pen = Pens.Orange;
            if (viewInfo.ButtonState == State.Pressed)
                pen = Pens.Green;
            return pen;
        }

        private Brush GetButtonBrush(ButtonViewInfo viewInfo)
        {
            Brush brush = Brushes.Indigo;
            if (viewInfo.ButtonState == State.Hot)
                brush = Brushes.Blue;
            if (viewInfo.ButtonState == State.Pressed)
                brush = Brushes.White;
            return brush;
        }

        private Brush GetGlyphBrush(ButtonViewInfo viewInfo)
        {
            Brush brush = Brushes.White;
            if (viewInfo.ButtonState == State.Hot)
                brush = Brushes.Orange;
            if (viewInfo.ButtonState == State.Pressed)
                brush = Brushes.Green;
            return brush;
        }
    }
}
