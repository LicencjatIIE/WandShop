using System.ComponentModel.DataAnnotations;
using System;

namespace Logic.Entities
{
    /// <summary>
    /// Klasa zawierająca wszystkie dane rundy
    /// </summary>
    [Serializable]
    public class PlayerRound
    {
        public int PlayerRoundId { get; set; }

        //For Ef
        public int PlayerPartId { get; set; }
        public virtual PlayerPart PlayerPart { get; set; }

        public int PlayerRoundNumber { get; set; } = 1;

        #region Variables
        public double Gold { get; set; }
        public double LoanRemaining { get; set; }

        public int WoodReserves { get; set; }
        public int CrystalReserves { get; set; }

        public int WoodPurchased { get; set; }
        public int CrystalPurchased { get; set; }

        public double WoodAverage { get; set; }
        public double CrystalAverage { get; set; }
        public double WoodAveragePrevious { get; set; }
        public double CrystalAveragePrevious { get; set; }

        
        public int MachinesOwned { get; set; }
        
        public int MachinesPurchased { get; set; }
        
        public int MachinesSold { get; set; }

        
        public int DwarfWorkers { get; set; }
        public int ElfWorkers { get; set; }
        public int HumanWorkers { get; set; }
        public int DwarfWorkersEmployed { get; set; }
        public int ElfWorkersEmployed { get; set; }
        public int HumanWorkersEmployed { get; set; }
        public int DwarfWorkersDismissed { get; set; }
        public int ElfWorkersDismissed { get; set; }
        public int HumanWorkersDismissed { get; set; }

        public double QualityExpense { get; set; }
        public double AdExpense { get; set; }
        public double QualityExpensePrevious { get; set; }
        public double AdExpensePrevious { get; set; }

        public double LoanPaid { get; set; }
        public double LoanTaken { get; set; }
        
        public double WandPrice { get; set; }
        public int WandsProducedAmount { get; set; }

        public double Income { get; set; }
        public int WandsSoldAmount { get; set; }
        public int WandsReservesAmount { get; set; }
        #endregion
        
    }
}