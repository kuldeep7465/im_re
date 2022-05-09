using radio.connetion;
using radio.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace radio.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            radioEntities obj1 = new radioEntities();
            var res = obj1.Tables.ToList();
            return View(res);
        }

        public ActionResult form()
        {


            return View();
        }
        [HttpPost]
        public ActionResult form(Class1 emp)
        {
            radioEntities obj1 = new radioEntities();
            Table obj2 = new Table();
            obj2.id = emp.id;
            obj2.name = emp.name;
            obj2.gender = emp.gender;
            obj2.Date = emp.Date;
            obj2.image = emp.image;
           
            obj1.Tables.Add(obj2);
            obj1.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}