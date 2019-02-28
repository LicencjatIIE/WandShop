using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Entities;
using Web.Models;

namespace Web.Helpers
{
    public static class Validation
    {
        public static List<string> PlayerPlayRoundValidation(PlayerPlayRoundModel pR, PlayerPart pP)
        {
            List<string> validationErrors = new List<string>();

            if (pR.WoodReserves < pP.Game.WoodConsumption * pR.WandsProducedAmount)
                validationErrors.Add("WoodReserves");
            if (pR.CrystalReserves < pP.Game.CrystalConsumption * pR.WandsProducedAmount)
                validationErrors.Add("CrystalReserves");
            if (pR.WandsProducedAmount > pP.Game.Rounds[pP.CurrentRound].CrystalEfficiency)
                validationErrors.Add("MachinesOwn");

            if (pR.WandsProducedAmount > pR.ElfWorkers * pP.Game.Rounds[pP.CurrentRound].HoursPerPeriod)
                validationErrors.Add("ElfWorkers");
            if (pR.WandsProducedAmount > pR.DwarfWorkers * pP.Game.Rounds[pP.CurrentRound].HoursPerPeriod)
                validationErrors.Add("DwarfWorkers");
            if (pR.WandsProducedAmount > pR.HumanWorkers * pP.Game.Rounds[pP.CurrentRound].HoursPerPeriod)
                validationErrors.Add("HumanWorkers");

            if (pR.ElfWorkersDismissed > pR.ElfWorkers)
                validationErrors.Add("ElfWorkersDismissed");
            if (pR.DwarfWorkersDismissed > pR.DwarfWorkers)
                validationErrors.Add("DwarfWorkersDismissed");
            if (pR.HumanWorkersDismissed > pR.HumanWorkers)
                validationErrors.Add("HumanWorkersDismissed");

            if (pR.LoanPaid > pR.LoanRemaining)
                validationErrors.Add("LoanPaid");
            if (pR.LoanTaken + pR.LoanRemaining > pP.Game.MaxLoan)
                validationErrors.Add("LoanTaken");

            return validationErrors;
        }     
        public static double TotalExpansesForThisRound(PlayerPart pP)
        {
            return pP.CountAllExpenses(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound);
        }
        public static double ForeseenExpansesForNextRound(PlayerPart nextRound, PlayerRound pR)
        {
            return  nextRound.GetWorkersPrice(pR, nextRound.CurrentRound)
                + nextRound.GetGeneralProcessingRateCost(pR, nextRound.CurrentRound)
                + nextRound.Game.Rounds[nextRound.CurrentRound].ManagementCosts
                + nextRound.GetLoanRateCost(pR, nextRound.CurrentRound);
        }
    }
}
