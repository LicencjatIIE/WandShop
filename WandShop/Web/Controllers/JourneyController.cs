using Logic.Abstract;
using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class JourneyController : Controller
    {
        private IGameRepository gameRepository;
        private IPlayerPartRepository playerPartRepository;
        private IPlayerRepository playerRepository;
        private IPlayerRoundRepository playerRoundRepository;
        private IRoundRepository roundRepository;

        public JourneyController(
            IGameRepository gameRepository,
            IPlayerPartRepository playerPartRepository,
            IPlayerRepository playerRepository,
            IPlayerRoundRepository playerRoundRepository,
            IRoundRepository roundRepository)
        {
            this.gameRepository = gameRepository;
            this.playerPartRepository = playerPartRepository;
            this.playerRepository = playerRepository;
            this.playerRoundRepository = playerRoundRepository;
            this.roundRepository = roundRepository;
        }

        public ActionResult Index()
        {
            GameListViewModel model = new GameListViewModel() { Games = gameRepository.Games.ToList() };
            
            return View(model);
        }
        public ActionResult CreateFullGame()
        {
            return View(new CreateFullGameModel());
        }
        [HttpPost]
        public ActionResult CreateFullGame(CreateFullGameModel model)
        {
            if (ModelState.IsValid)
            {
                Game game = SaveGame(model.ConvertToGame());
                SavePlayers(SavePlayerPartsList(game.GameId, model.PlayersNumber));
                return RedirectToAction("CreateGameParam", new { gameId = game.GameId });
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult CreateGameParam(int gameId)
        {
            return View(new CreateGameParamModel());
        }
        [HttpPost]
        public ActionResult CreateGameParam(CreateGameParamModel model)
        {
            if (ModelState.IsValid)
            {
                SaveGame(SaveGameParams(model));
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult Check(int gameId)
        {
            return View(new GameViewModel() { Game = gameRepository.Games.FirstOrDefault(x => x.GameId == gameId) });
        }

        public ActionResult Begin(int gameId)
        {
            if (gameId != 0)
            {
                Game game = gameRepository.Games.FirstOrDefault(x => x.GameId == gameId);
                if (game != null)
                {
                    if (game.CurrentRound == 0)
                    {
                        BeginGame(game);
                    }
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult NextRound(int gameId)
        {
            if (gameId != 0)
            {
                Game game = gameRepository.Games.FirstOrDefault(x => x.GameId == gameId);
                if (game != null)
                {
                    if (game.CurrentRound < game.MaxRounds && game.CurrentRound > 0)
                    {
                        NextRound(game);
                    }
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult FinishGame(int gameId)
        {
            if (gameId != 0)
            {
                Game game = gameRepository.Games.FirstOrDefault(x => x.GameId == gameId);
                if (game != null)
                {
                    if (game.CurrentRound == game.MaxRounds)
                    {
                        FinishGame(game);
                    }
                }
            }
            return RedirectToAction("Index");
        }


        public Game BeginGame(Game game)
        {
            SaveRound(game.GameId);

            foreach (var pP in game.PlayersPart)
            {
                pP.CurrentRound = 0;
                SavePlayerPart(pP);
                SavePlayerRound(pP, SetStartingRound(pP));
            }
            return SaveGame(UpdateCurrentGameRoundNumber(game));
        }
        private Game NextRound(Game game)
        {
            SaveRound(game.GameId);
            foreach (var pP in game.PlayersPart)
            {
                PlayerRound oldPlayerRound = SavePlayerRound(pP, FinishRound(pP));
                SavePlayerPart(pP);
                PlayerRound newPlayerRound = SavePlayerRound(pP, new PlayerRound());
                newPlayerRound.WandsReservesAmount = oldPlayerRound.WandsReservesAmount;
                SavePlayerRound(pP, SetNextRound(pP, newPlayerRound));
                SavePlayerPart(UpdateCurrentPlayerRoundNumber(pP));
            }
            return SaveGame(UpdateCurrentGameRoundNumber(game));
        }
        private Game FinishGame(Game game)
        {
            foreach (var pP in game.PlayersPart)
            {
                SavePlayerRound(pP, FinishRound(pP));
                playerRepository.DeletePlayer(pP.PlayerPartId);
            }
            Game model = gameRepository.DeleteGame(game.GameId);

            return model;
        }

        #region Helpers

        public Game SaveGame(Game game)
        {
            return gameRepository.SaveGame(game);
        }
        public List<PlayerPart> SavePlayerPartsList(int gameId, int playersNumber)
        {
            return playerPartRepository.AddPlayerParts(gameId, playersNumber).ToList();
        }
        public void SavePlayers(List<PlayerPart> list)
        {
            for (int i = 0; i < list.Count; i++)
            {
                playerRepository.SavePlayer(list[i].PlayerPartId, new Player());
            }
        }
        public Game SaveGameParams(CreateGameParamModel cgpm)
        {
            Game game = gameRepository.Games.FirstOrDefault(x => x.GameId == cgpm.GameId);
            game.Tax = cgpm.Tax;
            game.Dividend = cgpm.Dividend;
            game.OwnContribution = cgpm.OwnContribution;
            game.ForeignShares = cgpm.ForeignShares;
            game.Loan = cgpm.Loan;
            game.InterestRate = cgpm.InterestRate;
            game.ManagementCosts = cgpm.ManagementCosts;
            game.TransportCosts = cgpm.TransportCosts;
            return game;
        }
        public PlayerPart SavePlayerPart(PlayerPart pP)
        {
            return playerPartRepository.SavePlayerPart(pP);
        }
        public PlayerRound SavePlayerRound(PlayerPart pP, PlayerRound round)
        {
            return playerRoundRepository.SavePlayerRound(pP.PlayerPartId, round);
        }
        public void SaveRound(int gameId)
        {
            roundRepository.SaveRound(gameId, new Round());
        }

        public static PlayerRound SetStartingRound(PlayerPart pP)
        {
            return new PlayerRound()
            {
                Gold = pP.Game.OwnContribution + pP.Game.Loan + pP.Game.ForeignShares - pP.Game.BuildingCost,
                LoanRemaining = pP.Game.Loan,
                WoodReserves = 0,
                CrystalReserves = 0,
                WoodPurchased = 0,
                CrystalPurchased = 0,
                WoodAverage = 0,
                CrystalAverage = 0,
                WoodAveragePrevious = 0,
                CrystalAveragePrevious = 0,
                MachinesOwned = 0,
                MachinesPurchased = 0,
                MachinesSold = 0,
                DwarfWorkers = 0,
                ElfWorkers = 0,
                HumanWorkers = 0,
                DwarfWorkersEmployed = 0,
                ElfWorkersEmployed = 0,
                HumanWorkersEmployed = 0,
                DwarfWorkersDismissed = 0,
                ElfWorkersDismissed = 0,
                HumanWorkersDismissed = 0,
                QualityExpense = 0,
                AdExpense = 0,
                QualityExpensePrevious = 0,
                AdExpensePrevious = 0,
                LoanPaid = 0,
                LoanTaken = 0,
                WandsProducedAmount = 0,
                WandPrice = 0,
                WandsSoldAmount = 0,
                Income = 0,
                WandsReservesAmount = 0,
                PlayerRoundNumber = 1
            };
        }
        public static PlayerRound FinishRound(PlayerPart pP)
        {
            PlayerRound pr = new PlayerRound();

            pr.PlayerPartId = pP.PlayerPartId;
            pr.PlayerPart = pP;
            pr.PlayerRoundId = pP.PlayerRounds[pP.CurrentRound].PlayerRoundId;
            pr.Gold = pP.PlayerRounds[pP.CurrentRound].Gold;
            pr.LoanRemaining = pP.PlayerRounds[pP.CurrentRound].LoanRemaining;
            pr.WoodReserves = pP.PlayerRounds[pP.CurrentRound].WoodReserves;
            pr.CrystalReserves = pP.PlayerRounds[pP.CurrentRound].CrystalReserves;
            pr.WoodPurchased = pP.PlayerRounds[pP.CurrentRound].WoodPurchased;
            pr.CrystalPurchased = pP.PlayerRounds[pP.CurrentRound].CrystalPurchased;
            pr.WoodAveragePrevious = pP.PlayerRounds[pP.CurrentRound].WoodAveragePrevious;
            pr.CrystalAveragePrevious = pP.PlayerRounds[pP.CurrentRound].CrystalAveragePrevious;
            pr.MachinesOwned = pP.PlayerRounds[pP.CurrentRound].MachinesOwned;
            pr.MachinesPurchased = pP.PlayerRounds[pP.CurrentRound].MachinesPurchased;
            pr.MachinesSold = pP.PlayerRounds[pP.CurrentRound].MachinesSold;
            pr.DwarfWorkers = pP.PlayerRounds[pP.CurrentRound].DwarfWorkers;
            pr.ElfWorkers = pP.PlayerRounds[pP.CurrentRound].ElfWorkers;
            pr.HumanWorkers = pP.PlayerRounds[pP.CurrentRound].HumanWorkers;
            pr.DwarfWorkersEmployed = pP.PlayerRounds[pP.CurrentRound].DwarfWorkersEmployed;
            pr.ElfWorkersEmployed = pP.PlayerRounds[pP.CurrentRound].ElfWorkersEmployed;
            pr.HumanWorkersEmployed = pP.PlayerRounds[pP.CurrentRound].HumanWorkersEmployed;
            pr.DwarfWorkersDismissed = pP.PlayerRounds[pP.CurrentRound].DwarfWorkersDismissed;
            pr.ElfWorkersDismissed = pP.PlayerRounds[pP.CurrentRound].ElfWorkersDismissed;
            pr.HumanWorkersDismissed = pP.PlayerRounds[pP.CurrentRound].HumanWorkersDismissed;
            pr.QualityExpense = pP.PlayerRounds[pP.CurrentRound].QualityExpense;
            pr.AdExpense = pP.PlayerRounds[pP.CurrentRound].AdExpense;
            pr.QualityExpensePrevious = pP.PlayerRounds[pP.CurrentRound].QualityExpensePrevious;
            pr.AdExpensePrevious = pP.PlayerRounds[pP.CurrentRound].AdExpensePrevious;
            pr.LoanPaid = pP.PlayerRounds[pP.CurrentRound].LoanPaid;
            pr.LoanTaken = pP.PlayerRounds[pP.CurrentRound].LoanTaken;
            pr.WandsProducedAmount = pP.PlayerRounds[pP.CurrentRound].WandsProducedAmount;
            pr.WandPrice = pP.PlayerRounds[pP.CurrentRound].WandPrice;
            pr.PlayerRoundNumber = pP.PlayerRounds[pP.CurrentRound].PlayerRoundNumber;

            pr.WoodAverage = pP.GetAndSetAverageWoodPrice(pr, pP.CurrentRound);
            pr.CrystalAverage = pP.GetAndSetAverageCrystalPrice(pr, pP.CurrentRound);
            pr.WandsSoldAmount = pP.CountWandsSoldAmount(pr,pP.CurrentRound);
            pr.WandsReservesAmount = pP.CountWandsRemainingAmount(pr, pP.CurrentRound);
            pr.Income = pP.CountIncome(pr,pP.CurrentRound);
             
            return pr;
        }
        public static PlayerRound SetNextRound(PlayerPart pP, PlayerRound pR)
        {
            PlayerRound pr = new PlayerRound()
            { 
                Gold = pP.CountRemainingGold(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
                LoanRemaining = pP.GetLoanRemaining(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
                WoodReserves = pP.GetWoodRemaining(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
                CrystalReserves = pP.GetCrystalRemaining(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
                WoodPurchased = 0,
                CrystalPurchased = 0,
                WoodAverage = 0,
                CrystalAverage = 0,
                WoodAveragePrevious = pP.GetAndSetAverageWoodPrice(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
                CrystalAveragePrevious = pP.GetAndSetAverageCrystalPrice(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
                MachinesOwned = pP.GetMachineRemaining(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
                MachinesPurchased = 0,
                MachinesSold = 0,
                DwarfWorkers = pP.GetWorkerDwarfRemaining(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
                ElfWorkers = pP.GetWorkerElfRemaining(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
                HumanWorkers = pP.GetWorkerHumanRemaining(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
                DwarfWorkersEmployed = 0,
                ElfWorkersEmployed = 0,
                HumanWorkersEmployed = 0,
                DwarfWorkersDismissed = 0,
                ElfWorkersDismissed = 0,
                HumanWorkersDismissed = 0,
                QualityExpense = 0,
                AdExpense = 0,
                QualityExpensePrevious = pP.GetQualityFading(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
                AdExpensePrevious = pP.GetAdFading(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
                LoanPaid = 0,
                LoanTaken = 0,
                WandsProducedAmount = 0,
                WandPrice = 0,
                WandsSoldAmount = 0,
                Income = 0,
                WandsReservesAmount = 0,
                PlayerRoundNumber = pP.PlayerRounds[pP.CurrentRound].PlayerRoundNumber + 1,
                PlayerPartId = pP.PlayerPartId,
                PlayerRoundId = pR.PlayerRoundId,
                PlayerPart = pP
            };

            pr.WandsReservesAmount = pR.WandsReservesAmount;
            return pr;
        }
        public static PlayerPart UpdateCurrentPlayerRoundNumber(PlayerPart pP)
        {
            pP.CurrentRound++;
            return pP;
        }
        public static Game UpdateCurrentGameRoundNumber(Game g)
        {
            g.CurrentRound++;
            return g;
        }

        #endregion
    }

}