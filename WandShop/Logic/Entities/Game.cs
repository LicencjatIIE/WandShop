using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Entities
{
    public class Game
    {
        public int GameId { get; set; }
        public int MaxRounds { get; set; }
        public int CurrentRound { get; set; }

        //For EF
        public virtual List<PlayerPart> PlayersPart { get; private set; }


        public Game() { }
        public Game(int maxRound)
        {
            MaxRounds = maxRound;
        }


        public void AddPlayers()
        {

        }
        public void StartGame()
        {

        }
        public void NextRound()
        {

        }
    }
}
