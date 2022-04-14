using ListOfEmployees.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ListOfEmployees.ViewModels
{
    public class HomeEmployeesVM
    {
        public IEnumerable<Employee> Employees { get; set; }
        public SelectList Organizations { get; set; }
        public SelectList Positions { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string FirstMidName { get; set; }
    }
}
