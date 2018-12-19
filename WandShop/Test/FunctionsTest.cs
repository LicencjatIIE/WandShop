using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Entities;

namespace Test
{
    [TestClass]
    public class FunctionsTest
    {
        [TestMethod]
        public void CountResourcesExpenseTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { WoodPurchased = g.WoodAmountLow };
            Round r2 = new Round() { CrystalPurchased = 1 };
            Round r3 = new Round() { WoodPurchased = g.WoodAmountLow, CrystalPurchased = 1 };
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = f.CountResourcesExpense(r1);
            d2 = f.CountResourcesExpense(r2);
            d3 = f.CountResourcesExpense(r3);
            //Sprawdzenie
            Assert.AreEqual(d1, f.GetTotalWoodPrice(r1));
            Assert.AreEqual(d2, f.GetTotalCrystalPrice(r2));
            Assert.AreEqual(d3, f.GetTotalWoodPrice(r1) + f.GetTotalCrystalPrice(r2));
        }
        [TestMethod]
        public void CountWorkersExpenseTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { DwarfWorkers = 1, DwarfWorkersDismissed = 1, DwarfWorkersEmployed = 1};
            Round r2 = new Round() { ElfWorkers = 1, ElfWorkersDismissed = 1, ElfWorkersEmployed = 1 };
            Round r3 = new Round() { HumanWorkers = 1, HumanWorkersDismissed = 1, HumanWorkersEmployed = 1 };
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = f.CountWorkersExpense(r1);
            d2 = f.CountWorkersExpense(r2);
            d3 = f.CountWorkersExpense(r3);
            //Sprawdzenie
            Assert.AreEqual(d1, g.WorkerDwarfPrice * g.HoursPerPeriod + g.DismissalPrice + g.EmploymentPrice);
            Assert.AreEqual(d2, g.WorkerElfPrice * g.HoursPerPeriod + g.DismissalPrice + g.EmploymentPrice);
            Assert.AreEqual(d3, g.WorkerHumanPrice * g.HoursPerPeriod + g.DismissalPrice + g.EmploymentPrice);
        }
        [TestMethod]
        public void CountMachineExpenseTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { MachinesPurchased = 1 };
            double d1 = 0;
            //Działanie
            d1 = f.CountMachineExpense(r1);
            //Sprawdzenie
            Assert.AreEqual(d1, g.MachinePrice * 1);
        }


        [TestMethod]
        public void GetDismissedWorkerPriceTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { DwarfWorkersDismissed = 1 };
            Round r2 = new Round() { ElfWorkersDismissed = 1 };
            Round r3 = new Round() { HumanWorkersDismissed = 1 };
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = f.GetDismissedWorkerPrice(r1);
            d2 = f.GetDismissedWorkerPrice(r2);
            d3 = f.GetDismissedWorkerPrice(r3);
            //Sprawdzenie
            Assert.AreEqual(d1, g.DismissalPrice);
            Assert.AreEqual(d2, g.DismissalPrice);
            Assert.AreEqual(d3, g.DismissalPrice);
        }
        [TestMethod]
        public void GetEmployedWorkerPriceTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { DwarfWorkersEmployed = 1 };
            Round r2 = new Round() { ElfWorkersEmployed = 1 };
            Round r3 = new Round() { HumanWorkersEmployed = 1 };
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = f.GetEmployedWorkerPrice(r1);
            d2 = f.GetEmployedWorkerPrice(r2);
            d3 = f.GetEmployedWorkerPrice(r3);
            //Sprawdzenie
            Assert.AreEqual(d1, g.EmploymentPrice);
            Assert.AreEqual(d2, g.EmploymentPrice);
            Assert.AreEqual(d3, g.EmploymentPrice);
        }
        [TestMethod]
        public void GetWorkersPriceTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { DwarfWorkers = 1 };
            Round r2 = new Round() { ElfWorkers = 1 };
            Round r3 = new Round() { HumanWorkers = 1 };
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = f.GetWorkersPrice(r1);
            d2 = f.GetWorkersPrice(r2);
            d3 = f.GetWorkersPrice(r3);
            //Sprawdzenie
            Assert.AreEqual(d1, g.WorkerDwarfPrice * g.HoursPerPeriod);
            Assert.AreEqual(d2, g.WorkerElfPrice * g.HoursPerPeriod);
            Assert.AreEqual(d3, g.WorkerHumanPrice * g.HoursPerPeriod);
        }


        [TestMethod]
        public void GetWoodPriceTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { WoodPurchased = g.WoodAmountLow };
            Round r2 = new Round() { WoodPurchased = ( g.WoodAmountHigh - g.WoodAmountLow) };
            Round r3 = new Round() { WoodPurchased = g.WoodAmountHigh };
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = f.GetWoodPrice(r1);
            d2 = f.GetWoodPrice(r2);
            d3 = f.GetWoodPrice(r3);
            //Sprawdzenie
            Assert.AreEqual(d1, g.WoodPriceHigh);
            Assert.AreEqual(d2, g.WoodPriceMedium);
            Assert.AreEqual(d3, g.WoodPriceLow);
        }
        [TestMethod]
        public void GetTotalWoodPriceTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { WoodPurchased = g.WoodAmountLow };
            Round r2 = new Round() { WoodPurchased = (g.WoodAmountHigh - g.WoodAmountLow) };
            Round r3 = new Round() { WoodPurchased = g.WoodAmountHigh };
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = f.GetTotalWoodPrice(r1);
            d2 = f.GetTotalWoodPrice(r2);
            d3 = f.GetTotalWoodPrice(r3);
            //Sprawdzenie
            Assert.AreEqual(d1, g.WoodPriceHigh * g.WoodAmountLow);
            Assert.AreEqual(d2, g.WoodPriceMedium * (g.WoodAmountHigh - g.WoodAmountLow));
            Assert.AreEqual(d3, g.WoodPriceLow * g.WoodAmountHigh);

        }
        [TestMethod]
        public void GetCrystalConstCostTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { CrystalPurchased = 1 };
            Round r2 = new Round() { CrystalPurchased = 0 };
            double d1 = 0, d2 = 0;
            //Działanie
            d1 = f.GetCrystalConstCost(r1);
            d2 = f.GetCrystalConstCost(r2);
            //Sprawdzenie
            Assert.AreEqual(d1, g.CrystalPriceConst);
            Assert.AreEqual(d2, 0);
        }
        [TestMethod]
        public void GetTotalCrystalPriceTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { CrystalPurchased = 1 };
            Round r2 = new Round() { CrystalPurchased = 100 };
            double d1 = 0, d2 = 0;
            //Działanie
            d1 = f.GetTotalCrystalPrice(r1);
            d2 = f.GetTotalCrystalPrice(r2);
            //Sprawdzenie
            Assert.AreEqual(d1, g.CrystalPriceConst + g.CrystalPrice);
            Assert.AreEqual(d2, g.CrystalPriceConst + (g.CrystalPrice * 100));
        }
        [TestMethod]
        public void GetWoodUseTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { WandsAmount = 0 };
            Round r2 = new Round() { WandsAmount = 1 };
            Round r3 = new Round() { WandsAmount = 1000 };
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = f.GetWoodUse(r1);
            d2 = f.GetWoodUse(r2);
            d3 = f.GetWoodUse(r3);
            //Sprawdzenie
            Assert.AreEqual(d1, 0 * g.WoodConsumption);
            Assert.AreEqual(d2, 1 * g.WoodConsumption);
            Assert.AreEqual(d3, 1000 * g.WoodConsumption);
        }
        [TestMethod]
        public void GetCrystalUseTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { WandsAmount = 0 };
            Round r2 = new Round() { WandsAmount = 1 };
            Round r3 = new Round() { WandsAmount = 1000 };
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = f.GetCrystalUse(r1);
            d2 = f.GetCrystalUse(r2);
            d3 = f.GetCrystalUse(r3);
            //Sprawdzenie
            Assert.AreEqual(d1, 0 * g.CrystalConsumption);
            Assert.AreEqual(d2, 1 * g.CrystalConsumption);
            Assert.AreEqual(d3, 1000 * g.CrystalConsumption);
        }
        [TestMethod]
        public void GetAndSetAverageWoodPriceTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { WoodAveragePrevious = 0, WoodPurchased = g.WoodAmountLow };
            Round r2 = new Round() { WoodAveragePrevious = 0, WoodPurchased = 0 };
            Round r3 = new Round() { WoodAveragePrevious = g.WoodPriceHigh, WoodPurchased = g.WoodAmountLow, WoodReserves = g.WoodAmountLow };
            Round r4 = new Round() { WoodAveragePrevious = g.WoodPriceHigh, WoodPurchased = 0, WoodReserves = g.WoodAmountLow };
            double d1 = 0, d2 = 0, d3 = 0, d4 = 0;
            //Działanie
            d1 = f.GetAndSetAverageWoodPrice(r1);
            d2 = f.GetAndSetAverageWoodPrice(r2);
            d3 = f.GetAndSetAverageWoodPrice(r3);
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
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { CrystalAveragePrevious = 0, CrystalPurchased = 1 };
            Round r2 = new Round() { CrystalAveragePrevious = 0, CrystalPurchased = 0 };
            Round r3 = new Round() { CrystalAveragePrevious = g.CrystalPriceConst + g.CrystalPrice, CrystalPurchased = 1, CrystalReserves = 1 };
            Round r4 = new Round() { CrystalAveragePrevious = g.CrystalPriceConst + g.CrystalPrice, CrystalPurchased = 0, CrystalReserves = 1 };
            double d1 = 0, d2 = 0, d3 = 0, d4 = 0;
            //Działanie
            d1 = f.GetAndSetAverageCrystalPrice(r1);
            d2 = f.GetAndSetAverageCrystalPrice(r2);
            d3 = f.GetAndSetAverageCrystalPrice(r3);
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
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { WandsAmount = 10 };
            double d1 = 0;
            //Działanie
            d1 = f.GetTransportCost(r1);
            //Sprawdzenie
            Assert.AreEqual(d1, g.TransportCosts * 10);
        }
        [TestMethod]
        public void GetLoanRateCostTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { LoanRemaining = 100 };
            double d1 = 0;
            //Działanie
            d1 = f.GetLoanRateCost(r1);
            //Sprawdzenie
            Assert.AreEqual(d1, 100 * g.InterestRate);
        }
        [TestMethod]
        public void GetMachineDepreciationCostTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { WandsAmount = 100 };
            double d1 = 0;
            //Działanie
            d1 = f.GetMachineDepreciationCost(r1);
            //Sprawdzenie
            Assert.AreEqual(d1, 100 * g.Depreciation);
        }
        [TestMethod]
        public void GetGeneralMaterialRateCostTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { WandsAmount = 1000, WoodPurchased = 2000, CrystalPurchased = 1000 };
            double d1 = 0;
            //Działanie
            d1 = f.GetGeneralMaterialRateCost(r1);
            //Sprawdzenie
            Assert.AreEqual(d1, (f.GetWoodUse(r1) * f.GetAndSetAverageWoodPrice(r1) * f.GetCrystalUse(r1) * f.GetAndSetAverageCrystalPrice(r1)) * g.GeneralMaterialRateCosts);
        }
        [TestMethod]
        public void GetGeneralProcessingRateCostTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { ElfWorkers = 1};
            double d1 = 0;
            //Działanie
            d1 = f.GetGeneralProcessingRateCost(r1);
            //Sprawdzenie
            Assert.AreEqual(d1, g.GeneralProcessingRateCosts * g.WorkerElfPrice * g.HoursPerPeriod);
        }


        [TestMethod]
        public void GetQualityFadingTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { QualityExpense = 100 };
            double d1 = 0;
            //Działanie
            d1 = f.GetQualityFading(r1);
            //Sprawdzenie
            Assert.AreEqual(d1, 100 * g.QualityFading);
        }
        [TestMethod]
        public void GetAdFadingTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { AdExpense = 100 };
            double d1 = 0;
            //Działanie
            d1 = f.GetAdFading(r1);
            //Sprawdzenie
            Assert.AreEqual(d1, 100 * g.AdFading);
        }
        [TestMethod]
        public void GetQualityInfluenceTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { QualityExpense = 100, QualityExpensePrevious = 60 };
            double d1 = 0;
            //Działanie
            d1 = f.GetQualityInfluence(r1);
            //Sprawdzenie
            Assert.AreEqual(d1, 160);
        }
        [TestMethod]
        public void GetAdInfluenceTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { AdExpense = 100, AdExpensePrevious = 60 };
            double d1 = 0;
            //Działanie
            d1 = f.GetAdInfluence(r1);
            //Sprawdzenie
            Assert.AreEqual(d1, 160);
        }


        [TestMethod]
        public void GetLoanRemainingTest()
        {
            //Przygotowanie
            GameParams g = new GameParams();
            Functions f = new Functions();
            Round r1 = new Round() { LoanRemaining = 5, LoanPaid = 5, LoanTaken  = 1};
            double d1 = 0;
            //Działanie
            d1 = f.GetLoanRemaining(r1);
            //Sprawdzenie
            Assert.AreEqual(d1, 1);
        }
    }
}
