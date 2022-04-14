using ListOfEmployees.Models;
using ListOfEmployees.ViewModels;
using ListOfEmployees.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ListOfEmployees.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IOrganizationRepository _organizationRepository;

        public OrganizationController(IOrganizationRepository organizationRepository)
        {
            _organizationRepository = organizationRepository;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _organizationRepository.GetAllAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrganizationVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var listOrganization = await _organizationRepository.GetAllAsync();

            if (listOrganization.Any(s => s.Name == model.Name))
            {
                ModelState.AddModelError("Name", "Это организация уже существует.");
                return View(model);
            }
            Organization organization = new Organization()
            {
                Id = model.NameId,
                Name = model.Name
            };
            await _organizationRepository.CreateAsync(organization);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            Organization organization = await _organizationRepository.GetOrganization(id);
            OrganizationVM model = new OrganizationVM()
            {
                NameId = organization.Id,
                Name = organization.Name
            };
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(OrganizationVM model, int id)
        {           
            if (id != model.NameId)
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var valueOrganization = await _organizationRepository.GetAllAsync();
            if (valueOrganization.Any(s => s.Name == model.Name))
            {
                ModelState.AddModelError("Name", "Такая организация уже существует.");
                return View(model);
            }
            Organization organization = await _organizationRepository.GetOrganization(id);
            organization.Id = model.NameId;
            organization.Name = model.Name;
            await _organizationRepository.UpdateAsync(organization);
            TempData["SM"] = "Успешно отредактированно: " + organization.Name;
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var organization = await _organizationRepository.GetOrganization(id);
            await _organizationRepository.DeleteAsync(organization);
            return RedirectToAction("Index");
        }
    }
}
