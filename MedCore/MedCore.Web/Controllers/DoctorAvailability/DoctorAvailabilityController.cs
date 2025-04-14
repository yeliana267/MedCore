using MedCore.Web.Interfaces.appointments.appointment;
using MedCore.Web.Interfaces.appointments.doctorAvailability;
using MedCore.Web.Models.appointments;
using MedCore.Web.Models.doctorAvailability;
using Microsoft.AspNetCore.Mvc;

namespace MedCore.Web.Controllers.Appointments
{
    public class DoctorAvailabilityController : Controller
    {
        private readonly IDoctorAvailabilityWeb _Availability;
        private readonly ILogger<DoctorAvailabilityController> _logger;

        public DoctorAvailabilityController(
            IDoctorAvailabilityWeb availabilityWeb,
            ILogger<DoctorAvailabilityController> logger)
        {
            _Availability = availabilityWeb;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var Availability = await _Availability.GetAllAsync();
                return View(Availability);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener lista");
                ViewBag.ErrorMessage = "Error al cargar . Por favor intente nuevamente.";
                return View(new List<DoctorAvailabilityModel>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var Availability = await _Availability.GetByIdAsync(id);
                return View(Availability);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener detalles con ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDoctorAvailabilityModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var success = await _Availability.CreateAsync(model);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al crear";
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var Availability = await _Availability.GetEditModelByIdAsync(id);
                return View(Availability);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al cargar disponibilidad para edición con ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditDoctorAvailabilityModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var success = await _Availability.UpdateAsync(id, model);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al actualizar la disponibilidad.";
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar disponibilidad con ID {id}");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var Availability = await _Availability.GetByIdAsync(id);
                return View(Availability);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al cargar disponibilidad para eliminación con ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _Availability.DeleteAsync(id);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al eliminar la disponibilidad.";
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar disponibilidad con ID {id}");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View();
            }
        }
    }
}