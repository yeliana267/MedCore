using MedCore.Application.Dtos.Medical.AvailibilityModesDto;
using MedCore.Application.Interfaces.Medical;
using MedCore.Domain.Base;
using MedCore.Web.Models.Medical.AvailabilityModes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AvailabilityModesModel = MedCore.Web.Models.Medical.AvailabilityModes.AvailabilityModesModel;

namespace MedCore.Web.Controllers.Medical
{
    public class AvailabilityModesController : Controller
    {
        private readonly IAvailabilityModesService _availabilityModesService;

        public AvailabilityModesController(IAvailabilityModesService availabilityModesService)
        {
            _availabilityModesService = availabilityModesService;
        }

        // GET: AvailabilityModesController
        public async Task<IActionResult> Index()
        {
            List<AvailabilityModesModel> availabilityModes = new List<AvailabilityModesModel>();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync("AvailabilityModes/GetAvailability");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    availabilityModes = JsonConvert.DeserializeObject<List<AvailabilityModesModel>>(result.Data.ToString());
                    return View(availabilityModes);
                }
            }

            return View(new List<AvailabilityModesModel>());
        }
        
        // GET: AvailabilityModesController/Details/5
        public async Task<IActionResult> Details(short id)
        {
            AvailabilityModesModel availabilityMode = new AvailabilityModesModel();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"AvailabilityModes/GetAvailabilityById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    availabilityMode = JsonConvert.DeserializeObject<AvailabilityModesModel>(result.Data.ToString());
                    return View(availabilityMode);
                }
            }

            return View(availabilityMode);
        }

        // GET: AvailabilityModesController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AvailabilityModesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAvailabilityModesModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5266/api/");

                var dto = new SaveAvailabilityModesDto
                {
                    AvailabilityMode = model.AvailabilityMode,
                    CreatedAt = DateTime.UtcNow
                };

                var response = await client.PostAsJsonAsync("AvailabilityModes/SaveAvailability", dto);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResult>();

                    if (result != null && result.Success)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ViewBag.Message = result?.Message ?? "Error desconocido al crear el modo de disponibilidad.";
                }
                else
                {
                    ViewBag.Message = "Error al conectar con la API.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Error inesperado: " + ex.Message;
            }

            return View(model);
        }

        // GET: AvailabilityModesController/Edit/5
        public async Task<IActionResult> Edit(short id)
        {
            var availabilityMode = new EditAvailabilityModesModel
            {
                AvailabilityMode = string.Empty 
            };

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"AvailabilityModes/GetAvailabilityById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    availabilityMode = JsonConvert.DeserializeObject<EditAvailabilityModesModel>(result.Data.ToString());
                    return View(availabilityMode);
                }
            }
            return View(availabilityMode);
        }

        // POST: AvailabilityModesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, EditAvailabilityModesModel availabilityMode)
        {
            if (!ModelState.IsValid)
            {
                return View(availabilityMode);
            }

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var dto = new UpdateAvailabilityModesDto
            {
                AvailabilityModesId = availabilityMode.AvailabilityModesId,
                AvailabilityMode = availabilityMode.AvailabilityMode,
                UpdatedAt = DateTime.UtcNow
            };

            var response = await client.PutAsJsonAsync($"AvailabilityModes/UpdateAvailability/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Error al actualizar el modo de disponibilidad.";
            return View(availabilityMode);
        }

        // GET: AvailabilityModesController/Delete/5
        public async Task<IActionResult> Delete(short id)
        {
            AvailabilityModesModel availabilityMode = new AvailabilityModesModel();

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"AvailabilityModes/GetAvailabilityById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    availabilityMode = JsonConvert.DeserializeObject<AvailabilityModesModel>(result.Data.ToString());
                    return View(availabilityMode); 
                }
            }

            return NotFound();
        }

        // POST: AvailabilityModesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var deleteDto = new RemoveAvailabilityModesDto
            {
                AvailabilityModesId = id
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(client.BaseAddress + "AvailabilityModes/DeleteAvailability"),
                Content = JsonContent.Create(deleteDto)
            };

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = "Error al eliminar el modo de disponibilidad.";
            return View();
        }
    }
}