using Logic.Concrete;
using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private WandShopContext context = new WandShopContext();

        public ActionResult Index()
        {
            List<Game> games = context.Game.ToList();
            return View(games);
        }

    }
}