using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Abstract
{
    public interface IPlayerRepository
    {

        IEnumerable<Player> Players { get; }
        void SavePlayer(int playerPartId, Player player);
        Player DeletePlayer(int playerId);
    }
}
