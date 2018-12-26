using Logic.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Entities;

namespace Logic.Concrete
{
    public class EfPlayerRepository : IPlayerRepository
    {
        private EfWandShopContext context = new EfWandShopContext();
        public IEnumerable<Player> Players { get { return context.Players; } }

        public Player DeletePlayer(int playerId)
        {
            Player p = context.Players.Find(playerId);
            if (p != null)
            {
                context.Players.Remove(p);
                context.SaveChanges();
            }
            return p;
        }

        public void SavePlayer(int playerPartId, Player player)
        {
            if (player.PlayerId == 0)
            {
                PlayerPart pp = context.PlayerParts.Find(playerPartId);
                Player p = new Player()
                {
                    PlayerId = pp.PlayerPartId,
                    PlayerPart = pp
                };
                context.Players.Add(p);
            }
            context.SaveChanges();
        }
    }
}
