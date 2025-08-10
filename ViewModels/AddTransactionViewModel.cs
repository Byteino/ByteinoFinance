using ByteinoFinance.Data;
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
using System.Windows;
using System.Windows.Input;

namespace ByteinoFinance.ViewModels
{
    public partial class AddTransactionViewModel : ObservableObject
    {
        public MainViewModel? MainViewModel { get; set; }
        public TransactionListViewModel? ParentViewModel { get; set; }

        [ObservableProperty] private decimal amount;
        [ObservableProperty] private string type = "Expense";
        [ObservableProperty] private string category;
        [ObservableProperty] private string description;
        [ObservableProperty] private DateTime date = DateTime.Now;

        public Action<Transaction>? OnTransactionAdded { get; set; }

        public List<string> TransactionTypes { get; } = new List<string> { "Income", "Expense" };

        [RelayCommand]
        private void SaveTransaction()
        {
            if (string.IsNullOrWhiteSpace(Description) || Amount <= 0)
            {
                MessageBox.Show("Please enter valid data.");
                return;
            }

            var newTransaction = new Transaction
            {
                Description = Description,
                Category = Category,
                Type = Type,
                Amount = Amount,
                Date = Date
            };

            bool handled = false;

            if (MainViewModel != null)
            {
                MainViewModel.Transactions.Add(newTransaction);
                MainViewModel.SaveAndRefresh();
                handled = true;
            }
            else if (ParentViewModel != null)
            {
                ParentViewModel.Transactions.Add(newTransaction);
                ParentViewModel.CalculateTotals();
                TransactionService.SaveTransactions(ParentViewModel.Transactions);
                handled = true;
            }
            else if (OnTransactionAdded != null)
            {
                OnTransactionAdded.Invoke(newTransaction);
                handled = true;
            }

            if (!handled)
            {
                var coll = TransactionService.LoadTransactions();
                coll.Add(newTransaction);
                TransactionService.SaveTransactions(coll);
            }

            var currentWindow = Application.Current.Windows
                .OfType<Window>()
                .FirstOrDefault(w => w.DataContext == this);

            if (currentWindow != null)
            {
                currentWindow.DialogResult = true;
                currentWindow.Close();
            }

        }
    }
}