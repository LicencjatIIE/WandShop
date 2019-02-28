using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Entities
{
    [Serializable]
    public class PlayerPart
    {
        public int PlayerPartId { get; set; }
        public int CurrentRound { get; set; } = 0;
        
        //For EF
        public virtual List<PlayerRound> PlayerRounds { get; set; } = new List<PlayerRound>();
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        public virtual Player Player { get; set; }

        #region Functions
        public double CountResourcesExpense(PlayerRound round, int currentRound )
        {
            return GetTotalWoodPrice(round, CurrentRound) + GetTotalCrystalPrice(round, CurrentRound);
        }
        public double CountWorkersExpense(PlayerRound round, int currentRound )
        {
            return GetDismissedWorkerPrice(round, CurrentRound) + GetEmployedWorkerPrice(round, CurrentRound) + GetWorkersPrice(round, CurrentRound);
        }
        public double CountMachineExpense(PlayerRound round, int currentRound )
        {
            return round.MachinesPurchased * Game.Rounds[currentRound].MachinePrice;
        }

        public double GetDismissedWorkerPrice(PlayerRound round, int currentRound )
        {
            return (round.DwarfWorkersDismissed + round.ElfWorkersDismissed + round.HumanWorkersDismissed) * Game.Rounds[currentRound].DismissalPrice;
        }
        public double GetEmployedWorkerPrice(PlayerRound round, int currentRound )
        {
            return (round.DwarfWorkersEmployed + round.ElfWorkersEmployed + round.HumanWorkersEmployed) * Game.Rounds[currentRound].EmploymentPrice;
        }
        public double GetWorkersPrice(PlayerRound round, int currentRound )
        {
            return Game.Rounds[currentRound].HoursPerPeriod * 
                (round.DwarfWorkers * Game.Rounds[currentRound].WorkerDwarfPrice + 
                round.ElfWorkers * Game.Rounds[currentRound].WorkerElfPrice + 
                round.HumanWorkers * Game.Rounds[currentRound].WorkerHumanPrice);
        }

        public double GetWoodPrice(PlayerRound round, int currentRound )
        {
            if (round.WoodPurchased <= Game.Rounds[currentRound].WoodAmountLow)
                return Game.Rounds[currentRound].WoodPriceHigh;
            else if (round.WoodPurchased >= Game.Rounds[currentRound].WoodAmountHigh)
                return Game.Rounds[currentRound].WoodPriceLow;
            return Game.Rounds[currentRound].WoodPriceMedium;
        }
        public double GetTotalWoodPrice(PlayerRound round, int currentRound )
        {
            return round.WoodPurchased * GetWoodPrice(round, CurrentRound);
        }
        public double GetCrystalConstCost(PlayerRound round, int currentRound )
        {
            if (round.CrystalPurchased > 0)
                return Game.Rounds[currentRound].CrystalPriceConst;
            return 0;
        }
        public double GetTotalCrystalPrice(PlayerRound round, int currentRound )
        {
            return (round.CrystalPurchased * Game.Rounds[currentRound].CrystalPrice) + GetCrystalConstCost(round, CurrentRound);
        }
        public int GetWoodUse(PlayerRound round, int currentRound )
        {
            return round.WandsProducedAmount * (int)Game.Rounds[currentRound].WoodConsumption;
        }
        public int GetCrystalUse(PlayerRound round, int currentRound )
        {
            return round.WandsProducedAmount * (int)Game.Rounds[currentRound].CrystalConsumption;
        }
        public double GetAndSetAverageWoodPrice(PlayerRound round, int currentRound )
        {
            if (round.WoodAveragePrevious == 0)
            {
                if (round.WoodPurchased != 0)
                    round.WoodAverage = (GetTotalWoodPrice(round, CurrentRound)) / (round.WoodPurchased);
            }
            else
            {
                if (round.WoodPurchased != 0)
                    round.WoodAverage = (GetTotalWoodPrice(round, CurrentRound) + round.WoodAveragePrevious * round.WoodReserves) / (round.WoodPurchased + round.WoodReserves);
            }
            return round.WoodAverage;
        }
        public double GetAndSetAverageCrystalPrice(PlayerRound round, int currentRound )
        {
            if (round.CrystalAveragePrevious == 0)
            {
                if (round.CrystalPurchased != 0)
                    round.CrystalAverage = Math.Round((GetTotalCrystalPrice(round, CurrentRound)) / (round.CrystalPurchased));
            }
            else
            {
                if (round.CrystalPurchased != 0)
                    round.CrystalAverage = Math.Round((GetTotalCrystalPrice(round, CurrentRound) + round.CrystalReserves * round.CrystalAveragePrevious) / (round.CrystalPurchased + round.CrystalReserves));
            }
            return round.CrystalAverage;
        }
        public int GetWoodRemaining(PlayerRound round, int currentRound)
        {
            return PlayerRounds[currentRound].WoodPurchased + PlayerRounds[currentRound].WoodReserves - GetWoodUse(round, currentRound);
        }
        public int GetCrystalRemaining(PlayerRound round, int currentRound)
        {
            return PlayerRounds[currentRound].CrystalPurchased + PlayerRounds[currentRound].CrystalReserves - GetCrystalUse(round, currentRound);
        }
        
        public int GetMachineRemaining(PlayerRound round, int currentRound)
        {
            return PlayerRounds[currentRound].MachinesOwned + PlayerRounds[currentRound].MachinesPurchased - PlayerRounds[currentRound].MachinesSold;
        }
        public int GetWorkerDwarfRemaining(PlayerRound round, int currentRound)
        {
            return PlayerRounds[currentRound].DwarfWorkers + PlayerRounds[currentRound].DwarfWorkersEmployed - PlayerRounds[currentRound].DwarfWorkersDismissed;
        }
        public int GetWorkerElfRemaining(PlayerRound round, int currentRound)
        {
            return PlayerRounds[currentRound].ElfWorkers + PlayerRounds[currentRound].ElfWorkersEmployed - PlayerRounds[currentRound].ElfWorkersDismissed;
        }
        public int GetWorkerHumanRemaining(PlayerRound round, int currentRound)
        {
            return PlayerRounds[currentRound].HumanWorkers + PlayerRounds[currentRound].HumanWorkersEmployed - PlayerRounds[currentRound].HumanWorkersDismissed;
        }

        public double GetTransportCost(PlayerRound round, int currentRound )
        {
            return round.WandsProducedAmount * Game.Rounds[currentRound].TransportCosts;
        }
        public double GetGeneralMaterialRateCost(PlayerRound round, int currentRound )
        {
            return (GetWoodUse(round, CurrentRound) * GetAndSetAverageWoodPrice(round, CurrentRound) +
                GetCrystalUse(round, CurrentRound) * GetAndSetAverageCrystalPrice(round, CurrentRound)) * Game.Rounds[currentRound].GeneralMaterialRateCosts;
        }
        public double GetGeneralProcessingRateCost(PlayerRound round, int currentRound )
        {
            return GetWorkersPrice(round, CurrentRound) * Game.Rounds[currentRound].GeneralProcessingRateCosts;
        }
        public double GetMachineDepreciationCost(PlayerRound round, int currentRound )
        {
            return round.WandsProducedAmount * Game.Rounds[currentRound].Depreciation;
        }
        public double GetLoanRateCost(PlayerRound round, int currentRound )
        {
            return round.LoanRemaining * Game.Rounds[currentRound].InterestRate;
        }

        public double GetQualityFading(PlayerRound round, int currentRound )
        {
            return Math.Round(GetQualityInfluence(round, CurrentRound) * Game.Rounds[currentRound].QualityFading);
        }
        public double GetAdFading(PlayerRound round, int currentRound )
        {
            return Math.Round(GetAdInfluence(round, CurrentRound) * Game.Rounds[currentRound].AdFading);
        }
        public double GetQualityInfluence(PlayerRound round, int currentRound )
        {
            return (round.QualityExpense + round.QualityExpensePrevious);
        }
        public double GetAdInfluence(PlayerRound round, int currentRound )
        {
            return (round.AdExpense + round.AdExpensePrevious);
        }

        public double GetLoanRemaining(PlayerRound round, int currentRound )
        {
            return round.LoanRemaining - round.LoanPaid + round.LoanTaken;
        }

        public double CountAllExpenses(PlayerRound round, int currentRound )
        {
            return CountResourcesExpense(round, CurrentRound)
                + CountWorkersExpense(round, CurrentRound)
                + CountMachineExpense(round, CurrentRound)
                + round.LoanPaid
                + round.AdExpense
                + round.QualityExpense
                + CountBonusCosts(round, currentRound); 
        }

        public double CountBonusCosts(PlayerRound round, int currentRound)
        {
            return GetTransportCost(round, CurrentRound)
                + GetGeneralMaterialRateCost(round, CurrentRound)
                + GetGeneralProcessingRateCost(round, CurrentRound)
                + GetLoanRateCost(round, CurrentRound)
                + Game.Rounds[currentRound].ManagementCosts;
        }
        /*
        public double CountYourQualityReduction(PlayerRound round, int currentRound)
        {
            return 1.0;
        }
        */
        public double CountIncome(PlayerRound round, int currentRound )
        {
            return CountWandsSoldAmount(round, currentRound) * round.WandPrice;
        }
        public double CountAllCosts(PlayerRound round, int currentRound )
        {
            double d = CountResourcesExpense(round, CurrentRound) + /*CountWorkersExpense(round, CurrentRound) +*/ GetMachineDepreciationCost(round, CurrentRound) +
                GetTransportCost(round, CurrentRound) + GetGeneralMaterialRateCost(round, CurrentRound) + GetGeneralProcessingRateCost(round, CurrentRound) + GetLoanRateCost(round, CurrentRound) +
                round.AdExpense + round.QualityExpense + Game.Rounds[currentRound].ManagementCosts;
            return d;
        }
        public double CountProfit(PlayerRound round, int currentRound )
        {
            return (round.Income - CountAllCosts(round, CurrentRound));
        }
        public double CountPaymentForTheLord(PlayerRound round, int currentRound )
        {
            return CountProfit(round, CurrentRound) > 0 ? CountProfit(round, CurrentRound) * Game.Rounds[currentRound].Tax : 0;
        }
        public double CountPaymentForMages(PlayerRound round, int currentRound )
        {
            return CountProfit(round, CurrentRound) > 0 ? (CountProfit(round, CurrentRound) - CountPaymentForTheLord(round, CurrentRound)) * Game.Rounds[currentRound].Dividend : 0;
        }

        /// <summary>
        /// To będzie funkcja wyniku by sprawdzić kto wygra.
        /// </summary>
        /// <param name="round"></param>
        /// <param name="currentRound"></param>
        /// <returns></returns>
        public double CountYourTruePoorProfit(PlayerRound round, int currentRound )
        {
            return CountProfit(round, CurrentRound) - CountPaymentForMages(round, CurrentRound) - CountPaymentForTheLord(round, CurrentRound);
        }
        public int CountWandsRemainingAmount(PlayerRound round, int currentRound )
        {
            return round.WandsProducedAmount + round.WandsReservesAmount - round.WandsSoldAmount;
        }
        public double CountRemainingGold(PlayerRound round, int currentRound)
        {
            return Math.Round(round.Gold - CountAllExpenses(round, currentRound) + CountIncome(round, currentRound) - CountPaymentForMages(round, CurrentRound) - CountPaymentForTheLord(round, CurrentRound), 2)
                + round.LoanTaken;
        }
        //TODO To jest najważniejsza funkcja do wrzucenia
        public int CountWandsSoldAmount(PlayerRound round, int currentRound)
        {
            return round.WandsProducedAmount + round.WandsReservesAmount;
        }
        public int GetMaxWandProdction(PlayerRound round, int currentRound)
        {
            int max = Int32.MaxValue;

            if (round.WoodReserves / (int)Game.Rounds[currentRound].WoodConsumption <= max)
                max = round.WoodReserves / (int)Game.Rounds[currentRound].WoodConsumption;
            if (round.CrystalReserves / (int)Game.Rounds[currentRound].CrystalConsumption <= max)
                max = round.CrystalReserves / (int)Game.Rounds[currentRound].CrystalConsumption;
            
            if (round.MachinesOwned * (int)Game.Rounds[currentRound].CrystalEfficiency <= max)
                max = round.CrystalReserves * (int)Game.Rounds[currentRound].CrystalEfficiency;

            if (round.ElfWorkers * (int)Game.Rounds[currentRound].WorkerElfEfficiency * Game.Rounds[currentRound].HoursPerPeriod <= max)
                max = round.ElfWorkers * (int)Game.Rounds[currentRound].WorkerElfEfficiency * Game.Rounds[currentRound].HoursPerPeriod;
            if (round.DwarfWorkers * (int)Game.Rounds[currentRound].WorkerDwarfEfficiency * Game.Rounds[currentRound].HoursPerPeriod <= max)
                max = round.DwarfWorkers * (int)Game.Rounds[currentRound].WorkerDwarfEfficiency * Game.Rounds[currentRound].HoursPerPeriod;
            if (round.HumanWorkers * (int)Game.Rounds[currentRound].WorkerHumanEfficiency * Game.Rounds[currentRound].HoursPerPeriod <= max)
                max = round.HumanWorkers * (int)Game.Rounds[currentRound].WorkerHumanEfficiency * Game.Rounds[currentRound].HoursPerPeriod;

            return max;
        }
        #endregion
    }
}
