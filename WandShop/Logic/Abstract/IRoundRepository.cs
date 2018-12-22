using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Abstract
{
    public interface IRoundRepository
    {
        IEnumerable<Round> Rounds { get; }
        void SaveRound(int gameId,Round round);
    }
}
