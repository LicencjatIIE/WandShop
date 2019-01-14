using Logic.Abstract;
using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models;
using Web.Helpers;

namespace Web.Controllers
{
    public class ExploreController : Controller
    {
        private IGameRepository gameRepository;
        private IPlayerPartRepository playerPartRepository;
        private IPlayerRepository playerRepository;
        private IPlayerRoundRepository playerRoundRepository;
        private IRoundRepository roundRepository;

        public ExploreController(
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
            if (AccessFirst())
                return View("Act");
            return View();
        }
        [HttpPost]
        public ActionResult Authorize(Player player)
        {
            if(player != null)
            {
                Player p = playerRepository.Players.Where(x => x.Login == player.Login && x.Password == player.Password).FirstOrDefault();
                if (p != null)
                {
                    Session["PlayerId"] = p.PlayerId;
                    return RedirectToAction("Act"); 
                }
            }
            return View("Index");
        }
        public ActionResult Act()
        {
            if(!AccessFirst())
                return View("Index");

            return View();
        }
        public ActionResult Games()
        {
            if (!AccessFirst())
                return View("Index");
            return View(new PlayerRoundsViewModel(playerPartRepository.PlayerParts.FirstOrDefault(x => x.PlayerPartId == (int)Session["PlayerId"]).PlayerRounds));
        }
        public ActionResult Round(int playerRoundIndex)
        {
            if (!AccessFirst())
                return View("Index");

            PlayerRound pr = playerRoundRepository.PlayerRounds.FirstOrDefault(x => x.PlayerRoundId == playerRoundIndex);

            if (pr == null)
                return RedirectToAction("Games");

            if (pr.PlayerPartId != (int)Session["PlayerId"])
                return RedirectToAction("Games");

            PlayerRoundViewModel prvm = new PlayerRoundViewModel(pr);
            return View(prvm);
        }
        public ActionResult Play()
        {
            if (!AccessFirst())
                return View("Index");

            PlayerPart pr = playerPartRepository.PlayerParts.FirstOrDefault(x => x.PlayerPartId == (int)Session["PlayerId"]);
            if (pr.PlayerRounds.Count == 0)
                return RedirectToAction("Act");

            PlayerPlayRoundModel pprm = new PlayerPlayRoundModel(pr);
            return View(pprm);
        }
        [HttpPost]
        public ActionResult Play(PlayerPlayRoundModel model)
        {
            if (!AccessFirst())
                return View("Index");
            if (ModelState.IsValid )
            {
                PlayerPart pp = playerPartRepository.PlayerParts.FirstOrDefault(x => x.PlayerPartId == (int)Session["PlayerId"]);

                model.SetLoanRemaining(pp.PlayerRounds[pp.CurrentRound].LoanRemaining);
                model.ValidationMes = new List<string>();
                model.ValidationMes = Validation.PlayerPlayRoundValidation(model, pp);
                if(model.ValidationMes.Count == 0)
                {
                    SavePlayerPart(pp, UpdatePlayerRound(pp, model));
                    return View("Act");
                }
                else
                {
                    PlayerPlayRoundModel pprm = new PlayerPlayRoundModel(playerPartRepository.PlayerParts.FirstOrDefault(x => x.PlayerPartId == (int)Session["PlayerId"]), model);
                    return View(pprm);
                }

            }
            else
            {
                PlayerPlayRoundModel pprm = new PlayerPlayRoundModel(playerPartRepository.PlayerParts.FirstOrDefault(x => x.PlayerPartId == (int)Session["PlayerId"]), model);
                return View(pprm);
            }
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return View("Index");
        }

        private bool AccessFirst()
        {
            //int id = playerRepository.Players.FirstOrDefault(x => x.PlayerId == (int)Session["PlayerId"]).PlayerId;

            if (Session["PlayerId"] == null || (int)Session["PlayerId"] == 0)
                return false;
            return true;
        }

        #region Helpers
        public static PlayerRound UpdatePlayerRound(PlayerPart pP, PlayerPlayRoundModel pprm)
        {
            PlayerRound pr = pP.PlayerRounds[pP.CurrentRound];
            pr.WoodPurchased = pprm.WoodPurchased;
            pr.CrystalPurchased = pprm.CrystalPurchased;
            pr.MachinesPurchased = pprm.MachinesPurchased;
            pr.DwarfWorkersEmployed = pprm.DwarfWorkersEmployed;
            pr.ElfWorkersEmployed = pprm.ElfWorkersEmployed;
            pr.HumanWorkersEmployed = pprm.HumanWorkersEmployed;
            pr.DwarfWorkersDismissed = pprm.DwarfWorkersDismissed;
            pr.ElfWorkersDismissed = pprm.ElfWorkersDismissed;
            pr.HumanWorkersDismissed = pprm.HumanWorkersDismissed;
            pr.QualityExpense = pprm.QualityExpense;
            pr.AdExpense = pprm.AdExpense;
            pr.LoanPaid = pprm.LoanPaid;
            pr.LoanTaken = pprm.LoanTaken;
            pr.WandPrice = pprm.WandPrice;
            pr.WandsProducedAmount = pprm.WandsProducedAmount;
            return pr;
        }
        public PlayerRound SavePlayerPart(PlayerPart pP, PlayerRound pr)
        {
            return playerRoundRepository.SavePlayerRound(pP.PlayerPartId, pr);
        }
        #endregion
    }
}