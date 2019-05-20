using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting
{
    public class Account
    {

        public string fio { get; set; }
        public DateTime StDate { get; set; }
        public int days { get; set; }
        public List<DateTime> usedDates = new List<DateTime>();
    }
}
