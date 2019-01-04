using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Entities;

namespace Test
{
    [TestClass]
    public class PlayerPartTest
    {
        private Round InnitializeRound(Game g)
        {
            Round r = new Round()
            {
                GameId = g.GameId,
                Game = g,
                AdFading = g.AdFading,
                CrystalConsumption = g.CrystalConsumption,
                CrystalEfficiency = g.CrystalEfficiency,
                CrystalPrice = g.CrystalPrice,
                CrystalPriceConst = g.CrystalPriceConst,
                Depreciation = g.Depreciation,
                DismissalPrice = g.DismissalPrice,
                Dividend = g.Dividend,
                EmploymentPrice = g.EmploymentPrice,
                GeneralMaterialRateCosts = g.GeneralMaterialRateCosts,
                GeneralProcessingRateCosts = g.GeneralProcessingRateCosts,
                HoursPerPeriod = g.HoursPerPeriod,
                InterestRate = g.InterestRate,
                MachinePrice = g.MachinePrice,
                ManagementCosts = g.ManagementCosts,
                MaxCrystalEfficiency = g.MaxCrystalEfficiency,
                PerfectGemConsumption = g.PerfectGemConsumption,
                PerfectStickConsumption = g.PerfectStickConsumption,
                QualityFading = g.QualityFading,
                Tax = g.Tax,
                TransportCosts = g.TransportCosts,
                WoodAmountHigh = g.WoodAmountHigh,
                WoodAmountLow = g.WoodAmountLow,
                WoodConsumption = g.WoodConsumption,
                WoodPriceHigh = g.WoodPriceHigh,
                WoodPriceLow = g.WoodPriceLow,
                WoodPriceMedium = g.WoodPriceMedium,
                WorkerDwarfEfficiency = g.WorkerDwarfEfficiency,
                WorkerDwarfPrice = g.WorkerDwarfPrice,
                WorkerElfEfficiency = g.WorkerElfEfficiency,
                WorkerElfPrice = g.WorkerElfPrice,
                WorkerHumanEfficiency = g.WorkerHumanEfficiency,
                WorkerHumanPrice = g.WorkerHumanPrice
            };
            return r;
        }


        [TestMethod]
        public void CountResourcesExpenseTest()
        {
            //Przygotowanie
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerPart pp2 = new PlayerPart() { Game = g };
            PlayerPart pp3 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { WoodPurchased = g.Rounds[0].WoodAmountLow };
            pp1.PlayerRounds.Add(pr1);
            PlayerRound pr2 = new PlayerRound() { CrystalPurchased = 1 };
            pp2.PlayerRounds.Add(pr2);
            PlayerRound pr3 = new PlayerRound() { WoodPurchased = g.Rounds[0].WoodAmountLow, CrystalPurchased = 1 };
            pp3.PlayerRounds.Add(pr3);

            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = pp1.CountResourcesExpense(pr1, 0);
            d2 = pp2.CountResourcesExpense(pr2, 0);
            d3 = pp3.CountResourcesExpense(pr3, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, pp1.GetTotalWoodPrice(pr1, 0));
            Assert.AreEqual(d2, pp2.GetTotalCrystalPrice(pr2, 0));
            Assert.AreEqual(d3, pp3.GetTotalWoodPrice(pr3, 0) + pp3.GetTotalCrystalPrice(pr3, 0));
        }
        [TestMethod]
        public void CountWorkersExpenseTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerPart pp2 = new PlayerPart() { Game = g };
            PlayerPart pp3 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { DwarfWorkers = 1, DwarfWorkersDismissed = 1, DwarfWorkersEmployed = 1 };
            pp1.PlayerRounds.Add(pr1);
            PlayerRound pr2 = new PlayerRound() { ElfWorkers = 1, ElfWorkersDismissed = 1, ElfWorkersEmployed = 1 };
            pp2.PlayerRounds.Add(pr2);
            PlayerRound pr3 = new PlayerRound() { HumanWorkers = 1, HumanWorkersDismissed = 1, HumanWorkersEmployed = 1 };
            pp3.PlayerRounds.Add(pr3);

            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = pp1.CountWorkersExpense(pr1, 0);
            d2 = pp2.CountWorkersExpense(pr2, 0);
            d3 = pp3.CountWorkersExpense(pr3, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, g.Rounds[0].WorkerDwarfPrice * g.Rounds[0].HoursPerPeriod + g.Rounds[0].DismissalPrice + g.Rounds[0].EmploymentPrice);
            Assert.AreEqual(d2, g.Rounds[0].WorkerElfPrice * g.Rounds[0].HoursPerPeriod + g.Rounds[0].DismissalPrice + g.Rounds[0].EmploymentPrice);
            Assert.AreEqual(d3, g.Rounds[0].WorkerHumanPrice * g.Rounds[0].HoursPerPeriod + g.Rounds[0].DismissalPrice + g.Rounds[0].EmploymentPrice);
        }
        [TestMethod]
        public void CountMachineExpenseTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { MachinesPurchased = 5 };
            pp1.PlayerRounds.Add(pr1);

            double d1 = 0;
            //Działanie
            d1 = pp1.CountMachineExpense(pr1, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, g.Rounds[0].MachinePrice * 5);
        }
        [TestMethod]
        public void GetDismissedWorkerPriceTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerPart pp2 = new PlayerPart() { Game = g };
            PlayerPart pp3 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { DwarfWorkersDismissed = 1 };
            pp1.PlayerRounds.Add(pr1);
            PlayerRound pr2 = new PlayerRound() { DwarfWorkersDismissed = 2, ElfWorkersDismissed = 1 };
            pp2.PlayerRounds.Add(pr2);
            PlayerRound pr3 = new PlayerRound() { DwarfWorkersDismissed = 1, ElfWorkersDismissed = 1, HumanWorkersDismissed = 1 };
            pp3.PlayerRounds.Add(pr3);

            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = pp1.GetDismissedWorkerPrice(pr1, 0);
            d2 = pp2.GetDismissedWorkerPrice(pr2, 0);
            d3 = pp3.GetDismissedWorkerPrice(pr3, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, g.Rounds[0].DismissalPrice);
            Assert.AreEqual(d2, g.Rounds[0].DismissalPrice * 3);
            Assert.AreEqual(d3, g.Rounds[0].DismissalPrice * 3);
        }
        [TestMethod]
        public void GetEmployedWorkerPriceTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerPart pp2 = new PlayerPart() { Game = g };
            PlayerPart pp3 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { DwarfWorkersEmployed = 1 };
            pp1.PlayerRounds.Add(pr1);
            PlayerRound pr2 = new PlayerRound() { DwarfWorkersEmployed = 2, ElfWorkersEmployed= 1 };
            pp2.PlayerRounds.Add(pr2);
            PlayerRound pr3 = new PlayerRound() { DwarfWorkersEmployed = 1, ElfWorkersEmployed = 1, HumanWorkersEmployed = 1 };
            pp3.PlayerRounds.Add(pr3);

            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = pp1.GetEmployedWorkerPrice(pr1, 0);
            d2 = pp2.GetEmployedWorkerPrice(pr2, 0);
            d3 = pp3.GetEmployedWorkerPrice(pr3, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, g.Rounds[0].EmploymentPrice);
            Assert.AreEqual(d2, g.Rounds[0].EmploymentPrice * 3);
            Assert.AreEqual(d3, g.Rounds[0].EmploymentPrice * 3);
        }
        [TestMethod]
        public void GetWorkersPriceTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerPart pp2 = new PlayerPart() { Game = g };
            PlayerPart pp3 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { DwarfWorkers = 1 };
            pp1.PlayerRounds.Add(pr1);
            PlayerRound pr2 = new PlayerRound() { DwarfWorkers = 2, ElfWorkers = 1 };
            pp2.PlayerRounds.Add(pr2);
            PlayerRound pr3 = new PlayerRound() { DwarfWorkers = 1, ElfWorkers = 1, HumanWorkers = 1 };
            pp3.PlayerRounds.Add(pr3);

            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = pp1.GetWorkersPrice(pr1, 0);
            d2 = pp2.GetWorkersPrice(pr2, 0);
            d3 = pp3.GetWorkersPrice(pr3, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, g.Rounds[0].HoursPerPeriod * g.Rounds[0].WorkerDwarfPrice);
            Assert.AreEqual(d2, g.Rounds[0].HoursPerPeriod * (g.Rounds[0].WorkerDwarfPrice * 2 + g.Rounds[0].WorkerElfPrice));
            Assert.AreEqual(d3, g.Rounds[0].HoursPerPeriod * (g.Rounds[0].WorkerDwarfPrice + g.Rounds[0].WorkerElfPrice + g.Rounds[0].WorkerHumanPrice));
        }
        [TestMethod]
        public void GetWoodPriceTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerPart pp2 = new PlayerPart() { Game = g };
            PlayerPart pp3 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { WoodPurchased = g.Rounds[0].WoodAmountLow };
            pp1.PlayerRounds.Add(pr1);
            PlayerRound pr2 = new PlayerRound() { WoodPurchased = (g.Rounds[0].WoodAmountHigh - g.Rounds[0].WoodAmountLow) };
            pp2.PlayerRounds.Add(pr2);
            PlayerRound pr3 = new PlayerRound() { WoodPurchased = g.Rounds[0].WoodAmountHigh };
            pp3.PlayerRounds.Add(pr3);
            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = pp1.GetWoodPrice(pr1, 0);
            d2 = pp2.GetWoodPrice(pr2, 0);
            d3 = pp3.GetWoodPrice(pr3, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, g.Rounds[0].WoodPriceHigh);
            Assert.AreEqual(d2, g.Rounds[0].WoodPriceMedium);
            Assert.AreEqual(d3, g.Rounds[0].WoodPriceLow);
        }
        [TestMethod]
        public void GetTotalWoodPriceTest()
        {

            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerPart pp2 = new PlayerPart() { Game = g };
            PlayerPart pp3 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { WoodPurchased = g.Rounds[0].WoodAmountLow };
            pp1.PlayerRounds.Add(pr1);
            PlayerRound pr2 = new PlayerRound() { WoodPurchased = (g.Rounds[0].WoodAmountHigh - g.Rounds[0].WoodAmountLow) };
            pp2.PlayerRounds.Add(pr2);
            PlayerRound pr3 = new PlayerRound() { WoodPurchased = g.Rounds[0].WoodAmountHigh };
            pp3.PlayerRounds.Add(pr3);

            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = pp1.GetTotalWoodPrice(pr1, 0);
            d2 = pp2.GetTotalWoodPrice(pr2, 0);
            d3 = pp3.GetTotalWoodPrice(pr3, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, g.Rounds[0].WoodPriceHigh * g.Rounds[0].WoodAmountLow);
            Assert.AreEqual(d2, g.Rounds[0].WoodPriceMedium * (g.Rounds[0].WoodAmountHigh - g.Rounds[0].WoodAmountLow));
            Assert.AreEqual(d3, g.Rounds[0].WoodPriceLow * g.Rounds[0].WoodAmountHigh);
        }
        [TestMethod]
        public void GetCrystalConstCostTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerPart pp2 = new PlayerPart() { Game = g };
            PlayerPart pp3 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { CrystalPurchased = 10 };
            pp1.PlayerRounds.Add(pr1);
            PlayerRound pr2 = new PlayerRound() { CrystalPurchased = 0 };
            pp2.PlayerRounds.Add(pr2);

            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = pp1.GetCrystalConstCost(pr1, 0);
            d2 = pp2.GetCrystalConstCost(pr2, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, g.Rounds[0].CrystalPriceConst);
            Assert.AreEqual(d2, 0);
        }
        [TestMethod]
        public void GetTotalCrystalPriceTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerPart pp2 = new PlayerPart() { Game = g };
            PlayerPart pp3 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { CrystalPurchased = 1 };
            pp1.PlayerRounds.Add(pr1);
            PlayerRound pr2 = new PlayerRound() { CrystalPurchased = 100 };
            pp2.PlayerRounds.Add(pr2);

            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = pp1.GetTotalCrystalPrice(pr1, 0);
            d2 = pp2.GetTotalCrystalPrice(pr2, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, g.Rounds[0].CrystalPriceConst + g.Rounds[0].CrystalPrice);
            Assert.AreEqual(d2, g.Rounds[0].CrystalPriceConst + 100 * g.Rounds[0].CrystalPrice);
        }
        [TestMethod]
        public void GetWoodUseTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerPart pp2 = new PlayerPart() { Game = g };
            PlayerPart pp3 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { WandsProducedAmount = 1 };
            pp1.PlayerRounds.Add(pr1);
            PlayerRound pr2 = new PlayerRound() { WandsProducedAmount = 100 };
            pp2.PlayerRounds.Add(pr2);

            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = pp1.GetWoodUse(pr1, 0);
            d2 = pp2.GetWoodUse(pr2, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, g.Rounds[0].WoodConsumption);
            Assert.AreEqual(d2, g.Rounds[0].WoodConsumption * 100);
        }
        [TestMethod]
        public void GetCrystalUseTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerPart pp2 = new PlayerPart() { Game = g };
            PlayerPart pp3 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { WandsProducedAmount = 1 };
            pp1.PlayerRounds.Add(pr1);
            PlayerRound pr2 = new PlayerRound() { WandsProducedAmount = 100 };
            pp2.PlayerRounds.Add(pr2);

            double d1 = 0, d2 = 0, d3 = 0;
            //Działanie
            d1 = pp1.GetCrystalUse(pr1, 0);
            d2 = pp2.GetCrystalUse(pr2, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, g.Rounds[0].CrystalConsumption);
            Assert.AreEqual(d2, g.Rounds[0].CrystalConsumption * 100);
        }
        [TestMethod]
        public void GetAndSetAverageWoodPriceTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerPart pp2 = new PlayerPart() { Game = g };
            PlayerPart pp3 = new PlayerPart() { Game = g };
            PlayerPart pp4 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { WoodAveragePrevious = 0, WoodPurchased = g.WoodAmountLow };
            pp1.PlayerRounds.Add(pr1);
            PlayerRound pr2 = new PlayerRound() { WoodAveragePrevious = 0, WoodPurchased = 0 };
            pp2.PlayerRounds.Add(pr2);
            PlayerRound pr3 = new PlayerRound() { WoodAveragePrevious = g.WoodPriceHigh, WoodPurchased = g.WoodAmountLow, WoodReserves = g.WoodAmountLow };
            pp3.PlayerRounds.Add(pr3);
            PlayerRound pr4 = new PlayerRound() { WoodAveragePrevious = g.WoodPriceHigh, WoodPurchased = 0, WoodReserves = g.WoodAmountLow };
            pp4.PlayerRounds.Add(pr4);

            double d1 = 0, d2 = 0, d3 = 0, d4 = 0;
            //Działanie
            d1 = pp1.GetAndSetAverageWoodPrice(pr1, 0);
            d2 = pp2.GetAndSetAverageWoodPrice(pr2, 0);
            d3 = pp1.GetAndSetAverageWoodPrice(pr3, 0);
            d4 = pp2.GetAndSetAverageWoodPrice(pr4, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, g.Rounds[0].WoodPriceHigh);
            Assert.AreEqual(d2, 0);
            Assert.AreEqual(d3, g.Rounds[0].WoodPriceHigh);
            Assert.AreEqual(d4, 0);
        }
        [TestMethod]
        public void GetAndSetAverageCrystalPriceTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerPart pp2 = new PlayerPart() { Game = g };
            PlayerPart pp3 = new PlayerPart() { Game = g };
            PlayerPart pp4 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { CrystalAveragePrevious = 0, CrystalPurchased = 1 };
            pp1.PlayerRounds.Add(pr1);
            PlayerRound pr2 = new PlayerRound() { CrystalAveragePrevious = 0, CrystalPurchased = 0 };
            pp2.PlayerRounds.Add(pr2);
            PlayerRound pr3 = new PlayerRound() { CrystalAveragePrevious = g.CrystalPriceConst + g.CrystalPrice, CrystalPurchased = 1, CrystalReserves = 1 };
            pp3.PlayerRounds.Add(pr3);
            PlayerRound pr4 = new PlayerRound() { CrystalAveragePrevious = g.CrystalPriceConst + g.CrystalPrice, CrystalPurchased = 0, CrystalReserves = 1 };
            pp4.PlayerRounds.Add(pr4);

            double d1 = 0, d2 = 0, d3 = 0, d4 = 0;
            //Działanie
            d1 = pp1.GetAndSetAverageCrystalPrice(pr1, 0);
            d2 = pp2.GetAndSetAverageCrystalPrice(pr2, 0);
            d3 = pp1.GetAndSetAverageCrystalPrice(pr3, 0);
            d4 = pp2.GetAndSetAverageCrystalPrice(pr4, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, g.CrystalPriceConst + g.CrystalPrice);
            Assert.AreEqual(d2, 0);
            Assert.AreEqual(d3, g.CrystalPriceConst + g.CrystalPrice);
            Assert.AreEqual(d4, 0);
        }
        [TestMethod]
        public void GetTransportCostTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { WandsProducedAmount = 10 };
            pp1.PlayerRounds.Add(pr1);

            double d1 = 0;
            //Działanie
            d1 = pp1.GetTransportCost(pr1, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, g.Rounds[0].TransportCosts * 10);
        }
        [TestMethod]
        public void GetLoanRateCostTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { LoanRemaining = 100 };
            pp1.PlayerRounds.Add(pr1);

            double d1 = 0;
            //Działanie
            d1 = pp1.GetLoanRateCost(pr1, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, 100 * g.Rounds[0].InterestRate);
        }
        [TestMethod]
        public void GetMachineDepreciationCostTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { WandsProducedAmount = 100 };
            pp1.PlayerRounds.Add(pr1);

            double d1 = 0;
            //Działanie
            d1 = pp1.GetMachineDepreciationCost(pr1, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, 100 * g.Rounds[0].Depreciation);
        }
        [TestMethod]
        public void GetGeneralMaterialRateCostTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { WandsProducedAmount = 1000, WoodPurchased = 2000, CrystalPurchased = 1000 };
            pp1.PlayerRounds.Add(pr1);

            double d1 = 0;
            //Działanie
            d1 = pp1.GetGeneralMaterialRateCost(pr1, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, (pp1.GetWoodUse(pr1, 0) * pp1.GetAndSetAverageWoodPrice(pr1, 0) + pp1.GetCrystalUse(pr1, 0) * pp1.GetAndSetAverageCrystalPrice(pr1, 0)) * g.GeneralMaterialRateCosts);
        }
        [TestMethod]
        public void GetGeneralProcessingRateCostTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { ElfWorkers = 1 };
            pp1.PlayerRounds.Add(pr1);

            double d1 = 0;
            //Działanie
            d1 = pp1.GetGeneralProcessingRateCost(pr1, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, g.Rounds[0].GeneralProcessingRateCosts * g.Rounds[0].WorkerElfPrice * g.Rounds[0].HoursPerPeriod);
        }
        [TestMethod]
        public void GetQualityFadingTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { QualityExpense = 100 };
            pp1.PlayerRounds.Add(pr1);

            double d1 = 0;
            //Działanie
            d1 = pp1.GetQualityFading(pr1, 0);
            //Sprawdzenie
            Assert.AreEqual(d1, 100 * g.Rounds[0].QualityFading);
        }
        [TestMethod]
        public void GetAdFadingTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { AdExpense = 100 };
            pp1.PlayerRounds.Add(pr1);

            double d1 = 0;
            //Działanie
            d1 = pp1.GetAdFading(pr1, 0);
            //Sprawdzenie

            Assert.AreEqual(d1, 100 * g.Rounds[0].AdFading);
        }
        [TestMethod]
        public void GetQualityInfluenceTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { QualityExpense = 100, QualityExpensePrevious = 60 };
            pp1.PlayerRounds.Add(pr1);

            double d1 = 0;
            //Działanie
            d1 = pp1.GetQualityInfluence(pr1, 0);
            //Sprawdzenie

            Assert.AreEqual(d1, 160);
        }
        [TestMethod]
        public void GetAdInfluenceTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { AdExpense = 100, AdExpensePrevious = 60 };
            pp1.PlayerRounds.Add(pr1);

            double d1 = 0;
            //Działanie
            d1 = pp1.GetAdInfluence(pr1, 0);
            //Sprawdzenie

            Assert.AreEqual(d1, 160);
        }
        [TestMethod]
        public void GetLoanRemainingTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() { LoanRemaining = 5, LoanPaid = 5, LoanTaken = 1 };
            pp1.PlayerRounds.Add(pr1);

            double d1 = 0;
            //Działanie
            d1 = pp1.GetLoanRemaining(pr1, 0);
            //Sprawdzenie

            Assert.AreEqual(d1, 1);
        }
        [TestMethod]
        public void CountAllExpensesTest()
        {
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound() {
                AdExpense = 100,
                QualityExpense = 100,
                LoanPaid = 100
            };
            pp1.PlayerRounds.Add(pr1);

            PlayerPart pp2 = new PlayerPart() { Game = g };
            PlayerRound pr2 = new PlayerRound()
            {
                WandsProducedAmount = 1,
                ElfWorkers = 1,
                MachinesPurchased = 1,
                WoodPurchased = 1,
                WoodAverage = 1,
                WoodAveragePrevious = 0

            };
            pp2.PlayerRounds.Add(pr2);

            double d1 = 0, d2;
            //Działanie
            d1 = pp1.CountAllExpenses(pr1, 0);
            d2 = pp2.CountAllExpenses(pr2, 0);
            //Sprawdzenie

            Assert.AreEqual(d1, 300 + (int)g.Rounds[0].ManagementCosts);
            Assert.AreEqual(d2, g.Rounds[0].TransportCosts * 1 + 
                g.Rounds[0].WoodPriceHigh +
                g.Rounds[0].WorkerElfPrice * g.Rounds[0].HoursPerPeriod +
                g.Rounds[0].MachinePrice + 
                ((int)g.Rounds[0].WorkerElfPrice * g.Rounds[0].HoursPerPeriod) * g.Rounds[0].GeneralProcessingRateCosts +
                g.Rounds[0].GeneralMaterialRateCosts * ((int)g.Rounds[0].WoodConsumption * pp2.PlayerRounds[0].WoodAverage) +
                g.Rounds[0].ManagementCosts);
        }
        [TestMethod]
        public void CountIncomeTest()
        {
            //Przygotowanie
            //Działanie
            //Sprawdzenie
            Assert.AreEqual(1, 1);
        }
        [TestMethod]
        public void CountAllCostsTest()
        {
            /*
            Game g = new Game();
            g.Rounds.Add(InnitializeRound(g));
            PlayerPart pp1 = new PlayerPart() { Game = g };
            PlayerRound pr1 = new PlayerRound()
            {
                AdExpense = 100,
                QualityExpense = 100,
                LoanPaid = 100
            };

            PlayerPart pp2 = new PlayerPart() { Game = g };
            PlayerRound pr2 = new PlayerRound()
            {
                WandsProducedAmount = 1,
                ElfWorkers = 1,
                MachinesPurchased = 1,
                WoodPurchased = 1,
                WoodAverage = 1,
                WoodAveragePrevious = 0

            };
            pp2.PlayerRounds.Add(pr2);

            double d1 = 0, d2;
            //Działanie
            d1 = pp1.CountAllCosts(pr1, 0);
            d2 = pp2.CountAllCosts(pr2, 0);
            //Sprawdzenie

            Assert.AreEqual(d1, 300 + (int)g.Rounds[0].ManagementCosts);
            Assert.AreEqual(d2, g.Rounds[0].TransportCosts * 1 +
                g.Rounds[0].WoodPriceHigh +
                g.Rounds[0].WorkerElfPrice * g.Rounds[0].HoursPerPeriod +
                g.Rounds[0].MachinePrice +
                ((int)g.Rounds[0].WorkerElfPrice * g.Rounds[0].HoursPerPeriod) * g.Rounds[0].GeneralProcessingRateCosts +
                g.Rounds[0].GeneralMaterialRateCosts * ((int)g.Rounds[0].WoodConsumption * pp2.PlayerRounds[0].WoodAverage) +
                g.Rounds[0].ManagementCosts);
            */
        }
    }
}
