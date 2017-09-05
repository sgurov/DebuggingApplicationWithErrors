using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.ComponentModel;

namespace DebuggingApplication
{
    

    public class Note
    {

        public Note(){ }

        // Fields...
        private RepeatMode _RepeatMode;
        private DateTime _Date;
        private string _Text;

        public string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
            }
        }

        public DateTime Date
        {
            get { return _Date; }
            set
            {
                _Date = value;
            }
        }

        public RepeatMode RepeatMode
        {
            get { return _RepeatMode; }
            set { _RepeatMode = value; }
        }
        
    }
}