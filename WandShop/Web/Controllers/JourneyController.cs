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
            ViewBag.TestMariusz = "To działa";
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
                Game game = model.ConvertToGame();
                game = gameRepository.SaveGame(game);
                List<PlayerPart> list = playerPartRepository.AddPlayerParts(game.GameId, model.PlayersNumber).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    playerRepository.SavePlayer(list[i].PlayerPartId, new Player());
                }
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
                Game game = gameRepository.Games.FirstOrDefault(x => x.GameId == model.GameId);
                game.Tax = model.Tax;
                game.Dividend = model.Dividend;
                game.OwnContribution = model.OwnContribution;
                game.ForeignShares = model.ForeignShares;
                game.Loan = model.Loan;
                game.InterestRate = model.InterestRate;
                game.ManagementCosts = model.ManagementCosts;
                game.TransportCosts = model.TransportCosts;
                gameRepository.SaveGame(game);

                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }


        public ActionResult Check(string gameId)
        {
            int id = Int32.Parse(Encryption.decrypt(gameId));
            GameViewModel model = new GameViewModel()
            {
                Game = gameRepository.Games.FirstOrDefault(x => x.GameId == id)
            };
            return View(model);
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

        private Game BeginGame(Game game)
        {
            Game model = gameRepository.Games.FirstOrDefault(x => x.GameId == game.GameId);

            roundRepository.SaveRound(model.GameId, new Round());
            foreach (var pP in model.PlayersPart)
            {
                pP.CurrentRound = 0;
                playerPartRepository.SavePlayerPart(pP);
                SetStartingRound(pP);
            }
            model.CurrentRound++;
            gameRepository.SaveGame(model);

            return game;
        }
        private Game NextRound(Game game)
        {
            Game model = gameRepository.Games.FirstOrDefault(x => x.GameId == game.GameId);
            roundRepository.SaveRound(model.GameId, new Round());

            foreach (var pP in model.PlayersPart)
            {
                FinishRound(pP);
                SetNextRound(pP);
            }
            model.CurrentRound++;
            gameRepository.SaveGame(model);
            return model;
        }
        private Game FinishGame(Game game)
        {
            foreach (var pP in game.PlayersPart)
            {
                FinishRound(pP);
                playerRepository.DeletePlayer(pP.PlayerPartId);
            }
            Game model = gameRepository.DeleteGame(game.GameId);
            //TODO CreateArch

            return model;
        }

        public void FinishRound(PlayerPart playerPart)
        {
            PlayerPart pP = playerPartRepository.PlayerParts.FirstOrDefault(x => x.PlayerPartId == playerPart.PlayerPartId);
            PlayerRound nextRound = new PlayerRound()
            {
                Gold = pP.PlayerRounds[pP.CurrentRound].Gold,
                LoanRemaining = pP.PlayerRounds[pP.CurrentRound].LoanRemaining,
                WoodReserves = pP.PlayerRounds[pP.CurrentRound].WoodReserves,
                CrystalReserves = pP.PlayerRounds[pP.CurrentRound].CrystalReserves,
                WoodPurchased = pP.PlayerRounds[pP.CurrentRound].WoodPurchased,
                CrystalPurchased = pP.PlayerRounds[pP.CurrentRound].CrystalPurchased,
                WoodAverage = pP.GetAndSetAverageWoodPrice(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
                CrystalAverage = pP.GetAndSetAverageCrystalPrice(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
                WoodAveragePrevious = pP.PlayerRounds[pP.CurrentRound].WoodAveragePrevious,
                CrystalAveragePrevious = pP.PlayerRounds[pP.CurrentRound].CrystalAveragePrevious,
                MachinesOwned = pP.PlayerRounds[pP.CurrentRound].MachinesOwned,
                MachinesPurchased = pP.PlayerRounds[pP.CurrentRound].MachinesPurchased,
                MachinesSold = pP.PlayerRounds[pP.CurrentRound].MachinesSold,
                DwarfWorkers = pP.PlayerRounds[pP.CurrentRound].DwarfWorkers,
                ElfWorkers = pP.PlayerRounds[pP.CurrentRound].ElfWorkers,
                HumanWorkers = pP.PlayerRounds[pP.CurrentRound].HumanWorkers,
                DwarfWorkersEmployed = pP.PlayerRounds[pP.CurrentRound].DwarfWorkersEmployed,
                ElfWorkersEmployed = pP.PlayerRounds[pP.CurrentRound].ElfWorkersEmployed,
                HumanWorkersEmployed = pP.PlayerRounds[pP.CurrentRound].HumanWorkersEmployed,
                DwarfWorkersDismissed = pP.PlayerRounds[pP.CurrentRound].DwarfWorkersDismissed,
                ElfWorkersDismissed = pP.PlayerRounds[pP.CurrentRound].ElfWorkersDismissed,
                HumanWorkersDismissed = pP.PlayerRounds[pP.CurrentRound].HumanWorkersDismissed,
                QualityExpense = pP.PlayerRounds[pP.CurrentRound].QualityExpense,
                AdExpense = pP.PlayerRounds[pP.CurrentRound].AdExpense,
                QualityExpensePrevious = pP.PlayerRounds[pP.CurrentRound].QualityExpensePrevious,
                AdExpensePrevious = pP.PlayerRounds[pP.CurrentRound].AdExpensePrevious,
                LoanPaid = pP.PlayerRounds[pP.CurrentRound].LoanPaid,
                LoanTaken = pP.PlayerRounds[pP.CurrentRound].LoanTaken,
                WandsProducedAmount = pP.PlayerRounds[pP.CurrentRound].WandsProducedAmount,
                WandPrice = pP.PlayerRounds[pP.CurrentRound].WandPrice,
                WandsSoldAmount = pP.CountWandsSoldAmount(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
                Income = pP.CountIncome(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
                WandsReservesAmount = pP.CountWandsRemainingAmount(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
                PlayerRoundNumber = pP.PlayerRounds[pP.CurrentRound].PlayerRoundNumber,
                PlayerPartId = pP.PlayerPartId,
                PlayerPart = pP,
                PlayerRoundId = pP.PlayerRounds[pP.CurrentRound].PlayerRoundId
        };
            playerRoundRepository.SavePlayerRound(pP.PlayerPartId, nextRound);
        }
        public void SetNextRound(PlayerPart playerPart)
        {
            PlayerPart pP = playerPartRepository.PlayerParts.FirstOrDefault(x => x.PlayerPartId == playerPart.PlayerPartId);
            PlayerRound g = playerRoundRepository.SavePlayerRound(pP.PlayerPartId, new PlayerRound());
            
            PlayerRound nextRound = new PlayerRound()
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
                CrystalAveragePrevious = pP.GetAndSetAverageWoodPrice(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
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
                WandsReservesAmount = pP.CountWandsRemainingAmount(pP.PlayerRounds[pP.CurrentRound], pP.CurrentRound),
                PlayerRoundNumber = pP.PlayerRounds[pP.CurrentRound].PlayerRoundNumber + 1,
                PlayerPartId = g.PlayerPartId,
                PlayerRoundId = g.PlayerRoundId,
                PlayerPart = g.PlayerPart
            };
            playerRoundRepository.SavePlayerRound(pP.PlayerPartId, nextRound);

            pP.CurrentRound++;
            playerPartRepository.SavePlayerPart(pP);
        }
        public void SetStartingRound(PlayerPart playerPart)
        {
            PlayerPart pP = playerPartRepository.PlayerParts.FirstOrDefault(x => x.PlayerPartId == playerPart.PlayerPartId);
            PlayerRound startingRound = new PlayerRound()
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
            playerRoundRepository.SavePlayerRound(pP.PlayerPartId, startingRound);
        }
    }

}