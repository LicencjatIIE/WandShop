using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Entities;

namespace Test
{
    [TestClass]
    public class FunctionsTest
    {
        /*
        [TestMethod]
        public void CountResourcesExpenseTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { WoodPurchased = g.WoodAmountLow };
            PlayerRound pr2 = new PlayerRound() { CrystalPurchased = 1 };
            PlayerRound pr3 = new PlayerRound() { WoodPurchased = g.WoodAmountLow, CrystalPurchased = 1 };
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = f.CountResourcesExpense(pr1);
            d2 = f.CountResourcesExpense(pr2);
            d3 = f.CountResourcesExpense(pr3);
            //Sprawdzenie
            Assert.AreEqual(d1, f.GetTotalWoodPrice(pr1));
            Assert.AreEqual(d2, f.GetTotalCrystalPrice(pr2));
            Assert.AreEqual(d3, f.GetTotalWoodPrice(pr1) + f.GetTotalCrystalPrice(pr2));
        }
        [TestMethod]
        public void CountWorkersExpenseTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { DwarfWorkers = 1, DwarfWorkersDismissed = 1, DwarfWorkersEmployed = 1};
            PlayerRound pr2 = new PlayerRound() { ElfWorkers = 1, ElfWorkersDismissed = 1, ElfWorkersEmployed = 1 };
            PlayerRound pr3 = new PlayerRound() { HumanWorkers = 1, HumanWorkersDismissed = 1, HumanWorkersEmployed = 1 };
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = f.CountWorkersExpense(pr1);
            d2 = f.CountWorkersExpense(pr2);
            d3 = f.CountWorkersExpense(pr3);
            //Sprawdzenie
            Assert.AreEqual(d1, g.WorkerDwarfPrice * g.HoursPerPeriod + g.DismissalPrice + g.EmploymentPrice);
            Assert.AreEqual(d2, g.WorkerElfPrice * g.HoursPerPeriod + g.DismissalPrice + g.EmploymentPrice);
            Assert.AreEqual(d3, g.WorkerHumanPrice * g.HoursPerPeriod + g.DismissalPrice + g.EmploymentPrice);
        }
        [TestMethod]
        public void CountMachineExpenseTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { MachinesPurchased = 1 };
            double d1 = 0;
            //Działanie
            d1 = f.CountMachineExpense(pr1);
            //Sprawdzenie
            Assert.AreEqual(d1, g.MachinePrice * 1);
        }


        [TestMethod]
        public void GetDismissedWorkerPriceTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { DwarfWorkersDismissed = 1 };
            PlayerRound pr2 = new PlayerRound() { ElfWorkersDismissed = 1 };
            PlayerRound pr3 = new PlayerRound() { HumanWorkersDismissed = 1 };
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = f.GetDismissedWorkerPrice(pr1);
            d2 = f.GetDismissedWorkerPrice(pr2);
            d3 = f.GetDismissedWorkerPrice(pr3);
            //Sprawdzenie
            Assert.AreEqual(d1, g.DismissalPrice);
            Assert.AreEqual(d2, g.DismissalPrice);
            Assert.AreEqual(d3, g.DismissalPrice);
        }
        [TestMethod]
        public void GetEmployedWorkerPriceTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { DwarfWorkersEmployed = 1 };
            PlayerRound pr2 = new PlayerRound() { ElfWorkersEmployed = 1 };
            PlayerRound pr3 = new PlayerRound() { HumanWorkersEmployed = 1 };
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = f.GetEmployedWorkerPrice(pr1);
            d2 = f.GetEmployedWorkerPrice(pr2);
            d3 = f.GetEmployedWorkerPrice(pr3);
            //Sprawdzenie
            Assert.AreEqual(d1, g.EmploymentPrice);
            Assert.AreEqual(d2, g.EmploymentPrice);
            Assert.AreEqual(d3, g.EmploymentPrice);
        }
        [TestMethod]
        public void GetWorkersPriceTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { DwarfWorkers = 1 };
            PlayerRound pr2 = new PlayerRound() { ElfWorkers = 1 };
            PlayerRound pr3 = new PlayerRound() { HumanWorkers = 1 };
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = f.GetWorkersPrice(pr1);
            d2 = f.GetWorkersPrice(pr2);
            d3 = f.GetWorkersPrice(pr3);
            //Sprawdzenie
            Assert.AreEqual(d1, g.WorkerDwarfPrice * g.HoursPerPeriod);
            Assert.AreEqual(d2, g.WorkerElfPrice * g.HoursPerPeriod);
            Assert.AreEqual(d3, g.WorkerHumanPrice * g.HoursPerPeriod);
        }


        [TestMethod]
        public void GetWoodPriceTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { WoodPurchased = g.WoodAmountLow };
            PlayerRound pr2 = new PlayerRound() { WoodPurchased = ( g.WoodAmountHigh - g.WoodAmountLow) };
            PlayerRound pr3 = new PlayerRound() { WoodPurchased = g.WoodAmountHigh };
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = f.GetWoodPrice(pr1);
            d2 = f.GetWoodPrice(pr2);
            d3 = f.GetWoodPrice(pr3);
            //Sprawdzenie
            Assert.AreEqual(d1, g.WoodPriceHigh);
            Assert.AreEqual(d2, g.WoodPriceMedium);
            Assert.AreEqual(d3, g.WoodPriceLow);
        }
        [TestMethod]
        public void GetTotalWoodPriceTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { WoodPurchased = g.WoodAmountLow };
            PlayerRound pr2 = new PlayerRound() { WoodPurchased = (g.WoodAmountHigh - g.WoodAmountLow) };
            PlayerRound pr3 = new PlayerRound() { WoodPurchased = g.WoodAmountHigh };
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = f.GetTotalWoodPrice(pr1);
            d2 = f.GetTotalWoodPrice(pr2);
            d3 = f.GetTotalWoodPrice(pr3);
            //Sprawdzenie
            Assert.AreEqual(d1, g.WoodPriceHigh * g.WoodAmountLow);
            Assert.AreEqual(d2, g.WoodPriceMedium * (g.WoodAmountHigh - g.WoodAmountLow));
            Assert.AreEqual(d3, g.WoodPriceLow * g.WoodAmountHigh);

        }
        [TestMethod]
        public void GetCrystalConstCostTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { CrystalPurchased = 1 };
            PlayerRound pr2 = new PlayerRound() { CrystalPurchased = 0 };
            double d1 = 0, d2 = 0;
            //Działanie
            d1 = f.GetCrystalConstCost(pr1);
            d2 = f.GetCrystalConstCost(pr2);
            //Sprawdzenie
            Assert.AreEqual(d1, g.CrystalPriceConst);
            Assert.AreEqual(d2, 0);
        }
        [TestMethod]
        public void GetTotalCrystalPriceTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { CrystalPurchased = 1 };
            PlayerRound pr2 = new PlayerRound() { CrystalPurchased = 100 };
            double d1 = 0, d2 = 0;
            //Działanie
            d1 = f.GetTotalCrystalPrice(pr1);
            d2 = f.GetTotalCrystalPrice(pr2);
            //Sprawdzenie
            Assert.AreEqual(d1, g.CrystalPriceConst + g.CrystalPrice);
            Assert.AreEqual(d2, g.CrystalPriceConst + (g.CrystalPrice * 100));
        }
        [TestMethod]
        public void GetWoodUseTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { WandsAmount = 0 };
            PlayerRound pr2 = new PlayerRound() { WandsAmount = 1 };
            PlayerRound pr3 = new PlayerRound() { WandsAmount = 1000 };
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = f.GetWoodUse(pr1);
            d2 = f.GetWoodUse(pr2);
            d3 = f.GetWoodUse(pr3);
            //Sprawdzenie
            Assert.AreEqual(d1, 0 * g.WoodConsumption);
            Assert.AreEqual(d2, 1 * g.WoodConsumption);
            Assert.AreEqual(d3, 1000 * g.WoodConsumption);
        }
        [TestMethod]
        public void GetCrystalUseTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { WandsAmount = 0 };
            PlayerRound pr2 = new PlayerRound() { WandsAmount = 1 };
            PlayerRound pr3 = new PlayerRound() { WandsAmount = 1000 };
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = f.GetCrystalUse(pr1);
            d2 = f.GetCrystalUse(pr2);
            d3 = f.GetCrystalUse(pr3);
            //Sprawdzenie
            Assert.AreEqual(d1, 0 * g.CrystalConsumption);
            Assert.AreEqual(d2, 1 * g.CrystalConsumption);
            Assert.AreEqual(d3, 1000 * g.CrystalConsumption);
        }
        [TestMethod]
        public void GetAndSetAverageWoodPriceTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { WoodAveragePrevious = 0, WoodPurchased = g.WoodAmountLow };
            PlayerRound pr2 = new PlayerRound() { WoodAveragePrevious = 0, WoodPurchased = 0 };
            PlayerRound pr3 = new PlayerRound() { WoodAveragePrevious = g.WoodPriceHigh, WoodPurchased = g.WoodAmountLow, WoodReserves = g.WoodAmountLow };
            PlayerRound r4 = new PlayerRound() { WoodAveragePrevious = g.WoodPriceHigh, WoodPurchased = 0, WoodReserves = g.WoodAmountLow };
            double d1 = 0, d2 = 0, d3 = 0, d4 = 0;
            //Działanie
            d1 = f.GetAndSetAverageWoodPrice(pr1);
            d2 = f.GetAndSetAverageWoodPrice(pr2);
            d3 = f.GetAndSetAverageWoodPrice(pr3);
            d4 = f.GetAndSetAverageWoodPrice(r4);
            //Sprawdzenie
            Assert.AreEqual(d1, g.WoodPriceHigh);
            Assert.AreEqual(d2, 0);
            Assert.AreEqual(d3, g.WoodPriceHigh);
            Assert.AreEqual(d4, 0);
        }
        [TestMethod]
        public void GetAndSetAverageCrystalPriceTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { CrystalAveragePrevious = 0, CrystalPurchased = 1 };
            PlayerRound pr2 = new PlayerRound() { CrystalAveragePrevious = 0, CrystalPurchased = 0 };
            PlayerRound pr3 = new PlayerRound() { CrystalAveragePrevious = g.CrystalPriceConst + g.CrystalPrice, CrystalPurchased = 1, CrystalReserves = 1 };
            PlayerRound r4 = new PlayerRound() { CrystalAveragePrevious = g.CrystalPriceConst + g.CrystalPrice, CrystalPurchased = 0, CrystalReserves = 1 };
            double d1 = 0, d2 = 0, d3 = 0, d4 = 0;
            //Działanie
            d1 = f.GetAndSetAverageCrystalPrice(pr1);
            d2 = f.GetAndSetAverageCrystalPrice(pr2);
            d3 = f.GetAndSetAverageCrystalPrice(pr3);
            d4 = f.GetAndSetAverageCrystalPrice(r4);
            //Sprawdzenie
            Assert.AreEqual(d1, g.CrystalPriceConst + g.CrystalPrice);
            Assert.AreEqual(d2, 0);
            Assert.AreEqual(d3, g.CrystalPriceConst + g.CrystalPrice);
            Assert.AreEqual(d4, 0);
        }

        
        [TestMethod]
        public void GetTransportCostTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { WandsAmount = 10 };
            double d1 = 0;
            //Działanie
            d1 = f.GetTransportCost(pr1);
            //Sprawdzenie
            Assert.AreEqual(d1, g.TransportCosts * 10);
        }
        [TestMethod]
        public void GetLoanRateCostTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { LoanRemaining = 100 };
            double d1 = 0;
            //Działanie
            d1 = f.GetLoanRateCost(pr1);
            //Sprawdzenie
            Assert.AreEqual(d1, 100 * g.InterestRate);
        }
        [TestMethod]
        public void GetMachineDepreciationCostTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { WandsAmount = 100 };
            double d1 = 0;
            //Działanie
            d1 = f.GetMachineDepreciationCost(pr1);
            //Sprawdzenie
            Assert.AreEqual(d1, 100 * g.Depreciation);
        }
        [TestMethod]
        public void GetGeneralMaterialRateCostTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { WandsAmount = 1000, WoodPurchased = 2000, CrystalPurchased = 1000 };
            double d1 = 0;
            //Działanie
            d1 = f.GetGeneralMaterialRateCost(pr1);
            //Sprawdzenie
            Assert.AreEqual(d1, (f.GetWoodUse(pr1) * f.GetAndSetAverageWoodPrice(pr1) * f.GetCrystalUse(pr1) * f.GetAndSetAverageCrystalPrice(pr1)) * g.GeneralMaterialRateCosts);
        }
        [TestMethod]
        public void GetGeneralProcessingRateCostTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { ElfWorkers = 1};
            double d1 = 0;
            //Działanie
            d1 = f.GetGeneralProcessingRateCost(pr1);
            //Sprawdzenie
            Assert.AreEqual(d1, g.GeneralProcessingRateCosts * g.WorkerElfPrice * g.HoursPerPeriod);
        }


        [TestMethod]
        public void GetQualityFadingTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { QualityExpense = 100 };
            double d1 = 0;
            //Działanie
            d1 = f.GetQualityFading(pr1);
            //Sprawdzenie
            Assert.AreEqual(d1, 100 * g.QualityFading);
        }
        [TestMethod]
        public void GetAdFadingTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { AdExpense = 100 };
            double d1 = 0;
            //Działanie
            d1 = f.GetAdFading(pr1);
            //Sprawdzenie
            Assert.AreEqual(d1, 100 * g.AdFading);
        }
        [TestMethod]
        public void GetQualityInfluenceTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { QualityExpense = 100, QualityExpensePrevious = 60 };
            double d1 = 0;
            //Działanie
            d1 = f.GetQualityInfluence(pr1);
            //Sprawdzenie
            Assert.AreEqual(d1, 160);
        }
        [TestMethod]
        public void GetAdInfluenceTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { AdExpense = 100, AdExpensePrevious = 60 };
            double d1 = 0;
            //Działanie
            d1 = f.GetAdInfluence(pr1);
            //Sprawdzenie
            Assert.AreEqual(d1, 160);
        }


        [TestMethod]
        public void GetLoanRemainingTest()
        {
            //Przygotowanie
            GameParam g = new GameParam();
            Functions f = new Functions();
            PlayerRound pr1 = new PlayerRound() { LoanRemaining = 5, LoanPaid = 5, LoanTaken  = 1};
            double d1 = 0;
            //Działanie
            d1 = f.GetLoanRemaining(pr1);
            //Sprawdzenie
            Assert.AreEqual(d1, 1);
        }
        */
    }
}
