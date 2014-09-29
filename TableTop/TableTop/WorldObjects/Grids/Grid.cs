using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using TableTop.Interfaces;

namespace TableTop.WorldObjects.Grids
{
    abstract class Grid : WorldObject, IDrawable
    {
        private GameCanvas canvas;

        protected GameCanvas Canvas
        {
            get { return canvas; }
            set { canvas = value; }
        }

        public bool EnableHighlight { get; set; }

        bool isVisible = true;
        public bool Visible
        {
            get { return isVisible; }
            protected set { isVisible = value; }
        }

        protected PointF[] getScreenBoundsInWorldCoordinates(Graphics g)
        {
            Rectangle screenBounds = canvas.ClientRectangle;
            PointF[] transformedPoints = new PointF[] { new PointF((float)screenBounds.X, (float)screenBounds.Y), new PointF((float)screenBounds.Width, (float)screenBounds.Height) };
            g.TransformPoints(CoordinateSpace.World, CoordinateSpace.Page, transformedPoints);
            return transformedPoints;
        }

        public Grid(GameCanvas canvas)
        {
            Canvas = canvas;
            EnableHighlight = false;
        }

        public abstract void Draw(Graphics g);

        public abstract void DrawHighlight(Graphics g);
    }
}
