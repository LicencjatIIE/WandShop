using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Entities
{
    public class PlayerPart
    {
        public int GameID;
        public int CurrentRound = 0;
        public Functions Functions = new Functions();
        public GameParams GameParams;
        public List<Round> Rounds = new List<Round>();

        public PlayerPart() {}
        public PlayerPart(GameParams gameParams,  Game game)
        {
            GameParams = gameParams;
            GameID = game.GameID;
            Functions = new Functions(GameParams);
        }

        //To pewnie przeniesie się do repozytorium
        public void NextRound()
        {
            FinishRound();
            Round nextRound = new Round()
            {
                RoundID = GameID /*+ Member.MemberID*/ + ":" + (CurrentRound + 1),
                Gold = Rounds[CurrentRound].Gold - Functions.CountAllExpenses(Rounds[CurrentRound]) + Functions.CountYourTruePoorProfit(Rounds[CurrentRound]),
                LoanRemaining = Rounds[CurrentRound].LoanRemaining - Rounds[CurrentRound].LoanPaid + Rounds[CurrentRound].LoanTaken,
                WoodReserves = Rounds[CurrentRound].WoodPurchased - Functions.GetWoodUse(Rounds[CurrentRound]) + Rounds[CurrentRound].WoodReserves,
                CrystalReserves = Rounds[CurrentRound].CrystalPurchased - Functions.GetCrystalUse(Rounds[CurrentRound]) + Rounds[CurrentRound].CrystalReserves,
                WoodPurchased = 0,
                CrystalPurchased = 0,
                WoodAverage = 0,
                CrystalAverage = 0,
                WoodAveragePrevious = Functions.GetAndSetAverageWoodPrice(Rounds[CurrentRound]),
                CrystalAveragePrevious = Functions.GetAndSetAverageCrystalPrice(Rounds[CurrentRound]),
                MachinesOwned = Rounds[CurrentRound].MachinesPurchased - Rounds[CurrentRound].MachinesSold + Rounds[CurrentRound].MachinesOwned,
                MachinesPurchased = 0,
                MachinesSold = 0,
                DwarfWorkers = Rounds[CurrentRound].DwarfWorkers - Rounds[CurrentRound].DwarfWorkersDismissed + Rounds[CurrentRound].DwarfWorkersEmployed,
                ElfWorkers = Rounds[CurrentRound].ElfWorkers - Rounds[CurrentRound].ElfWorkersDismissed + Rounds[CurrentRound].ElfWorkersEmployed,
                HumanWorkers = Rounds[CurrentRound].HumanWorkers - Rounds[CurrentRound].HumanWorkersDismissed + Rounds[CurrentRound].HumanWorkersEmployed,
                DwarfWorkersEmployed = 0,
                ElfWorkersEmployed = 0,
                HumanWorkersEmployed = 0,
                DwarfWorkersDismissed = 0,
                ElfWorkersDismissed = 0,
                HumanWorkersDismissed = 0,
                QualityExpense = 0,
                AdExpense = 0,
                QualityExpensePrevious = Functions.GetQualityFading(Rounds[CurrentRound]),
                AdExpensePrevious = Functions.GetAdFading(Rounds[CurrentRound]),
                LoanPaid = 0,
                LoanTaken = 0,
                WandsAmount = 0,
                WandPrice = 0,
                WandsSoldAmount = 0,
                Income = 0,
                RemainingWandsAmount = Functions.CountRemainingWandsAmount(Rounds[CurrentRound])
            };
            CurrentRound++;

            Rounds.Add(nextRound);
        }
        public void UpadateRound()
        {

        }
        public void FinishRound()
        {
            Rounds[CurrentRound].Income = Functions.CountIncome(Rounds[CurrentRound]);
        }
        public void SetStartingRound()
        {
            Round startingRound = new Round()
            {
                RoundID = GameID /*+ Member.MemberID*/ + ":" + CurrentRound,
                Gold = GameParams.OwnContribution + GameParams.Loan + GameParams.ForeignShares - GameParams.BuildingCost,
                LoanRemaining = GameParams.Loan,
                WoodReserves = 0,
                CrystalReserves = 0,
                WoodPurchased = 0,
                CrystalPurchased = 0,
                WoodAverage = 0,
                CrystalAverage = 0,
                WoodAveragePrevious = 0,
                CrystalAveragePrevious = 0,
                MachinesOwned = 0,
                MachinesPurchased = 0,
                MachinesSold = 0,
                DwarfWorkers = 0,
                ElfWorkers = 0,
                HumanWorkers = 0,
                DwarfWorkersEmployed = 0,
                ElfWorkersEmployed = 0,
                HumanWorkersEmployed = 0,
                DwarfWorkersDismissed = 0,
                ElfWorkersDismissed = 0,
                HumanWorkersDismissed = 0,
                QualityExpense = 0,
                AdExpense = 0,
                QualityExpensePrevious = 0,
                AdExpensePrevious = 0,
                LoanPaid = 0,
                LoanTaken = 0,
                WandsAmount = 0,
                WandPrice = 0,
                WandsSoldAmount = 0,
                Income = 0,
                RemainingWandsAmount = 0
            };
            Rounds.Add(startingRound);
        }
    }
}
