using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accounting
{
    public class AccountVisit
    {
        public string fio { get; set; }
        public DateTime date { get; set; }
        public Account acc;
        public bool isVisited { get; set; }

        public AccountVisit(Account ac)
        {
            this.acc = ac;
            this.fio = ac.fio;
        }


    }
}
