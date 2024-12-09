using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Emlaksite.Identity
{
	//Bu klası ekledikten sonra App_Start klasına Add new itemdan OWINStartup clasını ekliyoruz sonra aşağıdaki kodu yazıyoruz
	public class ApplicationRole :IdentityRole
	{
        public string Description { get; set; }
        public ApplicationRole()
        {

        }
        public ApplicationRole(string roleName, string Description)
        {
            this.Description = Description;

        }
      
    }
}