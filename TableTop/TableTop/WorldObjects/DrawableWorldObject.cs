using TableTop.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace TableTop.WorldObjects
{
    abstract class DrawableWorldObject : WorldObject, IDrawable
    {
        bool isVisible = true;
        public bool Visible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        public DrawableWorldObject()
            : base()
        {
        }

        public DrawableWorldObject(PointF position)
            : base(position)
        {
        }

        public abstract void Draw(System.Drawing.Graphics g);
    }
}
