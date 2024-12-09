using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emlaksite.Models
{
	public class Resim
	{
        [Key]
        public int ResimID { get; set; }
        public string ResimAd { get; set; }
        public int IlanID { get; set; }
        public virtual Ilan Ilan { get; set; }
       



    }
}