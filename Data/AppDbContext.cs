using System.Diagnostics.Tracing;
using System.Security.Principal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using dotnet.Models;
using Users.Models;
using student.Models;
using Login.Models;



namespace dotnet.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Product { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Student { get; set; }
        public DbSet<Login.Models.Login> Logins { get; set; }
    }

}




