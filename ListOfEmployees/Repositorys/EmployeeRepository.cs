using ListOfEmployees.Data;
using ListOfEmployees.Interfaces;
using ListOfEmployees.Models;
using ListOfEmployees.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ListOfEmployees.Repositorys
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext db;

        public EmployeeRepository(ApplicationDbContext _db)
        {
            db = _db;
        }
        public async Task CreateAsync(Employee employee)
        {
            db.Employees.Add(employee);
            await db.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await db.Employees.OrderByDescending(e => e.Id).ToListAsync();
        }

        public async Task UpdateAsync(Employee employee)
        {
            db.Employees.Update(employee);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Employee employee)
        {
            db.Employees.Remove(employee);
            await db.SaveChangesAsync();
        }

        public async Task<Employee> GetEmployee(int id)
        {
            return await db.Employees.FindAsync(id);
        }

        public IQueryable<Employee> GetEmployees()
        {
            return db.Employees.Include(e => e.Organizations).Include(e => e.Positions);
        }
    }
}
