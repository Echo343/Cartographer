using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using TableTop.Interfaces;

namespace TableTop.WorldObjects.Cards
{
    class Card : DrawableWorldObject, IZoneEntity
    {

        private PointF sizeP = new PointF(50, 100);
        PointF[] arrPoints;

        public Card()
            : base()
        {
            arrPoints = new PointF[] { sizeP };
            this.Position = new PointF(10, 10);
        }

        public String Name
        {
            get;
            set;
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            //g.DrawRectangle(Pens.Green, 10, 10, 50, 80);

            GraphicsState gState = g.Save();
            //Matrix scaleM = new Matrix();
            //scaleM.Scale(1, 1);

            //scaleM.TransformPoints(arrPoints);

            base.translationMatrix.Reset();
            base.translationMatrix.Translate(this.Position.X, this.Position.Y);
            g.MultiplyTransform(base.translationMatrix, MatrixOrder.Prepend);
            g.DrawRectangle(Pens.Green, 0, 0, 50, 100);
            //g.FillRectangle(Brushes.Blue, 0, 0, 500, 250);

            g.Restore(gState);


#if CONSOLE
            Console.WriteLine(Name);
#endif
        }
    }
}
