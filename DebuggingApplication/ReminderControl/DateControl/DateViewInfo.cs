using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace DebuggingApplication
{
    public class DateViewInfo
    {
        public DateViewInfo(DateControl owner) {
            _Owner = owner;
            _HorIndent = 2;
            _VertIndent = 2;
            _Font = new Font("Calibry", 12);
        }

        // Fields...
        private Rectangle _SelectionBounds;
        private Font _Font;
        private int _VertIndent;
        private int _HorIndent;
        private DateControl _Owner;
        private Rectangle _MinuteBounds;
        private Rectangle _HourBounds;
        private Rectangle _DayBounds;
        private Rectangle _Bounds;

        public Rectangle Bounds
        {
            get { return _Bounds; }
            set
            {
                _Bounds = value;
            }
        }


        public Rectangle DayBounds
        {
            get { return _DayBounds; }
            set
            {
                _DayBounds = value;
            }
        }
        

        public Rectangle HourBounds
        {
            get { return _HourBounds; }
            set
            {
                _HourBounds = value;
            }
        }


        public Rectangle SelectionBounds
        {
            get { return _SelectionBounds; }
            set
            {
                _SelectionBounds = value;
            }
        }
        

        public DateControl Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }

        public Rectangle MinuteBounds
        {
            get { return _MinuteBounds; }
            set
            {
                _MinuteBounds = value;
            }
        }

        protected virtual int HorIndent
        {
            get { return _HorIndent; }
        }


        protected virtual int VertIndent
        {
            get { return _VertIndent; }
        }

        public Font Font
        {
            get { return _Font; }
        }
        
        public virtual void CalcViewInfo(Graphics graphics)
        {
            CalcBounds();
            CalcDayBounds();
            CalcHourBounds();
            CalcMinuteBounds();
            CalcSelectionBounds();
        }

        protected virtual void CalcBounds()
        {
            Bounds = Owner.ClientRectangle;
        }

        protected virtual void CalcDayBounds()
        {
            DayBounds = new Rectangle(Bounds.X + HorIndent, Bounds.Y + VertIndent, Bounds.Width / 2 - HorIndent, Bounds.Height - VertIndent - 1);
        }

        protected virtual void CalcHourBounds()
        {
            HourBounds = new Rectangle(DayBounds.Right + HorIndent, Bounds.Y + VertIndent, Bounds.Width / 4 - HorIndent, Bounds.Height - VertIndent - 1);
        }

        protected virtual void CalcMinuteBounds()
        {
            MinuteBounds = new Rectangle(HourBounds.Right + HorIndent, Bounds.Y + VertIndent, Bounds.Width / 4 - HorIndent - 1, Bounds.Height - VertIndent - 1);
        }

        protected virtual void CalcSelectionBounds()
        {
            SelectionBounds = new Rectangle(DayBounds.X + 2, DayBounds.Bottom / 3, Bounds.Width - HorIndent - 4, Bounds.Height / 3);
        }

        public Rectangle GetRect(Rectangle rect, PositionType positionType)
        {
            if (positionType == PositionType.Top)
                return new Rectangle(rect.X, rect.Y, rect.Width, rect.Height / 3);
            if(positionType == PositionType.Middle)
                return new Rectangle(rect.X, rect.Bottom / 3, rect.Width, rect.Height / 3);
            return new Rectangle(rect.X, rect.Bottom * 2 / 3, rect.Width, rect.Height / 3);
        }

        public virtual int CalcMinHeight(Graphics graphics)
        {
            int height = VertIndent * 4;
            height += graphics.MeasureString("l", Font).ToSize().Height * 3;
            return height;
        }

        public DateHitInfo CalcHitInfo(Point point)
        {
            if (DayBounds.Contains(point))
                return new DateHitInfo() { Point = point, HitInfoType = DateInfoType.Day};
            if (HourBounds.Contains(point))
                return new DateHitInfo() { Point = point, HitInfoType = DateInfoType.Hour };
            if (MinuteBounds.Contains(point))
                return new DateHitInfo() { Point = point, HitInfoType = DateInfoType.Minute };
            return new DateHitInfo() { Point = point, HitInfoType = DateInfoType.None };
        }
    }
}
