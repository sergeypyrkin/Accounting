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

        }

        public bool check()
        {
            return false;
        }
    }
}
