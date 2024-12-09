using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emlaksite.Models
{
	public class Semt
	{
        [Key]
        public int SemtID { get; set; }
        public string SemtAd { get; set; }
        public int SehirID { get; set; }
        public virtual Sehir Sehir { get; set; }
        public List<Mahalle> Mahalles { get; set; }
    }
}