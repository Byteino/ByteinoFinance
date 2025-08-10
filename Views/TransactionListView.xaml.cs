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

            var addVm = new AddTransactionViewModel
            {
                ParentViewModel = vm
            };


            var ownerWindow = Window.GetWindow(this);
            if (ownerWindow?.DataContext is MainViewModel mainVm)
            {
                addVm.MainViewModel = mainVm;
            }

            var addView = new AddTransactionView(addVm);

            if (ownerWindow != null)
                addView.Owner = ownerWindow;

            addView.ShowDialog();
        }

    }
}
