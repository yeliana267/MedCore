using MedCore.Web.Interfaces.appointments.appointment;
using MedCore.Web.Repositories.appointmentsRepository.appointment;
using MedCore.Web.Models.appointments;
using Microsoft.AspNetCore.Mvc;

namespace MedCore.Web.Controllers.Appointments
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentWeb _appointmentWeb;
        private readonly ILogger<AppointmentsController> _logger;

        public AppointmentsController(
            IAppointmentWeb  appointmentWeb,
            ILogger<AppointmentsController> logger)
        {
            _appointmentWeb = appointmentWeb;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var appointments = await _appointmentWeb.GetAllAsync();
                return View(appointments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener lista de citas");
                ViewBag.ErrorMessage = "Error al cargar las citas. Por favor intente nuevamente.";
                return View(new List<AppointmentModel>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var appointment = await _appointmentWeb.GetByIdAsync(id);
                return View(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener detalles de cita con ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAppointmentModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var success = await _appointmentWeb.CreateAsync(model);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al crear la cita.";
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear cita");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var appointment = await _appointmentWeb.GetEditModelByIdAsync(id);
                return View(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al cargar cita para edición con ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditAppointmentModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var success = await _appointmentWeb.UpdateAsync(id, model);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al actualizar la cita.";
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar cita con ID {id}");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var appointment = await _appointmentWeb.GetByIdAsync(id);
                return View(appointment);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al cargar cita para eliminación con ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _appointmentWeb.DeleteAsync(id);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al eliminar la cita.";
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar cita con ID {id}");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View();
            }
        }
    }
}