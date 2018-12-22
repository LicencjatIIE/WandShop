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
        public virtual List<PlayerRound> PlayerRounds { get; set; } = new List<PlayerRound>();
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        public virtual Player Player { get; set; }
        


        /*
        //To pewnie przeniesie się do repozytorium
        public void NextRound()
        {
            FinishRound();
            PlayerRound nextRound = new PlayerRound()
            {
                Gold = PlayerRounds[CurrentRound].Gold - CountAllExpenses(PlayerRounds[CurrentRound]) + CountYourTruePoorProfit(PlayerRounds[CurrentRound]),
                LoanRemaining = GetLoanRemaining(PlayerRounds[CurrentRound]),
                WoodReserves = PlayerRounds[CurrentRound].WoodPurchased - GetWoodUse(PlayerRounds[CurrentRound]) + PlayerRounds[CurrentRound].WoodReserves,
                CrystalReserves = PlayerRounds[CurrentRound].CrystalPurchased - GetCrystalUse(PlayerRounds[CurrentRound]) + PlayerRounds[CurrentRound].CrystalReserves,
                WoodPurchased = 0,
                CrystalPurchased = 0,
                WoodAverage = 0,
                CrystalAverage = 0,
                WoodAveragePrevious = GetAndSetAverageWoodPrice(PlayerRounds[CurrentRound]),
                CrystalAveragePrevious = GetAndSetAverageCrystalPrice(PlayerRounds[CurrentRound]),
                MachinesOwned = PlayerRounds[CurrentRound].MachinesPurchased - PlayerRounds[CurrentRound].MachinesSold + PlayerRounds[CurrentRound].MachinesOwned,
                MachinesPurchased = 0,
                MachinesSold = 0,
                DwarfWorkers = PlayerRounds[CurrentRound].DwarfWorkers - PlayerRounds[CurrentRound].DwarfWorkersDismissed + PlayerRounds[CurrentRound].DwarfWorkersEmployed,
                ElfWorkers = PlayerRounds[CurrentRound].ElfWorkers - PlayerRounds[CurrentRound].ElfWorkersDismissed + PlayerRounds[CurrentRound].ElfWorkersEmployed,
                HumanWorkers = PlayerRounds[CurrentRound].HumanWorkers - PlayerRounds[CurrentRound].HumanWorkersDismissed + PlayerRounds[CurrentRound].HumanWorkersEmployed,
                DwarfWorkersEmployed = 0,
                ElfWorkersEmployed = 0,
                HumanWorkersEmployed = 0,
                DwarfWorkersDismissed = 0,
                ElfWorkersDismissed = 0,
                HumanWorkersDismissed = 0,
                QualityExpense = 0,
                AdExpense = 0,
                QualityExpensePrevious = GetQualityFading(PlayerRounds[CurrentRound]),
                AdExpensePrevious = GetAdFading(PlayerRounds[CurrentRound]),
                LoanPaid = 0,
                LoanTaken = 0,
                WandsAmount = 0,
                WandPrice = 0,
                WandsSoldAmount = 0,
                Income = 0,
                RemainingWandsAmount = CountRemainingWandsAmount(PlayerRounds[CurrentRound])
            };
            CurrentRound++;

            PlayerRounds.Add(nextRound);
        }
        public void FinishRound()
        {
            PlayerRounds[CurrentRound].Income = CountIncome(PlayerRounds[CurrentRound]);
        }
        public void SetStartingRound()
        {
            PlayerRound startingRound = new PlayerRound()
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
            PlayerRounds.Add(startingRound);
        }

        #region Functions
        public double CountResourcesExpense(PlayerRound round)
        {
            return GetTotalWoodPrice(round) + GetTotalCrystalPrice(round);
        }
        public double CountWorkersExpense(PlayerRound round)
        {
            return GetDismissedWorkerPrice(round) + GetEmployedWorkerPrice(round) + GetWorkersPrice(round);
        }
        public double CountMachineExpense(PlayerRound round)
        {
            return round.MachinesPurchased * GameParam.MachinePrice;
        }

        public double GetDismissedWorkerPrice(PlayerRound round)
        {
            return (round.DwarfWorkersDismissed + round.ElfWorkersDismissed + round.HumanWorkersDismissed) * GameParam.DismissalPrice;
        }
        public double GetEmployedWorkerPrice(PlayerRound round)
        {
            return (round.DwarfWorkersEmployed + round.ElfWorkersEmployed + round.HumanWorkersEmployed) * GameParam.EmploymentPrice;
        }
        public double GetWorkersPrice(PlayerRound round)
        {
            return GameParam.HoursPerPeriod * (round.DwarfWorkers * GameParam.WorkerDwarfPrice + round.ElfWorkers * GameParam.WorkerElfPrice + round.HumanWorkers * GameParam.WorkerHumanPrice);
        }

        public double GetWoodPrice(PlayerRound round)
        {
            if (round.WoodPurchased <= GameParam.WoodAmountLow)
                return GameParam.WoodPriceHigh;
            else if (round.WoodPurchased >= GameParam.WoodAmountHigh)
                return GameParam.WoodPriceLow;
            return GameParam.WoodPriceMedium;
        }
        public double GetTotalWoodPrice(PlayerRound round)
        {
            return round.WoodPurchased * GetWoodPrice(round);
        }
        public double GetCrystalConstCost(PlayerRound round)
        {
            if (round.CrystalPurchased > 0)
                return GameParam.CrystalPriceConst;
            return 0;
        }
        public double GetTotalCrystalPrice(PlayerRound round)
        {
            return round.CrystalPurchased * GameParam.CrystalPrice + GetCrystalConstCost(round); ;
        }
        public int GetWoodUse(PlayerRound round)
        {
            return round.WandsAmount * (int)GameParam.WoodConsumption;
        }
        public int GetCrystalUse(PlayerRound round)
        {
            return round.WandsAmount * (int)GameParam.CrystalConsumption;
        }
        public double GetAndSetAverageWoodPrice(PlayerRound round)
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
        public double GetAndSetAverageCrystalPrice(PlayerRound round)
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

        public double GetTransportCost(PlayerRound round)
        {
            return round.WandsAmount * GameParam.TransportCosts;
        }
        public double GetGeneralMaterialRateCost(PlayerRound round)
        {
            return (GetWoodUse(round) * GetAndSetAverageWoodPrice(round) * GetCrystalUse(round) * GetAndSetAverageCrystalPrice(round)) * GameParam.GeneralMaterialRateCosts;
        }
        public double GetGeneralProcessingRateCost(PlayerRound round)
        {
            return GetWorkersPrice(round) * GameParam.GeneralProcessingRateCosts;
            //return CountWorkersExpense(round) * GameParams.GeneralProcessingRateCosts;
        }
        public double GetMachineDepreciationCost(PlayerRound round)
        {
            return round.WandsAmount * GameParam.Depreciation;
        }
        public double GetLoanRateCost(PlayerRound round)
        {
            return round.LoanRemaining * GameParam.InterestRate;
        }

        public double GetQualityFading(PlayerRound round)
        {
            return GetQualityInfluence(round) * GameParam.QualityFading;
        }
        public double GetAdFading(PlayerRound round)
        {
            return GetAdInfluence(round) * GameParam.AdFading;
        }
        public double GetQualityInfluence(PlayerRound round)
        {
            return (round.QualityExpense + round.QualityExpensePrevious);
        }
        public double GetAdInfluence(PlayerRound round)
        {
            return (round.AdExpense + round.AdExpensePrevious);
        }

        public double GetLoanRemaining(PlayerRound round)
        {
            return round.LoanRemaining - round.LoanPaid + round.LoanTaken;
        }

        public double CountAllExpenses(PlayerRound round)
        {
            return CountResourcesExpense(round) + CountWorkersExpense(round) + CountMachineExpense(round) +
                GetTransportCost(round) + GetGeneralMaterialRateCost(round) + GetGeneralProcessingRateCost(round) + GetLoanRateCost(round) +
                round.LoanPaid + round.AdExpense + round.QualityExpense + GameParam.ManagementCosts;
        }

        public double CountIncome(PlayerRound round)
        {
            return 0;
        }
        public double CountAllCosts(PlayerRound round)
        {
            double d = CountResourcesExpense(round) + CountWorkersExpense(round) + GetMachineDepreciationCost(round) +
                GetTransportCost(round) + GetGeneralMaterialRateCost(round) + GetGeneralProcessingRateCost(round) + GetLoanRateCost(round) +
                round.AdExpense + round.QualityExpense + GameParam.ManagementCosts;
            return d;
        }
        public double CountProfit(PlayerRound round)
        {
            return (round.Income - CountAllCosts(round));// > 0) ? round.Income - CountAllCosts(round) : 0;
        }
        public double CountPaymentForTheLord(PlayerRound round)
        {
            return CountProfit(round) > 0 ? CountProfit(round) * GameParam.Tax : 0;
        }
        public double CountPaymentForMages(PlayerRound round)
        {
            return CountProfit(round) > 0 ? (CountProfit(round) - CountPaymentForTheLord(round)) * GameParam.Dividend : 0;
        }
        public double CountYourTruePoorProfit(PlayerRound round)
        {
            return CountProfit(round) > 0 ? (CountProfit(round) - CountPaymentForMages(round) - CountPaymentForTheLord(round)) : 0;
        }
        public int CountRemainingWandsAmount(PlayerRound round)
        {
            return round.WandsAmount - round.WandsSoldAmount;
        }
        #endregion
        */
    }
}
