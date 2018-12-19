using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Entities;

namespace Logic.Entities
{
    public class Functions
    {
        public GameParams GameParams { get; set; } = new GameParams();

        public Functions() { }
        public Functions(GameParams gameParams) { GameParams = gameParams; }

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
            return round.MachinesPurchased * GameParams.MachinePrice;
        }

        public double GetDismissedWorkerPrice(Round round)
        {
            return (round.DwarfWorkersDismissed + round.ElfWorkersDismissed + round.HumanWorkersDismissed) * GameParams.DismissalPrice;
        }
        public double GetEmployedWorkerPrice(Round round)
        {
            return (round.DwarfWorkersEmployed + round.ElfWorkersEmployed + round.HumanWorkersEmployed) * GameParams.EmploymentPrice;
        }
        public double GetWorkersPrice(Round round)
        {
            return GameParams.HoursPerPeriod * (round.DwarfWorkers * GameParams.WorkerDwarfPrice + round.ElfWorkers * GameParams.WorkerElfPrice + round.HumanWorkers * GameParams.WorkerHumanPrice);
        }

        public double GetWoodPrice(Round round)
        {
            if (round.WoodPurchased <= GameParams.WoodAmountLow)
                return GameParams.WoodPriceHigh;
            else if (round.WoodPurchased >= GameParams.WoodAmountHigh)
                return GameParams.WoodPriceLow;
            return GameParams.WoodPriceMedium;
        }
        public double GetTotalWoodPrice(Round round)
        {
            return round.WoodPurchased * GetWoodPrice(round);
        }
        public double GetCrystalConstCost(Round round)
        {
            if (round.CrystalPurchased > 0)
                return GameParams.CrystalPriceConst;
            return 0;
        }
        public double GetTotalCrystalPrice(Round round)
        {
            return round.CrystalPurchased * GameParams.CrystalPrice + GetCrystalConstCost(round); ;
        }
        public int GetWoodUse(Round round)
        {
            return round.WandsAmount * (int)GameParams.WoodConsumption;
        }
        public int GetCrystalUse(Round round)
        {
            return round.WandsAmount * (int)GameParams.CrystalConsumption;
        }
        public double GetAndSetAverageWoodPrice(Round round)
        {
            if (round.WoodAveragePrevious == 0)
            {
                if(round.WoodPurchased != 0)
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
            return round.WandsAmount * GameParams.TransportCosts;
        }
        public double GetGeneralMaterialRateCost(Round round)
        {
            return (GetWoodUse(round) * GetAndSetAverageWoodPrice(round) * GetCrystalUse(round) * GetAndSetAverageCrystalPrice(round)) * GameParams.GeneralMaterialRateCosts;
        }
        public double GetGeneralProcessingRateCost(Round round)
        {
            return GetWorkersPrice(round) * GameParams.GeneralProcessingRateCosts;
            //return CountWorkersExpense(round) * GameParams.GeneralProcessingRateCosts;
        }
        public double GetMachineDepreciationCost(Round round)
        {
            return round.WandsAmount * GameParams.Depreciation;
        }
        public double GetLoanRateCost(Round round)
        {
            return round.LoanRemaining * GameParams.InterestRate;
        }

        public double GetQualityFading(Round round)
        {
            return GetQualityInfluence(round) * GameParams.QualityFading;
        }
        public double GetAdFading(Round round)
        {
            return GetAdInfluence(round) * GameParams.AdFading;
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
                round.LoanPaid + round.AdExpense + round.QualityExpense + GameParams.ManagementCosts;
        }

        public double CountIncome(Round round)
        {
            return 0;
        }
        public double CountAllCosts(Round round)
        {
            double d = CountResourcesExpense(round) + CountWorkersExpense(round) + GetMachineDepreciationCost(round) +
                GetTransportCost(round) + GetGeneralMaterialRateCost(round) + GetGeneralProcessingRateCost(round) + GetLoanRateCost(round) +
                round.AdExpense + round.QualityExpense + GameParams.ManagementCosts;
            return d;
        }
        public double CountProfit(Round round)
        {
            return (round.Income - CountAllCosts(round));// > 0) ? round.Income - CountAllCosts(round) : 0;
        }
        public double CountPaymentForTheLord(Round round)
        {
            return CountProfit(round) > 0 ? CountProfit(round) * GameParams.Tax : 0;
        }
        public double CountPaymentForMages(Round round)
        {
            return CountProfit(round) > 0 ? (CountProfit(round) - CountPaymentForTheLord(round)) * GameParams.Dividend : 0;
        }
        public double CountYourTruePoorProfit(Round round)
        {
            return CountProfit(round) > 0 ? (CountProfit(round) - CountPaymentForMages(round) - CountPaymentForTheLord(round)) : 0;
        }
        public int CountRemainingWandsAmount(Round round)
        {
            return round.WandsAmount - round.WandsSoldAmount;
        }
    }
}
 