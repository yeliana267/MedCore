using System.Diagnostics;
using System.Threading.Tasks;
using MedCore.Application.Dtos.Insurance.InsuranceProviders;
using MedCore.Application.Interfaces.Insurance;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.Insurance;
using MedCore.Model.Models.appointments;
using MedCore.Web.Models;
using MedCore.Web.Models.Insurance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MedCore.Web.Controllers.Insurance
{
    public class InsuranceProvidersController : Controller
    {
        private readonly IInsuranceProvidersService _insuranceProvidersService;
        public InsuranceProvidersController(IInsuranceProvidersService InsuranceProvidersService)
        {
            _insuranceProvidersService = InsuranceProvidersService;
        }
        // GET: InsuranceProvidersController
        public async Task<IActionResult> Index()
        {
            List<InsuranceProvidersModel> insuranceprovider = new List<InsuranceProvidersModel>();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync("InsuranceProviders/GetInsuranceProviders");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    insuranceprovider = JsonConvert.DeserializeObject<List<InsuranceProvidersModel>>(result.Data.ToString());
                    return View(insuranceprovider);
                }
            }
            return View(new List<InsuranceProvidersModel>());
        }

        // GET: InsuranceProvidersController/Details/5
        public async Task<IActionResult> Details(int Id)
        {
            InsuranceProvidersModel insuranceproviders = new InsuranceProvidersModel();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"InsuranceProviders/GetInsuranceProvidersById?Id={Id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    insuranceproviders = JsonConvert.DeserializeObject<InsuranceProvidersModel>(result.Data.ToString());
                    return View(insuranceproviders);
                }
            }

            return View(insuranceproviders);
        }



        // GET: InsuranceProvidersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InsuranceProvidersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsuranceProvidersModel insuranceProviders)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5266/api/");
                    var response = await client.PostAsJsonAsync<InsuranceProvidersModel>($"InsuranceProviders/SaveInsuranceProviders", insuranceProviders);

                    if (response.IsSuccessStatusCode)
                    {
                        operationResult = await response.Content.ReadFromJsonAsync<OperationResult>();
                    }
                    else
                    {
                        ViewBag.Message = "Error guardandado el proveedor";
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

        // GET: InsuranceProvidersController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            InsuranceProvidersModel insuranceprovider = new InsuranceProvidersModel();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"InsuranceProviders/UpdateInsuranceProviders{id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    insuranceprovider = JsonConvert.DeserializeObject<InsuranceProvidersModel>(result.Data.ToString());
                    return View(insuranceprovider);
                }
            }

            return View(insuranceprovider);
        }

        // POST: InsuranceProvidersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(InsuranceProvidersModel insuranceProviders)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5266/api/");
                    var response = await client.PostAsJsonAsync<InsuranceProvidersModel>($"InsuranceProviders/UpdateInsuranceProviders", insuranceProviders);

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





        // GET: InsuranceProvidersController/Delete/5
        //public async Task<IActionResult> Delete(int id)
        //{
        //    OperationResult operationResult = new OperationResult();
        //    using var client = new HttpClient();
        //    client.BaseAddress = new Uri("http://localhost:5266/api/");
        //    var response = await client.DeleteAsync("NetworkType/DeleteNetworkType");


        //    return View();
        //}
       


        // POST: InsuranceProvidersController/Delete/5
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
