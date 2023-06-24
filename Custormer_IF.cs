using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Motel
{
    internal class Custormer_IF
    {
        public string name, phone, sex, birth, CMND, HKTT;
        public Custormer_IF(string name, string phone, string sex, string birth, string cMND, string hKTT)
        {
            this.name = name;
            this.phone = phone;
            this.sex = sex;
            this.birth = birth;
            CMND = cMND;
            HKTT = hKTT;
        }
        public Custormer_IF()
        {
            this.name = "";
            this.phone = "";
            this.birth = "";
            CMND = "";
            HKTT = "";
        }
    }
}
