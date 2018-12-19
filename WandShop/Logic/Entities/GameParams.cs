using System;
using System.ComponentModel.DataAnnotations;

namespace Logic.Entities
{
    public class GameParams
    {
        public double Tax { get; set; } = 0.4;
        public double Dividend { get; set; } = 0.5;
        public double OwnContribution { get; set; } = 2400000;
        public double ForeignShares { get; set; } = 1000000;
        
        public double Loan { get; set; } = 600000;
        public double InterestRate { get; set; } = 0.08;
        public double BuildingCost { get; set; } = 1000000;

        public double WoodPriceLow { get; set; } = 11;
        public double WoodPriceMedium { get; set; } = 13;
        public double WoodPriceHigh { get; set; } = 15;
        public double CrystalPrice { get; set; } = 180;
        public double CrystalPriceConst { get; set; } = 180000;
        public double WoodConsumption { get; set; } = 2.0;
        public double CrystalConsumption { get; set; } = 1.0;
        public double PerfectStickConsumption { get; set; } = 1.0;
        public double PerfectGemConsumption { get; set; } = 1.0;
        public int WoodAmountLow { get; set; } = 30000;
        public int WoodAmountHigh { get; set; } = 100000;

        public int HoursPerPeriod { get; set; } = 640;
        public double EmploymentPrice { get; set; } = 1200;
        public double DismissalPrice { get; set; } = 1000;
        public double WorkerDwarfPrice { get; set; } = 13;
        public double WorkerElfPrice { get; set; } = 8;
        public double WorkerHumanPrice { get; set; } = 10;
        public double WorkerDwarfEfficiency { get; set; } = 1.0;
        public double WorkerElfEfficiency { get; set; } = 2.0;
        public double WorkerHumanEfficiency { get; set; } = 0.5;

        public double MachinePrice { get; set; } = 115200;
        public int CrystalEfficiency { get; set; } = 1280;
        public double Depreciation { get; set; } = 3;
        public double MaxCrystalEfficiency { get; set; } = 38400;

        public double QualityFading { get; set; } = 0.6;
        public double AdFading { get; set; } = 0.4;
        public double ManagementCosts { get; set; } = 200000;
        public double TransportCosts { get; set; } = 10;

        public double GeneralMaterialRateCosts { get; set; } = 0.1;
        public double GeneralProcessingRateCosts { get; set; } = 0.5;
        
        public GameParams() { }
    }
}