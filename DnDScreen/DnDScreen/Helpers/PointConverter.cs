using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace DnDScreen.Helpers
{
    static class PointConverter
    {
        [Obsolete ("Don't need a helper class to replace one line of code.", true)]
        public static void ConvertToWorld(Graphics g, ref PointF[] pointArray)
        {
            g.TransformPoints(CoordinateSpace.World, CoordinateSpace.Page, pointArray);
        }
    }
}
