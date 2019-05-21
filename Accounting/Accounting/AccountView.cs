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
        public AccountView(Account ac)
        {
            this.fio = ac.fio;
            this.StDate = ac.StDate;
            this.days = ac.days;
            this.usedDates = ac.usedDates;
            finDate = StDate.AddDays(days);
        }

        public bool isCurrentAcc(DateTime date)
        {
            if ((date.CompareTo(StDate) == 1 || date.CompareTo(StDate) == 0) && (date.CompareTo(finDate) == -1 || date.CompareTo(finDate) == 0))
            {
                return true;
            }
            return false;
        }
    }
}
