using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Entities;
using Web.Controllers;
using System.Collections.Generic;

namespace Test
{
    [TestClass]
    public class JourneyControllerTest
    {
        [TestMethod]
        public void SetStartingRoundTest()
        {
            Game g = new Game();
            PlayerPart pP = new PlayerPart()
            {
                Game = g
            };

            PlayerRound pr = JourneyController.SetStartingRound(pP);

            Assert.AreEqual(pr.Gold, g.ForeignShares + g.Loan + g.OwnContribution - g.BuildingCost);
            Assert.AreEqual(pr.LoanRemaining, g.Loan);
            Assert.AreEqual(pr.WoodReserves, 0);
            Assert.AreEqual(pr.CrystalReserves, 0);
            Assert.AreEqual(pr.WoodPurchased, 0);
            Assert.AreEqual(pr.CrystalPurchased, 0);
            Assert.AreEqual(pr.WoodAverage, 0);
            Assert.AreEqual(pr.CrystalAverage, 0);
            Assert.AreEqual(pr.WoodAveragePrevious, 0);
            Assert.AreEqual(pr.CrystalAveragePrevious, 0);
            Assert.AreEqual(pr.MachinesOwned, 0);
            Assert.AreEqual(pr.MachinesPurchased, 0);
            Assert.AreEqual(pr.MachinesSold, 0);
            Assert.AreEqual(pr.DwarfWorkers, 0);
            Assert.AreEqual(pr.ElfWorkers, 0);
            Assert.AreEqual(pr.HumanWorkers, 0);
            Assert.AreEqual(pr.DwarfWorkersEmployed, 0);
            Assert.AreEqual(pr.ElfWorkersEmployed, 0);
            Assert.AreEqual(pr.HumanWorkersEmployed, 0);
            Assert.AreEqual(pr.DwarfWorkersDismissed, 0);
            Assert.AreEqual(pr.ElfWorkersDismissed, 0);
            Assert.AreEqual(pr.HumanWorkersDismissed, 0);
            Assert.AreEqual(pr.QualityExpense, 0);
            Assert.AreEqual(pr.AdExpense, 0);
            Assert.AreEqual(pr.QualityExpensePrevious, 0);
            Assert.AreEqual(pr.AdExpensePrevious, 0);
            Assert.AreEqual(pr.LoanPaid, 0);
            Assert.AreEqual(pr.LoanTaken, 0);
            Assert.AreEqual(pr.WandsProducedAmount, 0);
            Assert.AreEqual(pr.WandPrice, 0);
            Assert.AreEqual(pr.WandsSoldAmount, 0);
            Assert.AreEqual(pr.Income, 0);
            Assert.AreEqual(pr.WandsReservesAmount, 0);
            Assert.AreEqual(pr.PlayerRoundNumber, 1);
        }
        [TestMethod]
        public void FinishRoundTest()
        {
            Game g = new Game();
            PlayerPart pP = new PlayerPart()
            {
                Game = g
            };
            pP.PlayerRounds.Add(new PlayerRound()
            {
                Gold = 1,
                LoanRemaining = 1,
                WoodReserves = 1,
                CrystalReserves = 1,
                WoodPurchased = 0,
                CrystalPurchased = 0,
                WoodAverage = 0,
                CrystalAverage = 0,
                WoodAveragePrevious = 0,
                CrystalAveragePrevious = 0
            });

            PlayerRound pr = JourneyController.FinishRound(pP);

            Assert.AreEqual(pr.CrystalAverage, 0);
            Assert.AreEqual(pr.WoodAverage, 0);
            Assert.AreEqual(pr.Gold, 1);
        }
        [TestMethod]
        public void SetNextRound()
        {
            Game g = new Game()
            {
                Rounds = new List<Round>() { new Round() }
            };
            PlayerPart pP = new PlayerPart()
            {
                Game = g
            };
            pP.PlayerRounds.Add(new PlayerRound()
            {
                Gold = 0,
                LoanRemaining = 1000
            });

            PlayerRound pr = JourneyController.SetNextRound(pP, pP.PlayerRounds[0]);

            Assert.AreEqual(pr.Gold, pP.CountRemainingGold(pP.PlayerRounds[0],0));
        }
    }
}
