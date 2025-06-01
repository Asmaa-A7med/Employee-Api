using Microsoft.EntityFrameworkCore;
using Repositories.Context;
using Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
   public class EmployeeRepository : IEmployeeRepository
    {
        private readonly MyDbContext _context;

        public EmployeeRepository(MyDbContext context)
        {
            _context = context;
        }
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<List<Employee>> SearchByNameOrEmailAsync(string searchTerm)
        {
            return await _context.Employees
                .Where(e => e.Name.Contains(searchTerm) || e.Email.Contains(searchTerm))
                .ToListAsync();
        }

        public async Task<List<Employee>> GetEmployeesSortedAsync(string sortBy, bool ascending)
        {
            IQueryable<Employee> query = _context.Employees;

            if (sortBy.ToLower() == "name")
                query = ascending ? query.OrderBy(e => e.Name) : query.OrderByDescending(e => e.Name);
            else if (sortBy.ToLower() == "email")
                query = ascending ? query.OrderBy(e => e.Email) : query.OrderByDescending(e => e.Email);
            else
                query = query.OrderBy(e => e.Name);

            return await query.ToListAsync();
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees.FindAsync(id);
        }

        public void Delete(Employee employee)
        {
            _context.Employees.Remove(employee);
        }

        public void DeleteRange(IEnumerable<Employee> employees)
        {
            _context.Employees.RemoveRange(employees);
        }

        public void Update(Employee employee)
        {
            _context.Employees.Update(employee);
        }
        public async Task<List<Employee>> GetPaginatedEmployeesAsync(int pageNumber, int pageSize, string sortKey, string sortDirection)
        {
            IQueryable<Employee> query = _context.Employees;

            // Apply sorting
            if (!string.IsNullOrEmpty(sortKey))
            {
                bool ascending = sortDirection?.ToLower() == "asc";

                query = sortKey.ToLower() switch
                {
                    "name" => ascending ? query.OrderBy(e => e.Name) : query.OrderByDescending(e => e.Name),
                    "email" => ascending ? query.OrderBy(e => e.Email) : query.OrderByDescending(e => e.Email),
                  //  "phone" => ascending ? query.OrderBy(e => e.Phone) : query.OrderByDescending(e => e.Phone),
                    _ => query.OrderBy(e => e.Name) // default
                };
            }

            // Apply pagination
            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }



        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Employees.CountAsync();
        }
    }
}