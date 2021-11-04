using school.BLL.IOC;
using school.IBLL.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace school.BLL.Factory
{
    public class SessionFactory
    {
        private static SessionService _SessionService = BLLRegister.Instance.GetObject<SessionService>();
        public static SessionService SessionService
        {
            get { return _SessionService; }
        }
    }
}
