using Logic.Abstract;
using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Concrete
{
    public class EfGameRepository : IGameRepository
    {
        private EfWandShopContext context = new EfWandShopContext();
        public IEnumerable<Game> Games { get { return context.Games; } }

        public Game SaveGame(Game game)
        {
            if(game.GameId == 0)
            {
                context.Games.Add(game);
            }
            context.SaveChanges();
            return game;
        }
    }
}
