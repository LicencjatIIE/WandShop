using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Abstract;
using Logic.Entities;

namespace Logic.Concrete
{
    public class EfRoundRepository : IRoundRepository
    {
        private EfWandShopContext context = new EfWandShopContext();
        public IEnumerable<Round> Rounds { get { return context.Rounds; } }

        public void SaveRound(int gameId, Round round)
        {
            if (round.RoundId == 0)
            {
                Game g = context.Games.Find(gameId);
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
                    //,Segments = g.GenerateSegments()
                };
                context.Rounds.Add(r);
            }
            else
            {
                Round dbEntry = context.Rounds.Find(round.RoundId);
                if (dbEntry != null)
                {
                    dbEntry.AdFading = round.AdFading;
                    dbEntry.CrystalConsumption = round.CrystalConsumption;
                    dbEntry.CrystalEfficiency = round.CrystalEfficiency;
                    dbEntry.CrystalPrice = round.CrystalPrice;
                    dbEntry.CrystalPriceConst = round.CrystalPriceConst;
                    dbEntry.Depreciation = round.Depreciation;
                    dbEntry.DismissalPrice = round.DismissalPrice;
                    dbEntry.Dividend = round.Dividend;
                    dbEntry.EmploymentPrice = round.EmploymentPrice;
                    dbEntry.GeneralMaterialRateCosts = round.GeneralMaterialRateCosts;
                    dbEntry.GeneralProcessingRateCosts = round.GeneralProcessingRateCosts;
                    dbEntry.HoursPerPeriod = round.HoursPerPeriod;
                    dbEntry.InterestRate = round.InterestRate;
                    dbEntry.MachinePrice = round.MachinePrice;
                    dbEntry.ManagementCosts = round.ManagementCosts;
                    dbEntry.MaxCrystalEfficiency = round.MaxCrystalEfficiency;
                    dbEntry.PerfectGemConsumption = round.PerfectGemConsumption;
                    dbEntry.PerfectStickConsumption = round.PerfectStickConsumption;
                    dbEntry.QualityFading = round.QualityFading;
                    dbEntry.Tax = round.Tax;
                    dbEntry.TransportCosts = round.TransportCosts;
                    dbEntry.WoodAmountHigh = round.WoodAmountHigh;
                    dbEntry.WoodAmountLow = round.WoodAmountLow;
                    dbEntry.WoodConsumption = round.WoodConsumption;
                    dbEntry.WoodPriceHigh = round.WoodPriceHigh;
                    dbEntry.WoodPriceLow = round.WoodPriceLow;
                    dbEntry.WoodPriceMedium = round.WoodPriceMedium;
                    dbEntry.WorkerDwarfEfficiency = round.WorkerDwarfEfficiency;
                    dbEntry.WorkerDwarfPrice = round.WorkerDwarfPrice;
                    dbEntry.WorkerElfEfficiency = round.WorkerElfEfficiency;
                    dbEntry.WorkerElfPrice = round.WorkerElfPrice;
                    dbEntry.WorkerHumanEfficiency = round.WorkerHumanEfficiency;
                    dbEntry.WorkerHumanPrice = round.WorkerHumanPrice;
                    //dbEntry.Segments = round.UpdateSegments();
                }
            }
            context.SaveChanges();
        }
    }
}
