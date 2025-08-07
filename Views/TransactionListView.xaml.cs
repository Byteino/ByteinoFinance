using ByteinoFinance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ByteinoFinance.Services;

namespace ByteinoFinance.Views
{

    public partial class TransactionListView : UserControl
    {
        public TransactionListView()
        {
            InitializeComponent();
        }

        private void OpenAddTransactionForm(object sender, RoutedEventArgs e)
        {
            var vm = (TransactionListViewModel)DataContext;

            var addVm = new AddTransactionViewModel();
            addVm.OnTransactionAdded = (newTx) =>
            {
                vm.Transactions.Add(newTx);
                vm.CalculateTotals();
                TransactionService.SaveTransactions(vm.Transactions);
            };

            var addView = new AddTransactionView(addVm); 
            addView.Owner = Window.GetWindow(this);
            addView.ShowDialog();

        }
    }
}
