using MedCore.Application.Interfaces.appointments;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.Insurance;
using MedCore.Model.Models.Insurance;
using MedCore.Web.Models.appointments;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MedCore.Web.Controllers.Appointments
{
    public class AppointmentsController : Controller
    {
        public IAppointmentsService _appointmentsService;

        public AppointmentsController(IAppointmentsService appointmentsService)
        {
            _appointmentsService = appointmentsService;
        }

        // GET: AppointmentsController
        public async Task<IActionResult> Index()
        {
            List<AppointmentModel> appointment = new List<AppointmentModel>();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync("Appointments/GetAppointments");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                   appointment = JsonConvert.DeserializeObject<List<AppointmentModel>>(result.Data.ToString());
                    return View(appointment);
                }
            }

            return View(new List<AppointmentModel>());
        }




        // GET: AppointmentsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            AppointmentModel appointment = new AppointmentModel();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"Appointments/GetAppointmentsById?Id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    appointment = JsonConvert.DeserializeObject<AppointmentModel>(result.Data.ToString());
                    return View(appointment);
                }
            }

            return View(appointment);
        }

        // GET: AppointmentsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppointmentsController/Create
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

        // GET: AppointmentsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            AppointmentModel appointment = new AppointmentModel();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"Appointments/GetAppointmentsById?Id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    appointment = JsonConvert.DeserializeObject<AppointmentModel>(result.Data.ToString());
                    return View(appointment);
                }
            }

            return View(appointment);
        }

        // POST: AppointmentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AppointmentModel appointment)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5266/api/");
                  
                    var response = await client.PutAsJsonAsync<AppointmentModel>($"Appointments/UpdateAppointment",appointment);
                    if (response.IsSuccessStatusCode)
                    {
                        operationResult = await response.Content.ReadFromJsonAsync<OperationResult>();
                    }
                    else
                    {
                        ViewBag.Message = "Error actualizado el proveedor de seguros";
                        return View();
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }

        }

        // GET: AppointmentsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            AppointmentModel appointment = new AppointmentModel();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.DeleteAsync($"Appointments/DeleteAppointment?Id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    appointment = JsonConvert.DeserializeObject<AppointmentModel>(result.Data.ToString());
                    return View(appointment);
                }
            }

            return NotFound(); // O redirige si prefieres
        }

        // POST: AppointmentsController/Delete/5
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
