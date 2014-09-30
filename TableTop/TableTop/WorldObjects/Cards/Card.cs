using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using TableTop.Interfaces;

namespace TableTop.WorldObjects.Cards
{
    class Card : DrawableWorldObject, IZoneEntity
    {

        public String Name
        {
            get;
            set;
        }

        public override void Draw(System.Drawing.Graphics g)
        {
            g.DrawRectangle(Pens.Green, 10, 10, 50, 80);


#if CONSOLE
            Console.WriteLine(Name);
#endif
        }
    }
}
