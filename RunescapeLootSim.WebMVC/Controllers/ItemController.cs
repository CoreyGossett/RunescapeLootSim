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
    public class ItemController : Controller
    {
        // GET: Item
        [Authorize]
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            var service = new ItemService(userId);
            var model = service.GetItems();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemCreate model)
        {
            if (ModelState.IsValid) return View(model);

            var service = CreateItemService();

            if (service.CreateItem(model))
            {
                TempData["SaveResult"] = "Your note was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Note could note be created.");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int itemId, ItemEdit model, string userId)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ItemId != itemId)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateItemService();

            if (service.UpdateItem(model, userId))
            {
                TempData["Save Result"] = "Your item was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your item could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(string userId)
        {
            var svc = CreateItemService();
            var model = svc.GetItemsByUser(userId);

            return View(model);
        }

        private ItemService CreateItemService()
        {
            var userId = User.Identity.GetUserId();
            var service = new ItemService(userId);
            return service;
        }
    }
}