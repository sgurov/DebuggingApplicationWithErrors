using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace DebuggingApplication
{
    public class Notes : BindingList<Note>
    {

        public Notes(){ }

        public void AddNote(string text, DateTime date, RepeatMode repeatMode)
        {
            Add(new Note() {Text =  text, Date = date, RepeatMode = repeatMode});
        }
    }
}
