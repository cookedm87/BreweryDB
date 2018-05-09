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
    public class StylesController : Controller
    {
        private breweriesEntities6 db = new breweriesEntities6();

        // GET: Styles
        public ActionResult Index()
        {
            var styles = db.Styles.Include(s => s.Country);
            return View(styles.ToList());
        }

        // GET: Styles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Style style = db.Styles.Find(id);
            if (style == null)
            {
                return HttpNotFound();
            }
            return View(style);
        }

        // GET: Styles/Create
        public ActionResult Create()
        {
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Region");
            return View();
        }

        // POST: Styles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CountryId,Style1,Colour,lowABV,highABV,styleWiki")] Style style)
        {
            if (ModelState.IsValid)
            {
                if (db.Styles.Any(m => m.Style1 == style.Style1))
                {
                    ModelState.AddModelError("", "This style already exists");
                }
                else
                {
                    db.Styles.Add(style);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                
            }

            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Region", style.CountryId);
            return View(style);
        }

        // GET: Styles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Style style = db.Styles.Find(id);
            if (style == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Region", style.CountryId);
            return View(style);
        }

        // POST: Styles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CountryId,Style1,Colour,lowABV,highABV,styleWiki")] Style style)
        {
            if (ModelState.IsValid)
            {
                db.Entry(style).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction(actionName: "Index");
            }
            ViewBag.CountryId = new SelectList(db.Countries, "Id", "Region", style.CountryId);
            return View(style);
        }

        // GET: Styles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Style style = db.Styles.Find(id);
            if (style == null)
            {
                return HttpNotFound();
            }
            return View(style);
        }

        // POST: Styles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Style style = db.Styles.Find(id);
            db.Styles.Remove(style);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //CHECK IF STYLE EXISTS

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
