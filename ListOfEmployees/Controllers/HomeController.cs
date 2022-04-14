using ListOfEmployees.Models;
using ListOfEmployees.ViewModels;
using ListOfEmployees.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ListOfEmployees.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ISubdivisionRepository _subdivisionRepository;
        private readonly IPositionRepository _positionRepository;

        public HomeController
            (
                IEmployeeRepository employeeRepository,
                IOrganizationRepository organizationRepository,
                ISubdivisionRepository subdivisionRepository,
                IPositionRepository positionRepository
            )
        {
            _employeeRepository = employeeRepository;
            _organizationRepository = organizationRepository;
            _subdivisionRepository = subdivisionRepository;
            _positionRepository = positionRepository;
        }

        public async Task<IActionResult> Index(int? organization, int? position, string lastName, string name, string firstMidName)
        {
            var employees = _employeeRepository.GetEmployees(); 
            if (organization != null && organization != 0)
            {
                employees = employees.Where(p => p.OrganizationId == organization);
            }
            if (position != null && position != 0)
            {
                employees = employees.Where(p => p.PositionId == position);
            }
            if (!String.IsNullOrEmpty(lastName))
            {
                employees = employees.Where(p => p.LastName.Contains(lastName));
            }
            if (!String.IsNullOrEmpty(name))
            {
                employees = employees.Where(p => p.Name.Contains(name));
            }
            if (!String.IsNullOrEmpty(firstMidName))
            {
                employees = employees.Where(p => p.FirstMidName.Contains(firstMidName));
            }
            var organizations = await _organizationRepository.GetAllAsync();
            var positions = await _positionRepository.GetAllAsync();
            // устанавливаем начальный элемент, который позволит выбрать всех
            organizations.Insert(0, new Organization { Name = "Все", Id = 0 });
            positions.Insert(0, new Position { Name = "Все", Id = 0 });

            HomeEmployeesVM model = new HomeEmployeesVM
            {
                Employees = employees.ToList(),
                Organizations = new SelectList(organizations, "Id", "Name"),
                Positions = new SelectList(positions, "Id", "Name"),
                LastName = lastName,
                Name = name,
                FirstMidName = firstMidName,
            };
            return View(model);
        }
    }
}
