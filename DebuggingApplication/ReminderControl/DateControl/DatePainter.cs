using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace DebuggingApplication
{
    public class DatePainter
    {
        public DatePainter(){ }

        public virtual void Draw(System.Drawing.Graphics graphics, DateViewInfo dateViewInfo)
        {
            DrawDay(graphics, dateViewInfo);
            DrawHour(graphics, dateViewInfo);
            DrawMinute(graphics, dateViewInfo);
            DrawSelection(graphics, dateViewInfo);
        }

        protected virtual void DrawDay(System.Drawing.Graphics graphics, DateViewInfo dateViewInfo)
        {
            DrawDigit(graphics, dateViewInfo, dateViewInfo.DayBounds, DateInfoType.Day);
            graphics.DrawRectangle(Pens.Black, dateViewInfo.DayBounds);
        }

        protected virtual void DrawDigit(System.Drawing.Graphics graphics, DateViewInfo dateViewInfo, Rectangle rect, DateInfoType datePart)
        {
            Array positions = Enum.GetValues(typeof(PositionType));
            Brush brush = Brushes.LightGray;
            foreach (PositionType positionType in positions)
            {
                Rectangle dateRect = dateViewInfo.GetRect(rect, positionType);
                if (dateViewInfo.Owner.SelectedDatePart == datePart)
                    graphics.FillRectangle(Brushes.CornflowerBlue, dateRect);

                string dayValue = dateViewInfo.Owner.GetDayValueByPosition(datePart, positionType).ToString();
                graphics.DrawString(dayValue, dateViewInfo.Font, GetBrush(positionType), dateRect);
                
            }
        }

        protected virtual Brush GetBrush(PositionType positionType)
        {
            if (positionType == PositionType.Middle)
                return Brushes.Black;
            return Brushes.LightGray;
        }

        protected virtual Pen GetSelectionPen()
        {
           return new Pen(Brushes.Indigo, 5);
        }

        protected virtual void DrawHour(System.Drawing.Graphics graphics, DateViewInfo dateViewInfo)
        {
            DrawDigit(graphics, dateViewInfo, dateViewInfo.HourBounds, DateInfoType.Hour);
            graphics.DrawRectangle(Pens.Black, dateViewInfo.HourBounds);
        }

        protected virtual void DrawMinute(System.Drawing.Graphics graphics, DateViewInfo dateViewInfo)
        {
            DrawDigit(graphics, dateViewInfo, dateViewInfo.MinuteBounds, DateInfoType.Minute);
            graphics.DrawRectangle(Pens.Black, dateViewInfo.MinuteBounds);
        }

        protected virtual void DrawSelection(System.Drawing.Graphics graphics, DateViewInfo dateViewInfo)
        {
            graphics.DrawRectangle(GetSelectionPen(), dateViewInfo.SelectionBounds);
        }
    }
}
