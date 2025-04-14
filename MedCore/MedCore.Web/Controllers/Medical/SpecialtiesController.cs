using MedCore.Application.Dtos.Medical.SpecialtiesDto;
using MedCore.Application.Interfaces.Medical;
using MedCore.Domain.Base;
using MedCore.Web.Models.Medical.SpecialtiesModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MedCore.Web.Controllers.Medical
{
    public class SpecialtiesController : Controller
    {
        private readonly ISpecialtiesService _specialtiesService;

        public SpecialtiesController(ISpecialtiesService specialtiesService)
        {
            _specialtiesService = specialtiesService;
        }

        // GET: SpecialtiesController
        public async Task<IActionResult> Index()
        {
            List<SpecialtiesModel> specialties = new List<SpecialtiesModel>();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync("Specialties/GetSpecialties");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    specialties = JsonConvert.DeserializeObject<List<SpecialtiesModel>>(result.Data.ToString());
                    return View(specialties);
                }
            }

            return View(new List<SpecialtiesModel>());
        }

        // GET: SpecialtiesController/Details/5
        public async Task<IActionResult> Details(short id)
        {
            SpecialtiesModel specialty = new SpecialtiesModel();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"Specialties/GetSpecialtyById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    specialty = JsonConvert.DeserializeObject<SpecialtiesModel>(result.Data.ToString());
                    return View(specialty);
                }
            }

            return View(specialty);
        }

        // GET: SpecialtiesController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SpecialtiesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSpecialtiesModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5266/api/");

                var dto = new SaveSpecialtiesDto
                {
                    SpecialtyName = model.SpecialtyName,
                    CreatedAt = DateTime.UtcNow
                };

                var response = await client.PostAsJsonAsync("Specialties/SaveSpecialty", dto);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResult>();

                    if (result != null && result.Success)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ViewBag.Message = result?.Message ?? "Error desconocido al crear la especialidad.";
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

        // GET: SpecialtiesController/Edit/5
        public async Task<IActionResult> Edit(short id)
        {
            var specialty = new EditSpecialtiesModel
            {
                SpecialtyName = string.Empty
            };

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"Specialties/GetSpecialtyById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    specialty = JsonConvert.DeserializeObject<EditSpecialtiesModel>(result.Data.ToString());
                    return View(specialty);
                }
            }
            return View(specialty);
        }

        // POST: SpecialtiesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, EditSpecialtiesModel specialty)
        {
            if (!ModelState.IsValid)
            {
                return View(specialty);
            }

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var dto = new UpdateSpecialtiesDto
            {
                SpecialtiesId = specialty.SpecialtiesId,
                SpecialtyName = specialty.SpecialtyName,
                UpdatedAt = DateTime.UtcNow
            };

            var response = await client.PutAsJsonAsync($"Specialties/UpdateSpecialty/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Error al actualizar la especialidad.";
            return View(specialty);
        }

        // GET: SpecialtiesController/Delete/5
        public async Task<IActionResult> Delete(short id)
        {
            SpecialtiesModel specialty = new SpecialtiesModel();

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"Specialties/GetSpecialtyById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    specialty = JsonConvert.DeserializeObject<SpecialtiesModel>(result.Data.ToString());
                    return View(specialty);
                }
            }

            return NotFound();
        }

        // POST: SpecialtiesController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var deleteDto = new RemoveSpecialtiesDto
            {
                SpecialtiesId = id
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(client.BaseAddress + "Specialties/DeleteSpecialty"),
                Content = JsonContent.Create(deleteDto)
            };

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = "Error al eliminar la especialidad.";
            return View();
        }
    }
}