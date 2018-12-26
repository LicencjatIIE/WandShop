using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models
{
    public class PlayerPlayRoundModel
    {

        [HiddenInput(DisplayValue = false)]
        public int PlayerPartId { get; set; }

        #region ToEdit
        public int WoodPurchased { get; set; }
        public int CrystalPurchased { get; set; }
        public int MachinesPurchased { get; set; }
        public int DwarfWorkersEmployed { get; set; }
        public int ElfWorkersEmployed { get; set; }
        public int HumanWorkersEmployed { get; set; }
        public int DwarfWorkersDismissed { get; set; }
        public int ElfWorkersDismissed { get; set; }
        public int HumanWorkersDismissed { get; set; }
        public double QualityExpense { get; set; }
        public double AdExpense { get; set; }
        public double LoanPaid { get; set; }
        public double LoanTaken { get; set; }
        public double WandPrice { get; set; }
        public int WandsProducedAmount { get; set; }
        #endregion

        #region ToView
        public double Gold { get; }
        public double GoldRemaining { get { return Gold - GoldPaid; } }
        public double GoldPaid { get; private set; }
        public double LoanRemaining { get;  }
        public int WoodReserves { get;  }
        public int CrystalReserves { get;  }
        public int MachinesOwned { get;  }
        public int DwarfWorkers { get;  }
        public int ElfWorkers { get;  }
        public int HumanWorkers { get;  }
        public int WandsReservesAmount { get;  }
        public PlayerPart PlayerPart { get; }
        #endregion

        public double GoldPaidAfterSavingDb(int currentRound)
        {
            GoldPaid = PlayerPart.CountAllExpenses(PlayerPart.PlayerRounds[currentRound], currentRound);
            return GoldPaid;
        }
        public PlayerPlayRoundModel() { }
        public PlayerPlayRoundModel(PlayerPart playerPart)
        {
            PlayerPart = playerPart;
            WoodPurchased = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].WoodPurchased;
            CrystalPurchased = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].CrystalPurchased;
            MachinesPurchased = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].MachinesPurchased;
            DwarfWorkersEmployed = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].DwarfWorkersEmployed;
            ElfWorkersEmployed = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].ElfWorkersEmployed;
            HumanWorkersEmployed = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].HumanWorkersEmployed;
            DwarfWorkersDismissed = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].DwarfWorkersDismissed;
            ElfWorkersDismissed = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].ElfWorkersDismissed;
            HumanWorkersDismissed = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].HumanWorkersDismissed;
            QualityExpense = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].QualityExpense;
            AdExpense = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].AdExpense;
            LoanPaid = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].LoanPaid;
            LoanTaken = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].LoanTaken;
            WandPrice = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].WandPrice;
            WandsProducedAmount = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].WandsProducedAmount;
            Gold = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].Gold;
            LoanRemaining = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].LoanRemaining;
            WoodReserves = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].WoodReserves;
            CrystalReserves = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].CrystalReserves;
            MachinesOwned = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].MachinesOwned;
            DwarfWorkers = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].DwarfWorkers;
            ElfWorkers = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].ElfWorkers;
            HumanWorkers = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].HumanWorkers;
            WandsReservesAmount = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].WandsReservesAmount;
            PlayerPartId = PlayerPart.PlayerPartId;
        }
        public PlayerPlayRoundModel(PlayerPart playerPart, PlayerPlayRoundModel playerPlayRoundModel)
        {
            PlayerPart = playerPart;
            WoodPurchased = playerPlayRoundModel.WoodPurchased;
            CrystalPurchased = playerPlayRoundModel.CrystalPurchased;
            MachinesPurchased = playerPlayRoundModel.MachinesPurchased;
            DwarfWorkersEmployed = playerPlayRoundModel.DwarfWorkersEmployed;
            ElfWorkersEmployed = playerPlayRoundModel.ElfWorkersEmployed;
            HumanWorkersEmployed = playerPlayRoundModel.HumanWorkersEmployed;
            DwarfWorkersDismissed = playerPlayRoundModel.DwarfWorkersDismissed;
            ElfWorkersDismissed = playerPlayRoundModel.ElfWorkersDismissed;
            HumanWorkersDismissed = playerPlayRoundModel.HumanWorkersDismissed;
            QualityExpense = playerPlayRoundModel.QualityExpense;
            AdExpense = playerPlayRoundModel.AdExpense;
            LoanPaid = playerPlayRoundModel.LoanPaid;
            LoanTaken = playerPlayRoundModel.LoanTaken;
            WandPrice = playerPlayRoundModel.WandPrice;
            WandsProducedAmount = playerPlayRoundModel.WandsProducedAmount;
            Gold = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].Gold;
            LoanRemaining = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].LoanRemaining;
            WoodReserves = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].WoodReserves;
            CrystalReserves = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].CrystalReserves;
            MachinesOwned = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].MachinesOwned;
            DwarfWorkers = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].DwarfWorkers;
            ElfWorkers = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].ElfWorkers;
            HumanWorkers = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].HumanWorkers;
            WandsReservesAmount = PlayerPart.PlayerRounds[PlayerPart.CurrentRound].WandsReservesAmount;
            PlayerPartId = PlayerPart.PlayerPartId;
        }
    }
}