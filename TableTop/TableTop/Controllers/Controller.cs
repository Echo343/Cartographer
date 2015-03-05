using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TableTop.Controllers
{
    public abstract class Controller
    {
        public GameCanvas view;
        protected GameWorld gameWorld = new GameWorld();
    }
}
