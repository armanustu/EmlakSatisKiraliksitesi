using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emlaksite.Models
{
	public class Sehir
	{
        [Key]
        public int SehirID { get; set; }
        public string SehirAd { get; set; }
        public List<Semt> Semts { get; set; }
    }
}