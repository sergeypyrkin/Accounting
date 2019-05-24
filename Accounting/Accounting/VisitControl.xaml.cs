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
        public VisitControl()
        {
            InitializeComponent();
            datePicker1.SelectedDate = DateTime.Today;
            b0.Focus();
        }

        private void getAccount(object sender, RoutedEventArgs e)
        {
        }
    }
}
