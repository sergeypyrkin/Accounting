﻿using System;
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
        public AccountView(Account ac)
        {
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
            dayOst = Convert.ToInt32(span.TotalDays);
            daysPos = usedDates.Count;
            lastPos = "";

            if (dayOst <= 7 )
            {
                isNeedNewAcc = true;
                isOk = "⛔";
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

    }
}