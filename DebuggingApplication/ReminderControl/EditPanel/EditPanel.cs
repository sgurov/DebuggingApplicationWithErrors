using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DebuggingApplication
{
    public class EditPanel : Panel
    {

        public EditPanel(ReminderControl reminderControl) : base() {
            _Owner = reminderControl;
            _TextBox = CreateTextBox();
            _DateControl = CreateDateControl();
        }

        // Fields...
        private Note _Note;
        private DateControl _DateControl;
        private TextBox _TextBox;
        private ReminderControl _Owner;

        public ReminderControl Owner
        {
            get { return _Owner; }
        }

        public TextBox TextBox
        {
            get { return _TextBox; }
        }


        public DateControl DateControl
        {
            get { return _DateControl; }
        }


        public Note Note
        {
            get { return _Note; }
            set {
                if (_Note != value)
                {
                    _Note = value;
                    TextBox.DataBindings.Clear();
                    TextBox.DataBindings.Add("Text", _Note, "Text", false, DataSourceUpdateMode.OnPropertyChanged);
                    DateControl.DataBindings.Clear();
                    DateControl.DataBindings.Add("Date", _Note, "Date", false, DataSourceUpdateMode.OnPropertyChanged);
                }
            }
        }

        protected virtual TextBox CreateTextBox()
        {
            TextBox textBox = new TextBox();
            textBox.Parent = this;
            textBox.Dock = DockStyle.Top;
            return textBox;
        }

        protected virtual DateControl CreateDateControl()
        {
            DateControl dateControl = new DateControl();
            dateControl.Parent = this;
            dateControl.Dock = DockStyle.Top;
            return dateControl;
        }
    }
}
