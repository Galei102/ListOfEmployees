using ListOfEmployees.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ListOfEmployees.Interfaces
{
    public interface IEmployeeRepository
    {
        Task CreateAsync(Employee employee);

        Task<List<Employee>> GetAllAsync();

        Task UpdateAsync(Employee employee);

        Task DeleteAsync(Employee employee);

        Task<Employee> GetEmployee(int id);

        IQueryable<Employee> GetEmployees();
    }
}
