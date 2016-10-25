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
    public class SessionsController : Controller
    {
        private UsersContext db = new UsersContext();

        //
        // GET: /Sessions/

        public ActionResult Index(string NameOfFilm)
        {
            TempData["NameOfFilm"] = NameOfFilm;
            ViewBag.FilmName = new SelectList(db.Films, "FilmName", "FilmName");
            ViewBag.Cinema = new SelectList(db.MovieHouses, "MovieHouseName", "MovieHouseName");

            if (TempData.Peek("_UserRole") as string == "Admin" || TempData.Peek("_UserRole") as string == "Moderator")
            {
                var sessions = db.Sessions.Include(s => s.Film).Include(s => s.MovieHouse);
                var sessions1 = NameOfFilm != null ? sessions.Where(x => x.Film.FilmName.ToUpper()
                                                             .Contains(NameOfFilm.ToUpper()))
                                                             .Where(x=>x.SessionTimePokaz>=DateTime.Now)             
                                                             .OrderByDescending(x => x.SessionTimePokaz) : sessions.OrderByDescending(x => x.SessionTimePokaz);
                return View(sessions1.ToList());
            }
            else 
            {
                var sessions = db.Sessions.Include(s => s.Film).Include(s => s.MovieHouse);
                var sessions1 = NameOfFilm != null ? sessions.Where(x => x.Film.FilmName.ToUpper().Contains(NameOfFilm.ToUpper())).Where(x => x.SessionTimePokaz > DateTime.Now) : sessions.Where(x => x.SessionTimePokaz > DateTime.Now);
                return View(sessions1.ToList());
            }
           
        }
        [HttpPost]
        public ActionResult Index(string FilmName, string Cinema, DateTime? start,DateTime? end)
        {
           
            var sessions = db.Sessions.Include(s => s.Film).Include(s => s.MovieHouse);

            ViewBag.FilmName = new SelectList(db.Films, "FilmName", "FilmName");
            ViewBag.Cinema = new SelectList(db.MovieHouses, "MovieHouseName", "MovieHouseName");

            var NAME = FilmName != "" ? sessions.Where(x => x.Film.FilmName==FilmName) : sessions;
            var CINEMA = Cinema != "" ? sessions.Where(x => x.MovieHouse.MovieHouseName==Cinema) : sessions;


            var TIME1 = start != null ? sessions.Where(x => x.SessionTimePokaz >= start) : sessions;
            var TIME2 = end!=null? sessions.Where(x=>x.SessionTimePokaz<=end):sessions;

            if (TempData.Peek("_UserRole") as string == "Admin" || TempData.Peek("_UserRole") as string == "Moderator")
            {
                var All = NAME.Intersect(CINEMA).Intersect(TIME1).Intersect(TIME2);
               
                return View(All.ToList());
            }
            else
            {
                var All = NAME.Intersect(CINEMA).Intersect(TIME1).Intersect(TIME2);
                return View(All.Where(x => x.SessionTimePokaz >= DateTime.Now));

            }
          


           
            
        }

        ////Для адміна......................................
        //................................................
        //................................................
        //...............................................
       
        // GET: /Sessions/Details/5
        [Authorize(Roles = "Admin")] 
        public ActionResult Details(int id = 0)
        {
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        //
        // GET: /Sessions/Create
        [Authorize(Roles = "Admin")] 
        public ActionResult Create()
        {
            
            ViewBag.FilmId = new SelectList(db.Films, "FilmId", "FilmName");
            ViewBag.MovieHouseId = new SelectList(db.MovieHouses, "MovieHouseId", "MovieHouseName");
            return View();
        }

        //
        // POST: /Sessions/Create

        [HttpPost]
        [Authorize(Roles = "Admin")] 
        public ActionResult Create(Session session)
        {
            

            if(ModelState.IsValid)
            {
                db.Sessions.Add(session);
                db.SaveChanges();
               
                return RedirectToAction("Index");
            }

            ViewBag.FilmId = new SelectList(db.Films, "FilmId", "FilmName", session.FilmId);
            ViewBag.MovieHouseId = new SelectList(db.MovieHouses, "MovieHouseId", "MovieHouseName", session.MovieHouseId);
            
            return View(session);
        }

        //
        // GET: /Sessions/Edit/5
        [Authorize(Roles = "Admin")] 
        public ActionResult Edit(int id = 0)
        {
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            ViewBag.FilmId = new SelectList(db.Films, "FilmId", "FilmName", session.FilmId);
            ViewBag.MovieHouseId = new SelectList(db.MovieHouses, "MovieHouseId", "MovieHouseName", session.MovieHouseId);
            return View(session);
        }

        //
        // POST: /Sessions/Edit/5

        [HttpPost]
        [Authorize(Roles = "Admin")] 
        public ActionResult Edit(Session session)
        {
            if (ModelState.IsValid)
            {
                db.Entry(session).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FilmId = new SelectList(db.Films, "FilmId", "FilmName", session.FilmId);
            ViewBag.MovieHouseId = new SelectList(db.MovieHouses, "MovieHouseId", "MovieHouseName", session.MovieHouseId);
            return View(session);
        }

        //
        // GET: /Sessions/Delete/5
        [Authorize(Roles = "Admin")] 
        public ActionResult Delete(int id = 0)
        {
            Session session = db.Sessions.Find(id);
            if (session == null)
            {
                return HttpNotFound();
            }
            return View(session);
        }

        //
        // POST: /Sessions/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")] 
        public ActionResult DeleteConfirmed(int id)
        {
            Session session = db.Sessions.Find(id);
            db.Sessions.Remove(session);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}