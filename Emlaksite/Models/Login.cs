using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Emlaksite.Models
{
	public class Login
	{
        [Required]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }
        [Required]
        [DisplayName("Şİfre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
       
        public bool RememberMe { get; set; }
       
    }
}