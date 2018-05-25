using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Serveur.Models.BatailleModels
{
    public class CardComparator : Comparer<Card>
    {
        public override int Compare(Card x, Card y)
        {
            if (x.Value == y.Value)
            {
                return 0;
            }
            else
            {
                if(x.Value > y.Value)
                {
                    if (x.Value > 10 && y.Value == 1)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
                else
                {
                    if (y.Value > 10 && x.Value == 1)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
        }
    }
}
