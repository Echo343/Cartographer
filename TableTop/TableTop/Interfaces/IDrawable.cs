using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace TableTop.Interfaces
{
    interface IDrawable
    {
        bool Visible { get; }

        void Draw(Graphics g);
    }
}
