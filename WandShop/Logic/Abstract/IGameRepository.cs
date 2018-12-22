using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Abstract
{
    public interface IGameRepository
    {
        IEnumerable<Game> Games { get; }
        Game SaveGame(Game game);
        Game DeleteGame(int gameId);

    }
}
