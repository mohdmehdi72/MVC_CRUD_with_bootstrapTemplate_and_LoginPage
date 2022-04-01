using CRUDAppWithLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CRUDAppWithLogin.Data;
using System.Web.Security;

namespace CRUDAppWithLogin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
            
        }

        public ActionResult Table()
        {
            studentInfoEntities entities = new studentInfoEntities();
            List<MyModel> model = new List<MyModel>();
            var data = entities.empdatas.ToList();
            foreach (var item in data)
            {
                model.Add(new MyModel
                {
                    id=item.id,
                    ename=item.ename,
                    age = item.age,
                    salay=item.salay,
                    city=item.city
                });
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Forms()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult Forms(MyModel model)
        {
            if (ModelState.IsValid)
            {
                studentInfoEntities entities = new studentInfoEntities();
                empdata table = new empdata();
                table.id = model.id;
                table.ename = model.ename;
                table.age = model.age;
                table.salay = model.salay;
                table.city = model.city;
                if (model.id == 0)
                {
                    entities.empdatas.Add(table);
                    entities.SaveChanges();
                }
                else
                {
                    entities.Entry(table).State = System.Data.Entity.EntityState.Modified;
                    entities.SaveChanges();
                }
                return RedirectToAction("Table");
            }


            return View();
        }
        public ActionResult Edit(int id)
        {
            studentInfoEntities entities = new studentInfoEntities();
            
            
            var edit = entities.empdatas.Where(m => m.id == id).First();
            MyModel model = new MyModel();
            model.id = edit.id;
            model.ename = edit.ename;
            model.age = edit.age;
            model.salay = edit.salay;
            model.city = edit.city;

            return View("Forms",model);
        }
        public ActionResult Delete(int id)
        {
            studentInfoEntities entities = new studentInfoEntities();
            var del = entities.empdatas.Where(m => m.id == id).First();
            entities.empdatas.Remove(del);
            entities.SaveChanges();


            return RedirectToAction("Table");
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Regis()
        {
            
            return View();

        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Regis(LogInModel model)
        {
            studentInfoEntities entities = new studentInfoEntities();
            log_in table = new log_in();
            table.uname = model.uname;
            table.email = model.email;
            table.password = model.password;
            entities.log_in.Add(table);
            entities.SaveChanges();

            return View();

        }
        public ActionResult UserTable()
        {

            studentInfoEntities entities = new studentInfoEntities();
            List<LogInModel> model = new List<LogInModel>();
            var data = entities.log_in.ToList();
            foreach (var item in data)
            {
                model.Add(new LogInModel
                {
                    id = item.id,
                    uname = item.uname,
                    email = item.email,
                    password = item.password
                });
            }

            return View(model);

        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult LogIn(LogInModel model)
        {
            studentInfoEntities entities = new studentInfoEntities();
            var login = entities.log_in.Where(m => m.email == model.email).FirstOrDefault();
            if(login == null)
            {
                TempData["wrongid"] = "You have Entered wrong E-mail!";
            }
            else
            {
                if (login.email == model.email && login.password == model.password)
                {
                    FormsAuthentication.SetAuthCookie(login.email, false);
                    Session["Username"] = login.uname;


                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["wrongpass"] = "You have entered wrong password";
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LogIn");
        }

    }
}