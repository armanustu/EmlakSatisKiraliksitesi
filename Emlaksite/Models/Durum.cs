using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emlaksite.Models
{
	public class Durum
	{
        [Key]
        public int DurumID { get; set; }
        public string DurumAd { get; set; }//satılık mı kiralık mı
        public List<Tip> Tips { get; set; }//Birden fazla Satılık veya kiralık tipinde arsa olabilir
    }
}