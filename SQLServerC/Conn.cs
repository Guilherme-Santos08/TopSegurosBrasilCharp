using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLServerC
{
    internal class Conn
    {
        public static string StrCon
        {
            get { return "Server=REVISION-PC;Database=TopSegurosBrasilAppDb;Trusted_Connection=True;MultipleActiveResultSets=true"; }
        }
    }
}
