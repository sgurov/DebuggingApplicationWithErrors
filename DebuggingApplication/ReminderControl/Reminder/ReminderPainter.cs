using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace DebuggingApplication
{
    public class ReminderPainter
    {
        public ReminderPainter(){ }
      
        public virtual void Draw(System.Drawing.Graphics graphics, DebuggingApplication.ReminderViewInfo viewInfo)
        {
            DrawContent(graphics, viewInfo);
            DrawBorder(graphics, viewInfo);
        }

        protected virtual void DrawContent(System.Drawing.Graphics graphics, DebuggingApplication.ReminderViewInfo viewInfo)
        {
            DrawHeader(graphics, viewInfo);
        }

        protected virtual void DrawHeader(System.Drawing.Graphics graphics, ReminderViewInfo viewInfo)
        {
            FillHeader(graphics, viewInfo);
            DrawHeaderText(graphics, viewInfo);
            DrawHeaderButtons(graphics, viewInfo);
        }

        protected virtual void FillHeader(Graphics graphics, ReminderViewInfo viewInfo)
        {
            graphics.FillRectangle(Brushes.Indigo, viewInfo.HeaderBounds);
        }

        protected virtual void DrawHeaderText(System.Drawing.Graphics graphics, ReminderViewInfo viewInfo)
        {
            graphics.DrawString(viewInfo.Owner.HeaderText, viewInfo.Owner.Font, Brushes.White, viewInfo.HeaderTextBounds);
        }

        protected virtual void DrawHeaderButtons(System.Drawing.Graphics graphics, ReminderViewInfo viewInfo)
        {
            DrawHeaderButton(graphics, viewInfo.Owner.HeaderButton);
            DrawHeaderButton(graphics, viewInfo.Owner.BackButton);
        }

        private void DrawHeaderButton(System.Drawing.Graphics graphics, Button button)
        {
           button.ButtonPainter.Draw(graphics, button.ButtonViewInfo);
        }

        protected virtual void DrawBorder(System.Drawing.Graphics graphics, DebuggingApplication.ReminderViewInfo viewInfo)
        {
            Rectangle rect = viewInfo.Bounds;
            graphics.DrawLine(Pens.Orange, rect.X, rect.Y, rect.Right, rect.Y);
            graphics.DrawLine(Pens.Orange, rect.X, rect.Y, rect.X, rect.Bottom);
            graphics.DrawLine(Pens.White, rect.Right - 1, rect.Y, rect.Right - 1, rect.Bottom);
            graphics.DrawLine(Pens.White, rect.X, rect.Bottom - 1, rect.Right, rect.Bottom - 1);
        }
    }
}
