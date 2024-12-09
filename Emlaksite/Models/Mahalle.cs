using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emlaksite.Models
{
    public class Mahalle
    {
        [Key] 
        public int MahalleID { get; set; }
        public string MahalleAd { get; set; }
        //Bir mahhalenin bir semti olabilir birsemtin birden fazla mahalesi olur
        public int SemtID { get; set; }
        public virtual Semt Semt { get; set; }
      
    }
}