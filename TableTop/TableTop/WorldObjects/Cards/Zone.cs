using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TableTop.Interfaces;

namespace TableTop.WorldObjects.Cards
{
    class Zone : DrawableWorldObject
    {
        List<IZoneEntity> cards = new List<IZoneEntity>();

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
            g.FillRectangle(Brushes.Blue, 0, 0, 500, 250);

            foreach (IDrawable e in cards)
            {
                if (e.Visible)
                {
                    e.Draw(g);
                }
            }
        }
    }
}
