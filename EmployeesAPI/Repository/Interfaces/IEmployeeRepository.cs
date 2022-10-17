using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interfaces
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        bool ExistsById(int id);
        Task<bool> ExistsByIdAsync(int id);

        bool ExistsByRFC(string rfc);
        Task<bool> ExistsByRFCAsync(string rfc);
    }
}
