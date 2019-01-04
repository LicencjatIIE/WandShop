using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Entities
{
    public class Archives
    {
        public int ArchivesId { get; set; }
        public List<double> Scores { get; set; } = new List<double>();

        public void CountScores(PlayerPart pP)
        {
            double innitialScore = 1.0;
            for(int i = 0; i < pP.Game.MaxRounds; i++)
            {
                innitialScore = innitialScore + CountSingleScore(pP, i);
                Scores.Add(innitialScore);
            }
        }
        public double CountSingleScore(PlayerPart pP, int round)
        {
            double startingGold = pP.Game.OwnContribution + pP.Game.ForeignShares + pP.Game.Loan;
            return (pP.CountIncome(pP.PlayerRounds[round], round) - pP.CountAllExpenses(pP.PlayerRounds[round], round)) / startingGold;
        }
    }
}
