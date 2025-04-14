

using MedCore.Web.Interfaces.users;
using MedCore.Web.Models.users.User;
using MedCore.Web.Models.users.users;
using Microsoft.AspNetCore.Mvc;


namespace MedCore.Web.Controllers.usersController
{
    public class UserController : Controller
    {
        private readonly IUsersWeb _usersWeb;
        private readonly ILogger<UserController> _logger;

        public UserController(
            IUsersWeb usersWeb,
            ILogger<UserController> logger)
        {
            _usersWeb = usersWeb;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var users = await _usersWeb.GetAllAsync();
                return View(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener lista de usuarios");
                ViewBag.ErrorMessage = "Error al cargar los usuarios. Por favor intente nuevamente.";
                return View(new List<UsersModel>());
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var user = await _usersWeb.GetByIdAsync(id);
                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al obtener detalles de usuario con ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        public async Task<IActionResult> Create()
        {
            try
            {
                var model = new CreateUsersModel
                {
                    IsActive = true, 
                    CreatedAt = DateTime.UtcNow
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cargar la vista de creación de usuario");
                ViewBag.ErrorMessage = "Error al inicializar el formulario de creación";
                return RedirectToAction(nameof(Index));
            }
        }

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
                var success = await _usersWeb.CreateAsync(model);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al crear el usuario.";
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear usuario");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View(model);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var user = await _usersWeb.GetEditModelByIdAsync(id);
                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al cargar usuario para edición con ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditUsersModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var success = await _usersWeb.UpdateAsync(id, model);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al actualizar el usuario.";
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al actualizar usuario con ID {id}");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View(model);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var user = await _usersWeb.GetByIdAsync(id);
                return View(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al cargar usuario para eliminación con ID {id}");
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var success = await _usersWeb.DeleteAsync(id);
                if (success)
                {
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Message = "Error al eliminar el usuario.";
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error al eliminar usuario con ID {id}");
                ViewBag.Message = $"Error inesperado: {ex.Message}";
                return View();
            }
        }
    }
}

