using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.DTO.Employees
{
    public class EmployeeCreationDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is mandatory.")]
        [MaxLength(50, ErrorMessage = "Name should not exceed 50 characters.")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is mandatory.")]
        [MaxLength(50, ErrorMessage = "Last name should not exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "RFC is mandatory.")]
        [MaxLength(13, ErrorMessage = "RFC must be 13 characters length.")]
        public string RFC { get; set; }

        [Required(ErrorMessage = "Born date is mandatory.")]
        public DateTime BornDate { get; set; }

        public EmployeeStatus? Status { get; set; }
    }
}
