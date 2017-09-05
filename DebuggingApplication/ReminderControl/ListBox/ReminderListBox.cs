using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace DebuggingApplication
{
    public class ReminderListBox : ListBox
    {
        public ReminderListBox(ReminderControl owner)
            : base()
        {
            DoubleBuffered = true;
            _Owner = owner;
            _ItemIndent = 15;
            _NotePainter = CreatePainter();
            _ButtonsInfo = new List<ButtonViewInfo>();
        }

        // Fields...
        private ReminderListBoxPainter _NotePainter;
        private ReminderControl _Owner;
        private int _ItemIndent;
        private Font _DescriptionFont;
        private List< ButtonViewInfo> _ButtonsInfo;
        private int _HotTrackNoteIndex;

        public Font DescriptionFont
        {
            get { return _DescriptionFont; }
            set { _DescriptionFont = value; }
        }

        public int ItemIndent
        {
            get { return _ItemIndent; }
            set
            {
                _ItemIndent = value;
            }
        }

        public ReminderControl Owner
        {
            get { return _Owner; }
            set { _Owner = value; }
        }

        protected Button NoteButton
        {
            get { return Owner.NoteButton; }
        }


        public ReminderListBoxPainter NotePainter
        {
            get { return _NotePainter; }
        }

        protected internal List< ButtonViewInfo> ButtonsInfo
        {
            get { return _ButtonsInfo; }
        }

        protected virtual ReminderListBoxPainter CreatePainter()
        {
            return new ReminderListBoxPainter();
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            base.OnDrawItem(e);
            if (e.Index != -1)
                NotePainter.DrawItem(e, Owner);
        }

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            base.OnMeasureItem(e);
            if (e.Index == -1) return;
            Note note = Items[e.Index] as Note;
            int textHeight = GetTextHeight(e.Graphics, ClientRectangle.Width, note.Text, Font);
            string description = GetDescription(note);
            int descriptionHeight = e.Graphics.MeasureString(description, DescriptionFont, e.ItemWidth).ToSize().Height;  
            e.ItemHeight = textHeight + descriptionHeight + ItemIndent;
        }

        private int GetTextHeight(Graphics graphics, int width, string text, Font font)
        {
            return graphics.MeasureString(text, font, width).ToSize().Height;
        }
     
        public string GetDescription(Note note)
        {
            return string.Format("{0}, {1}", note.Date, note.RepeatMode);
        }

        public void CalcButton(Graphics graphics)
        {
            NoteButton.CalcViewInfo(graphics, ClientRectangle);
            ButtonsInfo.Clear();
            for (int i = 0; i < Items.Count; i++)
            {
                ButtonViewInfo viewInfo = NoteButton.ButtonViewInfo.Clone();
                ButtonsInfo.Add(viewInfo);
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            DescriptionFont = new Font("Calibry", Font.Size / 3 * 2);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Owner.OnMouseDownInternal(new MouseEventArgs(e.Button, e.Clicks, e.X, e.Y + Bounds.Y, e.Delta));
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            Owner.OnMouseMoveInternal(new MouseEventArgs(e.Button, e.Clicks, e.X, e.Y + Bounds.Y, e.Delta));         
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            Owner.OnMouseUpInternal(new MouseEventArgs(e.Button, e.Clicks, e.X, e.Y + Bounds.Y, e.Delta));
        }
    }
}