using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting
{
    public class AccountView
    {

        public string fio { get; set; }
        public DateTime StDate { get; set; }
        public DateTime finDate { get; set; }

        public int days { get; set; }
        public List<DateTime> usedDates = new List<DateTime>();
        public Account acc;
        public List<Account> accounts;
        public AccountView(Account ac, List<Account> accounts)
        {
            this.accounts = accounts;

            this.acc = ac;
            this.fio = ac.fio;
            this.StDate = ac.StDate;
            this.days = ac.days;
            this.usedDates = ac.usedDates;
            finDate = StDate.AddDays(days);
            stDateText = StDate.ToString("dd-MM-yyyy");
            finDateText = StDate.AddDays(days).ToString("dd-MM-yyyy");
            DateTime now = DateTime.Now;
            DateTime last = StDate.AddDays(days);
            TimeSpan span = last - now;
            dayOst = Convert.ToInt32(span.TotalDays) + 1;
            daysPos = usedDates.Count;
            lastPos = "";

            if (dayOst <= 7 )
            {
                isNeedNewAcc = true;
                isOk = "⛔";
            }

            if (accounts.Exists(o => o.fio == this.fio && o != this.acc && o.StDate.CompareTo(DateTime.Now) == 1))
            {
                isNextExist = true;
            }

            if (isNextExist)
            {
                isNeedNewAcc = false;
                isOk = "➕";
            }


            if (acc.usedDates.Count == 0)
            {
                lastPos = "";
            }
            if (acc.usedDates.Count > 0)
            {
                List<DateTime> dates = acc.usedDates.OrderByDescending(o => o.Date).ToList();
                DateTime lipos  = dates[0];
                TimeSpan ts = lipos - DateTime.Now;
                int kk = ts.Days;
                if (kk == 0)
                {
                    lastPos = "СЕГОДНЯ";
                    return;
                }

                if (kk == -1)
                {
                    lastPos = "ВЧЕРА";
                    return;

                }
                if (kk == -2)
                {
                    lastPos = "ПОЗАВЧЕРА";
                    return;

                }
                lastPos = "" + (0 - kk) + "  дня назад";


            }

        }

        public bool isCurrentAcc(DateTime date)
        {
            if ((date.CompareTo(StDate) == 1 || date.CompareTo(StDate) == 0) && (date.CompareTo(finDate) == -1 || date.CompareTo(finDate) == 0))
            {
                return true;
            }
            return false;
        }

        public string stDateText  { get; set; }
        public string finDateText { get; set; }
        public int    dayOst { get; set; }
        public int daysPos { get; set; }
        public string lastPos { get; set; }



        public bool isNeedNewAcc { get; set; }
        public string isOk { get; set; }

        public bool isNextExist { get; set; }

    }
}
