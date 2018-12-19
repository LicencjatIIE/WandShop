using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Entities
{
    public class Game
    {
        public int GameID { get; set; }
        public int MaxRounds { get; set; }
        public int CurrentRound { get; set; }
        public GameParams GameParams { get; set; }
        public List<PlayerPart> PlayersPart { get; private set; }

        public Game() { }
        public Game(int maxRound, GameParams gameParams)
        {
            MaxRounds = maxRound;
            GameParams = gameParams;
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
