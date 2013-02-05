using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using DnDScreen.Interfaces;

namespace DnDScreen.WorldObjects.Grids
{
    abstract class WorldGridObjectBase : WorldObject, IDrawable
    {
        private GameCanvas canvas;

        protected GameCanvas Canvas
        {
            get { return canvas; }
            set { canvas = value; }
        }

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

        public WorldGridObjectBase(GameCanvas canvas)
        {
            Canvas = canvas;
        }

        public abstract void Draw(Graphics g);
    }
}
