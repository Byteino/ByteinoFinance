using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ByteinoFinance.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; } // "Income" or "Expense"
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
