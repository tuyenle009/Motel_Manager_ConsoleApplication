using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Motel
{
    public class Solve_elect
    {
        public int old_num;
        public int new_num;
        public int use;
        public double unit_price;
        public double total_cost;
        public Solve_elect(int old_num, int new_num, int use, double unit_price, double total_cost)
        {
            this.old_num = old_num;
            this.new_num = new_num;
            this.use = use;
            this.unit_price = 1700;
            this.total_cost = total_cost;
        }
        public Solve_elect()
        {
            this.old_num = 0;
            this.new_num = 0;
            this.use = 0;
            this.unit_price = 1700;
            this.total_cost = 0;
        }
    }
}
