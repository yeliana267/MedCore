
using MedCore.Web.Interfaces.users;
using MedCore.Web.Models.users.Doctors;
using Microsoft.AspNetCore.Mvc;
using DoctorsModel = MedCore.Model.Models.users.DoctorsModel;


namespace MedCore.Web.Controllers.usersController
{
    public class DoctorsController : Controller
    {
        private readonly IDoctorsWeb _doctorsWeb;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(
            IDoctorsWeb doctorsWeb,
            ILogger<DoctorsController> logger)
        {
            _doctorsWeb = doctorsWeb;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var doctors = await _doctorsWeb.GetAllAsync();
                return View(doctors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener lista de doctores");
                ViewBag.ErrorMessage = "Error al cargar los doctores. Por favor intente nuevamente.";
                return View(new List<DoctorsModel>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var doctor = await _doctorsWeb.GetByIdAsync(id);
                return View(doctor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener detalles de doctor con ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var model = new CreateDoctorsModel
                {
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar la vista de creación de doctor");
                ViewBag.ErrorMessage = "Error al inicializar el formulario de doctor";
                return RedirectToAction(nameof(Index));
            }
        }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(CreateDoctorsModel model)
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                try
                {
                    var success = await _doctorsWeb.CreateAsync(model);
                    if (success)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ViewBag.Message = "Error al crear el doctor.";
                    return View(model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al crear doctor");
                    ViewBag.Message = $"Error inesperado: {ex.Message}";
                    return View(model);
                }
            }
        

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var doctor = await _doctorsWeb.GetEditModelByIdAsync(id);
                return View(doctor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al cargar doctor para edición con ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditDoctorsModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var success = await _doctorsWeb.UpdateAsync(id, model);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al actualizar el doctor.";
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar doctor con ID {id}");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var doctor = await _doctorsWeb.GetByIdAsync(id);
                return View(doctor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al cargar doctor para eliminación con ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _doctorsWeb.DeleteAsync(id);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al eliminar el doctor.";
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar doctor con ID {id}");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View();
            }
        }
    }
}

