using Emlaksite.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Emlaksite.Models
{
	public class DataContext :IdentityDbContext<ApplicationUser>
	{
        public DataContext() : base("IdentityConnection")
	{
           
        } 
public DbSet<Durum> Durums{get;set;}
public DbSet<Ilan> Ilans{get;set;}
public DbSet<Tip> Tips{ get; set; }
public DbSet<Resim> Resims{ get; set; }
public DbSet<Sehir> Sehirs{ get; set; }
public DbSet<Semt> Semts{ get; set; }
public DbSet<Mahalle> Mahalles{get;set;}
public DbSet<Profile>Profiles { get; set; }
	}
}