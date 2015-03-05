using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TableTop.WorldObjects.Cards;

namespace TableTop.Commands.Cards
{
    public abstract class CardCommand : Command
    {
        protected Card receivingCard
        {
            get
            {
                return (Card)receiver;
            }
        }

        public CardCommand(Card card) : base(card) { }

        public abstract void Execute();
    }
}
