using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ByteinoFinance.Models;
using Microsoft.EntityFrameworkCore;

namespace ByteinoFinance.Data
{
    public class AppDbContext:DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=byteino.db");
        }
    }
}
