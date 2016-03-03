using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KinoAfishaDP.Models;

namespace KinoAfishaDP.Controllers
{
    public class MovieHousesController : Controller
    {
        private AFISHAContext db = new AFISHAContext();

        //
        // GET: /MovieHouses/

        public ActionResult Index(string name)
        {
            TempData["cinemainfo"] = name;

            var stations = name != null ? db.MovieHouses.Where(x => x.MovieHouseName.Contains(name)).OrderByDescending(x => x.MovieHouseRating) : db.MovieHouses.OrderByDescending(x => x.MovieHouseRating);
            //var stations = db.MovieHouses.Take(5).OrderByDescending(x => x.MovieHouseRating);
            //var stations = name != null ? db.MovieHouses.Where(x => x.MovieHouseName.Contains(name)) : db.MovieHouses;

            return View(stations.ToList());
        }
        [HttpPost]
        public ActionResult Index(string action, string name1)
        {

            var NAME = TempData.Peek("cinemainfo") as string;
            MovieHouse reiting = db.MovieHouses.Find(1);
            switch (action)
            {
                case "add": { reiting.MovieHouseRating++; } break;
                case "minus": { reiting.MovieHouseRating--; } break;

                default: ViewBag.Result = "ERRor"; break;
            }
            
            db.Entry(reiting).State = EntityState.Modified;
            db.SaveChanges();

            MovieHouse reiting1 = db.MovieHouses.Find(1);
            
            var stations = NAME != null ? db.MovieHouses.Where(x => x.MovieHouseName.Contains(NAME)).OrderByDescending(x => x.MovieHouseRating) : db.MovieHouses.OrderByDescending(x => x.MovieHouseRating);


            return View(stations.ToList());
        }

        
        //
        // GET: /MovieHouses/Details/5
        [Authorize(Roles = "Admin")] 
        public ActionResult Details(int id = 0)
        {
            MovieHouse moviehouse = db.MovieHouses.Find(id);
            if (moviehouse == null)
            {
                return HttpNotFound();
            }
            return View(moviehouse);
        }

        //
        // GET: /MovieHouses/Create
        [Authorize(Roles = "Admin")] 
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /MovieHouses/Create

        [HttpPost]
        [Authorize(Roles = "Admin")] 
        public ActionResult Create(MovieHouse moviehouse)
        {
            if (ModelState.IsValid)
            {
                db.MovieHouses.Add(moviehouse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(moviehouse);
        }

        //
        // GET: /MovieHouses/Edit/5
        [Authorize(Roles = "Admin")] 
        public ActionResult Edit(int id = 0)
        {
            MovieHouse moviehouse = db.MovieHouses.Find(id);
            if (moviehouse == null)
            {
                return HttpNotFound();
            }
            return View(moviehouse);
        }

        //
        // POST: /MovieHouses/Edit/5

        [HttpPost]
        [Authorize(Roles = "Admin")] 
        public ActionResult Edit(MovieHouse moviehouse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(moviehouse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(moviehouse);
        }

        //
        // GET: /MovieHouses/Delete/5
        [Authorize(Roles = "Admin")] 
        public ActionResult Delete(int id = 0)
        {
            MovieHouse moviehouse = db.MovieHouses.Find(id);
            if (moviehouse == null)
            {
                return HttpNotFound();
            }
            return View(moviehouse);
        }

        //
        // POST: /MovieHouses/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")] 
        public ActionResult DeleteConfirmed(int id)
        {
            MovieHouse moviehouse = db.MovieHouses.Find(id);
            db.MovieHouses.Remove(moviehouse);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


        public JsonResult GetData()
        {
            string cinemainfo=TempData.Peek("cinemainfo") as string;
           
            
            var stations = cinemainfo != null ? db.MovieHouses.Where(x => x.MovieHouseName.Contains(cinemainfo)).ToList() : db.MovieHouses.ToList();
            

            return Json(stations, JsonRequestBehavior.AllowGet);
        }
    }
}