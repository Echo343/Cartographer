using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using TableTop.Interfaces;
using System.Windows.Forms;

namespace TableTop.WorldObjects.Grids.Cartesian
{
    class MouseHighlight : WorldObject, IDrawable
    {
        RectangleF mouseRectangle = new RectangleF();
        Cartesian grid;
        GameCanvas canvas;

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

        public MouseHighlight(GameCanvas canvas, Cartesian grid)
        {
            this.grid = grid;
            this.canvas = canvas;
        }

        public void Draw(System.Drawing.Graphics g)
        {
            GraphicsState gs = g.Save();

            g.FillRectangle(new SolidBrush(Color.FromArgb(127, Color.LightBlue)), HighlightRectangle);

            g.Restore(gs);
        }

        internal void drawingCanvas_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                Visible = false;
            }
            else
            {
                PointF[] ptArr = { new PointF(e.X, e.Y) };
                canvas.CreateGraphicsWithWorldTransform().TransformPoints(CoordinateSpace.World, CoordinateSpace.Page, ptArr);
                //canvas.MouseLocation = ptArr[0];
                float gridSize = grid.GridSize;
                PointF pos = new PointF((float)Math.Floor(ptArr[0].X / gridSize) * gridSize, (float)Math.Floor(ptArr[0].Y / gridSize) * gridSize);
                RectangleF newMouseRectangle = new RectangleF(pos, new SizeF(gridSize, gridSize));
                if (!newMouseRectangle.Equals(HighlightRectangle))
                {
                    HighlightRectangle = newMouseRectangle;
                    ((PictureBox)sender).Invalidate();
                }
            }
        }

        internal void drawingCanvas_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!Visible)
            {
                Visible = true;
                ((PictureBox)sender).Invalidate();
            }
        }
    }
}
