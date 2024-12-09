using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Emlaksite.Models
{
	public class Profile
	{
		[Key]	
		public int ProfileID { get; set; }
		public string ID { get; set; }
		[Required]
		[DisplayName("Adı")]
        public string Name { get; set; }
		[Required]
		[DisplayName("SoyAdı")]
		public string Surname { get; set; }
		[Required]
		[DisplayName("Email")]
		[EmailAddress(ErrorMessage="Geçerli bir mail giriniz")]
		public string Email { get; set; }
		[Required]
		[DisplayName("Kullanıcı Adı")]
		public string Username { get; set; }
		
		

	}
}