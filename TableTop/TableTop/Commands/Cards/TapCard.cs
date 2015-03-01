using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TableTop.WorldObjects.Cards;

namespace TableTop.Commands.Cards
{
    public class TapCard : CardCommand
    {
        public TapCard(Card card) : base(card)
        {
        }

        public override void Execute()
        {
            receivingCard.Tap();
        }
    }
}
