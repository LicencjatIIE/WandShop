using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Entities
{
    public class Round
    {
        public int RoundId { get; set; }

        //For Ef
        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        #region Variables
        public double Tax { get; set; }
        public double Dividend { get; set; }

        public double InterestRate { get; set; }

        public double WoodPriceLow { get; set; }
        public double WoodPriceMedium { get; set; }
        public double WoodPriceHigh { get; set; }
        public double CrystalPrice { get; set; }
        public double CrystalPriceConst { get; set; }
        public double WoodConsumption { get; set; }
        public double CrystalConsumption { get; set; }
        public double PerfectStickConsumption { get; set; }
        public double PerfectGemConsumption { get; set; }
        public int WoodAmountLow { get; set; }
        public int WoodAmountHigh { get; set; }

        public int HoursPerPeriod { get; set; }
        public double EmploymentPrice { get; set; }
        public double DismissalPrice { get; set; }
        public double WorkerDwarfPrice { get; set; }
        public double WorkerElfPrice { get; set; }
        public double WorkerHumanPrice { get; set; }
        public double WorkerDwarfEfficiency { get; set; }
        public double WorkerElfEfficiency { get; set; } 
        public double WorkerHumanEfficiency { get; set; }

        public double MachinePrice { get; set; }
        public int CrystalEfficiency { get; set; }
        public double Depreciation { get; set; } 
        public double MaxCrystalEfficiency { get; set; } 

        public double QualityFading { get; set; } 
        public double AdFading { get; set; }
        public double ManagementCosts { get; set; } 
        public double TransportCosts { get; set; } 

        public double GeneralMaterialRateCosts { get; set; } 
        public double GeneralProcessingRateCosts { get; set; }
        #endregion
    }
}
