using ListOfEmployees.Models;
using ListOfEmployees.Data;
using ListOfEmployees.ViewModels;
using ListOfEmployees.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ListOfEmployees.Controllers
{
    public class PositionController : Controller
    {
        private readonly IPositionRepository _positionRepository;

        public PositionController(IPositionRepository positionRepository)
        {
            _positionRepository = positionRepository;
        }
        public async Task<IActionResult> Index(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var positions = await _positionRepository.ListPositions(id);          
            ViewBag.Subdivision = id;
            return View(positions);
        }
        public IActionResult Create(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            PositionVM model = new PositionVM
            {
                SubdivisionId = id
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(PositionVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var listPosition = await _positionRepository.ListPositions(model.SubdivisionId);
            if (listPosition.Any(s => s.Name == model.Name))
            {
                ModelState.AddModelError("Name", "Такая должность уже существует.");
                return View(model);
            }
            Position position = new Position
            {                
                Name = model.Name,
                SubdivisionId = model.SubdivisionId
            };
            await _positionRepository.CreateAsync(position);
            return RedirectToAction("Index", "Position", new { id = position.SubdivisionId } );
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var position = await _positionRepository.GetPosition(id);
            PositionVM model = new PositionVM
            {
                NameId = id,
                Name = position.Name,
                SubdivisionId = position.SubdivisionId
            };
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(PositionVM model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var listPosition = await _positionRepository.ListPositions(model.SubdivisionId);
            if (listPosition.Any(s => s.Name == model.Name))
            {
                ModelState.AddModelError("Name", "Такая должность уже существует.");
                return View(model);
            }
            Position position = await _positionRepository.GetPosition(model.NameId);
            position.Id = model.NameId;
            position.Name = model.Name;
            position.SubdivisionId = model.SubdivisionId;
            await _positionRepository.UpdateAsync(position);
            TempData["SM"] = "Успешно отредактированно: " + position.Name;
            return RedirectToAction("Index", new { id = position.SubdivisionId});
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var position = await _positionRepository.GetPosition(id);
            await _positionRepository.DeleteAsync(position);
            return RedirectToAction("Index", new { id = position.SubdivisionId });
        }
    }
}
