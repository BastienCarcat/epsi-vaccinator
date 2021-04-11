using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vaccinator.Models
{
    public class ContexteBDD : DbContext
    {
        public DbSet<Injection> Injections { get; set; }
        public DbSet<Vaccin> Vaccins { get; set; }
        public DbSet<Personne> Personnes { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=vaccinator.db");
        }
    }
}
