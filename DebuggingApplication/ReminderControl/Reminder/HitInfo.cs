using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace DebuggingApplication
{

    public class ReminderHitInfo : BaseHitInfo
    {
        public ReminderHitInfo() : base() { }

        // Fields...
        private int _NoteIndex;
        private Note _Note;
        private HitInfoType _HitInfoType;

        public HitInfoType HitInfoType
        {
            get { return _HitInfoType; }
            set { _HitInfoType = value; }
        }

        public Note Note
        {
            get { return _Note; }
            set { _Note = value; }
        }


        public int NoteIndex
        {
            get { return _NoteIndex; }
            set
            {
                _NoteIndex = value;
            }
        }
    }
}
