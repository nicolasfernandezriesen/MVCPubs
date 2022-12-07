using Microsoft.AspNetCore.Mvc;
using MVCPubs.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVCPubs.Controllers
{
    public class StoreController : Controller
    {
        private readonly pubsContext context;

        public StoreController(pubsContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View(context.Stores.ToList());
        }

        public IActionResult Create()
        {
            Store store = new Store();

            return View(store);
        }

        [HttpPost]
        public IActionResult Create(Store store)
        {
            if (ModelState.IsValid)
            {
                context.Stores.Add(store);

                context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View("Create", store);
            }
        }

        public IActionResult Details(string id)
        {
            Store store = context.Stores.Find(id);

            if (store == null)
            {
                return NotFound();
            }
            else
            {
                return View(store);
            }
        }

        public IActionResult StoreByCity(string city)
        {
            List<Store> storeList = (from store in context.Stores
                                     where store.City == city
                                     select store).ToList();

            return View("Index", storeList);
        }

        public IActionResult Modify(string id)
        {
            Store store = context.Stores.Find(id);

            if (store == null)
            {
                return BadRequest();
            }
            else
            {
                return View(store);
            }
        }

        [HttpPost]
        public IActionResult Modify(Store store)
        {
            Store DbStore = context.Stores.Find(store.StorId);

            if (DbStore != null)
            {
                DbStore.StorName = store.StorName;
                DbStore.StorAddress = store.StorAddress;
                DbStore.City = store.City;
                DbStore.State = store.State;
                DbStore.Zip= store.Zip;

                context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
        }

        public IActionResult Delete(string id)
        {
            Store store = context.Stores.Find(id);

            if (store == null)
            {
                return NotFound();
            }
            else
            {
                return View(store);
            }
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(string id)
        {
            Store store = context.Stores.Find(id);

            if (store != null)
            {
                context.Remove(store);

                context.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
