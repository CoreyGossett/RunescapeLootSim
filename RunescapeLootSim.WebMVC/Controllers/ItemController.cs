using RunescapeLootSim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RunescapeLootSim.WebMVC.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        // GET: Item
        public ActionResult Index()
        {
            var model = new ItemListItem[0];
            return View(model);
        }
    }
}