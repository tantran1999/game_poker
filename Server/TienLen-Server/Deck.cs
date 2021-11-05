using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TienLen_Server
{
    class Deck
    {
        public List<Card> Cards;

        public Deck()
        {
            Cards = new List<Card>();
            for (int la = 3; la < 16; la++)
                for (int chat = 1; chat < 5; chat++)
                    Cards.Add(new Card(la, chat));
        }

        /// <summary>
        /// xáo bài
        /// </summary>
        public void Shuffle()
        {
            Random r = new Random();
            int randomIndex = 0;
            for (int i=0;i<52;i++)
            {
                randomIndex = r.Next(0, 52-i); //chọn một lá bài ngẫu nhiên trong những lá chưa được xáo

                //đưa lá bài được chọn về cuối bộ bài
                Cards.Add(Cards[randomIndex]);
                Cards.RemoveAt(randomIndex);
            }
        }

        //
        //code_s below are for debug
        //
        public void Debug_Print()
        {
            foreach (Card card in Cards)
            {
                card.Debug_Print();
            }
        }
    }
}
