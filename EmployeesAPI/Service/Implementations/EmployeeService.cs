using DataTransfer.DTO.Employees;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Interfaces;
using DataAccess;
using Repository.Implementations;
using System.Text.RegularExpressions;
using DataAccess.Models;
using Tools.Formatting;
using Repository.Base;

namespace Service.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(DatabaseContext context)
        {
            _unitOfWork = new UnitOfWork(context);
        }

        public bool Exists(int id)
        {
            return _unitOfWork.Employees.ExistsById(id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _unitOfWork.Employees.ExistsByIdAsync(id);
        }

        public int Create(EmployeeCreationDTO employee)
        {
            var name = StringFormat.Trim(employee.Name);
            if (!StringFormat.IsNotEmpty(name, max: 50))
                throw new Exception("A valid name must be provided.");

            var lastName = StringFormat.Trim(employee.LastName);
            if (!StringFormat.IsNotEmpty(lastName, max: 50))
                throw new Exception("A valid last name must be provided.");

            var rfc = StringFormat.Trim(employee.RFC);
            if (!StringFormat.IsNotEmpty(rfc, max: 13) || !IsRFC(rfc))
                throw new Exception("A valid RFC must be provided.");
            else if (_unitOfWork.Employees.ExistsByRFC(rfc))
                throw new Exception($"Employee with RFC {rfc} already exists.");

            if (employee.BornDate.Date > DateTime.Now.Date)
                throw new Exception($"Born date should be in the past.");

            if (employee.Status != null && !Enum.IsDefined(typeof(EmployeeStatus), employee.Status))
                throw new Exception("A valid status must be provided.");

            var createdEmployee = new Employee
            {
                Name = name,
                LastName = lastName,
                RFC = rfc,
                BornDate = employee.BornDate.Date,
                Status = employee.Status ?? EmployeeStatus.NotSet
            };

            _unitOfWork.Employees.Create(createdEmployee);
            _unitOfWork.Save();

            return createdEmployee.ID;
        }

        public async Task<int> CreateAsync(EmployeeCreationDTO employee)
        {
            var name = StringFormat.Trim(employee.Name);
            if (!StringFormat.IsNotEmpty(name, max: 50))
                throw new Exception("A valid name must be provided.");

            var lastName = StringFormat.Trim(employee.LastName);
            if (!StringFormat.IsNotEmpty(lastName, max: 50))
                throw new Exception("A valid last name must be provided.");

            var rfc = StringFormat.Trim(employee.RFC);
            if (!StringFormat.IsNotEmpty(rfc, max: 13) || !IsRFC(rfc))
                throw new Exception("A valid RFC must be provided.");
            else if(_unitOfWork.Employees.ExistsByRFC(rfc))
                throw new Exception($"Employee with RFC {rfc} already exists.");

            if(employee.BornDate.Date > DateTime.Now.Date)
                throw new Exception($"Born date should be in the past.");

            if(employee.Status != null && !Enum.IsDefined(typeof(EmployeeStatus), employee.Status))
                throw new Exception("A valid status must be provided.");

            var createdEmployee = new Employee
            {
                Name = name,
                LastName = lastName,
                RFC = rfc,
                BornDate = employee.BornDate.Date,
                Status = employee.Status ?? EmployeeStatus.NotSet
            };

            await _unitOfWork.Employees.CreateAsync(createdEmployee);
            await _unitOfWork.SaveAsync();

            return createdEmployee.ID;
        }

        public int Update(int id, EmployeeUpdateDTO employee)
        {
            var targetEmployee = _unitOfWork.Employees.Get(id);

            if (targetEmployee == null)
                throw new Exception("Employee not found.");

            if (employee.Name != null)
            {
                var name = StringFormat.Trim(employee.Name);

                if (!StringFormat.IsNotEmpty(name, max: 50))
                    throw new Exception("A valid name must be provided.");

                targetEmployee.Name = name;
            }

            if (employee.LastName != null)
            {
                var lastName = StringFormat.Trim(employee.LastName);

                if (!StringFormat.IsNotEmpty(lastName, max: 50))
                    throw new Exception("A valid last name must be provided.");

                targetEmployee.LastName = lastName;
            }

            if (employee.RFC != null)
            {
                var rfc = StringFormat.Trim(employee.RFC);

                if (!StringFormat.IsNotEmpty(rfc, max: 13) || !IsRFC(rfc))
                    throw new Exception("A valid RFC must be provided.");
                else if (rfc != targetEmployee.RFC && _unitOfWork.Employees.ExistsByRFC(rfc))
                    throw new Exception($"Employee with RFC {rfc} already exists.");

                targetEmployee.RFC = rfc;
            }

            if (employee.BornDate != null)
            {
                if (employee.BornDate.Value.Date > DateTime.Now.Date)
                    throw new Exception($"Born date should be in the past.");

                targetEmployee.BornDate = employee.BornDate.Value.Date;
            }

            if (employee.Status != null)
            {
                if (!Enum.IsDefined(typeof(EmployeeStatus), employee.Status))
                    throw new Exception("A valid status must be provided.");

                targetEmployee.Status = employee.Status.Value;
            }

            _unitOfWork.Save();

            return targetEmployee.ID;
        }

        public async Task<int> UpdateAsync(int id, EmployeeUpdateDTO employee)
        {
            var targetEmployee = await _unitOfWork.Employees.GetAsync(id);

            if (targetEmployee == null)
                throw new Exception("Employee not found.");

            if(employee.Name != null)
            {
                var name = StringFormat.Trim(employee.Name);

                if (!StringFormat.IsNotEmpty(name, max: 50))
                    throw new Exception("A valid name must be provided.");

                targetEmployee.Name = name;
            }

            if (employee.LastName != null)
            {
                var lastName = StringFormat.Trim(employee.LastName);

                if (!StringFormat.IsNotEmpty(lastName, max: 50))
                    throw new Exception("A valid last name must be provided.");

                targetEmployee.LastName = lastName;
            }

            if (employee.RFC != null)
            {
                var rfc = StringFormat.Trim(employee.RFC);

                if (!StringFormat.IsNotEmpty(rfc, max: 13) || !IsRFC(rfc))
                    throw new Exception("A valid RFC must be provided.");
                else if (rfc != targetEmployee.RFC && _unitOfWork.Employees.ExistsByRFC(rfc))
                    throw new Exception($"Employee with RFC {rfc} already exists.");

                targetEmployee.RFC = rfc;
            }

            if (employee.BornDate != null)
            {
                if (employee.BornDate.Value.Date > DateTime.Now.Date)
                    throw new Exception($"Born date should be in the past.");

                targetEmployee.BornDate = employee.BornDate.Value.Date;
            }

            if (employee.Status != null)
            {
                if (!Enum.IsDefined(typeof(EmployeeStatus), employee.Status))
                    throw new Exception("A valid status must be provided.");

                targetEmployee.Status = employee.Status.Value;
            }

            await _unitOfWork.SaveAsync();

            return targetEmployee.ID;
        }

        public EmployeeViewDTO? Get(int id)
        {
            var employee = _unitOfWork.Employees.Get(id);

            if (employee == null)
                return null;

            return new EmployeeViewDTO
            {
                ID = employee.ID,
                Name = employee.Name,
                LastName = employee.LastName,
                RFC = employee.RFC,
                BornDate = employee.BornDate,
                Status = employee.Status
            };
        }

        public async Task<EmployeeViewDTO?> GetAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetAsync(id);

            if (employee == null)
                return null;

            return new EmployeeViewDTO
            {
                ID = employee.ID,
                Name = employee.Name,
                LastName = employee.LastName,
                RFC = employee.RFC,
                BornDate = employee.BornDate,
                Status = employee.Status
            };
        }

        public IEnumerable<EmployeeViewDTO> Get(EmployeeFilterDTO? filter = null)
        {
            IEnumerable<Employee> employees;

            if (filter == null)
            {
                employees = _unitOfWork.Employees.GetAll();
            }
            else
            {
                var name = StringFormat.Trim(filter.FullName);
                var rfc = StringFormat.Trim(filter.RFC);

                employees = _unitOfWork.Employees
                    .GetAll(
                        e => (e.Name + " " + e.LastName).Contains(name) &&
                            e.RFC.Contains(rfc) &&
                            (filter.BornDate == null || e.BornDate.Date == filter.BornDate.Value.Date) &&
                            (filter.Status == null || e.Status == filter.Status.Value)
                    );
            }

            return employees
                .Select(e => new EmployeeViewDTO
                {
                    ID = e.ID,
                    Name = e.Name,
                    LastName = e.LastName,
                    RFC = e.RFC,
                    BornDate = e.BornDate,
                    Status = e.Status
                })
                .OrderBy(s => s.BornDate)
                .ToList();
        }

        public async Task<IEnumerable<EmployeeViewDTO>> GetAsync(EmployeeFilterDTO? filter = null)
        {
            IEnumerable<Employee> employees;

            if (filter == null)
            {
                employees = await _unitOfWork.Employees.GetAllAsync();
            }
            else
            {
                var name = StringFormat.Trim(filter.FullName);
                var rfc = StringFormat.Trim(filter.RFC);

                employees = await _unitOfWork.Employees
                    .GetAllAsync(
                        e => (e.Name + " " + e.LastName).Contains(name) &&
                            e.RFC.Contains(rfc) &&
                            (filter.BornDate == null || e.BornDate.Date == filter.BornDate.Value.Date) &&
                            (filter.Status == null || e.Status == filter.Status.Value)
                    );
            }

            return employees
                .Select(e => new EmployeeViewDTO
                {
                    ID = e.ID,
                    Name = e.Name,
                    LastName = e.LastName,
                    RFC = e.RFC,
                    BornDate = e.BornDate,
                    Status = e.Status
                })
                .OrderBy(s => s.BornDate)
                .ToList();
        }

        public void Delete(int id)
        {
            var targetEmployee = _unitOfWork.Employees.Get(id);

            if (targetEmployee == null)
                throw new Exception("Employee not found.");

            _unitOfWork.Employees.Delete(id);
            _unitOfWork.Save();
        }

        private bool IsRFC(string rfc)
        {
            var pattern = @"^[A-ZÑ]{4}[0-9]{2}(0[1-9]|1[012])(0[1-9]|[12][0-9]|3[01])[A-Z0-9]{2}[0-9A]$";
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            return regex.IsMatch(rfc);
        }
    }
}
