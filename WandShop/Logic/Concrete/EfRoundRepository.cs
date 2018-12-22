using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Abstract;
using Logic.Entities;

namespace Logic.Concrete
{
    public class EfRoundRepository : IRoundRepository
    {
        private EfWandShopContext context = new EfWandShopContext();
        public IEnumerable<Round> Rounds { get { return context.Rounds; } }

        public void SaveRound(Round round)
        {
            if (round.RoundId == 0)
            {
                context.Rounds.Add(round);
            }
            context.SaveChanges();
        }
    }
}
