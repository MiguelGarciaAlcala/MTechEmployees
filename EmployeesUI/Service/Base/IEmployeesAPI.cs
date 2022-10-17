using API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Base
{
    public interface IEmployeesAPI
    {
        public IEmployeeService Employees { get; set; }
    }
}
