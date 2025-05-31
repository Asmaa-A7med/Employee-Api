using Repositories.Entities;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webApp.Dtos;

namespace Service
{
  public  class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Employee>> SearchEmployeesAsync(string searchTerm)
        {
            return await _unitOfWork.Employees.SearchByNameOrEmailAsync(searchTerm);
        }

        public async Task<List<Employee>> GetEmployeesSortedAsync(string sortBy, bool ascending)
        {
            return await _unitOfWork.Employees.GetEmployeesSortedAsync(sortBy, ascending);
        }
        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _unitOfWork.Employees.GetAllEmployeesAsync();
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _unitOfWork.Employees.AddEmployeeAsync(employee);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            return await _unitOfWork.Employees.GetEmployeeByIdAsync(id);
        }
        public async Task<bool> DeleteEmployeeAsync(int id)
        {
            var employee = await _unitOfWork.Employees.GetEmployeeByIdAsync(id);
            if (employee == null)
                return false;

            _unitOfWork.Employees.Delete(employee);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteMultipleEmployeesAsync(List<int> ids)
        {
            var employees = new List<Employee>();

            foreach (var id in ids)
            {
                var employee = await _unitOfWork.Employees.GetEmployeeByIdAsync(id);
                if (employee != null)
                    employees.Add(employee);
            }

            if (employees.Count == 0)
                return false;

            _unitOfWork.Employees.DeleteRange(employees);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateEmployeeAsync(int id, UpdateEmployeeDTO dto)
        {
            var existing = await _unitOfWork.Employees.GetEmployeeByIdAsync(id);
            if (existing == null)
                return false;

            existing.Name = dto.Name;
            existing.Email = dto.Email;
            existing.Phone = dto.Phone;
            existing.Address = dto.Address;

            _unitOfWork.Employees.Update(existing);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }



        public async Task<(List<Employee> Employees, int TotalCount, int TotalPages)> GetPaginatedEmployeesAsync(int pageNumber, int pageSize)
        {
            var employees = await _unitOfWork.Employees.GetPaginatedEmployeesAsync(pageNumber, pageSize);
            var totalCount = await _unitOfWork.Employees.GetTotalCountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            return (employees, totalCount, totalPages);
        }



    }
}
