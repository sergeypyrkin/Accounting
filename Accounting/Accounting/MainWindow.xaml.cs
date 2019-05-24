using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ListViewItem = System.Windows.Controls.ListViewItem;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace Accounting
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Account> accounts = new List<Account>();
        public List<AccountView> accountsViews = new List<AccountView>();
        public AccountView ovView;
        public DateTime curDate;
        public MainWindow()
        {
            InitializeComponent();
            datePicker1.SelectedDate = DateTime.Today;
            getAccounts();
            b0.Focus();
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
            accountsViews = accountsViews.OrderBy(o => o.fio).ToList();
            dataGridView.ItemsSource = accountsViews;
            dataGridView.Items.Refresh();
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
            getAccounts();
        }

        //нажатие на ссылку посещения
        private void posLink(object sender, RoutedEventArgs e)
        {
        }

        //удалить
        private void delLink(object sender, RoutedEventArgs e)
        {
            if (System.Windows.Forms.MessageBox.Show("УДАЛИТЬ АККАУНТ", "УДАЛЕНИЕ", System.Windows.Forms.MessageBoxButtons.YesNo,
                    System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                Account acc = ovView.acc;
                accounts.Remove(acc);
                saveAccounts();
                getAccounts();

            }
        }

        private void lvi_MouseEnter(object sender, MouseEventArgs e)
        {
            ListViewItem lv = sender as ListViewItem;
            ovView = lv.Content as AccountView;
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            changeSize();
        }

        public void changeSize()
        {
            dataGridView.Width = ActualWidth - 80;
            dataGridView.Height = ActualHeight - 200;
            visControl.Height = ActualHeight - 80;
            visControl.Width = ActualWidth - 80;
            visControl.dataGridView.Height = ActualHeight - 200;
            visControl.dataGridView.Width = ActualWidth - 100;

        }

        private void MainWindow_OnStateChanged(object sender, EventArgs e)
        {
            changeSize();

        }
    }
}
