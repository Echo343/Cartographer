using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using TableTop.Interfaces;
using System.Drawing.Drawing2D;

namespace TableTop.WorldObjects
{
    public abstract class WorldObject
    {
        private PointF position;

        protected Matrix translationMatrix = new Matrix();

#region Properties

        public PointF Position
        {
            get { return position; }
            set { position = value; }
        }
#endregion

        public WorldObject()
        {
            InitWorldObject(new PointF(0f,0f));
        }

        public WorldObject(PointF position)
        {
            InitWorldObject(position);
        }

        /// <summary>
        /// Initializes the World Object
        /// </summary>
        /// <param name="position"></param>
        private void InitWorldObject(PointF position)
        {
            if (position == null)
            {
                this.Position = new PointF(0f, 0f);
            }
            else
            {
                this.Position = position;
            }
        }
    }
}
