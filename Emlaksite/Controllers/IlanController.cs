using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Emlaksite.Models;

namespace Emlaksite.Controllers
{
    public class IlanController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Ilan
        public ActionResult Index()
        {
            // var ilans = db.Ilans.Include(i => i.Mahalle).Include(i => i.Tip);
            var username = User.Identity.Name;
			var ilans = db.Ilans.Where(i=>i.UserName==username).Include(i => i.Mahalle).Include(i => i.Tip);
			return View(ilans.ToList());
        }
      
        public List<Sehir> SehirGetir()
        {
            List<Sehir> sehirler = db.Sehirs.ToList();
            return sehirler;
        }
        public ActionResult SemtGetir(int sehirID)
        {
            List<Semt> semtlist = db.Semts.Where(m => m.SehirID == sehirID).ToList();
            ViewBag.semtlistesi = new SelectList(semtlist, "SemtID","SemtAd");
            return PartialView("SemtPartial");
        }
        public ActionResult MahalleGetir(int semtID)
        {
            List<Mahalle> mahallelist = db.Mahalles.Where(m=>m.SemtID==semtID).ToList();
            ViewBag.mahallelistesi = new SelectList(mahallelist, "MahalleID", "MahalleAd");
            return PartialView("MahallePartial");

        }
        public ActionResult ResimEkle(int id)
        {
            var ilanimg = db.Ilans.Where(s => s.IlanID == id).ToList();
            var resimler = db.Resims.Where(s => s.IlanID ==id).ToList();
            ViewBag.IlanResim = ilanimg;
            ViewBag.Resimler = resimler;

            return View();
        }
        [HttpPost]
        public ActionResult ResimEkle(int id,HttpPostedFileBase file)
        {
            string path = Path.Combine("/Content/images/" + file.FileName);
            file.SaveAs(Server.MapPath(path));
            Resim rsm = new Resim();
            rsm.ResimAd = file.FileName.ToString();
            rsm.IlanID = id;
            db.Resims.Add(rsm);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

		public List<Durum> DurumGetir()
        {
            List<Durum> durumlist = db.Durums.ToList();
            return durumlist;
        }
        public ActionResult TipGetir(int durumID)
        {
            List<Tip> tiplist = db.Tips.Where(x => x.DurumID == durumID).ToList();
            ViewBag.tiplistesi = new SelectList(tiplist, "TipID", "TipAd");
            return PartialView("TipPartial");
        }
        // GET: Ilan/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ilan ilan = db.Ilans.Find(id);
            if (ilan == null)
            {
                return HttpNotFound();
            }
            return View(ilan);
        }

        // GET: Ilan/Create
        public ActionResult Create()
        {
            ViewBag.sehirlist = new SelectList(SehirGetir(), "SehirID", "SehirAd");
            ViewBag.durumlist = new SelectList(DurumGetir(), "DurumID", "DurumAd");
            return View();
        }

        // POST: Ilan/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IlanID,Açıklama,Fiyat,OdaSayisi,BanyoSayisi,Kredi,Alan,Kat,Telefon,Adres,UserName,SehirID,SemtID,DurumID,MahalleID,TipID")] Ilan ilan)
        {
            if (ModelState.IsValid)
            {
                ilan.UserName = User.Identity.Name;
                db.Ilans.Add(ilan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
			ViewBag.sehirlist = new SelectList(SehirGetir(), "SehirID", "SehirAd");			
			ViewBag.durumlist = new SelectList(DurumGetir(), "DurumID", "DurumAd");
			return View(ilan);
        }

        // GET: Ilan/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ilan ilan = db.Ilans.Find(id);
            if (ilan == null)
            {
                return HttpNotFound();
            }
			ViewBag.sehirlist = new SelectList(SehirGetir(), "SehirID", "SehirAd");
			ViewBag.durumlist = new SelectList(DurumGetir(), "DurumID", "DurumAd");
			ViewBag.MahalleID = new SelectList(db.Mahalles, "MahalleID", "MahalleAd", ilan.MahalleID);
			ViewBag.SemtID = new SelectList(db.Semts, "SemtID", "SemtAd", ilan.SemtID);
			ViewBag.TipID = new SelectList(db.Tips, "TipID", "TipAd", ilan.TipID);
            return View(ilan);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IlanID,Açıklama,Fiyat,OdaSayisi,BanyoSayisi,Kredi,Alan,Kat,Telefon,Adres,UserName,SehirID,SemtID,DurumID,MahalleID,TipID")] Ilan ilan)
        {
            if (ModelState.IsValid)
            {
                ilan.UserName = User.Identity.Name;
                db.Entry(ilan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
			ViewBag.sehirlist = new SelectList(SehirGetir(), "SehirID", "SehirAd");
			ViewBag.durumlist = new SelectList(DurumGetir(), "DurumID", "DurumAd");		
            return View(ilan);
        }

        // GET: Ilan/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ilan ilan = db.Ilans.Find(id);
            if (ilan == null)
            {
                return HttpNotFound();
            }
            return View(ilan);
        }

        // POST: Ilan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ilan ilan = db.Ilans.Find(id);
            db.Ilans.Remove(ilan);
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
