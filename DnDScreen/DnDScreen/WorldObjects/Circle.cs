using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using DnDScreen.Interfaces;

namespace DnDScreen.WorldObjects
{
    class Circle : WorldObject, IDrawable
    {
        private float radius = 50;

        bool isVisible = true;
        public bool Visible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        public Circle() : base()
        {
        }

        public Circle(PointF position)
            : base(position)
        {
        }

        public Circle(PointF position, float radius)
            : base(position)
        {
            this.radius = radius;
            if (radius <= 1)
            {
                this.radius = 1;
            }
        }

        public void Draw(System.Drawing.Graphics g)
        {
            g.DrawEllipse(Pens.Blue, base.Position.X, base.Position.Y, radius * 2, radius * 2);
        }
    }
}
