using MedCore.Application.Dtos.Insurance.InsuranceProviders;
using MedCore.Application.Dtos.Insurance.NetworkType;
using MedCore.Application.Interfaces.Insurance;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.Insurance;
using MedCore.Persistence.Interfaces.Insurance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MedCore.Web.Models.Insurance.NetworkType;
using MedCore.Web.Models.appointments;
using MedCore.Web.Interfaces.appointments;
using MedCore.Web.Interfaces.Insurance;
using MedCore.Web.Repositories.InsuranceWeb.NetworkTypeWeb;

namespace MedCore.Web.Controllers.Insurance
{
    public class NetworkTypeController : Controller
    {
        private readonly INetworkTypeWeb _networkTypeWeb;
        private readonly ILogger<NetworkTypeController> _logger;


        public NetworkTypeController(INetworkTypeWeb networkTypeWeb,
            ILogger<NetworkTypeController> logger)
        {
            _networkTypeWeb = networkTypeWeb;
            _logger = logger;

        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var networktype = await _networkTypeWeb.GetAllAsync();
                return View(networktype);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los tipos de redes de seguro");
                ViewBag.ErrorMessage = "Error al obtener red de seguro";
                return View(new List<AppointmentModel>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var network = await _networkTypeWeb.GetByIdAsync(id);
                return View(network);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener red de seguro con el n ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNetworkTypeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var success = await _networkTypeWeb.CreateAsync(model);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al crear la red de seguros";
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear la red de seguros");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var network = await _networkTypeWeb.GetEditModelByIdAsync(id);
                return View(network);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al cargar el tipo de red {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditNetworkTypeModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var success = await _networkTypeWeb.UpdateAsync(id, model);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al actualizar eltipo de red.";
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar eltipo de red con ID {id}");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var network = await _networkTypeWeb.GetByIdAsync(id);
                return View(network);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar red con ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _networkTypeWeb.DeleteAsync(id);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al eliminar el tipo de red.";
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar tipo de red con ID {id}");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View();
            }
        }
    }
}
