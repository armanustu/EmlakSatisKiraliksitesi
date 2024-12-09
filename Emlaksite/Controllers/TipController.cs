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
    public class TipController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Tip
        public ActionResult Index()
        {
            var tips = db.Tips.Include(t => t.Durum);
            return View(tips.ToList());
        }
       
        public PartialViewResult DurumKiralıkMetni()
        {
            var durumkiralık = db.Durums.Where(i => i.DurumID == 3).FirstOrDefault();
            return PartialView(durumkiralık);
        }
		
		public PartialViewResult DurumSatılıkMetni()
        {
            var durumsatılık = db.Durums.Where(i => i.DurumID == 4).FirstOrDefault();
            return PartialView(durumsatılık);
        }
        public ActionResult Satılıkilanlar(int id)
        {

			var ilan = db.Ilans.Where(i=>i.TipID==id).Include(m => m.Mahalle).Include(e => e.Tip).ToList();
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

		////Bence burası durum kontrollırında oluşturulması gerekir
		public PartialViewResult DurumTipSatilikPartial()//Satılık ev arsa rezidans isimlerini getiriyoruz
		{
			var durum1 = db.Tips.Where(i => i.DurumID == 4).ToList();
			return PartialView(durum1.ToList());
		}
		public PartialViewResult DurumTipKiralikPartial()//Satılık ev arsa rezidans isimlerini getiriyoruz
		{
			var durum1 = db.Tips.Where(i => i.DurumID == 3).ToList();
			return PartialView(durum1.ToList());
		}
        public PartialViewResult DurumAd1()//Kiralık yazzısı
        {
			var durum1 = db.Durums.Where(i => i.DurumID == 3).FirstOrDefault();
			return PartialView(durum1);
		}
        public PartialViewResult DurumAd2()//Satılık yazısı
        {
			var durum2 = db.Durums.Where(i => i.DurumID == 4).FirstOrDefault();
			return PartialView(durum2);
		}
		//public PartialViewResult DurumTipPartial2()//Kiralık ev arsa rezidans isimlerini getiriyoruz
		//{
		//	var durum1 = db.Tips.Where(i => i.DurumID == 3).ToList();
		//	return PartialView(durum2);
		//}

		// GET: Tip/Details/5
		public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip tip = db.Tips.Find(id);
            if (tip == null)
            {
                return HttpNotFound();
            }
            return View(tip);
        }

        // GET: Tip/Create
        public ActionResult Create()
        {
            ViewBag.DurumID = new SelectList(db.Durums, "DurumID", "DurumAd");
            return View();
        }

        // POST: Tip/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TipID,TipAd,DurumID")] Tip tip)
        {
            if (ModelState.IsValid)
            {
                db.Tips.Add(tip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DurumID = new SelectList(db.Durums, "DurumID", "DurumAd", tip.DurumID);
            return View(tip);
        }

        // GET: Tip/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip tip = db.Tips.Find(id);
            if (tip == null)
            {
                return HttpNotFound();
            }
            ViewBag.DurumID = new SelectList(db.Durums, "DurumID", "DurumAd", tip.DurumID);
            return View(tip);
        }

        // POST: Tip/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TipID,TipAd,DurumID")] Tip tip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DurumID = new SelectList(db.Durums, "DurumID", "DurumAd", tip.DurumID);
            return View(tip);
        }

        // GET: Tip/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tip tip = db.Tips.Find(id);
            if (tip == null)
            {
                return HttpNotFound();
            }
            return View(tip);
        }

        // POST: Tip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tip tip = db.Tips.Find(id);
            db.Tips.Remove(tip);
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
