using Emlaksite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace Emlaksite.Controllers
{
	///Identity yapısını kullanmak için aşağıdaki paket leri yükle
	//Microsoft.AspNet.Identity.Core nuget paketi indir
	//   Microsoft.AspNet.Identity.Owin
	//    Microsoft.AspNet.Identity.EntityFramework
	//Systemweb yazarak Microsoft.Owin.Host.SystemWeb
	public class HomeController : Controller
	{
		DataContext db = new DataContext();
		public ActionResult Index()
		{
			var ilan = db.Ilans.Include(m => m.Mahalle).Include(e => e.Tip).ToList();
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
			return View(ilan);
		}
		public List<Sehir> SehirGetir()
		{
			List<Sehir> sehirler = db.Sehirs.ToList();
			return sehirler;
		}
		public List<Durum> DurumGetir()
		{
			List<Durum> durumlist = db.Durums.ToList();
			return durumlist;
		}
		public PartialViewResult Filtreleme()//Anasayafada combobox olan değerleri getirmek için yazıldı 
		{
			ViewBag.sehirlist = new SelectList(SehirGetir(), "SehirID", "SehirAd");
			ViewBag.durumlist = new SelectList(DurumGetir(), "DurumID", "DurumAd");
			return PartialView();
		}
		//Ansayfadaki filtreleme buttonuna tıklandığında yönlenen action metod
		public ActionResult Filtrele(int min, int max, int mahalleID, int semtID, int TipID, int DurumID)//fiyata durum tip semttipine göre filtreler
		{
			var imgs = db.Resims.ToList();
			ViewBag.imgs = imgs;
			var filtrele = db.Ilans.Where(i => i.Fiyat > min && i.Fiyat <= max && i.DurumID == DurumID &&i.MahalleID==mahalleID && i.TipID == TipID && i.SemtID==semtID).Include(m => m.Mahalle).Include(m => m.Tip).ToList();
			return View(filtrele);
		}
		
		public ActionResult MenuFiltrele(int id)
		{
			var imgs = db.Resims.ToList();
			ViewBag.imgs = imgs;
			var filtre = db.Ilans.Where(i => i.TipID == id).Include(i => i.Mahalle).Include(i => i.Tip).ToList();
			return View(filtre);
		}

		public ActionResult Search(string q)
		{
			var imgs = db.Resims.ToList();
			ViewBag.imgs = imgs;
			var ara = db.Ilans.Include(m => m.Mahalle).Include(m => m.Tip);
			if (!string.IsNullOrEmpty(q))
			{
				ara = ara.Where(i => i.Açıklama.Contains(q) || i.Mahalle.MahalleAd.Contains(q) || i.Tip.TipAd.Contains(q));
			}
			return View(ara.ToList());
		}
		public ActionResult IlanDetay(int id)
		{

			var ilan = db.Ilans.Where(i => i.IlanID == id).Include(m => m.Mahalle).Include(m => m.Tip).FirstOrDefault();
			var imgs = db.Resims.Where(i => i.IlanID == id).ToList();
			ViewBag.imgs = imgs;
			return View(ilan);
		}

		public PartialViewResult Slider()
		{
			var ilan = db.Ilans.Where(i=>i.TipID==28 || i.TipID==29).ToList();
			var imgs = db.Resims.ToList();
			ViewBag.imgs = imgs;
			return PartialView(ilan);
		}
	

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page.";

			return View();
		}
	}
}