
using MedCore.Application.Dtos.users.Users;
using MedCore.Application.Interfaces.users;
using MedCore.Domain.Base;
using MedCore.Web.Models.users.User;
using MedCore.Web.Models.users.users;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MedCore.Web.Controllers.usersController
{
    public class UserController : Controller
    {
        public IUsersService _usersService;

        public UserController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        // GET: UserController
        public async Task<IActionResult> Index()
        {
            List<UsersModel> user = new List<UsersModel>();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync("Users/GetUsers");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    user = JsonConvert.DeserializeObject<List<UsersModel>>(result.Data.ToString());
                    return View(user);
                }
            }

            return View(new List<UsersModel>());
        }

        // GET: UserController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            UsersModel user = new UsersModel();
            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"Users/GetUsersByID?Id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    user = JsonConvert.DeserializeObject<UsersModel>(result.Data.ToString());
                    return View(user);
                }
            }

            return View(user);
        }

        // GET: UserController/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUsersModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            try
            {
                using var client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:5266/api/");

                var dto = new SaveUsersDto
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password,
                    RoleID = model.RoleID,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };


                var response = await client.PostAsJsonAsync("Users/SaveUsers", dto);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<OperationResult>();

                    if (result != null && result.Success)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                    ViewBag.Message = result?.Message ?? "Error desconocido al crear el usuario.";
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

        // GET: UserController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            EditUsersModel user = new EditUsersModel();

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"Users/GetUsersByID?Id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {

                    user = JsonConvert.DeserializeObject<EditUsersModel>(result.Data.ToString());
                    return View(user);
                }
            }
            return View(user);
        }

        // POST: UserController/Edit/5
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

        // GET: UserController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            UsersModel user = new UsersModel();

            using var client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:5266/api/");

            var response = await client.GetAsync($"Users/GetUsersByID?Id={id}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OperationResult>(data);

                if (result != null && result.Success && result.Data != null)
                {
                    user = JsonConvert.DeserializeObject<UsersModel>(result.Data.ToString());
                    return View(user); 
                }
            }

            return NotFound();
        }

        // POST: UserController/Delete/5
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
