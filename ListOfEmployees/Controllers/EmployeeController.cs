using ListOfEmployees.Models;
using ListOfEmployees.ViewModels;
using ListOfEmployees.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ListOfEmployees.Controllers
{
    public class EmployeeController : Controller
    {       
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IOrganizationRepository _organizationRepository;
        private readonly ISubdivisionRepository _subdivisionRepository;
        private readonly IPositionRepository _positionRepository;
        public EmployeeController
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

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            SelectList listOrganizations = new SelectList(await _organizationRepository.GetAllAsync(), "Id", "Name");
            ViewBag.Organizations = listOrganizations;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(AddEmployeeVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var listEmployee = await _employeeRepository.GetAllAsync();
            if (listEmployee.Any(s => s.LastName == model.LastName) && (listEmployee.Any(s => s.Name == model.Name) && (listEmployee.Any(s => s.FirstMidName == model.FirstMidName))))
            {
                SelectList listOrganizations = new SelectList(await _organizationRepository.GetAllAsync(), "Id", "Name");
                ViewBag.Organizations = listOrganizations;
                SelectList listsubdivisions = new SelectList(await _subdivisionRepository.ListSubdivisions(model.OrganizationId), "Id", "Name");
                ViewBag.Subdivisions = listsubdivisions;
                SelectList listPositions = new SelectList(await _positionRepository.ListPositions(model.SubdivisionId), "Id", "Name");
                ViewBag.Positions = listPositions;
                ModelState.AddModelError("", "Такой сотрудник уже существует.");
                return View(model);
            }
            Employee employee = new Employee()
            {
                LastName = model.LastName,
                Name = model.Name,
                FirstMidName = model.FirstMidName,
                OrganizationId = model.OrganizationId,
                SubdivisionId = model.SubdivisionId,
                PositionId = model.PositionId,
                PhoneNumber = model.PhoneNumber,
                Email = model.Email
            };
            await _employeeRepository.CreateAsync(employee);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> ValueSubdivisions(int id)
        {
            return PartialView(await _subdivisionRepository.ListSubdivisions(id));
        }

        public async Task<IActionResult> ValuePosition(int id)
        {
            return PartialView(await _positionRepository.ListPositions(id));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var employee = await _employeeRepository.GetEmployee(id);
            AddEmployeeVM model = new AddEmployeeVM()
            {
                EmployeeID = employee.Id,
                LastName = employee.LastName,
                Name = employee.Name,
                FirstMidName = employee.FirstMidName,
                OrganizationId = employee.OrganizationId,
                SubdivisionId = employee.SubdivisionId,
                PositionId = employee.PositionId,
                PhoneNumber = employee.PhoneNumber,
                Email = employee.Email
            };
            SelectList listOrganizations = new SelectList(await _organizationRepository.GetAllAsync(), "Id", "Name");
            ViewBag.Organizations = listOrganizations;
            SelectList listsubdivisions = new SelectList(await _subdivisionRepository.ListSubdivisions(model.OrganizationId), "Id", "Name");
            ViewBag.Subdivisions = listsubdivisions;
            SelectList listPositions = new SelectList(await _positionRepository.ListPositions(model.SubdivisionId), "Id", "Name");
            ViewBag.Positions = listPositions;
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AddEmployeeVM model)
        {
            if (!ModelState.IsValid)
            {
                SelectList listOrganizations = new SelectList(await _organizationRepository.GetAllAsync(), "Id", "Name");
                ViewBag.Organizations = listOrganizations;
                SelectList listsubdivisions = new SelectList(await _subdivisionRepository.ListSubdivisions(model.OrganizationId), "Id", "Name");
                ViewBag.Subdivisions = listsubdivisions;
                SelectList listPositions = new SelectList(await _positionRepository.ListPositions(model.SubdivisionId), "Id", "Name");
                ViewBag.Positions = listPositions;
                return View(model);
            }
            var listEmployee = await _employeeRepository.GetAllAsync();
            var employee = await _employeeRepository.GetEmployee(model.EmployeeID);
            if (employee.LastName != model.LastName || employee.Name != model.Name || employee.FirstMidName != model.FirstMidName)
            {
                if (listEmployee.Any(s => s.LastName == model.LastName && s.Name == model.Name && s.FirstMidName == model.FirstMidName))
                {
                    SelectList listOrganizations = new SelectList(await _organizationRepository.GetAllAsync(), "Id", "Name");
                    ViewBag.Organizations = listOrganizations;
                    SelectList listsubdivisions = new SelectList(await _subdivisionRepository.ListSubdivisions(model.OrganizationId), "Id", "Name");
                    ViewBag.Subdivisions = listsubdivisions;
                    SelectList listPositions = new SelectList(await _positionRepository.ListPositions(model.SubdivisionId), "Id", "Name");
                    ViewBag.Positions = listPositions;
                    ModelState.AddModelError("", "Такой сотрудник уже существует.");
                    return View(model);
                }
                
            }
            employee.Id = model.EmployeeID;
            employee.LastName = model.LastName;
            employee.Name = model.Name;
            employee.FirstMidName = model.FirstMidName;
            employee.OrganizationId = model.OrganizationId;
            employee.SubdivisionId = model.SubdivisionId;
            employee.PositionId = model.PositionId;
            employee.PhoneNumber = model.PhoneNumber;
            employee.Email = model.Email;
            await _employeeRepository.UpdateAsync(employee);
            TempData["SM"] = "Успешно отредактированно: " + model.LastName + " " + model.Name + " " + model.FirstMidName;
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var employee = await _employeeRepository.GetEmployee(id);
            await _employeeRepository.DeleteAsync(employee);
            return RedirectToAction("Index", "Home");
        }
    }
}
