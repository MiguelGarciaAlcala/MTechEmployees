using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.DTO.Employees
{
    public class EmployeeUpdateDTO
    {
        [MaxLength(50, ErrorMessage = "Name should not exceed 50 characters.")]
        public string? Name { get; set; }

        [MaxLength(50, ErrorMessage = "Last name should not exceed 50 characters.")]
        public string? LastName { get; set; }

        [MaxLength(13, ErrorMessage = "RFC must be 13 characters length.")]
        public string? RFC { get; set; }

        public DateTime? BornDate { get; set; }

        public EmployeeStatus? Status { get; set; }
    }
}
