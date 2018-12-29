using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Entities;
using Logic.Abstract;

namespace Logic.Concrete
{
    public class EfPlayerRoundRepository : IPlayerRoundRepository
    {
        private EfWandShopContext context = new EfWandShopContext();
        public IEnumerable<PlayerRound> PlayerRounds { get { return context.PlayerRounds; } }

        public PlayerRound SavePlayerRound(int playerPartId, PlayerRound playerRound)
        {
            if (playerRound.PlayerRoundId == 0)
            {
                PlayerPart pP = context.PlayerParts.Find(playerPartId);
                if (pP != null)
                {
                    PlayerRound dbEntry = playerRound;
                    dbEntry.PlayerPartId = pP.PlayerPartId;
                    dbEntry.PlayerPart = pP;
                    context.PlayerRounds.Add(playerRound);
                    context.SaveChanges();
                    return dbEntry;
                }
            }
            else
            {
                PlayerPart pP = context.PlayerParts.Find(playerPartId);
                PlayerRound dbEntry = context.PlayerRounds.Find(playerRound.PlayerRoundId);
                if (dbEntry != null)
                {
                    dbEntry.AdExpense = playerRound.AdExpense;
                    dbEntry.AdExpensePrevious = playerRound.AdExpensePrevious;
                    dbEntry.CrystalAverage = playerRound.CrystalAverage;
                    dbEntry.CrystalAveragePrevious = playerRound.CrystalAveragePrevious;
                    dbEntry.CrystalReserves = playerRound.CrystalReserves;
                    dbEntry.CrystalPurchased = playerRound.CrystalPurchased;
                    dbEntry.DwarfWorkers = playerRound.DwarfWorkers;
                    dbEntry.DwarfWorkersDismissed = playerRound.DwarfWorkersDismissed;
                    dbEntry.DwarfWorkersEmployed = playerRound.DwarfWorkersEmployed;
                    dbEntry.ElfWorkers = playerRound.ElfWorkers;
                    dbEntry.ElfWorkersDismissed = playerRound.ElfWorkersDismissed;
                    dbEntry.ElfWorkersEmployed = playerRound.ElfWorkersEmployed;
                    dbEntry.Gold = playerRound.Gold;
                    dbEntry.HumanWorkers = playerRound.HumanWorkers;
                    dbEntry.HumanWorkersDismissed = playerRound.HumanWorkersDismissed;
                    dbEntry.HumanWorkersEmployed = playerRound.HumanWorkersEmployed;
                    dbEntry.Income = playerRound.Income;
                    dbEntry.LoanPaid = playerRound.LoanPaid;
                    dbEntry.LoanRemaining = playerRound.LoanRemaining;
                    dbEntry.LoanTaken = playerRound.LoanTaken;
                    dbEntry.MachinesOwned = playerRound.MachinesOwned;
                    dbEntry.MachinesPurchased = playerRound.MachinesPurchased;
                    dbEntry.MachinesSold = playerRound.MachinesSold;
                    dbEntry.QualityExpense = playerRound.QualityExpense;
                    dbEntry.QualityExpensePrevious = playerRound.QualityExpensePrevious;
                    dbEntry.WandPrice = playerRound.WandPrice;
                    dbEntry.WandsProducedAmount = playerRound.WandsProducedAmount;
                    dbEntry.WandsReservesAmount = playerRound.WandsReservesAmount;
                    dbEntry.WandsSoldAmount = playerRound.WandsSoldAmount;
                    dbEntry.WoodAverage = playerRound.WoodAverage;
                    dbEntry.WoodAveragePrevious = playerRound.WoodAveragePrevious;
                    dbEntry.WoodPurchased = playerRound.WoodPurchased;
                    dbEntry.WoodReserves = playerRound.WoodReserves;
                    dbEntry.PlayerRoundNumber = playerRound.PlayerRoundNumber;
                    dbEntry.PlayerPartId = pP.PlayerPartId;
                    dbEntry.PlayerPart = pP;
                    context.SaveChanges();
                    return dbEntry;
                }
            }
            return new PlayerRound();
        }

    }
}
