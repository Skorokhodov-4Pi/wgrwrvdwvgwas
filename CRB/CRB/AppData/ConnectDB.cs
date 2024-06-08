using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRB.AppData
{
    internal class ConnectDB
    {
        public static CRBEntities context;
        
        public static CRBEntities GetCont()
        {
            if (context == null) context = new CRBEntities();
            return context;
        }
    }
}
