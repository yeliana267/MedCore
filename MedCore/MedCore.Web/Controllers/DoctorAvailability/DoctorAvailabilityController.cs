using MedCore.Application.Interfaces.appointments;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.appointments;
using MedCore.Web.Models.appointments;
using MedCore.Web.Models.doctorAvailability;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MedCore.Web.Controllers.DoctorAvailability
{
    public class DoctorAvailabilityController : Controller
    {
        public IDoctorAvailabilityService _doctorAvailabilityService;

        public DoctorAvailabilityController(IDoctorAvailabilityService doctorAvailabilityService)
        {
            _doctorAvailabilityService = doctorAvailabilityService;
        }

        // GET: DoctorAvailabilityController
        public async Task<IActionResult> Index()
        {
            List<DoctorAvailabilityModel> doctorAvailability = new List<DoctorAvailabilityModel>();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync("DoctorAvailability/GetDoctorAvailability");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    doctorAvailability = JsonConvert.DeserializeObject<List<DoctorAvailabilityModel>>(result.Data.ToString());
                    return View(doctorAvailability);
                }
            }

            return View(new List<DoctorAvailabilityModel>());
        }

        // GET: DoctorAvailabilityController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            DoctorAvailabilityModel doctorAvailability = new DoctorAvailabilityModel();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"DoctorAvailability/GetDoctorAvailabilityById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    doctorAvailability = JsonConvert.DeserializeObject<DoctorAvailabilityModel>(result.Data.ToString());
                    return View(doctorAvailability);
                }
            }

            return View(doctorAvailability);
        }

        // GET: DoctorAvailabilityController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: DoctorAvailabilityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: DoctorAvailabilityController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            DoctorAvailabilityModel doctorAvailability = new DoctorAvailabilityModel();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"DoctorAvailability/GetDoctorAvailabilityById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    doctorAvailability = JsonConvert.DeserializeObject<DoctorAvailabilityModel>(result.Data.ToString());
                    return View(doctorAvailability);
                }
            }

            return View(doctorAvailability);
        }
        

        // POST: DoctorAvailabilityController/Edit/5
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

        // GET: DoctorAvailabilityController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            DoctorAvailabilityModel doctorAvailability = new DoctorAvailabilityModel();

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"DoctorAvailability/GetDoctorAvailabilityById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    doctorAvailability = JsonConvert.DeserializeObject<DoctorAvailabilityModel>(result.Data.ToString());
                    return View(doctorAvailability);
                }
            }

            return NotFound();
        }


        // POST: DoctorAvailabilityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
