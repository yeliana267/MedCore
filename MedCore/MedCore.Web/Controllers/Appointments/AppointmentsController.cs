using MedCore.Application.Dtos.appointments.Appointments;
using MedCore.Application.Interfaces.appointments;
using MedCore.Domain.Base;
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
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: AppointmentsController/Create
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
                using var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5266/api/");

                var dto = new SaveAppointmentsDto
                {
                    PatientID = model.PatientID,
                    DoctorID = model.DoctorID,
                    AppointmentDate = model.AppointmentDate,
                    StatusID = model.StatusID,
                    CreatedAt = DateTime.UtcNow
                };

                var response = await client.PostAsJsonAsync("Appointments/SaveAppointment", dto);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResult>();

                    if (result != null && result.Success)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ViewBag.Message = result?.Message ?? "Error desconocido al crear la cita.";
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
        // GET: AppointmentsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            EditAppointmentModel appointment = new EditAppointmentModel();

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"Appointments/GetAppointmentsById?Id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {

                    appointment = JsonConvert.DeserializeObject<EditAppointmentModel>(result.Data.ToString());
                    return View(appointment);
                }
            }
            return View(appointment);
        }

        // POST: AppointmentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditAppointmentModel appointment)
        {
            if (!ModelState.IsValid)
            {
                return View(appointment);
            }

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var dto = new UpdateAppointmentsDto
            {
                AppointmentID = appointment.AppointmentID,
                PatientID = appointment.PatientID,
                DoctorID = appointment.DoctorID,
                StatusID = appointment.StatusID,
                AppointmentDate = appointment.AppointmentDate,
                UpdatedAt = DateTime.UtcNow
            };

            var response = await client.PutAsJsonAsync($"Appointments/UpdateAppointment/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Error al actualizar la cita.";
            return View(appointment);
        }

        // GET: AppointmentsController/Delete/5
        public async Task<IActionResult> Delete(int id)
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

            return NotFound();
        }


        // POST: AppointmentsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var deleteDto = new RemoveAppointmentsDto
            {
                AppointmentID = id
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(client.BaseAddress + "Appointments/DeleteAppointment"),
                Content = JsonContent.Create(deleteDto)
            };

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = "Error al eliminar la cita.";
            return View();
        }
    }
}