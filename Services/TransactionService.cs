using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ByteinoFinance.Models;

namespace ByteinoFinance.Services
{
    public static class TransactionService
    {

        private static readonly string filePath = "transactions.json";

        public static ObservableCollection<Transaction> LoadTransactions()
        {
            if (!File.Exists(filePath))
                return new ObservableCollection<Transaction>();

            var json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<ObservableCollection<Transaction>>(json)
                   ?? new ObservableCollection<Transaction>();
        }

        public static void SaveTransactions(ObservableCollection<Transaction> transactions)
        {
            var json = JsonSerializer.Serialize(transactions, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
    }
}
