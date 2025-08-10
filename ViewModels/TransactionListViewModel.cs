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

        public ICommand DeleteTransactionCommand { get; }

        public decimal TotalIncome { get; private set; }
        public decimal TotalExpense { get; private set; }


        public Action OnListChanged { get; set; }

        public TransactionListViewModel(ObservableCollection<Transaction> transactions)
        {
            Transactions = transactions;
            DeleteTransactionCommand = new RelayCommand<Transaction>(DeleteTransaction);

            CalculateTotals();
        }

        private void DeleteTransaction(Transaction transaction)
        {
            if (transaction == null) return;

            if (Transactions.Contains(transaction))
            {
                Transactions.Remove(transaction);
                TransactionService.SaveTransactions(Transactions);
                CalculateTotals();

                OnListChanged?.Invoke();
            }
        }

        public void CalculateTotals()
        {
            TotalIncome = Transactions.Where(t => t.Type == "Income").Sum(t => t.Amount);
            TotalExpense = Transactions.Where(t => t.Type == "Expense").Sum(t => t.Amount);

            OnPropertyChanged(nameof(TotalIncome));
            OnPropertyChanged(nameof(TotalExpense));
        }
    }
}
