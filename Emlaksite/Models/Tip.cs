using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emlaksite.Models
{
	public class Tip
	{
        [Key]
        public int TipID { get; set; }
        public string TipAd { get; set; }//Dükkan mı arsa villa  kontrol ediyoruz
        public int DurumID { get; set; }
        public virtual Durum Durum { get; set; }

    }
}