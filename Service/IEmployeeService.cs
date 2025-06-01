using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webApp.Dtos;

namespace Service
{
    public interface IEmployeeService
    {
        Task<List<Employee>> SearchEmployeesAsync(string searchTerm);
        Task<List<Employee>> GetEmployeesSortedAsync(string sortBy, bool ascending);
        Task AddEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeByIdAsync(int id);
        Task<bool> DeleteEmployeeAsync(int id);
        Task<bool> DeleteMultipleEmployeesAsync(List<int> ids);
        Task<bool> UpdateEmployeeAsync(int id, UpdateEmployeeDTO dto);
        Task<List<Employee>> GetAllEmployeesAsync();

        //Task<bool> DeleteEmployeesAsync(List<int> ids);

        Task<(List<Employee> Employees, int TotalCount, int TotalPages)> GetPaginatedEmployeesAsync(
    int pageNumber, int pageSize, string sortKey, string sortDirection);



    }
}
