using API.DTO.Common;
using API.DTO.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<Response?> CreateAsync(EmployeeCreationDTO employee);
        Task<Response?> UpdateAsync(int id, EmployeeUpdateDTO employee);
        Task<Response?> GetAsync(int id);
        Task<Response?> GetAllAsync(EmployeeFilterDTO? filter = null);
        Task<Response?> DeleteAsync(int id);
    }
}
