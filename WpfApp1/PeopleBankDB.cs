using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeopleBank
{
    static class PeopleBankDB
    {
        private static readonly PeopleBank1Entities _context = new PeopleBank1Entities();

        public static PeopleBank1Entities Context => _context;
    }
}
