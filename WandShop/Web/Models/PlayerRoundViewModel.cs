using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class PlayerRoundViewModel
    {
        public int PlayerRoundNumber { get; set; }
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
        
        public PlayerRoundViewModel(PlayerRound pr)
        {
            PlayerRoundNumber = pr.PlayerRoundNumber;
            Gold = pr.Gold;
            LoanRemaining = pr.LoanRemaining;
            WoodReserves = pr.WoodReserves;
            CrystalReserves = pr.CrystalReserves;
            WoodPurchased = pr.WoodPurchased;
            CrystalPurchased = pr.CrystalPurchased;
            WoodAverage = pr.WoodAverage;
            CrystalAverage = pr.CrystalAverage;
            WoodAveragePrevious = pr.WoodAveragePrevious;
            CrystalAveragePrevious = pr.CrystalAveragePrevious;
            MachinesOwned = pr.MachinesOwned;
            MachinesPurchased = pr.MachinesPurchased;
            MachinesSold = pr.MachinesSold;
            DwarfWorkers = pr.DwarfWorkers;
            ElfWorkers = pr.ElfWorkers;
            HumanWorkers = pr.HumanWorkers;
            DwarfWorkersEmployed = pr.DwarfWorkersEmployed;
            ElfWorkersEmployed = pr.ElfWorkersEmployed;
            HumanWorkersEmployed = pr.HumanWorkersEmployed;
            DwarfWorkersDismissed = pr.DwarfWorkersDismissed;
            ElfWorkersDismissed = pr.ElfWorkersDismissed;
            HumanWorkersDismissed = pr.HumanWorkersDismissed;
            QualityExpense = pr.QualityExpense;
            AdExpense = pr.AdExpense;
            QualityExpensePrevious = pr.QualityExpensePrevious;
            AdExpensePrevious = pr.AdExpensePrevious;
            LoanPaid = pr.LoanPaid;
            LoanTaken = pr.LoanTaken;
            WandPrice = pr.WandPrice;
            WandsProducedAmount = pr.WandsProducedAmount;
            Income = pr.Income;
            WandsSoldAmount = pr.WandsSoldAmount;
            WandsReservesAmount = pr.WandsReservesAmount;
        }
    }
}