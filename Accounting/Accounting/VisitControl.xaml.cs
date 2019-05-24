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
using System.Windows.Shapes;

namespace Accounting
{
    /// <summary>
    /// Логика взаимодействия для VisitControl.xaml
    /// </summary>
    public partial class VisitControl : Canvas
    {

        public List<Account> accounts = new List<Account>();
        public List<AccountVisit> accountsVisits  = new List<AccountVisit>();

        public DateTime curDate;
        public VisitControl()
        {
            InitializeComponent();
            datePicker1.SelectedDate = DateTime.Today;
            b0.Focus();
            getAccounts();
        }

        public void getAccounts()
        {
            accounts = Account.LoadFromFile("accounts");
            curDate = datePicker1.SelectedDate.Value;
            accountsVisits.Clear();
            foreach (Account acc in accounts)
            {

                AccountVisit av = new AccountVisit(acc);
                AccountView a = new AccountView(acc);
                if (a.isCurrentAcc(curDate))
                {
                    av.date = curDate;
                    accountsVisits.Add(av);
                   
                }
            }
            accountsVisits = accountsVisits.OrderBy(o => o.fio).ToList();
            dataGridView.ItemsSource = accountsVisits;
            dataGridView.Items.Refresh();
        }

        private void getAccounts(object sender, RoutedEventArgs e)
        {
            getAccounts();
        }

        private void lvi_MouseEnter(object sender, MouseEventArgs e)
        {
        }

        private void save(object sender, RoutedEventArgs e)
        {
        }
    }




}
