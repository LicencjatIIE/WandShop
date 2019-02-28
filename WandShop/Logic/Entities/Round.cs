using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Entities
{
    [Serializable]
    public class Round
    {
        public int RoundId { get; set; }

        //For Ef
        public int GameId { get; set; }
        public virtual Game Game { get; set; }
        //public virtual List<Segment> Segments { get; set; }

        ////Tę funkcję należy dograć!
        //public List<Segment> UpdateSegments()
        //{
        //    double propSum = 0;
        //    double sum = 0;
        //    int left = 0;
        //    int diff = 2500;
        //    List<Segment> segments = new List<Segment>();

        //    foreach(var item in Segments)
        //    {
        //        segments.Add(new Segment()
        //        {
        //            AdsRatio = item.AdsRatio,
        //            AdsRatioPlayers = item.AdsRatioPlayers,
        //            Name = item.Name,
        //            NothingRatio = item.NothingRatio,
        //            QualityRatio = item.QualityRatio,
        //            WandAmountRatio = item.WandAmountRatio,
        //            WandPriceRatio = item.WandPriceRatio,
        //            Round = item.Round,
        //            RoundId = item.RoundId,
        //            Size = item.Size
        //        });
        //        propSum += item.AdsRatio;
        //    }
        //    for (int i = 0; i < Game.PlayersPart.Count(); i++)
        //    {
        //        sum += Game.PlayersPart[i].PlayerRounds[Game.PlayersPart[i].CurrentRound].AdExpense + 
        //            Game.PlayersPart[i].PlayerRounds[Game.PlayersPart[i].CurrentRound].AdExpensePrevious;
        //    }
        //    sum = Math.Round(sum / diff);
        //    left = (int)sum;
        //    if (left > 0)
        //    {
        //        for (int i = 0; i < segments.Count(); i++)
        //        {
        //            int help = (int)Math.Round(sum * Math.Round(segments[i].AdsRatio / propSum,2));
        //            if (0 <= left - help)
        //            {
        //                left -= help;
        //                segments[i].Size += help * 10;
        //            }
        //            else
        //            {
        //                segments[i].Size += left * 10;
        //                break;
        //            }
        //        }
        //    }
        //    return segments;
        //}

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
