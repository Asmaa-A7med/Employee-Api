using Repositories.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
   public class UnitOfWork : IUnitOfWork
    {
        private readonly MyDbContext _context;
        private readonly IEmployeeRepository _employeeRepository;

        public UnitOfWork(MyDbContext context, IEmployeeRepository employeeRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository;
        }

        public IEmployeeRepository Employees => _employeeRepository;
        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();



    }
}
