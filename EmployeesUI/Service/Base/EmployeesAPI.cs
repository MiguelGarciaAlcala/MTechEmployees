using API.Config;
using API.Services.Implementations;
using API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Base
{
    public class EmployeesAPI : IEmployeesAPI
    {
        private readonly IAPIConfig _config;
        public IEmployeeService Employees { get; set; }

        public EmployeesAPI(IAPIConfig config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            Employees = new EmployeeService(config, httpClientFactory);
        }
    }
}
