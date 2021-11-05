using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TienLen_Server
{
    class Dem
    {
        public List<Card>[] array = new List<Card>[13];
        
        public Dem(List<Card> cards)
        {
            for (int i = 0; i < 13; i++)
                array[i] = null;
            foreach(Card card in cards)
            {
                if (array[card.Rank - 3] == null)               
                    array[card.Rank - 3] = new List<Card>();           
                array[card.Rank - 3].Add(card);
            }
        }
    }
}
