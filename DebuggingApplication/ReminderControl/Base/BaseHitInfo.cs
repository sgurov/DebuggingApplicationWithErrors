using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace DebuggingApplication
{
    public class BaseHitInfo
    {
        public BaseHitInfo() { }

        // Fields...

        private Point _Point;

        public Point Point
        {
            get { return _Point; }
            set { _Point = value; }
        }
    }
}