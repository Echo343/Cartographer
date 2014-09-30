using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TableTop.WorldObjects;
using TableTop.WorldObjects.Cards;

namespace TableTop.Controllers
{
    public class CardTop : Controller
    {
        public CardTop()
        {
            view = new GameCanvas(gameWorld);
            view.Show();

            Circle c = new Circle();

            gameWorld.Add(c);

            Zone testZone = new Zone();
            testZone.Name = "Bob";
            Card testCard = new Card();
            testCard.Name = "Ace of Spades";
            testZone.Add(testCard);
            gameWorld.Add(testZone);
        }
    }
}
