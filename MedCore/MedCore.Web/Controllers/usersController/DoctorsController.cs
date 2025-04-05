using MedCore.Application.Dtos.users.Doctors;
using MedCore.Application.Interfaces.users;
using MedCore.Domain.Base;
using MedCore.Web.Models.users.Doctors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MedCore.Web.Controllers.usersController
{
    public class DoctorsController : Controller
    {
        private readonly IDoctorsService _doctorsService;

        public DoctorsController(IDoctorsService doctorsService)
        {
            _doctorsService = doctorsService;
        }
        // GET: DoctorsController
        public async Task<IActionResult> Index()
        {
            List<DoctorsModel> doctors = new List<DoctorsModel>();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync("Doctors/GetDoctors");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    doctors = JsonConvert.DeserializeObject<List<DoctorsModel>>(result.Data.ToString());
                    return View(doctors);
                }
            }

            return View(new List<DoctorsModel>());
        }

        // GET: DoctorsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            DoctorsModel doctors = new DoctorsModel();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"Doctors/GetDoctorByID?Id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    doctors = JsonConvert.DeserializeObject<DoctorsModel>(result.Data.ToString());
                    return View(doctors);
                }
            }

            return View(doctors);
        }

        // GET: DoctorsController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: DoctorsController/Create
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
                using var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5266/api/");

                var dto = new SaveDoctorsDto
                {
                    SpecialtyID = model.SpecialtyID,
                    LicenseNumber = model.LicenseNumber,
                    PhoneNumber = model.PhoneNumber,
                    YearsOfExperience = model.YearsOfExperience,
                    Education = model.Education,
                    Bio = model.Bio,
                    ConsultationFee = model.ConsultationFee,
                    ClinicAddress = model.ClinicAddress,
                    AvailabilityModeId = model.AvailabilityModeId,
                    LicenseExpirationDate = model.LicenseExpirationDate,
                    IsActive = true
                };


                var response = await client.PostAsJsonAsync("Doctors/Save", dto);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResult>();

                    if (result != null && result.Success)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ViewBag.Message = result?.Message ?? "Error desconocido al crear el doctor.";
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

        // GET: DoctorsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            EditDoctorsModel doctor = new EditDoctorsModel();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"Doctors/GetDoctorByID?Id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    doctor = JsonConvert.DeserializeObject<EditDoctorsModel>(result.Data.ToString());
                    return View(doctor);
                }
            }

            return View(doctor);
        }

        // POST: DoctorsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IFormCollection collection)
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

        // GET: DoctorsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            DoctorsModel doctors = new DoctorsModel();

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"Doctors/GetDoctorByID?Id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    doctors = JsonConvert.DeserializeObject<DoctorsModel>(result.Data.ToString());
                    return View(doctors);
                }
            }

            return NotFound();
        }

        // POST: DoctorsController/Delete/5
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
