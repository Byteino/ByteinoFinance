using ByteinoFinance.Models;
using ByteinoFinance.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ByteinoFinance.ViewModels
{
    public partial class TransactionListViewModel : ObservableObject
    {
        public ObservableCollection<Transaction> Transactions { get; set; }

        [ObservableProperty]
        private decimal totalIncome;

        [ObservableProperty]
        private decimal totalExpense;



        public TransactionListViewModel()
        {
            Transactions = TransactionService.LoadTransactions();
            Transactions.Add(new Transaction
            {
                Amount = 100,
                Type = "Income",
                Category = "Test",
                Date = DateTime.Now
            });
            CalculateTotals();
        }



        public ICommand DeleteTransactionCommand => new RelayCommand<Transaction>(DeleteTransaction);


        private void DeleteTransaction(Transaction transaction)
        {
            if (transaction != null)
            {
                Transactions.Remove(transaction);
                CalculateTotals();
                TransactionService.SaveTransactions(Transactions);
            }
        }

        public void CalculateTotals()
        {
            TotalIncome = Transactions
                .Where(t => t.Type == "Income")
                .Sum(t => t.Amount);

            TotalExpense = Transactions
                .Where(t => t.Type == "Expense")
                .Sum(t => t.Amount);
        }

    }
}
