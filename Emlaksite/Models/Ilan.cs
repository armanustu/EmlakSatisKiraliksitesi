using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emlaksite.Models
{
	public class Ilan
	{
		[Key]
        public int IlanID { get; set; }

		public string Açıklama { get; set; }
		public double Fiyat { get; set; }
		public int OdaSayisi { get; set; }
		public int BanyoSayisi { get; set; }
		public bool Kredi { get; set; }		
		public int Alan { get; set; }
		public string Kat { get; set; }
		public string Telefon { get; set; }
		public string Adres { get; set; }
		public string UserName { get; set; }
		public int SehirID { get; set; }
		public int SemtID { get; set; }
		public int DurumID { get; set; }
		public int MahalleID { get; set; }
		public Mahalle Mahalle { get; set; }
		public int TipID { get; set; }
		public Tip Tip { get; set; }
        public List<Resim> Resims { get; set; }
    }
}