using Microsoft.AspNet.Identity;
using RunescapeLootSim.Data;
using RunescapeLootSim.Models;
using RunescapeLootSim.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RunescapeLootSim.WebMVC.Controllers
{
    public class KillController : Controller
    {
        // GET: Kill
        public ActionResult Index()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var kills = ctx.Kills.ToList();
                var killCount = kills.Count;
                ViewBag.Data = killCount;

                return View();
            }
        }

        public ActionResult Loot()
        {
            using (var ctx = new ApplicationDbContext())
            {
                Random random = new Random();
                var items = ctx.Items.ToList();
                var size = items.Count;
                var prize = items[random.Next(size - 1)];
                ViewBag.Data = prize.Name;

                return View();
            }
        }

        public ActionResult Create()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var bosses = ctx.Bosses.ToList();
                if (bosses != null)
                {
                    ViewBag.Data = bosses;
                }
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(KillCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateKillService();

            if (service.CreateKill(model))
            {
                TempData["SaveResult"] = "You killed the boss and gained some loot!";
                return RedirectToAction("Loot");
            }

            ModelState.AddModelError("", "You couldn't kill the boss. I'm sorry.");

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteKill(int id)
        {
            var service = CreateKillService();
            service.DeleteKill(id);
            TempData["SaveResult"] = "Your kill was deleted.";
            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateKillService();
            var model = svc.GetKillById(id);

            return View(model);
        }

        private KillService CreateKillService()
        {
            var userId = User.Identity.GetUserId();
            var service = new KillService(userId);
            return service;
        }

        public ActionResult Details(int id)
        {
            var service = CreateKillService();
            var model = service.GetKillById(id);

            return View(model);
        }
    }
}