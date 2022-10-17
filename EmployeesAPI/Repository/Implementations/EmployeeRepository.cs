using DataAccess;
using DataAccess.Models;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementations
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(DatabaseContext context) 
            : base(context)
        {

        }

        public bool ExistsById(int id)
        {
            return Get(id) != null;
        }

        public async Task<bool> ExistsByIdAsync(int id)
        {
            return await GetAsync(id) != null;
        }

        public bool ExistsByRFC(string rfc)
        {
            return Get(e => e.RFC == rfc) != null;
        }

        public async Task<bool> ExistsByRFCAsync(string rfc)
        {
            return await GetAsync(e => e.RFC == rfc) != null;
        }
    }
}
