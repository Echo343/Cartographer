using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DnDScreen.WorldObjects.Grids
{
    class Grid : WorldGridObjectBase
    {
        float gridSize = 100f;

        public float GridSize
        {
            get { return gridSize; }
            set 
            {
                if (value > 0f)
                {
                    gridSize = value;
                }
            }
        }

        bool isVisible = true;
        public new bool Visible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        Pen gridPen = Pens.DarkCyan;

        public Grid(GameCanvas canvas)
            : base(canvas)
        {
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            GraphicsState gs = g.Save();

            PointF[] screenBounds = this.getScreenBoundsInWorldCoordinates(g);
            float left = screenBounds[0].X;
            float top = screenBounds[0].Y;
            float right = screenBounds[1].X;
            float bottom = screenBounds[1].Y;

            DrawVerticalLines(g, left, top, right, bottom);
            DrawHorizontalLines(g, left, top, right, bottom);

            g.Restore(gs);
        }

        private void DrawHorizontalLines(Graphics g, float left, float top, float right, float bottom)
        {
            float current, first = 0;
            
            first = (float)Math.Ceiling(top / gridSize) * gridSize;
            current = first;
            while (current <= bottom)
            {
                g.DrawLine(gridPen, left, current, right, current);
#if DEBUG
                g.FillEllipse(Brushes.Maroon, left, current, 10, 10);
                g.FillEllipse(Brushes.Maroon, right, current, -10, -10);
#endif
                current += gridSize;
                //TODO make option to toggle this
                if (current > -gridSize && current < gridSize) current += gridSize;
            }
        }

        private void DrawVerticalLines(System.Drawing.Graphics g, float left, float top, float right, float bottom)
        {
            float current, first = 0;

            first = (float)Math.Ceiling(left / gridSize) * gridSize;
            current = first;
            while (current <= right)
            {
                g.DrawLine(gridPen, current, top, current, bottom);
#if DEBUG
                g.FillEllipse(Brushes.Maroon, current, top, 10, 10);
                g.FillEllipse(Brushes.Maroon, current, bottom, -10, -10);
#endif
                current += gridSize;
                //TODO make option to toggle this
                if (current > -gridSize && current < gridSize) current += gridSize;
            }
        }
    }
}
