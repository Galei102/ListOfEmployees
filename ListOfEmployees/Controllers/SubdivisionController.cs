using ListOfEmployees.Models;
using ListOfEmployees.ViewModels;
using ListOfEmployees.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ListOfEmployees.Controllers
{
    public class SubdivisionController : Controller
    {
        private readonly ISubdivisionRepository _subdivisionRepository;

        public SubdivisionController(ISubdivisionRepository subdivisionRepository)
        {
            _subdivisionRepository = subdivisionRepository;
        }

        public async Task<IActionResult> Index(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var subdivisions = await _subdivisionRepository.ListSubdivisions(id);
            ViewBag.Organization = id;
            return View(subdivisions);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            SubdivisionVM model = new SubdivisionVM
            {
                OrganizationId = id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SubdivisionVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var listSubdivisions = await _subdivisionRepository.ListSubdivisions(model.OrganizationId);
            if (listSubdivisions.Any(s => s.Name == model.Name))
            {
                ModelState.AddModelError("Name", "Такое подразделение уже существует.");
                return View(model);
            }
            Subdivision subdivision = new Subdivision
            {
                Name = model.Name,
                OrganizationId = model.OrganizationId
            };           
            await _subdivisionRepository.CreateAsync(subdivision);
            return RedirectToAction("Index", "Subdivision", new { id = subdivision.OrganizationId } );
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }           
            var subdivision = await _subdivisionRepository.GetSubdivision(id);
            SubdivisionVM model = new SubdivisionVM
            { 
                NameId = id,
                Name = subdivision.Name,
                OrganizationId = subdivision.OrganizationId
            };
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SubdivisionVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var listSubdivisions = await _subdivisionRepository.ListSubdivisions(model.OrganizationId);
            if (listSubdivisions.Any(s => s.Name == model.Name))
            {
                ModelState.AddModelError("Name", "Такое подразделение уже существует.");
                return View(model);
            }
            Subdivision subdivision = await _subdivisionRepository.GetSubdivision(model.NameId);
            subdivision.Id = model.NameId;
            subdivision.Name = model.Name;
            subdivision.OrganizationId = model.OrganizationId;
            await _subdivisionRepository.UpdateAsync(subdivision);
            TempData["SM"] = "Успешно отредактированно: " + subdivision.Name;
            return RedirectToAction("Index", new { id = subdivision.OrganizationId });
        }

        public async Task<IActionResult> SubdivisionIndex(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var subdivision = await _subdivisionRepository.GetSubdivision(id);
            return RedirectToAction("Index", "Subdivision", new { id = subdivision.OrganizationId });
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var subdivision = await _subdivisionRepository.GetSubdivision(id);
            await _subdivisionRepository.DeleteAsync(subdivision);
            return RedirectToAction("Index", new { id = subdivision.OrganizationId});
        }
    }
}
