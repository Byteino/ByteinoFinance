using ByteinoFinance.Data;
using ByteinoFinance.Models;
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


        [ObservableProperty] private decimal amount;

        [ObservableProperty] private string type = "Expense";

        [ObservableProperty] private string category;

        [ObservableProperty] private string description;

        [ObservableProperty] private DateTime date = DateTime.Now;

        public Action<Transaction>? OnTransactionAdded { get; set; }

        [RelayCommand]
        private void Add()
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
            OnTransactionAdded?.Invoke(newTransaction);
            Application.Current.Windows[Application.Current.Windows.Count - 1]?.Close();

        }
        public ObservableCollection<string> TransactionTypes { get; } = new ObservableCollection<string>
        {
            "Income",
            "Expense"
        };

        private ICommand _saveTransactionCommand;
        public ICommand SaveTransactionCommand => _saveTransactionCommand ??= new RelayCommand(SaveTransaction);

        private void SaveTransaction()
        {
            var newTransaction = new Transaction
            {
                Amount = this.Amount,
                Type = this.Type,
                Category = this.Category,
                Description = this.Description,
                Date = this.Date
            };

            OnTransactionAdded?.Invoke(newTransaction);


        }
    }
}