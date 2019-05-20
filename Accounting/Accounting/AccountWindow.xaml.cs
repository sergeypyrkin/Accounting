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
    /// Логика взаимодействия для AccountWindow.xaml
    /// </summary>
    public partial class AccountWindow : Window
    {
        public Account acc = null;

        public AccountWindow()
        {
            InitializeComponent();
            datePicker1.SelectedDate = DateTime.Today;

        }

        //создание аккаунта
        private void createAccount(object sender, RoutedEventArgs e)
        {
            bool ch = check();
            if (ch)
            {
                acc = new Account();
                acc.fio = fio.Text;
                acc.StDate = datePicker1.SelectedDate.Value;
                acc.days = Convert.ToInt32(col.Text);
                DialogResult = true;
            }
        }

        public bool check()
        {
            if (String.IsNullOrEmpty(fio.Text))
            {
                MessageBox.Show("Введите фио");
                return false;
            }
            if (String.IsNullOrEmpty(col.Text))
            {
                MessageBox.Show("Введите кол-во дней");
                return false;
            }
            int res;
            if (Int32.TryParse(col.Text, out  res) == false)
            {
                MessageBox.Show("Число дней должно быть числом");
                return false;
            }
            return true;
        }
    }
}
