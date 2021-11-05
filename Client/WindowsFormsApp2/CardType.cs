using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TienLen_Client
{
    class CardType
    {
        public int type;
        public int count;
        public Card gr;
        public CardType(List<Card> cards)
        {
            if (cards.Count() == 0)
                this.type = -1;
            else if (!checkSame(cards) && !checkSanh(cards) && !checkDoiThong(cards))
            {
                this.type = 0;
                this.count = 0;
                this.gr = null;
            }

        }
        private bool checkSame(List<Card> cards)
        {
            int tmp = cards[0].Rank;
            foreach (Card card in cards)
            {
                if (card.Rank != tmp)
                    return false;
            }
            this.type = 1;
            this.count = cards.Count();
            this.gr = cards[cards.Count() - 1];
            return true;
        }
        private bool checkSanh(List<Card> cards)
        {
            if (cards.Count() == 1)
                return false;
            cards.Sort((x, y) => x.CompareTo(y));
            if (cards[cards.Count() - 1].Rank == 15 || cards.Count() < 3)
                return false;
            for (int i = 0; i < cards.Count() - 1; i++)
            {
                if (cards[i].Rank != (cards[i + 1].Rank - 1))
                    return false;
            }
            this.type = 2;
            this.count = cards.Count();
            this.gr = cards[cards.Count() - 1];
            return true;
        }
        private bool checkDoiThong(List<Card> cards)
        {
            if (cards.Count() == 1)
                return false;
            cards.Sort((x, y) => x.CompareTo(y));
            if (cards.Count() % 2 != 0 || cards.Count() < 6 || cards[cards.Count() - 1].Rank == 15)
                return false;

            for (int i = 0; i < cards.Count() - 2; i += 2)
            {
                if (cards[i].Rank != (cards[i + 2].Rank - 1) && (cards[i].Rank != cards[i + 1].Rank))
                    return false;
            }
            this.type = 3;
            this.count = cards.Count() / 2;
            this.gr = cards[cards.Count() - 1];
            return true;
        }
        /// <summary>
        /// Nếu hợp lí thì được đánh
        /// </summary>
        /// <param name="cardType2"></param>
        /// <returns></returns>
        public bool isHopLy(CardType cardType2)
        {
            if (this.type == 0)
                return false;
            if (cardType2.type == -1)
                return true;
            if (this.type != 0 && cardType2.type != 0)
            {
                if (this.type == cardType2.type && this.gr > cardType2.gr && this.count == cardType2.count)
                    return true;
            }
            if (this.type == 2) return false;

            if (cardType2.gr.Rank == 15 && this.gr.Rank != 15)
            {
                if (this.count > cardType2.count + 1 || (this.count == 4 && this.type == 3 && cardType2.count == 4 && cardType2.type == 1))
                    return true;
            }

            else if (cardType2.type == 3 || (cardType2.type==1&& cardType2.count == 4))
            {
                if (this.count > cardType2.count || (this.count == 4 && this.type == 3 && cardType2.count == 4 && cardType2.type == 1))
                    return true;
            }
            return false;
        }
        //debug
        public void print()
        {
            string str = type + " " + count + " " + (gr == null ? "null" : gr.Information);
            Console.WriteLine(str);
        }
    }
}
