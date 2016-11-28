using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DalSecurity
{
    class Config
    {

        public static Dictionary<string, int> RoleDictionary = new Dictionary<string, int>()
        {
            {"ADMINISTRATOR", 1},
            {"SUPERVISOR", 2},
            {"OPERATOR", 3},
        };
    }
}
