using Logic.Entities;
using Logic.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Concrete
{
    public class EfPlayerPartRepository : IPlayerPartRepository
    {
        private EfWandShopContext context = new EfWandShopContext();
        public IEnumerable<PlayerPart> PlayerParts { get { return context.PlayerParts; } }

        public void SavePlayerPart(PlayerPart playerPart)
        {
            if (playerPart.PlayerPartId == 0)
            {
                context.PlayerParts.Add(playerPart);
            }
            context.SaveChanges();
        }

        public IEnumerable<PlayerPart> AddPlayerParts(int gameId, int playersNumber)
        {
            Game model = new Game();
            List<PlayerPart> list = new List<PlayerPart>();
            if (gameId != 0)
            {
                model = context.Games.Find(gameId);

                for (int i = 0; i < playersNumber; i++)
                {
                    PlayerPart p = new PlayerPart()
                    {
                        CurrentRound = model.CurrentRound,
                        GameId = model.GameId,
                        Game = model
                    };
                    SavePlayerPart(p);
                    list.Add(p);
                }
            }
            return list;
        }
    }
}
