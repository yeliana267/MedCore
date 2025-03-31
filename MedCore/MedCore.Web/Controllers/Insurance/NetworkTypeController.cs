using MedCore.Application.Dtos.Insurance.InsuranceProviders;
using MedCore.Application.Dtos.Insurance.NetworkType;
using MedCore.Application.Interfaces.Insurance;
using MedCore.Domain.Base;
using MedCore.Domain.Entities.Insurance;
using MedCore.Web.Models.Insurance;
using MedCore.Persistence.Interfaces.Insurance;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MedCore.Web.Controllers.Insurance
{
    public class NetworkTypeController : Controller
    {
        private readonly INetworkTypeService _networkTypeService;
        public NetworkTypeController(INetworkTypeService NetworkTypeService)
        {
            _networkTypeService = NetworkTypeService;
        }


        // GET: NetworkTypeController
        public async Task<IActionResult> Index()
        {
            List<NetworkTypeModel> networktype = new List<NetworkTypeModel>();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync("NetworkType/GetNeworkType");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    networktype = JsonConvert.DeserializeObject<List<NetworkTypeModel>>(result.Data.ToString());
                    return View(networktype);
                }
            }
            return View(new List<NetworkTypeModel>());
        }



        // GET: NetworkTypeController/Details/5
        public async Task<IActionResult> Details(int networkTypeId)
        {
            NetworkTypeModel networktype = new NetworkTypeModel();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"NetworkType/GetNetworkTypeById?Id={networkTypeId}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    networktype = JsonConvert.DeserializeObject<NetworkTypeModel>(result.Data.ToString());
                    return View(networktype);
                }
            }

            return View(networktype);
        }

        // GET: NetworkTypeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NetworkTypeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NetworkTypeModel networkType)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5266/api/");
                    var response = await client.PostAsJsonAsync<NetworkTypeModel>($"NetworkType/SaveNetworkType", networkType);

                    if (response.IsSuccessStatusCode)
                    {
                        operationResult = await response.Content.ReadFromJsonAsync<OperationResult>();
                    }
                    else
                    {
                        ViewBag.Message = "Error guardandado Tipo de red";
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

        // GET: NetworkTypeController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            NetworkTypeModel network = new NetworkTypeModel();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"NetworkType/UpdateNetworkType?Id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    network = JsonConvert.DeserializeObject<NetworkTypeModel>(result.Data.ToString());
                    return View(network);
                }
            }

            return View(network);
        }

        // POST: NetworkTypeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NetworkTypeModel networkType)
        {
            OperationResult operationResult = new OperationResult();
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:5266/api/");
                    var response = await client.PostAsJsonAsync<NetworkTypeModel>($"NetworkType/UpdateNetworkType", networkType);

                    if (response.IsSuccessStatusCode)
                    {
                        operationResult = await response.Content.ReadFromJsonAsync<OperationResult>();
                    }
                    else
                    {
                        ViewBag.Message = "Error actualizado tipo de red";
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

        // GET: NetworkTypeController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return RedirectToAction("Index");
            }

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.DeleteAsync($"NetworkType/DeleteNetworkType?Id={id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        // POST: NetworkTypeController/Delete/5
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
