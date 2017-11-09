using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_OVL_Viewer
{
    class FindName
    {
        public string name(String str, int offset)
        {
            str = str.Replace(" ", "\0");
            int i = (int)offset;

            string s = string.Empty;

            while (str[i] != '\0')
            {
                s += str[i];
                i++;
            }

            return s;
        }
    }
}
