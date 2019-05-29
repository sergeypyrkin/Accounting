using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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
using Point = System.Windows.Point;
using TabControl = System.Windows.Controls.TabControl;

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
                AccountView av = new AccountView(acc, accounts);
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
            sendTextMember(0);
            //mouve_mouse(100, 100);
            return;
            var aw = new AccountWindow();
            aw.ShowDialog();
            if (aw.DialogResult == true)
            {
                accounts.Add(aw.acc);
                saveAccounts();
            }
            getAccounts();
        }

        private void mouve_mouse(int x, int y)
        {
            Thread.Sleep(5000);
            Point start = new Point(100, 100);
            LinearSmoothMove(start, new TimeSpan(0,0,1));
            SendKeys.SendWait("^+{TAB}");
            LinearSmoothMove(start, new TimeSpan(0, 0, 1));

        }

        public void sendTextMember(int member)
        {
            //ждем немножно
            Thread.Sleep(getRandom(1000,1500));
            if (member != 0) //переключаемся
            {
                SendKeys.SendWait("^+{TAB}");
            }
            //двигаемся на кнопку
            Thread.Sleep(getRandom(100, 200));
            Point start = new Point(720, 420);   //+-20  , +-10 
            LinearSmoothMove(start, new TimeSpan(0, 0, 0,0, getRandom(300, 700)));
            mouseClick();
            Thread.Sleep(getRandom(200, 300));
            SendKeys.SendWait("^+{TAB}");
        }

        public void mouseClick()
        {
            var point = MouseOperations.GetCursorPosition();
            int x = point.X;
            int y = point.Y;
            MouseOperations.mouse_event((int)(MouseOperations.MouseEventFlags.Absolute | MouseOperations.MouseEventFlags.LeftDown), x, y, 0, 0);
            MouseOperations.mouse_event((int)(MouseOperations.MouseEventFlags.Absolute | MouseOperations.MouseEventFlags.LeftUp), x, y, 0, 0);
        }

        public int getRandom(int min, int max)
        {
            Random rand = new Random();
            return rand.Next(min, max);
        }



        public void LinearSmoothMove(Point newPosition, TimeSpan duration)
        {
            var point = MouseOperations.GetCursorPosition();
            Point start = new Point(point.X, point.Y);

            // Find the vector between start and newPosition
            double deltaX = newPosition.X - start.X;
            double deltaY = newPosition.Y - start.Y;

            // start a timer
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            double timeFraction = 0.0;

            do
            {
                timeFraction = (double)stopwatch.Elapsed.Ticks / duration.Ticks;
                if (timeFraction > 1.0)
                    timeFraction = 1.0;

                PointF curPoint = new PointF(Convert.ToInt32(start.X + timeFraction * deltaX),
                    Convert.ToInt32(start.Y + timeFraction * deltaY));
                MouseOperations.SetCursorPosition(Convert.ToInt32(curPoint.X), Convert.ToInt32(curPoint.Y));
                Thread.Sleep(20);
            } while (timeFraction < 1.0);
        }


        //нажатие на ссылку посещения
        private void posLink(object sender, RoutedEventArgs e)
        {
            var form = new AccountPositions(ovView);
            form.ShowDialog();
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


        //переключение вкладки
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            TabControl tabControl = sender as TabControl; // e.Source could have been used instead of sender as well
            TabItem item = tabControl.SelectedValue as TabItem;
            if (item.Name == "calendarTab")
            {
                getAccounts();
            }
        }
    }
}
