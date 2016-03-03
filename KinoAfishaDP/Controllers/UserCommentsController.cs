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
    public class UserCommentsController : Controller
    {
        private AFISHAContext db = new AFISHAContext();

        //
        // GET: /UserComments/

        public ActionResult Index(string name,int? id, DateTime? start, DateTime? end,string nickname)
        {
            var FIRST = db.UserComments;
            var comment1 =name!=null ? db.UserComments.Where(x=>x.Name.Contains(name)):db.UserComments;
            var comment2 = id != null ? comment1.Where(x => x.ReviewID == id): db.UserComments;
            var date1 = start != null ? comment2.Where(x => x.Date >= start) : db.UserComments;
            var date2 = end != null ? comment2.Where(x => x.Date <= end) : db.UserComments;
            var nick = nickname != null ? date2.Where(x => x.UserNickName.Contains(nickname)) : db.UserComments;

            var comments = comment1.Intersect(comment2).Intersect(date1).Intersect(date2).Intersect(FIRST).Intersect(nick);
            return View(comments.OrderByDescending(x => x.Date).ToList());
        }

        //
        // GET: /UserComments/Details/5
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Details(int id = 0)
        {
            UserComment usercomment = db.UserComments.Find(id);
            if (usercomment == null)
            {
                return HttpNotFound();
            }
            return View(usercomment);
        }

        //
        // GET: /UserComments/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /UserComments/Create

        [HttpPost]
        [Authorize]
        public ActionResult Create(UserComment usercomment)
        {
            usercomment.Date = DateTime.Now;
            TempData["UserMail"] = usercomment.E_mail;
            //usercomment.ReviewID = Convert.ToInt32(TempData["num_of_tank"] as string);
            usercomment.ReviewID = Convert.ToInt32(TempData.Peek("num_of_film") as string);


            //Якщо ім'я користувача буду вказане із "пробіл", то видалимо ці пробіли. БО НЕКОРЕКТНО ШУКАЄ ЗАПИСИ В ТАБЛИЦІ КОМЕНТАРІВ
            string[] split = usercomment.Name.Split(new Char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            usercomment.Name = split[0];

            usercomment.UserNickName = Request.IsAuthenticated ? User.Identity.Name : User.Identity.Name;

            if (ModelState.IsValid)
            {
                db.UserComments.Add(usercomment);
                db.SaveChanges();
                return RedirectToAction("Review", "Films", new { num = usercomment.ReviewID });

            }

            return View(usercomment);
        }

        //
        // GET: /UserComments/Edit/5
       [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Edit(int id = 0)
        {
            UserComment usercomment = db.UserComments.Find(id);
            if (usercomment == null)
            {
                return HttpNotFound();
            }
            return View(usercomment);
        }

        //
        // POST: /UserComments/Edit/5

        [HttpPost]
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Edit(UserComment usercomment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usercomment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usercomment);
        }

        //
        // GET: /UserComments/Delete/5
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult Delete(int id = 0)
        {
            UserComment usercomment = db.UserComments.Find(id);
            if (usercomment == null)
            {
                return HttpNotFound();
            }
            return View(usercomment);
        }

        //
        // POST: /UserComments/Delete/5

        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult DeleteConfirmed(int id)
        {
            UserComment usercomment = db.UserComments.Find(id);
            db.UserComments.Remove(usercomment);
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