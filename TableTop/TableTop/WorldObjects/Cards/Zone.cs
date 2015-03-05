using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using TableTop.Interfaces;

namespace TableTop.WorldObjects.Cards
{
    class Zone : DrawableWorldObject
    {
        List<IZoneEntity> cards = new List<IZoneEntity>();
        private PointF sizeP = new PointF(500, 250);
        PointF[] arrPoints;

        public Zone()
            : base()
        {
            arrPoints = new PointF[]{ sizeP };
        }

        public String Name
        {
            get;
            set;
        }

        public void Add(IZoneEntity e)
        {
            if (e != null)
            {
                cards.Add(e);
            }
        }

        public override void Draw(System.Drawing.Graphics g)
        {
#if DEBUG
            Console.WriteLine("Zone");
#endif
            GraphicsState gState = g.Save();
            //Matrix scaleM = new Matrix();
            //scaleM.Scale(1, 1);

            //scaleM.TransformPoints(arrPoints);

            base.translationMatrix.Reset();
            base.translationMatrix.Translate(this.Position.X, this.Position.Y);
            g.MultiplyTransform(base.translationMatrix, MatrixOrder.Prepend);
            g.FillRectangle(Brushes.Blue, 0, 0, 500, 250);
            //g.FillRectangle(Brushes.Blue, 0, 0, 500, 250);

            foreach (IDrawable e in cards)
            {
                if (e.Visible)
                {
                    e.Draw(g);
                }
            }
            g.Restore(gState);
        }
    }
}
