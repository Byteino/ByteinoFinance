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
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Transaction> transactions;

        [ObservableProperty]
        private decimal totalIncome;

        [ObservableProperty]
        private decimal totalExpense;

        public MainViewModel()
        {
            Transactions = TransactionService.LoadTransactions();
            UpdateSummary();
        }

        [RelayCommand]
        private void AddTransaction(Transaction transaction)
        {
            if (transaction != null)
            {
                Transactions.Add(transaction);
                SaveAndRefresh();
            }
        }

        [RelayCommand]
        public void DeleteTransaction(Transaction transaction)
        {
            if (transaction != null && Transactions.Contains(transaction))
            {
                Transactions.Remove(transaction);
                SaveAndRefresh();
            }
        }

        public void SaveAndRefresh()
        {
            TransactionService.SaveTransactions(Transactions);
            UpdateSummary();
        }

        public void UpdateSummary()
        {
            TotalIncome = Transactions.Where(t => t.Type == "Income").Sum(t => t.Amount);
            TotalExpense = Transactions.Where(t => t.Type == "Expense").Sum(t => t.Amount);
        }
    }
}
