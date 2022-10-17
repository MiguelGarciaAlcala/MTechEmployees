using DataTransfer.DTO.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IEmployeeService
    {
        bool Exists(int id);
        Task<bool> ExistsAsync(int id);

        int Create(EmployeeCreationDTO employee);
        Task<int> CreateAsync(EmployeeCreationDTO employee);

        int Update(int id, EmployeeUpdateDTO employee);
        Task<int> UpdateAsync(int id, EmployeeUpdateDTO employee);

        EmployeeViewDTO? Get(int id);
        Task<EmployeeViewDTO?> GetAsync(int id);

        IEnumerable<EmployeeViewDTO> Get(EmployeeFilterDTO? filter = null);
        Task<IEnumerable<EmployeeViewDTO>> GetAsync(EmployeeFilterDTO? filter = null);

        void Delete(int id);
    }
}
