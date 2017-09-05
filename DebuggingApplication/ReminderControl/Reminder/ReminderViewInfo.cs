using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace DebuggingApplication
{
    public class ReminderViewInfo
    {
        public ReminderViewInfo(ReminderControl owner)
        {
            _Owner = owner;
        }

        // Fields...
        private Rectangle _Bounds;
        private Rectangle _ItemsRectangle;
        private Rectangle _HeaderTextBounds;
        private Rectangle _HeaderBounds;
        private ReminderControl _Owner;

        public ReminderControl Owner
        {
            get { return _Owner; }
        }

        protected virtual Padding HeaderPaddings
        {
            get { return new Padding(3, 5, 5, 3); }
        }


        public Rectangle Bounds
        {
            get { return _Bounds; }
            set
            {
                _Bounds = value;
            }
        }


        public Rectangle HeaderTextBounds
        {
            get { return _HeaderTextBounds; }
            set { _HeaderTextBounds = value; }
        }

        public Rectangle HeaderBounds
        {
            get { return _HeaderBounds; }
            set { _HeaderBounds = value; }
        }


        public Rectangle ItemsRectangle
        {
            get { return _ItemsRectangle; }
            set
            {
                _ItemsRectangle = value;
            }
        }

        protected Button HeaderButton
        {
            get { return Owner.HeaderButton; }
        }

        protected Button NoteButton
        {
            get { return Owner.NoteButton; }
        }

        protected Button BackButton
        {
            get { return Owner.BackButton; }
        }


        protected ReminderListBox ReminderListBox
        {
            get { return Owner.ReminderListBox; }
        }

        protected internal virtual void CalcViewInfo(Graphics graphics)
        {
            CalcBounds();
            CalcHeader(graphics);
            CalcItems(graphics);
        }

        protected virtual void CalcHeader(Graphics graphics)
        {
            Size headerSize = graphics.MeasureString(Owner.HeaderText, Owner.Font).ToSize();
            HeaderTextBounds = new Rectangle(HeaderPaddings.Left,
                               HeaderPaddings.Top,
                               headerSize.Width + HeaderPaddings.Right,
                               headerSize.Height + HeaderPaddings.Bottom);
            CalcHeaderButtons(graphics);
            HeaderBounds = new Rectangle(0, 
                               0,
                               Math.Max(HeaderButton.ButtonViewInfo.Bounds.Right + HeaderButton.ButtonViewInfo.ButtonPaddings.Right, 
                               BackButton.ButtonViewInfo.Bounds.Right + BackButton.ButtonViewInfo.ButtonPaddings.Right),
                               Math.Max(HeaderTextBounds.Bottom, 
                               Math.Max(HeaderButton.ButtonViewInfo.Bounds.Bottom + HeaderButton.ButtonViewInfo.ButtonPaddings.Bottom, 
                               (BackButton.ButtonViewInfo.Bounds.Bottom + BackButton.ButtonViewInfo.ButtonPaddings.Bottom))));
        }

        protected virtual void CalcHeaderButtons(Graphics graphics)
        {
            HeaderButton.CalcViewInfo(graphics, Bounds);
            BackButton.CalcViewInfo(graphics, Bounds);
        }

        protected virtual void CalcItems(Graphics graphics)
        {
            int height = Owner.ReminderListBox.PreferredHeight;
            ItemsRectangle = new Rectangle(HeaderBounds.X + 1, HeaderBounds.Bottom, HeaderBounds.Width - 2, Bounds.Height - HeaderBounds.Height); // Bounds.Height
        }

   
        protected virtual void CalcBounds()
        {
            Bounds = Owner.ClientRectangle;
        }

        public ReminderHitInfo CalcHitInfo(Point point)
        {
            point = Owner.PointToClient(point);
            ReminderHitInfo hitInfo = new ReminderHitInfo() { HitInfoType = HitInfoType.None, Point = point };
            if (!ReminderListBox.Bounds.Contains(point))
            {
                if (HeaderTextBounds.Contains(point))
                   hitInfo = new ReminderHitInfo() { HitInfoType = HitInfoType.HeaderText, Point = point };
                if (HeaderButton.Visible && HeaderButton.ButtonViewInfo.Bounds.Contains(point))
                    hitInfo = new ReminderHitInfo() { HitInfoType = HitInfoType.HeaderAddButton, Point = point };
                if (BackButton.Visible && BackButton.ButtonViewInfo.Bounds.Contains(point))
                    hitInfo = new ReminderHitInfo() { HitInfoType = HitInfoType.HeaderBackButton, Point = point };
            }
            else
            {
                point.Offset(0, -ReminderListBox.Bounds.Y);
                int index = ReminderListBox.IndexFromPoint(point);
                if (index != -1)
                {
                    Rectangle itemRect = ReminderListBox.GetItemRectangle(index);
                    Point pt = point;
                    pt.Offset(itemRect.X, -itemRect.Y);
                    if (NoteButton.ButtonViewInfo.Bounds.Contains(pt))
                        hitInfo = new ReminderHitInfo() { HitInfoType = HitInfoType.NoteButton, Note = ReminderListBox.Items[index] as Note, Point = point, NoteIndex = index };
                    else
                        hitInfo = new ReminderHitInfo() { HitInfoType = HitInfoType.Note, Note = ReminderListBox.Items[index] as Note, Point = point, NoteIndex = index };
                }
            }
            return hitInfo;
        }
    }
}