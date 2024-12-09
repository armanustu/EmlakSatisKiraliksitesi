using Emlaksite.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Emlaksite.Identity
{
	public class IdentityInitializer :CreateDatabaseIfNotExists<DataContext>
	{
		protected override void Seed(DataContext context)
		{
			if (!context.Roles.Any(i => i.Name == "admin"))
			{
				var store = new RoleStore<ApplicationRole>(context);
				var manager = new RoleManager<ApplicationRole>(store);
				var role = new ApplicationRole() { Name = "admin",Description = "admin rolü" };
				manager.Create(role);
			}
			if (!context.Roles.Any(i => i.Name == "user"))
			{
				var store = new RoleStore<ApplicationRole>(context);
				var manager = new RoleManager<ApplicationRole>(store);
				var role = new ApplicationRole() { Name = "user", Description = "user rolü" };
				manager.Create(role);

			}
			if (!context.Users.Any(i => i.Name == "ilyasdagdelen"))
			{
				var store = new UserStore<ApplicationUser>(context);
				var manager = new UserManager<ApplicationUser>(store);
				var user = new ApplicationUser() { Name = "ilyas",Surname="dagdelen" ,UserName ="ilyasdagdelen", Email="ilyas@hotmail.com",};
				manager.Create(user, "123456");
				manager.AddToRole(user.Id, "admin");
				manager.AddToRole(user.Id, "user");			
				

			}
			if (!context.Users.Any(i => i.Name == "hamzadagdelen"))
			{
				var store = new UserStore<ApplicationUser>(context);
				var manager = new UserManager<ApplicationUser>(store);
				var user = new ApplicationUser() { Name = "hamza", Surname = "dagdelen", UserName = "hamzadagdelen", Email = "hamza@hotmail.com" };
				manager.Create(user, "123456");
				manager.AddToRole(user.Id, "user");			
				

			}
			context.SaveChanges();
			base.Seed(context);
		}
	}
}