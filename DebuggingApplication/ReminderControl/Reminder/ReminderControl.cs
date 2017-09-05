using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace DebuggingApplication
{
    public class ReminderControl : Control
    {

        public ReminderControl()
            : base()
        {
            DoubleBuffered = true;
            _ViewInfo = CreateViewInfo();
            _Painter = CreatePainter();
            _ReminderListBox = CreateListBox();
            _EditPanel = CreateEditPanel();
            HeaderText = "Reminders";
            BackButton.Visible = false;
        }

        public event ButtonClickEventHandler ButtonClick;

        public delegate void ButtonClickEventHandler(object sender, ButtonClickEventArgs e);

        protected virtual void RaiseButtonClick(System.Object sender, ButtonClickEventArgs ea)
        {
            ButtonClickEventHandler handler = ButtonClick;
            if (handler != null)
                handler(sender, ea);
        }

        // Fields...
        private EditPanel _EditPanel;
        private Button _BackButton;
        private ViewKind _View;
        private ButtonViewInfo _HotTrackButtonInfo;
        private Button _NoteButton;
        private Notes _Notes;
        private ReminderViewInfo _ViewInfo;
        private ReminderPainter _Painter;
        private ReminderListBox _ReminderListBox;
        private string _HeaderText;
        private Button _HeaderButton;

        protected internal ReminderListBox ReminderListBox
        {
            get { return _ReminderListBox; }
        }


        protected internal EditPanel EditPanel
        {
            get { return _EditPanel; }
        }

        protected ReminderPainter Painter
        {
            get { return _Painter; }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public ReminderViewInfo ViewInfo
        {
            get { return _ViewInfo; }
        }

        internal protected string HeaderText
        {
            get
            {
                return _HeaderText;
            }
            set
            {
                if (_HeaderText != value)
                {
                    _HeaderText = value;
                    CalcViewInfo();
                }
            }
        }

        public Notes Notes
        {
            get
            {
                if (_Notes == null)
                    _Notes = CreateNotes();
                return _Notes;
            }
        }

        public Button HeaderButton
        {
            get
            {
                if (_HeaderButton == null)
                    _HeaderButton = CreateButton("+", ButtonKind.HeaderAdd, ButtonBorderStyle.Normal);
                return _HeaderButton;
            }
        }


        public Button NoteButton
        {
            get
            {
                if (_NoteButton == null)
                    _NoteButton = CreateButton(">", ButtonKind.Note, ButtonBorderStyle.Normal);
                return _NoteButton;
            }
        }


        public Button BackButton
        {
            get
            {
                if (_BackButton == null)
                    _BackButton = CreateButton("<-", ButtonKind.HeaderBack, ButtonBorderStyle.Normal);
                return _BackButton;
            }
        }


        public ViewKind View
        {
            get { return _View; }
            set {
                if (_View != value)
                {
                    _View = value;
                    if (_View == ViewKind.View)
                    {
                        ReminderListBox.Visible = true;
                        EditPanel.Visible = false;
                        HeaderText = "Reminders";
                        HeaderButton.Visible = true;
                        BackButton.Visible = false;
                    }
                    else
                    {
                        ReminderListBox.Visible = false;
                        EditPanel.Note = ReminderListBox.SelectedItem as Note;
                        EditPanel.Visible = true;
                        HeaderText = "Edit";
                        HeaderButton.Visible = false;
                        BackButton.Visible = true;
                    }
                    Invalidate(ViewInfo.HeaderBounds);
                }
            }
        }

        protected ButtonViewInfo HotTrackButtonInfo
        {
            get { return _HotTrackButtonInfo; }
            set
            {
                if (_HotTrackButtonInfo != value)
                {
                    if (_HotTrackButtonInfo != null)
                    {
                        _HotTrackButtonInfo.ButtonState = State.Normal;
                        InvalidateButtons(_HotTrackButtonInfo);
                    }
                    _HotTrackButtonInfo = value;
                    if(value != null)
                       InvalidateButtons(value);
                }
            }
        }

        private void InvalidateButtons(ButtonViewInfo buttonViewInfo)
        {
            if (buttonViewInfo.Owner.Kind == ButtonKind.HeaderAdd)
                Invalidate(ViewInfo.HeaderBounds);
            else
            {
                int index = ReminderListBox.ButtonsInfo.IndexOf(buttonViewInfo);
                if (index == -1) return;
                Rectangle itemRect = ReminderListBox.GetItemRectangle(index);
                ReminderListBox.Invalidate(itemRect);
            }
        }

        protected virtual Button CreateButton(string text, ButtonKind kind, ButtonBorderStyle style)
        {
            return new Button(text, kind, style);
        }

        protected virtual ReminderPainter CreatePainter()
        {
            return new ReminderPainter();
        }

        protected virtual ReminderViewInfo CreateViewInfo()
        {
            return new ReminderViewInfo(this);
        }

        protected virtual ReminderListBox CreateListBox()
        {
            ReminderListBox listBox = new ReminderListBox(this);
            listBox.DataSource = Notes;
            listBox.DrawMode = DrawMode.OwnerDrawVariable;
            listBox.Parent = this;
            return listBox;
        }

        protected virtual EditPanel CreateEditPanel()
        {
            EditPanel panel = new EditPanel(this);
            panel.Parent = this;
            panel.Visible = false;
            return panel;
        }

        protected virtual Notes CreateNotes()
        {
            Notes notes = new Notes();
            notes.ListChanged += OnNotesChanges;
            return notes;
        }

        void OnNotesChanges(object sender, ListChangedEventArgs e)
        {
            BeginInvoke(new Action(() =>
            {
                CalcViewInfo();
            }));
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CalcViewInfo();
        }

        internal void CalcViewInfo()
        {
            using (Graphics graphics = CreateGraphics())
            {
                ViewInfo.CalcViewInfo(graphics);
                ReminderListBox.Bounds = ViewInfo.ItemsRectangle;
                ReminderListBox.CalcButton(graphics);
                EditPanel.Bounds = ViewInfo.ItemsRectangle;
            }
            MinimumSize = new Size(ViewInfo.HeaderTextBounds.Width + HeaderButton.ButtonViewInfo.Bounds.Width, 0);
            ReminderListBox.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Painter.Draw(e.Graphics, ViewInfo);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            ReminderHitInfo hitInfo = CalcHitInfo(e.Location);
            UpdateButton(hitInfo, State.Hot);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            ReminderHitInfo hitInfo = CalcHitInfo(e.Location);
            HotTrackButtonInfo = null;
            UpdateButton(hitInfo, State.Pressed);
            if (HotTrackButtonInfo != null)
            {
                ButtonClickEventArgs eventArgs = new ButtonClickEventArgs() { Handled = false, Button = HotTrackButtonInfo.Owner };
                RaiseButtonClick(this, eventArgs);
                if (!eventArgs.Handled)
                {
                    if (eventArgs.Button.Kind == ButtonKind.HeaderAdd)
                    {
                        Notes.AddNote("New Note", DateTime.Now, RepeatMode.EveryDay);
                        ReminderListBox.SelectedIndex = ReminderListBox.Items.Count - 1;
                    }
                    else if (eventArgs.Button.Kind == ButtonKind.Note)
                        View = ViewKind.Edit;
                    else
                        View = ViewKind.View;
                }
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            ReminderHitInfo hitInfo = CalcHitInfo(e.Location);
            HotTrackButtonInfo = null;
            UpdateButton(hitInfo, State.Normal);
        }

        internal void OnMouseDownInternal(MouseEventArgs e)
        {
            this.OnMouseDown(e);
        }

        internal void OnMouseMoveInternal(MouseEventArgs e)
        {
            this.OnMouseMove(e);
        }

        internal void OnMouseUpInternal(MouseEventArgs e)
        {
            this.OnMouseUp(e);
        }

        public ReminderHitInfo CalcHitInfo(Point point)
        {
            return ViewInfo.CalcHitInfo(point);
        }

        private void UpdateButton(ReminderHitInfo hitInfo, State state)
        {
            if (hitInfo.HitInfoType == HitInfoType.HeaderAddButton)
            {
                HeaderButton.ButtonViewInfo.ButtonState = state;
                HotTrackButtonInfo = HeaderButton.ButtonViewInfo;
            }
            else if(hitInfo.HitInfoType == HitInfoType.HeaderBackButton){
                BackButton.ButtonViewInfo.ButtonState = state;
                HotTrackButtonInfo = BackButton.ButtonViewInfo;
            }
            else if (hitInfo.HitInfoType == HitInfoType.NoteButton)
            {
                ButtonViewInfo buttonViewInfo = ReminderListBox.ButtonsInfo[hitInfo.NoteIndex];
                buttonViewInfo.ButtonState = state;
                HotTrackButtonInfo = buttonViewInfo;
            }
            else
                HotTrackButtonInfo = null;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            CalcViewInfo();
        }
    }
}