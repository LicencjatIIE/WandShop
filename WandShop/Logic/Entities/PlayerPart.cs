using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Entities
{
    public class PlayerPart
    {
        public int PlayerPartId { get; set; }
        public int CurrentRound { get; set; } = 0;


        //For EF
        public virtual List<Round> Rounds { get; set; } = new List<Round>();
        public int GameId { get; set; } = 0;
        public virtual Game Game { get; set; }
        public int GameParamId { get; set; }
        public virtual GameParam GameParam { get; set; }


        public PlayerPart() {}
        public PlayerPart(GameParam gameParams)
        {
            GameParam = gameParams;
        }


        //To pewnie przeniesie się do repozytorium
        public void NextRound()
        {
            FinishRound();
            Round nextRound = new Round()
            {
                Gold = Rounds[CurrentRound].Gold - CountAllExpenses(Rounds[CurrentRound]) + CountYourTruePoorProfit(Rounds[CurrentRound]),
                LoanRemaining = GetLoanRemaining(Rounds[CurrentRound]),
                WoodReserves = Rounds[CurrentRound].WoodPurchased - GetWoodUse(Rounds[CurrentRound]) + Rounds[CurrentRound].WoodReserves,
                CrystalReserves = Rounds[CurrentRound].CrystalPurchased - GetCrystalUse(Rounds[CurrentRound]) + Rounds[CurrentRound].CrystalReserves,
                WoodPurchased = 0,
                CrystalPurchased = 0,
                WoodAverage = 0,
                CrystalAverage = 0,
                WoodAveragePrevious = GetAndSetAverageWoodPrice(Rounds[CurrentRound]),
                CrystalAveragePrevious = GetAndSetAverageCrystalPrice(Rounds[CurrentRound]),
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
                QualityExpensePrevious = GetQualityFading(Rounds[CurrentRound]),
                AdExpensePrevious = GetAdFading(Rounds[CurrentRound]),
                LoanPaid = 0,
                LoanTaken = 0,
                WandsAmount = 0,
                WandPrice = 0,
                WandsSoldAmount = 0,
                Income = 0,
                RemainingWandsAmount = CountRemainingWandsAmount(Rounds[CurrentRound])
            };
            CurrentRound++;

            Rounds.Add(nextRound);
        }
        public void FinishRound()
        {
            Rounds[CurrentRound].Income = CountIncome(Rounds[CurrentRound]);
        }
        public void SetStartingRound()
        {
            Round startingRound = new Round()
            {
                Gold = GameParam.OwnContribution + GameParam.Loan + GameParam.ForeignShares - GameParam.BuildingCost,
                LoanRemaining = GameParam.Loan,
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


        #region Functions
        public double CountResourcesExpense(Round round)
        {
            return GetTotalWoodPrice(round) + GetTotalCrystalPrice(round);
        }
        public double CountWorkersExpense(Round round)
        {
            return GetDismissedWorkerPrice(round) + GetEmployedWorkerPrice(round) + GetWorkersPrice(round);
        }
        public double CountMachineExpense(Round round)
        {
            return round.MachinesPurchased * GameParam.MachinePrice;
        }

        public double GetDismissedWorkerPrice(Round round)
        {
            return (round.DwarfWorkersDismissed + round.ElfWorkersDismissed + round.HumanWorkersDismissed) * GameParam.DismissalPrice;
        }
        public double GetEmployedWorkerPrice(Round round)
        {
            return (round.DwarfWorkersEmployed + round.ElfWorkersEmployed + round.HumanWorkersEmployed) * GameParam.EmploymentPrice;
        }
        public double GetWorkersPrice(Round round)
        {
            return GameParam.HoursPerPeriod * (round.DwarfWorkers * GameParam.WorkerDwarfPrice + round.ElfWorkers * GameParam.WorkerElfPrice + round.HumanWorkers * GameParam.WorkerHumanPrice);
        }

        public double GetWoodPrice(Round round)
        {
            if (round.WoodPurchased <= GameParam.WoodAmountLow)
                return GameParam.WoodPriceHigh;
            else if (round.WoodPurchased >= GameParam.WoodAmountHigh)
                return GameParam.WoodPriceLow;
            return GameParam.WoodPriceMedium;
        }
        public double GetTotalWoodPrice(Round round)
        {
            return round.WoodPurchased * GetWoodPrice(round);
        }
        public double GetCrystalConstCost(Round round)
        {
            if (round.CrystalPurchased > 0)
                return GameParam.CrystalPriceConst;
            return 0;
        }
        public double GetTotalCrystalPrice(Round round)
        {
            return round.CrystalPurchased * GameParam.CrystalPrice + GetCrystalConstCost(round); ;
        }
        public int GetWoodUse(Round round)
        {
            return round.WandsAmount * (int)GameParam.WoodConsumption;
        }
        public int GetCrystalUse(Round round)
        {
            return round.WandsAmount * (int)GameParam.CrystalConsumption;
        }
        public double GetAndSetAverageWoodPrice(Round round)
        {
            if (round.WoodAveragePrevious == 0)
            {
                if (round.WoodPurchased != 0)
                    round.WoodAverage = (GetTotalWoodPrice(round)) / (round.WoodPurchased);
            }
            else
            {
                if (round.WoodPurchased != 0)
                    round.WoodAverage = (GetTotalWoodPrice(round) + round.WoodAveragePrevious * round.WoodReserves) / (round.WoodPurchased + round.WoodReserves);
            }
            return round.WoodAverage;
        }
        public double GetAndSetAverageCrystalPrice(Round round)
        {
            if (round.CrystalAveragePrevious == 0)
            {
                if (round.CrystalPurchased != 0)
                    round.CrystalAverage = (GetTotalCrystalPrice(round)) / (round.CrystalPurchased);
            }
            else
            {
                if (round.CrystalPurchased != 0)
                    round.CrystalAverage = (GetTotalCrystalPrice(round) + round.CrystalReserves * round.CrystalAveragePrevious) / (round.CrystalPurchased + round.CrystalReserves);
            }
            return round.CrystalAverage;
        }

        public double GetTransportCost(Round round)
        {
            return round.WandsAmount * GameParam.TransportCosts;
        }
        public double GetGeneralMaterialRateCost(Round round)
        {
            return (GetWoodUse(round) * GetAndSetAverageWoodPrice(round) * GetCrystalUse(round) * GetAndSetAverageCrystalPrice(round)) * GameParam.GeneralMaterialRateCosts;
        }
        public double GetGeneralProcessingRateCost(Round round)
        {
            return GetWorkersPrice(round) * GameParam.GeneralProcessingRateCosts;
            //return CountWorkersExpense(round) * GameParams.GeneralProcessingRateCosts;
        }
        public double GetMachineDepreciationCost(Round round)
        {
            return round.WandsAmount * GameParam.Depreciation;
        }
        public double GetLoanRateCost(Round round)
        {
            return round.LoanRemaining * GameParam.InterestRate;
        }

        public double GetQualityFading(Round round)
        {
            return GetQualityInfluence(round) * GameParam.QualityFading;
        }
        public double GetAdFading(Round round)
        {
            return GetAdInfluence(round) * GameParam.AdFading;
        }
        public double GetQualityInfluence(Round round)
        {
            return (round.QualityExpense + round.QualityExpensePrevious);
        }
        public double GetAdInfluence(Round round)
        {
            return (round.AdExpense + round.AdExpensePrevious);
        }

        public double GetLoanRemaining(Round round)
        {
            return round.LoanRemaining - round.LoanPaid + round.LoanTaken;
        }

        public double CountAllExpenses(Round round)
        {
            return CountResourcesExpense(round) + CountWorkersExpense(round) + CountMachineExpense(round) +
                GetTransportCost(round) + GetGeneralMaterialRateCost(round) + GetGeneralProcessingRateCost(round) + GetLoanRateCost(round) +
                round.LoanPaid + round.AdExpense + round.QualityExpense + GameParam.ManagementCosts;
        }

        public double CountIncome(Round round)
        {
            return 0;
        }
        public double CountAllCosts(Round round)
        {
            double d = CountResourcesExpense(round) + CountWorkersExpense(round) + GetMachineDepreciationCost(round) +
                GetTransportCost(round) + GetGeneralMaterialRateCost(round) + GetGeneralProcessingRateCost(round) + GetLoanRateCost(round) +
                round.AdExpense + round.QualityExpense + GameParam.ManagementCosts;
            return d;
        }
        public double CountProfit(Round round)
        {
            return (round.Income - CountAllCosts(round));// > 0) ? round.Income - CountAllCosts(round) : 0;
        }
        public double CountPaymentForTheLord(Round round)
        {
            return CountProfit(round) > 0 ? CountProfit(round) * GameParam.Tax : 0;
        }
        public double CountPaymentForMages(Round round)
        {
            return CountProfit(round) > 0 ? (CountProfit(round) - CountPaymentForTheLord(round)) * GameParam.Dividend : 0;
        }
        public double CountYourTruePoorProfit(Round round)
        {
            return CountProfit(round) > 0 ? (CountProfit(round) - CountPaymentForMages(round) - CountPaymentForTheLord(round)) : 0;
        }
        public int CountRemainingWandsAmount(Round round)
        {
            return round.WandsAmount - round.WandsSoldAmount;
        }
        #endregion
    }
}
