
using MedCore.Application.Dtos.users.Patients;
using MedCore.Application.Interfaces.users;
using MedCore.Domain.Base;
using MedCore.Web.Models.users.Patients;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MedCore.Web.Controllers.usersController
{
    public class PatientsController : Controller
    {
        private readonly IPatientsService _patientsService;

        public PatientsController(IPatientsService patientsService)
        {
            _patientsService = patientsService;
        }
        // GET: PatientsController
        public async Task<IActionResult> Index()
        {
            List<PatientsModel> patient = new List<PatientsModel>();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync("Patients/GetPatients");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    patient = JsonConvert.DeserializeObject<List<PatientsModel>>(result.Data.ToString());
                    return View(patient);
                }
            }

            return View(new List<PatientsModel>());
        }

        // GET: PatientsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            PatientsModel patient = new PatientsModel();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"Patients/GetPatientByID?Id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    patient = JsonConvert.DeserializeObject<PatientsModel>(result.Data.ToString());
                    return View(patient);
                }
            }

            return View(patient);
        }

        // GET: PatientsController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: PatientsController/Create
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
                using var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5266/api/");

            var dto = new SavePatientsDto
                    {
                
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                EmergencyContactName = model.EmergencyContactName,
                EmergencyContactPhone = model.EmergencyContactPhone,
                BloodType = model.BloodType,
                Allergies = model.Allergies,
                InsuranceProviderID = model.InsuranceProviderID,
                IsActive = true,
                CreatedAt = DateTime.UtcNow

            };


                var response = await client.PostAsJsonAsync("Patients/Save", dto);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResult>();

                    if (result != null && result.Success)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ViewBag.Message = result?.Message ?? "Error desconocido al crear el paciente.";
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

        // GET: PatientsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            EditPatientsModel patient = new EditPatientsModel();

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"Patients/GetPatientByID?Id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {

                    patient = JsonConvert.DeserializeObject<EditPatientsModel>(result.Data.ToString());
                    return View(patient);
                }
            }
            return View(patient);
        }

        // POST: PatientsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PatientsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            PatientsModel patient = new PatientsModel();

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"Patients/GetPatientByID?Id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    patient = JsonConvert.DeserializeObject<PatientsModel>(result.Data.ToString());
                    return View(patient);
                }
            }
                
            return NotFound();
        }

        // POST: PatientsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
