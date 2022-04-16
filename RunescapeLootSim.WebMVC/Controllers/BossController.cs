using Microsoft.AspNet.Identity;
using RunescapeLootSim.Models;
using RunescapeLootSim.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RunescapeLootSim.WebMVC.Controllers
{
    public class BossController : Controller
    {
        // GET: Boss
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var service = new BossService(userId);
            var model = service.GetBosses();
            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BossCreate model)
        {
            if (ModelState.IsValid) return View(model);

            var service = CreateBossService();

            if (service.CreateBoss(model))
            {
                TempData["SaveResult"] = "Your boss was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Boss could not be created.");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int bossId, BossEdit model, string userId)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.BossId != bossId)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateBossService();

            if (service.UpdateBoss(model, userId))
            {
                TempData["Save Result"] = "Your boss was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your boss could not be updated.");
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBoss(int bossId, string userId)
        {
            var service = CreateBossService();

            service.DeleteBoss(bossId, userId);

            TempData["SaveResult"] = "Your boss was deleted.";

            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public ActionResult Delete(string userId)
        {
            var svc = CreateBossService();
            var model = svc.GetBosses();

            return View(model);
        }

        private BossService CreateBossService()
        {
            var userId = User.Identity.GetUserId();
            var service = new BossService(userId);
            return service;
        }

        
    }
}