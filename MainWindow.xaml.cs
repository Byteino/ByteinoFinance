using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ByteinoFinance.ViewModels;
using ByteinoFinance.Views;

namespace ByteinoFinance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TransactionListView listView;
        private TransactionListViewModel listVm;
        public MainWindow()
        {
            InitializeComponent();
            listVm = new TransactionListViewModel();
            listView = new TransactionListView()
            {
                DataContext = listVm
            };
            TransactionListControl.Content = listView;
        }

        private void OpenAddTransaction_Click(object sender, RoutedEventArgs e)
        {
            var addVm = new AddTransactionViewModel();
            addVm.OnTransactionAdded = (newTx) =>
            {
                listVm.Transactions.Add(newTx);
                listVm.CalculateTotals();
                ByteinoFinance.Services.TransactionService.SaveTransactions(listVm.Transactions);
            };

            var addView = new AddTransactionView(addVm); 
            addView.Owner = this;
            addView.ShowDialog();
        }
    }
}