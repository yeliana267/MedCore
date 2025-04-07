
using MedCore.Web.Interfaces.users;
using MedCore.Web.Models.users.Patients;
using Microsoft.AspNetCore.Mvc;
using PatientsModel = MedCore.Model.Models.users.PatientsModel;

namespace MedCore.Web.Controllers.usersController
{
    public class PatientsController : Controller
    {
        private readonly IPatientsWeb _patientWeb;
        private readonly ILogger<PatientsController> _logger;

        public PatientsController(
            IPatientsWeb patientWeb,
            ILogger<PatientsController> logger)
        {
            _patientWeb = patientWeb;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var patients = await _patientWeb.GetAllAsync();
                return View(patients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener lista de pacientes");
                ViewBag.ErrorMessage = "Error al cargar los pacientes. Por favor intente nuevamente.";
                return View(new List<PatientsModel>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var patient = await _patientWeb.GetByIdAsync(id);
                return View(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener detalles de paciente con ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                
                var bloodTypes = new List<string> { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
                ViewData["BloodTypes"] = bloodTypes;

                return View(new CreatePatientsModel
                {
                    
                    DateOfBirth = (DateTime.Now.AddYears(-18))
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar vista de creación");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreatePatientsModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var success = await _patientWeb.CreateAsync(model);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al crear el paciente.";
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear paciente");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var patient = await _patientWeb.GetEditModelByIdAsync(id);

                
                var bloodTypes = new List<string> { "A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-" };
                ViewData["BloodTypes"] = bloodTypes;

                return View(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al cargar paciente para edición con ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditPatientsModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var success = await _patientWeb.UpdateAsync(id, model);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al actualizar el paciente.";
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar paciente con ID {id}");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var patient = await _patientWeb.GetByIdAsync(id);
                return View(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al cargar paciente para eliminación con ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _patientWeb.DeleteAsync(id);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al eliminar el paciente.";
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar paciente con ID {id}");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View();
            }
        }
    }
}
