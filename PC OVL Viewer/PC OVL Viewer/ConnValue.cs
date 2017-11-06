using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC_OVL_Viewer
{
    public static class ConnValue
    {
        public static string connSt()
        {
            return "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\\OVL.mdf;Integrated Security=True;Connect Timeout=30";
        }
    }
}
