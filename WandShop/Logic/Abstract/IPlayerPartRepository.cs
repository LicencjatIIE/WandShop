﻿using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Abstract
{
    public interface IPlayerPartRepository
    {
        IEnumerable<PlayerPart> PlayerParts { get; }
        void SavePlayerPart(PlayerPart playerPart);
        IEnumerable<PlayerPart> AddPlayerParts(int gameId, int playersNumber);
    }
}