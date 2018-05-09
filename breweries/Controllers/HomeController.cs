using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using breweries.Models;
using System.Net;
using Newtonsoft.Json;
using System.Web.Helpers;
using System.Text;
using System.Web.Script.Serialization;
using System.Data.SqlClient;

namespace breweries.Controllers
{
    public class HomeController : Controller
    {
        private breweriesEntities6 db = new breweriesEntities6();
        // GET: Home
        public ActionResult Index()
        {
            var brewery = from b in db.Breweries
                          select b;
            return View(brewery);
        }
        //GET: Breweries/Create
        public ActionResult Create()
        {
            return View();
        }
        //POST: Breweries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name, Location, Website")]Brewery brewery)
        {
            if (ModelState.IsValid)
            {
                db.Breweries.Add(brewery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brewery);
        }
        public ActionResult Map()
        {
            return View();
        }
        //GET: StaffMembers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Brewery brew = db.Breweries.Find(id);
            if (brew == null)
            {
                return HttpNotFound();
            }
            return View(brew);
        }
        [HttpGet]
        //GET: /Home/
        public JsonResult GetBrewery()
        {
            using (db)
            {
                var result = (from b in db.Breweries
                              orderby b.Name ascending
                              select new { b.Name, b.Location, b.lat, b.lon, b.Id }).ToList();
                return Json(JsonConvert.SerializeObject(result), JsonRequestBehavior.AllowGet);
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}