using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DnDScreen.WorldObjects.Grids.Cartesian
{
    class CartesianAxes : Grid
    {
        Pen axisPen = new Pen(Color.Black, 2f);

        bool isVisible = true;
        public new bool Visible
        {
            get { return isVisible; }
            set { isVisible = value; }
        }

        public CartesianAxes(GameCanvas canvas) //TODO pass in a camera object instead of the canvas, or make a camera interface
            : base(canvas)
        {
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            GraphicsState gs = g.Save();

            PointF[] transformedPoints = this.getScreenBoundsInWorldCoordinates(g);

            g.DrawLine(axisPen, new PointF(base.Position.X, transformedPoints[0].Y), new PointF(base.Position.X, transformedPoints[1].Y));
            g.DrawLine(axisPen, new PointF(transformedPoints[0].X, base.Position.Y), new PointF(transformedPoints[1].X, base.Position.Y));
#if DEBUG
            g.FillEllipse(Brushes.Maroon, base.Position.X, transformedPoints[0].Y, 10, 10);
            g.FillEllipse(Brushes.Maroon, base.Position.X, transformedPoints[1].Y, -10, -10);
            g.FillEllipse(Brushes.Maroon, transformedPoints[0].X, base.Position.Y, 10, 10);
            g.FillEllipse(Brushes.Maroon, transformedPoints[1].X, base.Position.Y, -10, -10);
#endif
            g.Restore(gs);
        }

        public override void DrawHighlight(Graphics g)
        {
            return;
        }
    }
}
