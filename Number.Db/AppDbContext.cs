using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Number.Db
{
    public class AppDbContext :DbContext
    {
        public DbSet<Input> Inputs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
               @"Server=(localdb)\mssqllocaldb;Database=Inputs;Integrated Security=True");
        }
    }
}
