using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TableTop.WorldObjects.Cards;

namespace TableTop.Commands.Cards
{
    class UntapCard : CardCommand
    {
        public UntapCard(Card card) : base(card)
        {
        }

        public override void Execute()
        {
            receivingCard.Untap();
        }
    }
}
