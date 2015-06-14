using CampervibeSso.WebApi.Entities;
using CampervibeSso.WebApi.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CampervibeSso.WebApi.Data
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("CampervibeSso")
        {
        }

        public DbSet<Audience> Audiences { get; set; }
    }
}