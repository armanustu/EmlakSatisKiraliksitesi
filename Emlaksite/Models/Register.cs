using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emlaksite.Models
{
	public class Register
	{
		[Required]
		[DisplayName("Adı")]
        public string Name { get; set; }
		[Required]
		[DisplayName("Soyadı")]
		public string Surname { get; set; }
		[Required]
		[DisplayName("Email")]
		public string Email { get; set; }
		[Required]
		[DisplayName("Kullanıcı Adı")]
		public string UserName { get; set; }
		[Required]
		[DisplayName("Şİfre")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
		[Required]
		[Compare("Password",ErrorMessage ="şifreler aynı değil")]
		[DisplayName("Şİfre Tekrar")]
		[DataType(DataType.Password)]
		public string RepeatPasword { get; set; }
		
		
	
	}
}