using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienLen_Client
{
    [Serializable]
    class Card
    {
        public int Rank, Suit;
        public string Information => Rank.ToString() + "_" + Suit.ToString();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Rank">lá bài</param>
        /// <param name="Suit">chất bài</param>
        public Card(int Rank, int Suit)
        {
            this.Rank = Rank;
            this.Suit = Suit;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="info">Card's information. ex 3_4</param>
        public Card(string info)
        {
            string[] cardInfo = info.Split('_');
            Rank = int.Parse(cardInfo[0]);
            Suit = int.Parse(cardInfo[1]);
        }

        public static bool operator >(Card card1, Card card2)
        {
            if (card1.Rank > card2.Rank || (card1.Rank == card2.Rank && card1.Suit > card2.Suit)) return true;
            else return false;
        }
        public static bool operator <(Card card1, Card card2)
        {
            if (card1.Rank < card2.Rank || (card1.Rank == card2.Rank && card1.Suit < card2.Suit)) return true;
            else return false;
        }
        public static bool operator ==(Card card1, Card card2)
        {
            return card1.Rank == card2.Rank && card1.Suit == card2.Suit;
        }
        public static bool operator !=(Card card1, Card card2)
        {
            return card1.Rank != card2.Rank || card1.Suit != card2.Suit;
        }
        public int CompareTo(Card card1)
        {
            if (this < card1)
                return -1;
            else
                return 1;
        }


        //
        //code_s below are for debug
        //
        public void Debug_Print()
        {
            Console.WriteLine(Rank + "_" + Suit);
        }
    }
}
