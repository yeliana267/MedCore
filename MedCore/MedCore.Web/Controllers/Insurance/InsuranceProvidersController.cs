using System.Diagnostics;
using System.Threading.Tasks;
using MedCore.Application.Dtos.Insurance.InsuranceProviders;
using MedCore.Application.Interfaces.Insurance;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.Insurance;
using MedCore.Model.Models.appointments;
using MedCore.Web.Interfaces.Insurance;
using MedCore.Web.Models;
using MedCore.Web.Models.appointments;
using MedCore.Web.Models.Insurance.InsuranceProviders;
using MedCore.Web.Models.Insurance.NetworkType;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MedCore.Web.Controllers.Insurance
{
    public class InsuranceProvidersController : Controller
    {
        private readonly IInsuranceProvidersWeb _insuranceProvidersWeb;
        private readonly ILogger<InsuranceProvidersController> _logger;

        public InsuranceProvidersController(IInsuranceProvidersWeb insuranceProvidersWeb,
            ILogger<InsuranceProvidersController> logger)
        {
            _insuranceProvidersWeb = insuranceProvidersWeb;
            _logger = logger;
            
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var insuranceProviders = await _insuranceProvidersWeb.GetAllAsync();
                return View(insuranceProviders);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los proveedores de seguro");
                ViewBag.ErrorMessage = "Error al obtener red de seguroro";
                return View(new List<AppointmentModel>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var network = await _insuranceProvidersWeb.GetByIdAsync(id);
                return View(network);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener red de seguro con el conn ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateInsuranceProvidersModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var success = await _insuranceProvidersWeb.CreateAsync(model);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al crear Insurance Provider";
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear Insurance Provider");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var insuranceProvider = await _insuranceProvidersWeb.GetEditModelByIdAsync(id);
                return View(insuranceProvider);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al cargar el proveedor con ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditInsuranceProvidersModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var success = await _insuranceProvidersWeb.UpdateAsync(id, model);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al actualizar el tipo de proveedor de seguro.";
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar el proveedor de seguro con ID {id}");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var network = await _insuranceProvidersWeb.GetByIdAsync(id);
                return View(network);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar proveedor con ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _insuranceProvidersWeb.DeleteAsync(id);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al eliminar el tipo de red de seguro.";
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar proveedor con ID {id}");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View();
            }
        }
    }
}
