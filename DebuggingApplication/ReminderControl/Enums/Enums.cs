using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DebuggingApplication
{
    public enum ButtonBorderStyle { NoBorder, Normal }
    public enum ButtonKind { HeaderAdd, HeaderBack, Note }
    public enum State { Normal, Hot, Pressed }
    public enum RepeatMode { None, EveryHour, EveryDay, EveryWeek, EveryMonth, EveryYear }
    public enum HitInfoType { None, HeaderAddButton, HeaderBackButton, HeaderText, Note, NoteButton }
    public enum ViewKind {View, Edit }
    public enum PositionType {Top, Middle, Bottom }
    public enum DateInfoType {None, Day, Hour, Minute }
    
}
