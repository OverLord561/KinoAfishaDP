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
    public class UserPhotoesController : Controller
    {
        private AFISHAContext db = new AFISHAContext();

        //
        // GET: /UserPhotoes/
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Index( string nickname,int? id)
        {
            var prof = nickname !=null ? db.UserPhotoes.Where(x => x.UserName.Contains(nickname) ) : db.UserPhotoes;
            var profile = prof.Join(db.UserComments,
                x=>x.UserName,
                c=>c.UserNickName,
                (x,c)=> new Profile{
                NAME=x.UserName,
                PHOTO=x.Photo,
                EMAIL=c.E_mail,
                ProfileID=x.UserPhotoId});

            return View(profile.Distinct().ToList());
        }
        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Index(string name)
        {
            var photoes =name!=""? db.UserPhotoes.Where(x=>x.UserName.ToUpper().Contains(name.ToUpper())):db.UserPhotoes;
            return View(photoes);
        }
        //
        // GET: /UserPhotoes/Details/5
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Details(int id = 0)
        {
            UserPhoto userphoto = db.UserPhotoes.Find(id);
            if (userphoto == null)
            {
                return HttpNotFound();
            }
            return View(userphoto);
        }

        //
        // GET: /UserPhotoes/Create
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /UserPhotoes/Create

        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Create(UserPhoto userphoto)
        {
            if (ModelState.IsValid)
            {
                db.UserPhotoes.Add(userphoto);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userphoto);
        }

        //
        // GET: /UserPhotoes/Edit/5
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Edit(int id = 0)
        {
            UserPhoto userphoto = db.UserPhotoes.Find(id);
            if (userphoto == null)
            {
                return HttpNotFound();
            }
            return View(userphoto);
        }

        //
        // POST: /UserPhotoes/Edit/5

        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Edit(UserPhoto userphoto)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userphoto).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userphoto);
        }

        //
        // GET: /UserPhotoes/Delete/5
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Delete(int id = 0)
        {
            UserPhoto userphoto = db.UserPhotoes.Find(id);
            if (userphoto == null)
            {
                return HttpNotFound();
            }
            return View(userphoto);
        }

        //
        // POST: /UserPhotoes/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserPhoto userphoto = db.UserPhotoes.Find(id);
            db.UserPhotoes.Remove(userphoto);
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