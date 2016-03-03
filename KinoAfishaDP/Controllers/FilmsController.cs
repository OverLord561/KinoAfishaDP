﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using KinoAfishaDP.Models;

namespace KinoAfishaDP.Controllers
{
    public class FilmsController : Controller
    {
        private AFISHAContext db = new AFISHAContext();

        //
        // GET: /Films/

        
        public ActionResult Index()
        {
            
            ViewBag.FilmName = new SelectList(db.Films, "FilmName", "FilmName");
            ViewBag.FilmGenre = new SelectList(new[] {"Фантастика", "Боевик","Триллер","Приключения"});
            ViewBag.FilmCountry = new SelectList(db.Films, "FilmCountry", "FilmCountry" );
            //ViewBag.FilmRating = new SelectList( db.Films, "FilmRating", "FilmRating");


            return View(db.Films);
        }
        //[HttpPost]
        //public ActionResult Index(string actor, string FilmGenre, string FilmName, string FilmCountry,string data)
        //{

        //    //ViewBag.IsAdmin= TempData["_UserName"] as string =="Admin"?"Admin":null;

        //    ViewBag.FilmName = new SelectList(db.Films, "FilmName", "FilmName");
        //    ViewBag.FilmGenre = new SelectList(new[] { "Фантастика", "Боевик", "Триллер", "Приключения" });
        //    ViewBag.FilmCountry = new SelectList(db.Films, "FilmCountry", "FilmCountry");
        //    //ViewBag.FilmRating = new SelectList(db.Films, "FilmRating", "FilmRating");


           
          
        //    var NAME = FilmName != "" ? db.Films.Where(x => x.FilmName.ToLower().Contains(FilmName.ToLower())) : db.Films;
        //    var GANRE = FilmGenre != "" ? db.Films.Where(x => x.FilmGenre.ToLower().Contains(FilmGenre.ToLower())) : db.Films;
        //    var ACTOR = actor != "" ? db.Films.Where(x => x.FilmActors.ToLower().Contains(actor.ToLower())) : db.Films;
        //    var COUNTRY = FilmCountry != "" ? db.Films.Where(x => x.FilmCountry.ToLower().Contains(FilmCountry.ToLower())) : db.Films;
        //    var DATA = data != "" ? db.Films.Where(x => x.FilmAge.ToLower().Contains(data.ToLower())) : db.Films;

        //    var All = NAME.Intersect(GANRE).Intersect(ACTOR).Intersect(COUNTRY).Intersect(DATA);

        //    return View(All);



        //}

        public ActionResult Sort()
        {    
            return PartialView(db.Films);
            
        }
        [HttpPost]
        public ActionResult Sort(string actor, string FilmGenre, string FilmName, string FilmCountry, string data)
        {

            //ViewBag.IsAdmin= TempData["_UserName"] as string =="Admin"?"Admin":null;

            ViewBag.FilmName = new SelectList(db.Films, "FilmName", "FilmName");
            ViewBag.FilmGenre = new SelectList(new[] { "Фантастика", "Боевик", "Триллер", "Приключения" });
            ViewBag.FilmCountry = new SelectList(db.Films, "FilmCountry", "FilmCountry");
            //ViewBag.FilmRating = new SelectList(db.Films, "FilmRating", "FilmRating");




            var NAME = FilmName != "" ? db.Films.Where(x => x.FilmName.ToLower().Contains(FilmName.ToLower())) : db.Films;
            var GANRE = FilmGenre != "" ? db.Films.Where(x => x.FilmGenre.ToLower().Contains(FilmGenre.ToLower())) : db.Films;
            var ACTOR = actor != "" ? db.Films.Where(x => x.FilmActors.ToLower().Contains(actor.ToLower())) : db.Films;
            var COUNTRY = FilmCountry != "" ? db.Films.Where(x => x.FilmCountry.ToLower().Contains(FilmCountry.ToLower())) : db.Films;
            var DATA = data != "" ? db.Films.Where(x => x.FilmAge.ToLower().Contains(data.ToLower())) : db.Films;

            var All = NAME.Intersect(GANRE).Intersect(ACTOR).Intersect(COUNTRY).Intersect(DATA);

            return PartialView(All);



        }

        //
        // GET: /Films/Details/5
        [Authorize(Roles = "Admin")] 
        public ActionResult Details(int id = 0)
        {
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        //
        // GET: /Films/Create
        [Authorize(Roles = "Admin")]        
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Films/Create

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Create(Film film)
        {
            if (ModelState.IsValid)
            {
                db.Films.Add(film);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(film);
        }

        //
        // GET: /Films/Edit/5
        [Authorize(Roles = "Admin")] 
        public ActionResult Edit(int id = 0)
        {
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        //
        // POST: /Films/Edit/5

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(Film film)
        {
            if (ModelState.IsValid)
            {
                db.Entry(film).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(film);
        }

        //
        // GET: /Films/Delete/5
        [Authorize(Roles = "Admin")] 
        public ActionResult Delete(int id = 0)
        {
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        //
        // POST: /Films/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Film film = db.Films.Find(id);
            db.Films.Remove(film);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //вывод рецензии
        public ActionResult Review(string num)
        {
            
            if (num  !=null)
            {
                HttpContext.Response.Cookies["num_of_film"].Value = num;
                TempData["num_of_film"] = num;
            }
            else
            {
              num = HttpContext.Request.Cookies["num_of_film"].Value; 
            }

            int addressId = Int32.Parse(num);

            var review = db.Films.Where(s => s.FilmId == addressId).Select(n => new Review
            {

                FilmName_Review = n.FilmName,
                FilmGenre_Review = n.FilmGenre,
                FilmActors_Review = n.FilmActors,
                FilmId_Review = n.FilmId,
                FilmLength_Review=n.FilmLength,
                FilmReview_Review=n.FilmReview,
                FilmPictures_Review=n.FilmPictures,
                FilmAge_Review=n.FilmAge,
                FilmCountry_Review=n.FilmCountry,
                FilmRating_Review = n.FilmRating,
                Plus_Review = n.FilmPlus,
                Minus_Review = n.FilmMinus

            }).ToList()[0];
            
  
            return View(review);

            
            
        }
    [HttpPost]
        [Authorize]
        public ActionResult Review(int? numv,string action)
        {

            if (TempData.Peek("da"+ TempData.Peek("num_of_film") as string) as string == null)
            {
                HttpContext.Response.Cookies["coment"+TempData.Peek("num_of_film") as string].Value = "NO";
            }
            else
            {
                HttpContext.Response.Cookies["coment" + TempData.Peek("num_of_film") as string].Value = "DA";
            }

            string res = HttpContext.Request.Cookies["coment"+ TempData.Peek("num_of_film") as string].Value;

            if (res == "DA")
            {
                return Content("<h1> Ви уже голосували </h1>");
            }
            else
            {
                int num = Convert.ToInt32(TempData.Peek("num_of_film") as string);
                Film reiting = db.Films.Find(num);
                switch (action)
                {
                    case "add": { reiting.FilmPlus++; } break;
                    case "minus": { reiting.FilmMinus++; } break;

                    default: ViewBag.Result = "ERRor"; break;
                }

                db.Entry(reiting).State = EntityState.Modified;
                db.SaveChanges();
                //COOKIES
                HttpContext.Response.Cookies["coment" + TempData.Peek("num_of_film") as string].Value = "DA";
                TempData["da"+TempData.Peek("num_of_film") as string] = "DA";

                int addressId = num;

                var review = db.Films.Where(s => s.FilmId == addressId).Select(n => new Review
                {

                    FilmName_Review = n.FilmName,
                    FilmGenre_Review = n.FilmGenre,
                    FilmActors_Review = n.FilmActors,
                    FilmId_Review = n.FilmId,
                    FilmLength_Review = n.FilmLength,
                    FilmReview_Review = n.FilmReview,
                    FilmPictures_Review = n.FilmPictures,
                    FilmAge_Review = n.FilmAge,
                    FilmCountry_Review = n.FilmCountry,
                    FilmRating_Review = n.FilmRating,
                    Plus_Review = n.FilmPlus,
                    Minus_Review = n.FilmMinus

                }).ToList()[0];
                            

                return View(review);

            }
        }

  

        public ActionResult LabelView(int num)
        {
            //creates partial label view
                   
            var labels = db.UserComments.Where(s => s.ReviewID == num).Join(db.UserPhotoes,
                p=>p.UserNickName,
                c=>c.UserName,
                (p,c) => new Comment
            {
                FilmId = p.ReviewID,
                User_Name = p.Name,
                User_LabelText = p.LabelText,
                Date = p.Date,
                User_Photo=c.Photo
            }).ToList();

            var count = labels.Count();
            ViewBag.Count = count;
            ViewBag.User_Comments_List_ForThisFilm = labels;


            return PartialView();

        }


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}