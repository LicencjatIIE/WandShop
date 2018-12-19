using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Entities;

namespace Logic.Entities
{
    //TODO USUNĄC
    public class Functions
    {
        public GameParam GameParam { get; set; } = new GameParam();

        public Functions() { }
        public Functions(GameParam gameParams) { GameParam = gameParams; }

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
    }
}
 