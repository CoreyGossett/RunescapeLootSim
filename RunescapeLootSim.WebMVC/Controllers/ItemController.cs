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
            if (!ModelState.IsValid) return View(model);

            var service = CreateItemService();

            if (service.CreateItem(model))
            {
                TempData["SaveResult"] = "Your item was created.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Item could not be created.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var service = CreateItemService();
            var model = service.GetItemsById(id);

            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var service = CreateItemService();
            var detail = service.GetItemsById(id);
            var model =
                new ItemEdit
                {
                    ItemId = detail.ItemId,
                    Name = detail.Name,
                    Damage = detail.Damage,
                    DropRate = detail.DropRate
                };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ItemEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.ItemId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreateItemService();

            if (service.UpdateItem(model))
            {
                TempData["Save Result"] = "Your item was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Your item could not be updated.");
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteItem(int id)
        {
            var service = CreateItemService();
            service.DeleteItem(id);
            TempData["SaveResult"] = "Your item was deleted.";
            return RedirectToAction("Index");
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateItemService();
            var model = svc.GetItemsById(id);

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