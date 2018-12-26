using Logic.Abstract;
using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Concrete
{
    public class EfGameRepository : IGameRepository
    {
        private EfWandShopContext context = new EfWandShopContext();
        public IEnumerable<Game> Games { get { return context.Games; } }

        public Game DeleteGame(int gameId)
        {
            Game g = context.Games.Find(gameId);
            if (g != null)
            {
                context.Games.Remove(g);
                context.SaveChanges();
            }
            return g;
        }

        public Game SaveGame(Game game)
        {
            if (game.GameId == 0)
            {
                context.Games.Add(game);
            }
            else
            {
                Game dbEntry = context.Games.Find(game.GameId);
                if (dbEntry != null)
                {
                    dbEntry.Rounds = game.Rounds;
                    dbEntry.MaxRounds = game.MaxRounds;
                    dbEntry.CurrentRound = game.CurrentRound;
                    dbEntry.PlayersPart = game.PlayersPart;
                    dbEntry.Loan = game.Loan;
                    dbEntry.InterestRate = game.InterestRate;
                    dbEntry.BuildingCost = game.BuildingCost;
                    dbEntry.WoodPriceLow = game.WoodPriceLow;
                    dbEntry.WoodPriceMedium = game.WoodPriceMedium;
                    dbEntry.WoodPriceHigh = game.WoodPriceHigh;
                    dbEntry.CrystalPrice = game.CrystalPrice;
                    dbEntry.CrystalPriceConst = game.CrystalPriceConst;
                    dbEntry.WoodConsumption = game.WoodConsumption;
                    dbEntry.CrystalConsumption = game.CrystalConsumption;
                    dbEntry.PerfectStickConsumption = game.PerfectStickConsumption;
                    dbEntry.PerfectGemConsumption = game.PerfectGemConsumption;
                    dbEntry.WoodAmountLow = game.WoodAmountLow;
                    dbEntry.WoodAmountHigh = game.WoodAmountHigh;
                    dbEntry.HoursPerPeriod = game.HoursPerPeriod;
                    dbEntry.EmploymentPrice = game.EmploymentPrice;
                    dbEntry.DismissalPrice = game.DismissalPrice;
                    dbEntry.WorkerDwarfPrice = game.WorkerDwarfPrice;
                    dbEntry.WorkerElfPrice = game.WorkerElfPrice;
                    dbEntry.WorkerHumanPrice = game.WorkerHumanPrice;
                    dbEntry.WorkerDwarfEfficiency = game.WorkerDwarfEfficiency;
                    dbEntry.WorkerElfEfficiency = game.WorkerElfEfficiency;
                    dbEntry.WorkerHumanEfficiency = game.WorkerHumanEfficiency;
                    dbEntry.MachinePrice = game.MachinePrice;
                    dbEntry.CrystalEfficiency = game.CrystalEfficiency;
                    dbEntry.Depreciation = game.Depreciation;
                    dbEntry.MaxCrystalEfficiency = game.MaxCrystalEfficiency;
                    dbEntry.QualityFading = game.QualityFading;
                    dbEntry.AdFading = game.AdFading;
                    dbEntry.ManagementCosts = game.ManagementCosts;
                    dbEntry.TransportCosts = game.TransportCosts;
                    dbEntry.GeneralMaterialRateCosts = game.GeneralMaterialRateCosts;
                    dbEntry.GeneralProcessingRateCosts = game.GeneralProcessingRateCosts;
                }
            }
            context.SaveChanges();
            return game;
        }
    }
}
