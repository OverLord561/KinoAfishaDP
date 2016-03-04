using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using KinoAfishaDP.Filters;
using KinoAfishaDP.Models;
using WebMatrix.WebData;

namespace KinoAfishaDP.Controllers
{

    [InitializeSimpleMembershipAttribute]
    public class HomeController : Controller
    {

        private AFISHAContext db = new AFISHAContext();
        private UsersContext userprofile = new UsersContext();

        public ActionResult Index()
        {

            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";


            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {


            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize] // Запрещены анонимные обращения к данной странице
        public ActionResult Cabinet()
        {
            SimpleRoleProvider roles = (SimpleRoleProvider)Roles.Provider;
            SimpleMembershipProvider membership = (SimpleMembershipProvider)Membership.Provider;


            string username = Request.IsAuthenticated ? User.Identity.Name : "nothing";
            int UserIdFromTableUsers = membership.GetUserId(username);

            var ccc = db.UserPhotoes.Where(z => z.UserName == username).ToList();
            UserPhoto che = new UserPhoto();
            foreach (var item in ccc)
            {
                che = new UserPhoto { UserPhotoId = item.UserPhotoId, UserName = item.UserName, Photo = item.Photo };
            }
            int UserId = che.UserPhotoId;
            ViewBag.zzz = UserId;



            UserPhoto userprofile = db.UserPhotoes.Find(UserId);
            if (userprofile == null)
            {
                userprofile = new UserPhoto { UserPhotoId = UserId, UserName = username, Photo = "" };
                db.UserPhotoes.Add(userprofile);
                db.SaveChanges();

            }
            else
            {
                ViewBag.Mes = "Юзер уже у нашій таблиці";
            }


            var x = Request.IsAuthenticated ? User.Identity.Name : "nothing";
            var userphoto = db.UserPhotoes.Where(z => z.UserName == x).ToList();

            ViewBag.UserPhoto = userphoto;
            return View();
        }


        [HttpPost]
        [Authorize]
        public ActionResult Cabinet(HttpPostedFileBase file)
        {
            //UsersContext user = new UsersContext();


            SimpleRoleProvider roles = (SimpleRoleProvider)Roles.Provider;
            SimpleMembershipProvider membership = (SimpleMembershipProvider)Membership.Provider;

            var x = Request.IsAuthenticated ? User.Identity.Name : "nothing";
            var userphoto = db.UserPhotoes.Where(z => z.UserName == x).ToList();
            var UserIdFromTableUsers = membership.GetUserId(x);

            var ccc = db.UserPhotoes.Where(z => z.UserName == x).ToList();
            UserPhoto che = new UserPhoto();
            foreach (var item in ccc)
            {
                che = new UserPhoto { UserPhotoId = item.UserPhotoId, UserName = item.UserName, Photo = item.Photo };
            }
            int UserId = che.UserPhotoId;

            UserPhoto photo = db.UserPhotoes.Find(UserId);

            string fileName = Guid.NewGuid().ToString();
            ViewBag.FileName = TempData.Peek("FileName") as string;
            string extension = Path.GetExtension(file.FileName);
            fileName += extension;

            List<string> extensions = new List<string>() { ".txt", ".png", ".jpg", ".pdf", ".zip", ".jpeg" };
            if (extensions.Contains(extension))
            {
                //var FileName = "/Content/Images/Uploads/" + fileName;
                file.SaveAs(Server.MapPath("/Content/Images/Uploads/" + fileName));
                var FileName = "/Content/Images/Uploads/" + fileName;
                photo.Photo = FileName;

                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "Файл сохранен";
            }
            else
            {
                var FileName = "";
                photo.Photo = FileName;

                db.Entry(photo).State = EntityState.Modified;
                db.SaveChanges();
                ViewBag.Message = "Ошибка. Допустимые расширения файлов - '.txt', '.png', '.jpg', '.pdf', '.zip'";
            }



            return RedirectToAction("Cabinet");
        }






        [Authorize(Roles = "Admin")] // К данному методу действия могут получать доступ только пользователи с ролью Admin
        public ActionResult AdminPanel()
        {
            ViewBag.Message = "Admin Panel.";

            return View();
        }

        [Authorize(Roles = "Admin, Moderator")] // К данному методу действия могут получать доступ только пользователи с ролью Admin и Moderator
        public ActionResult ModeratorPanel()
        {
            ViewBag.Message = "Moderator Panel.";

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AdminPanel(string list1)
        {
            var Films = db.Films.ToList();
            var Sessions = db.Sessions.ToList(); ;
            var MovieHouses = db.MovieHouses.ToList(); ;
            var UserComments = db.UserComments.ToList();

            switch (list1)
            {
                case "Films": return RedirectToAction("Index", "Films");
                case "Sessions": return RedirectToAction("Index", "Sessions");
                case "MovieHouses": return RedirectToAction("Index", "MovieHouses");
                case "UserComments": return RedirectToAction("Index", "UserComments");
                default: return View("Index");
            }

            return View();
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult CreateUser(string NameOfUser, string ListOfUser, string PasOfUser)
        {
            SimpleRoleProvider roles = (SimpleRoleProvider)Roles.Provider;
            SimpleMembershipProvider membership = (SimpleMembershipProvider)Membership.Provider;



            if (membership.GetUser(NameOfUser, false) != null)
            {
                return Content("Такий користувач уже існує");
            }

            else
            {
                UserPhoto userprofile = new UserPhoto { UserPhotoId = 1, UserName = NameOfUser, Photo = "" };
                db.UserPhotoes.Add(userprofile);
                db.SaveChanges();
                membership.CreateUserAndAccount(NameOfUser, PasOfUser); // создание пользователя
                roles.AddUsersToRoles(new[] { NameOfUser }, new[] { ListOfUser }); // установка роли для пользователя
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteUser(int id = 0)
        {
            var user = userprofile.UserProfiles.ToList();
            SimpleRoleProvider roles = (SimpleRoleProvider)Roles.Provider;
            SimpleMembershipProvider membership = (SimpleMembershipProvider)Membership.Provider;
            
            UserProfile profile = userprofile.UserProfiles.Find(id);
            
            if (profile == null)
            {
                return View(user);
            }
            else
            {

                UserPhoto userphoto = db.UserPhotoes.FirstOrDefault(x => x.UserName == profile.UserName);

               var roole = roles.GetRolesForUser(profile.UserName);
               roles.RemoveUsersFromRoles(new[]{profile.UserName}, roole);
                membership.DeleteUser(profile.UserName, true);


              
                db.UserPhotoes.Remove(userphoto);
                db.SaveChanges();

                var autorised = Request.IsAuthenticated ? User.Identity.Name : "nothing";
                if (autorised == profile.UserName)
                {
                    WebSecurity.Logout();
                }

               

                TempData["_UserRole"] = "Prosto";
                return RedirectToAction("DeleteUser");
            }


        }
    }
}
