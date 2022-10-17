using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.DTO.Employees
{
    public class EmployeeFilterDTO
    {
        [MaxLength(100, ErrorMessage = "Full name should not exceed 100 characters.")]
        public string? FullName { get; set; }

        [MaxLength(13, ErrorMessage = "RFC should not exceed 13 characters.")]
        public string? RFC { get; set; }

        public DateTime? BornDate { get; set; }

        public EmployeeStatus? Status { get; set; }
    }
}
