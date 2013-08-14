using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using DnDScreen.WorldObjects;
using DnDScreen.WorldObjects.Grids;
using DnDScreen.WorldObjects.Grids.Cartesian;
using DnDScreen.Interfaces;

namespace DnDScreen
{
    class GameWorld
    {
        private List<WorldObject> gameObjects = new List<WorldObject>();
        private List<Grid> gameGrids = new List<Grid>();
        //private List<MouseHighlight> gameMouseHighlights = new List<MouseHighlight>();

        public GameWorld()
        {
        }

        public void Add(WorldObject obj)
        {
            if (obj == null)
            {
                return;
            }

            if (obj is Grid)
            {
                gameGrids.Add((Grid)obj);
            }
            //else if (obj is MouseHighlight)
            //{
            //    gameMouseHighlights.Add((MouseHighlight)obj);
            //}
            else
            {
                gameObjects.Add(obj);
            }
        }

        public void Draw(Graphics g)
        {
            foreach (IDrawable obj in gameGrids)
            {
                if (obj.Visible)
                {
                    obj.Draw(g);
                }
            }
            foreach (IDrawable obj in gameObjects)
            {
                if (obj.Visible)
                {
                    obj.Draw(g);
                }
            }
            foreach (Grid obj in gameGrids)
            {
                if (obj.EnableHighlight)
                {
                    obj.DrawHighlight(g);
                }
            }
        }

    }
}
