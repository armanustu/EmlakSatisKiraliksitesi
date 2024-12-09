using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Emlaksite.Models;

namespace Emlaksite.Controllers
{
    public class DurumController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Durum
        public ActionResult Index()
        {
            return View(db.Durums.ToList());
        }

        // GET: Durum/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Durum durum = db.Durums.Find(id);
            if (durum == null)
            {
                return HttpNotFound();
            }
            return View(durum);
        }

        // GET: Durum/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Durum/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DurumID,DurumAd")] Durum durum)
        {
            if (ModelState.IsValid)
            {
                db.Durums.Add(durum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(durum);
        }
        
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Durum durum = db.Durums.Find(id);
            if (durum == null)
            {
                return HttpNotFound();
            }
            return View(durum);
        }

        // POST: Durum/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DurumID,DurumAd")] Durum durum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(durum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(durum);
        }

        // GET: Durum/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Durum durum = db.Durums.Find(id);
            if (durum == null)
            {
                return HttpNotFound();
            }
            return View(durum);
        }

        // POST: Durum/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Durum durum = db.Durums.Find(id);
            db.Durums.Remove(durum);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
		public PartialViewResult Durumlist()
		{
            var durumlist = db.Durums.ToList();
			return PartialView(durumlist);
		}
		public ActionResult DurumlistSatılıkilanlar(int id)
		{
			var ilan = db.Ilans.Where(i => i.DurumID == id).Include(m => m.Mahalle).Include(e => e.Tip).ToList();
			int index = 0;
			for (int i = 0; i < ilan.Count; i++)
			{
				List<Resim> resimler = new List<Resim>();
				var _ilan = ilan[i];
				index = ilan.FindIndex(f => f.IlanID == _ilan.IlanID);
				var image = db.Resims.Where(w => w.IlanID == _ilan.IlanID).FirstOrDefault();
				resimler.Add(image);
				ilan[index].Resims = resimler;
			}
			return View(ilan.ToList());
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
