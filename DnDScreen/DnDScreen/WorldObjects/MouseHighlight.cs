using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using DnDScreen.Interfaces;

namespace DnDScreen.WorldObjects
{
    class MouseHighlight : WorldObject, IDrawable
    {
        RectangleF mouseRectangle = new RectangleF();
        
        bool isVisible = true;
        public bool Visible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        public new virtual PointF Position
        {
            get
            {
                return HighlightRectangle.Location;
            }
            set
            {
                mouseRectangle.Location = value;
            }
        }

        public RectangleF HighlightRectangle
        {
            get { return mouseRectangle; }
            set { mouseRectangle = value; }
        }

        public MouseHighlight()
        {
        }

        public void Draw(System.Drawing.Graphics g)
        {
            GraphicsState gs = g.Save();

            g.FillRectangle(new SolidBrush(Color.FromArgb(127, Color.LightBlue)), HighlightRectangle);

            g.Restore(gs);
        }
    }
}
