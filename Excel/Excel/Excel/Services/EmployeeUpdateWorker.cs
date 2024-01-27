using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Excel.Services
{
    
    public class EmployeeUpdateWorker : BackgroundService
    {
        private readonly EmployeeService _employeeService;

        public EmployeeUpdateWorker(EmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var employees = await _employeeService.GetAllEmployeesAsync();

                foreach (var employee in employees)
                {
                    await _employeeService.UpdateEmployeeAsync(employee);
                }

                await Task.Delay(10000, stoppingToken); // Wait 10 seconds before the next operation
            }
        }
    }

}
