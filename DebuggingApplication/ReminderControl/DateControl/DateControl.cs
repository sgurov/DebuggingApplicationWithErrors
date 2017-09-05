using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace DebuggingApplication
{
    public class DateControl : Control
    {
        public DateControl()
            : base()
        {
            DoubleBuffered = true;
            _Painter = CreatePainter();
            _ViewInfo = CreateViewInfo();
            _Date = DateTime.Now;
        }

        // Fields...
        private DateInfoType _SelectedDatePart;
        private DateTime _Date;
        private DateViewInfo _ViewInfo;
        private DatePainter _Painter;

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DateViewInfo DateViewInfo
        {
            get { return _ViewInfo; }
        }

        public DatePainter DatePainter
        {
            get { return _Painter; }
        }

        public DateTime Date
        {
            get { return _Date; }
            set
            {
                if (_Date != value)
                {
                    _Date = value;
                    Invalidate();
                }
            }
        }


        public DateInfoType SelectedDatePart
        {
            get { return _SelectedDatePart; }
            set {
                if (_SelectedDatePart != value)
                {
                    _SelectedDatePart = value;
                    Invalidate();
                }
            }
        }
        
        protected virtual DatePainter CreatePainter()
        {
            return new DatePainter();
        }

        protected virtual DateViewInfo CreateViewInfo()
        {
            return new DateViewInfo(this);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CalcViewInfo();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            DatePainter.Draw(e.Graphics, DateViewInfo);
        }

        internal void CalcViewInfo()
        {
            using (Graphics graphics = CreateGraphics())
            {
                MinimumSize = new Size(0, DateViewInfo.CalcMinHeight(graphics));
                DateViewInfo.CalcViewInfo(graphics);
            }
        }

        public string GetCurrentDayValue(DateInfoType datePart)
        {
            if (datePart == DateInfoType.Day)
                return Date.ToString("M");
            if (datePart == DateInfoType.Hour)
                return Date.Hour.ToString();
            return Date.Minute.ToString();
        }

        public string GetPreviousDayValue(DateInfoType datePart)
        {
            DateTime dt = GetPreviousDate(datePart);
            if (datePart == DateInfoType.Day)
                return dt.ToString("M");
            if (datePart == DateInfoType.Hour)
                return dt.Hour.ToString();
            return dt.Minute.ToString();
        }

        public string GetNextDayValue(DateInfoType datePart)
        {
            DateTime dt = GetNextDate(datePart);
            if (datePart == DateInfoType.Day)
                return dt.ToString("M");
            if (datePart == DateInfoType.Hour)
                return dt.Hour.ToString();
            return dt.Minute.ToString();
        }

        public DateTime GetPreviousDate(DateInfoType datePart)
        {
            if (datePart == DateInfoType.Day)
                return Date.AddDays(-1);
            if (datePart == DateInfoType.Hour)
                return Date.AddHours(-1);
            return Date.AddMinutes(-1);
        }

        public DateTime GetNextDate(DateInfoType datePart)
        {
            if (datePart == DateInfoType.Day)
                return Date.AddDays(1);
            if (datePart == DateInfoType.Hour)
                return Date.AddHours(1);
            return Date.AddMinutes(1);
        }

        public string GetDayValueByPosition(DateInfoType datePart, PositionType positionType)
        {
            if (positionType == PositionType.Top)
                return GetPreviousDayValue(datePart);
             if (positionType == PositionType.Middle)
                return GetCurrentDayValue(datePart);
            return GetNextDayValue(datePart);
        }


        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if(SelectedDatePart == DateInfoType.None) return;
            int selectedDatePartValue = (int)SelectedDatePart;
            if (e.KeyData == Keys.Up)
                Date = GetPreviousDate(SelectedDatePart);
            if (e.KeyData == Keys.Down)
                Date = GetNextDate(SelectedDatePart);
            if (e.KeyData == Keys.Left)
            {
                if(selectedDatePartValue > 1)
                  SelectedDatePart = (DateInfoType)((int)SelectedDatePart - 1);
            }
            if (e.KeyData == Keys.Right)
            {
                if (selectedDatePartValue < Enum.GetValues(typeof(DateInfoType)).Length - 1)
                    SelectedDatePart = (DateInfoType)((int)SelectedDatePart + 1);
            }
        }

        protected override bool IsInputKey(Keys keyData)
        {
            if (keyData == Keys.Up || keyData == Keys.Left || keyData == Keys.Right) return true; 
            return base.IsInputKey(keyData);
        }

        public DateHitInfo CalcHitInfo(Point point)
        {
            return DateViewInfo.CalcHitInfo(point);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Select();
            DateHitInfo hitInfo = CalcHitInfo(e.Location);
            SelectedDatePart = hitInfo.HitInfoType;
        }
    }
}