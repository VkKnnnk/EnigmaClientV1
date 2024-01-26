using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompClub.Model;

namespace CompClub
{
    public class Session
    {
        public static CompClubEntities _context;
        public static CompClubEntities Context
        {
            get
            {
                if (_context == null)
                {

                    _context = new CompClubEntities();
                }
                return _context;
            }
        }
    }
}
