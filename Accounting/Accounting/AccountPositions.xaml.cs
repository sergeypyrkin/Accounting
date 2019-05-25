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
    /// Логика взаимодействия для AccountPositions.xaml
    /// </summary>
    public partial class AccountPositions : Window
    {
        public Account account;
        public AccountView av;
        public List<APosition> aposss = new List<APosition>();
        public AccountPositions(AccountView av)
        {
            this.account = av.acc;
            this.av = av;
            InitializeComponent();
            fio.Content = account.fio;
            creatPosVies();


        }

        public void creatPosVies()
        {
            //List<Date> date 
            for (var date = account.StDate; date <= av.finDate; date = date.AddDays(1))
            {
                APosition apos = new APosition();
                apos.date = date.ToString("dd-MM-yyyy");
                apos.isVisited = account.usedDates.Contains(date);
                aposss.Add(apos);
            }
            dataGridView.ItemsSource = aposss;
            dataGridView.Items.Refresh();

        }


        public class APosition
        {
            public string date { get; set; }
            public bool isVisited { get; set; }
        }
    }
}
