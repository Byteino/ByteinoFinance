using ByteinoFinance.ViewModels;
using ByteinoFinance.Views;
using System.Windows;
using System.Windows.Input;

namespace ByteinoFinance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TransactionListView listView;
        private TransactionListViewModel listVm;
        private MainViewModel mainVm;

        public MainWindow()
        {
            InitializeComponent();

            mainVm = new MainViewModel();
            this.DataContext = mainVm;

            listVm = new TransactionListViewModel(mainVm.Transactions);
            listVm.OnListChanged = () =>
            {
                mainVm.UpdateSummary();
            };


            listView = new TransactionListView()
            {
                DataContext = listVm
            };

            TransactionListControl.Content = listView;
        }

        private void OpenAddTransaction_Click(object sender, RoutedEventArgs e)
        {
            var addVm = new AddTransactionViewModel();
            var addView = new AddTransactionView(addVm)
            {
                Owner = this
            };

            addVm.OnTransactionAdded = (newTx) =>
            {
                if (newTx == null) return;

                mainVm.Transactions.Add(newTx);
                mainVm.UpdateSummary();

                addView.DialogResult = true;
                addView.Close();
            };

            addView.ShowDialog();
        }

        private void AddTransactionButton_Click(object sender, RoutedEventArgs e)
        {
            OpenAddTransaction_Click(sender, e);
        }
    }
}