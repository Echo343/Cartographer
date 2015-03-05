using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TableTop.Interfaces;
using TableTop.WorldObjects;

namespace TableTop.Commands
{
    public abstract class Command : ICommand
    {
        protected WorldObject receiver;

        public Command(WorldObject receiver)
        {
            this.receiver = receiver;
        }

        public abstract void Execute();
    }
}
