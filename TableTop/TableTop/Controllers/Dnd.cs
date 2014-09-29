using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using TableTop.WorldObjects;
using TableTop.WorldObjects.Grids.Cartesian;

namespace TableTop.Controllers
{
    public class Dnd
    {
        GameWorld gameWorld = new GameWorld();
        Cartesian mainGrid;
        Random rnd = new Random(2);
        public GameCanvas view;

        public Dnd()
        {
            view = new GameCanvas(gameWorld);
            view.Show();
            //TODO why is the picturebox needed.
            mainGrid = new Cartesian(view, view.pictureBox);
            mainGrid.EnableHighlight = true;
            gameWorld.Add(mainGrid);
            //TODO have controls in mainGrid to control whether axes are drawn or not.
            gameWorld.Add(new CartesianAxes(view));
            for (int i = 1; i < 10; i++)
            {
                Circle c = new Circle(new PointF(rnd.Next(400), rnd.Next(400)), rnd.Next(390) + 10);
                //if (i > 1) c.Visible = false;
                gameWorld.Add(c);
            }
        }
    }
}
