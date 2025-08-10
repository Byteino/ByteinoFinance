using ByteinoFinance.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ByteinoFinance.Services
{
    public static class TransactionService
    {
        private static string FilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "transactions.json");

        public static ObservableCollection<Transaction> LoadTransactions()
        {
            try
            {
                if (!File.Exists(FilePath)) return new ObservableCollection<Transaction>();
                var json = File.ReadAllText(FilePath);
                var list = JsonConvert.DeserializeObject<List<Transaction>>(json) ?? new List<Transaction>();
                return new ObservableCollection<Transaction>(list);
            }
            catch
            {
                return new ObservableCollection<Transaction>();
            }
        }

        public static void SaveTransactions(IEnumerable<Transaction> transactions)
        {
            try
            {
                var json = JsonConvert.SerializeObject(transactions, Formatting.Indented);
                File.WriteAllText(FilePath, json);
            }
            catch
            {
                // ignore write errors for now or log
            }
        }
    }
}
