using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> SearchByNameOrEmailAsync(string searchTerm);
        Task<List<Employee>> GetAllEmployeesAsync();

        Task<List<Employee>> GetEmployeesSortedAsync(string sortBy, bool ascending);

        Task AddEmployeeAsync(Employee employee);
        Task<Employee> GetEmployeeByIdAsync(int id);

        void Delete(Employee employee);
        void DeleteRange(IEnumerable<Employee> employees);
        void Update(Employee employee);
        Task<List<Employee>> GetPaginatedEmployeesAsync(int pageNumber, int pageSize, string sortKey, string sortDirection);


        Task<int> GetTotalCountAsync();
       

    }
}
