using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Abstract
{
    public interface IPlayerRoundRepository
    {
        IEnumerable<PlayerRound> PlayerRounds { get; }
        PlayerRound SavePlayerRound(int playerPartID, PlayerRound playerRound);
    }
}
