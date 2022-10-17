using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DTO.Employees
{
    public class EmployeeFilterDTO
    {
        public string? FullName { get; set; }
        public string? RFC { get; set; }
        public DateTime? BornDate { get; set; }
        public EmployeeStatus? Status { get; set; }
    }
}
