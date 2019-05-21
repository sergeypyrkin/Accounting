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

namespace Accounting
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Account> accounts = new List<Account>();
        public List<AccountView> accountsViews = new List<AccountView>();
        public DateTime curDate;
        public MainWindow()
        {
            InitializeComponent();
            datePicker1.SelectedDate = DateTime.Today;
            getAccounts();
        }

        public void getAccounts()
        {
            accounts = Account.LoadFromFile("accounts");
            curDate = datePicker1.SelectedDate.Value;
            accountsViews.Clear();
            foreach (Account acc in accounts)
            {
                AccountView av = new AccountView(acc);
                if (av.isCurrentAcc(curDate))
                {
                    accountsViews.Add(av);
                }
            }
        }


        //получить список аккаунтов
        private void getAccount(object sender, RoutedEventArgs e)
        {

            getAccounts();
        }

        public void saveAccounts()
        {
            Account.SaveToFile("accounts", accounts);
        }

        private void newAccount(object sender, RoutedEventArgs e)
        {
            var aw = new AccountWindow();
            aw.ShowDialog();
            if (aw.DialogResult == true)
            {
                accounts.Add(aw.acc);
                saveAccounts();
            }
        }
    }
}
