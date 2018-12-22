using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Entities;
using Logic.Abstract;

namespace Logic.Concrete
{
    public class EfPlayerRoundRepository : IPlayerRoundRepository
    {
        private EfWandShopContext context = new EfWandShopContext();
        public IEnumerable<PlayerRound> PlayerRounds { get { return context.PlayerRounds; } }

        public void SavePlayerRound(PlayerRound playerRound)
        {
            if (playerRound.PlayerRoundId == 0)
            {
                context.PlayerRounds.Add(playerRound);
            }
            context.SaveChanges();
        }
    }
}
