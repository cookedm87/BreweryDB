using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using breweries.Models;

namespace breweries.Controllers
{
    public class beersController : Controller
    {
        private breweriesEntities6 db = new breweriesEntities6();

        // GET: beers
        public ActionResult Index(string Brew, string sty)
        {
            //return View(db.beers.ToList());
            //genre search
            var beers = from m in db.beers
                        select m;

            var BrewLst = new List<string>();
            var BrewQry = from d in db.beers
                          orderby d.Brewery.Name
                          select d.Brewery.Name;
            BrewLst.AddRange(BrewQry.Distinct());
            ViewBag.Brew = new SelectList(BrewLst);

            var styleLst = new List<string>();
            var styleQry = from d in db.beers
                           orderby d.Style.Style1
                           select d.Style.Style1;
            styleLst.AddRange(styleQry.Distinct());
            ViewBag.sty = new SelectList(styleLst);
            if (!String.IsNullOrEmpty(Brew))
            {
                beers = beers.Where(x => x.Brewery.Name == Brew);
            }
            if (!String.IsNullOrEmpty(sty))
            {
                beers = beers.Where(x => x.Style.Style1 == sty);
            }
            return View(beers.ToList());
        }
        // GET: beers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            beer beer = db.beers.Find(id);
            if (beer == null)
            {
                return HttpNotFound();
            }
            return View(beer);
        }

        // GET: beers/Create
        public ActionResult Create()
        {
            ViewBag.BreweryId = new SelectList(db.Breweries, "Id", "Name");
            ViewBag.StyleId = new SelectList(db.Styles, "Id", "Style1");
            return View();
        }

        // POST: beers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StyleId,BreweryId,Name,Strength,Can,Bottle,Keg,Badge")] beer beer)
        {
            if (ModelState.IsValid)
            {
                db.beers.Add(beer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BreweryId = new SelectList(db.Breweries, "Id", "Name", beer.BreweryId);
            ViewBag.StyleId = new SelectList(db.Styles, "Id", "Style1", beer.StyleId);
            return View(beer);
        }

        // GET: beers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            beer beer = db.beers.Find(id);
            if (beer == null)
            {
                return HttpNotFound();
            }
            ViewBag.BreweryId = new SelectList(db.Breweries, "Id", "Name", beer.BreweryId);
            ViewBag.StyleId = new SelectList(db.Styles, "Id", "Style1", beer.StyleId);
            return View(beer);
        }

        // POST: beers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StyleId,BreweryId,Name,Strength,Can,Bottle,Keg,Badge")] beer beer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(beer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BreweryId = new SelectList(db.Breweries, "Id", "Name", beer.BreweryId);
            ViewBag.StyleId = new SelectList(db.Styles, "Id", "Style1", beer.StyleId);
            return View(beer);
        }

        // GET: beers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            beer beer = db.beers.Find(id);
            if (beer == null)
            {
                return HttpNotFound();
            }
            return View(beer);
        }

        // POST: beers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            beer beer = db.beers.Find(id);
            db.beers.Remove(beer);
            db.SaveChanges();
            return RedirectToAction("Index");
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
