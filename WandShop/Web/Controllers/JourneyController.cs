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
                Game game = model.ConvertToGame();
                game = gameRepository.SaveGame(game);
                List<PlayerPart> list = playerPartRepository.AddPlayerParts(game.GameId, model.PlayersNumber).ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    playerRepository.SavePlayer(list[i].PlayerPartId, new Player());
                }
                return RedirectToAction("CreateGameParam", new { model = model.CreateGameParamModel});
            }
            else
            {
                return View(model);
            }
        }
        public ActionResult CreateGameParam()
        {
            return View(new CreateGameParamModel());
        }
        [HttpPost]
        public ActionResult CreateGameParam(CreateGameParamModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(model);
            }
        }
    }
}