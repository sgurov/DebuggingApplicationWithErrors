using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;

namespace DebuggingApplication
{
    public class ReminderListBoxPainter
    {

        public ReminderListBoxPainter() { }
            
        public virtual void DrawItem(DrawItemEventArgs e, ReminderControl reminderControl)
        {
            ReminderListBox listBox = reminderControl.ReminderListBox;
            Note note = listBox.Items[e.Index] as Note;
            e.Graphics.FillRectangle(Brushes.CornflowerBlue, e.Bounds);
            e.Graphics.DrawRectangle(Pens.Indigo, e.Bounds);
            Rectangle noteRect = GetNoteTextRect(e, note, listBox.Font);
            e.Graphics.DrawString(note.Text, listBox.Font, Brushes.White, noteRect);
            string description = listBox.GetDescription(note);
            Rectangle descriptionRect = GetDescriptionRect(e, noteRect, description, listBox.DescriptionFont);
            e.Graphics.DrawString(description, listBox.DescriptionFont, Brushes.DarkOrange, descriptionRect);

            DrawNoteButton(e, reminderControl);
            if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                e.Graphics.DrawRectangle(Pens.White, e.Bounds);
        }

        protected Rectangle GetNoteTextRect(DrawItemEventArgs e, Note note, Font font)
        {
            int noteHeight = e.Graphics.MeasureString(note.Text, font, e.Bounds.Width).ToSize().Height;
            return new Rectangle(e.Bounds.X, e.Bounds.Y, e.Bounds.Width, noteHeight);
        }

        protected Rectangle GetDescriptionRect(DrawItemEventArgs e, Rectangle noteRect, string description, Font font)
        {
            int descriptionHeight = e.Graphics.MeasureString(description, font, e.Bounds.Width).ToSize().Height;
            return new Rectangle(noteRect.X, noteRect.Bottom, e.Bounds.Width, descriptionHeight);
        }

        protected virtual void DrawNoteButton(DrawItemEventArgs e, ReminderControl reminderControl)
        {
            Button noteButton = reminderControl.NoteButton;
            ReminderListBox listBox = reminderControl.ReminderListBox;
            if (e.Index < listBox.ButtonsInfo.Count)
            {
                ButtonViewInfo buttonViewInfo = listBox.ButtonsInfo[e.Index];
                Rectangle buttonRect = buttonViewInfo.Bounds;
                Rectangle textRect = buttonViewInfo.TextBounds;

                buttonViewInfo.OffsetBounds(e.Bounds.Location, buttonViewInfo, buttonRect, textRect);

                noteButton.ButtonPainter.Draw(e.Graphics, buttonViewInfo);

                buttonViewInfo.RestoreBounds(e.Bounds.Location, buttonViewInfo, buttonRect, textRect);
            }
        }
    }
}
