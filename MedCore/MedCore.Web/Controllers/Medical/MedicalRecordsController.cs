using MedCore.Application.Dtos.Medical.MedicalRecordsDto;
using MedCore.Application.Interfaces.Medical;
using MedCore.Domain.Base;
using MedCore.Model.Models.medical;
using MedCore.Web.Models.Medical.MedicalRecordsModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using MedicalRecordsModel = MedCore.Web.Models.Medical.MedicalRecordsModels.MedicalRecordsModel;


namespace MedCore.Web.Controllers.Medical
{
    public class MedicalRecordsController : Controller
    {
        private readonly IMedicalRecordsService _medicalRecordsService;

        public MedicalRecordsController(IMedicalRecordsService medicalRecordsService)
        {
            _medicalRecordsService = medicalRecordsService;
        }

        // GET: MedicalRecordsController
        public async Task<IActionResult> Index()
        {
            List<MedicalRecordsModel> medicalRecords = new List<MedicalRecordsModel>();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync("MedicalRecords/GetMedicalRecords");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    medicalRecords = JsonConvert.DeserializeObject<List<MedicalRecordsModel>>(result.Data.ToString());
                    return View(medicalRecords);
                }
            }

            return View(new List<MedicalRecordsModel>());
        }

        // GET: MedicalRecordsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            MedicalRecordsModel medicalRecord = new MedicalRecordsModel();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"MedicalRecords/GetMedicalRecordById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    medicalRecord = JsonConvert.DeserializeObject<MedicalRecordsModel>(result.Data.ToString());
                    return View(medicalRecord);
                }
            }

            return View(medicalRecord);
        }

        // GET: MedicalRecordsController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MedicalRecordsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMedicalRecordsModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5266/api/");

                var dto = new SaveMedicalRecordsDto
                {
                    PatientID = model.PatientID,
                    DoctorID = model.DoctorID,
                    Diagnosis = model.Diagnosis,
                    Treatment = model.Treatment,
                    DateOfVisit = model.DateOfVisit,
                    CreatedAt = DateTime.UtcNow
                };

                var response = await client.PostAsJsonAsync("MedicalRecords/SaveMedicalRecord", dto);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResult>();

                    if (result != null && result.Success)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ViewBag.Message = result?.Message ?? "Error desconocido al crear el registro médico.";
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

        // GET: MedicalRecordsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var medicalRecord = new EditMedicalRecordsModel
            {
                Diagnosis = string.Empty,
                Treatment = string.Empty
            };

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"MedicalRecords/GetMedicalRecordById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    medicalRecord = JsonConvert.DeserializeObject<EditMedicalRecordsModel>(result.Data.ToString());
                    return View(medicalRecord);
                }
            }
            return View(medicalRecord);
        }

        // POST: MedicalRecordsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditMedicalRecordsModel medicalRecord)
        {
            if (!ModelState.IsValid)
            {
                return View(medicalRecord);
            }

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var dto = new UpdateMedicalRecordsDto
            {
                MedicalRecordsId = medicalRecord.MedicalRecordId,
                PatientID = medicalRecord.PatientID,
                DoctorID = medicalRecord.DoctorID,
                Diagnosis = medicalRecord.Diagnosis,
                Treatment = medicalRecord.Treatment,
                DateOfVisit = medicalRecord.DateOfVisit,
                UpdatedAt = DateTime.UtcNow
            };

            var response = await client.PutAsJsonAsync($"MedicalRecords/UpdateMedicalRecord/{id}", dto);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            ViewBag.Message = "Error al actualizar el registro médico.";
            return View(medicalRecord);
        }

        // GET: MedicalRecordsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            MedicalRecordsModel medicalRecord = new MedicalRecordsModel();

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"MedicalRecords/GetMedicalRecordById?id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    medicalRecord = JsonConvert.DeserializeObject<MedicalRecordsModel>(result.Data.ToString());
                    return View(medicalRecord);
                }
            }

            return NotFound();
        }

        // POST: MedicalRecordsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var deleteDto = new RemoveMedicalRecordsDto
            {
                MedicalRecordsId = id
            };

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(client.BaseAddress + "MedicalRecords/DeleteMedicalRecord"),
                Content = JsonContent.Create(deleteDto)
            };

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Message = "Error al eliminar el registro médico.";
            return View();
        }
    }
}