using API.Config;
using API.DTO.Common;
using API.DTO.Employees;
using API.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IAPIConfig _config;
        private readonly IHttpClientFactory _httpClientFactory;

        public EmployeeService(IAPIConfig config, IHttpClientFactory httpClientFactory)
        {
            _config = config;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<Response?> CreateAsync(EmployeeCreationDTO employee)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config.BaseUri);

            var response = await client.PostAsJsonAsync("/api/v1/employees", employee);

            if(response.IsSuccessStatusCode)
            {
                var createdEmployee = await response.Content.ReadFromJsonAsync<EmployeeViewDTO>();

                return new Response
                {
                    Status = ResponseStatus.Success,
                    Messages = new List<string> { "Employee created successfuly!" },
                    Data = createdEmployee
                };
            }

            return await response.Content.ReadFromJsonAsync<Response?>();
        }

        public async Task<Response?> UpdateAsync(int id, EmployeeUpdateDTO employee)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config.BaseUri);

            var response = await client.PutAsJsonAsync($"/api/v1/employees/{id}", employee);

            if (response.IsSuccessStatusCode)
            {
                var updatedEmployee = await response.Content.ReadFromJsonAsync<EmployeeViewDTO>();

                return new Response
                {
                    Status = ResponseStatus.Success,
                    Messages = new List<string> { "Employee updated successfuly!" },
                    Data = updatedEmployee
                };
            }

            return await response.Content.ReadFromJsonAsync<Response?>();
        }

        public async Task<Response?> GetAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config.BaseUri);

            var response = await client.GetAsync($"/api/v1/employees/{id}");

            if (response.IsSuccessStatusCode)
            {
                var employee = await response.Content.ReadFromJsonAsync<EmployeeViewDTO>();

                return new Response
                {
                    Status = ResponseStatus.Success,
                    Data = employee
                };
            }

            return await response.Content.ReadFromJsonAsync<Response?>();
        }

        public async Task<Response?> GetAllAsync(EmployeeFilterDTO? filter = null)
        {
            var client = _httpClientFactory.CreateClient();
            var query = string.Empty;

            client.BaseAddress = new Uri(_config.BaseUri);            

            if(filter != null)
            {
                var queryParams = new Dictionary<string, string>
                {
                    { "FullName", filter?.FullName?.Trim() ?? string.Empty },
                    { "RFC", filter?.RFC?.Trim() ?? string.Empty },
                    { "BornDate", filter?.BornDate?.ToString("s") ?? string.Empty },
                    { "Status", filter?.Status?.ToString() ?? string.Empty }
                };

                var stringParams = queryParams
                    .Select(s => $"{s.Key}={s.Value}")
                    .ToList();

                query = "?" + string.Join("&", stringParams);
            }

            var response = await client.GetAsync($"/api/v1/employees{query}");

            if (response.IsSuccessStatusCode)
            {
                var employees = await response.Content.ReadFromJsonAsync<IEnumerable<EmployeeViewDTO>>();

                return new Response
                {
                    Status = ResponseStatus.Success,
                    Data = employees
                };
            }

            return await response.Content.ReadFromJsonAsync<Response?>();
        }

        public async Task<Response?> DeleteAsync(int id)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_config.BaseUri);

            var response = await client.DeleteAsync($"/api/v1/employees{id}");

            if (response.IsSuccessStatusCode)
            {
                return new Response
                {
                    Status = ResponseStatus.Success,
                    Messages = new List<string> { "Employee deleted successfuly!" }
                };
            }

            return await response.Content.ReadFromJsonAsync<Response?>();
        }
    }
}
