using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using Excel.Pages.Data;

namespace Excel.Services
{
   
    public class EmployeeService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://localhost:7200/";

        public EmployeeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Employee>>("api/Employee");
        }

        public async Task<bool> UpdateEmployeeAsync(Employee employee)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/employee/{employee.Id}");
            var response = await _httpClient.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }

}
