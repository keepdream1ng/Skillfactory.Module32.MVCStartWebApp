using Microsoft.AspNetCore.Mvc;
using Skillfactory.Module32.MVCStartWebApp.Models;
using Skillfactory.Module32.MVCStartWebApp.Repositories;
using System.Diagnostics;

namespace Skillfactory.Module32.MVCStartWebApp.Controllers;

public class UsersController : Controller
{
    private readonly ILogger<UsersController> _logger;
    private readonly IBlogRepository _repository;

    public UsersController(ILogger<UsersController> logger, IBlogRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public IActionResult Register()
    {
        // Добавим создание нового пользователя
        var newUser = new User()
        {
            Id = Guid.NewGuid(),
        };

        return View(newUser);
    }
    [HttpPost]
    public async Task<IActionResult> Register(User newUser)
    {
        newUser.JoinDate = DateTime.Now;

        // Validate and save the user data to a data source
        if (ModelState.IsValid)
        {
            await _repository.AddUser(newUser);
            Console.WriteLine($"New User Created: {newUser.FirstName} {newUser.LastName}, Join Date: {newUser.JoinDate}");
            return RedirectToAction("UserAdded");
        }

        return View(newUser); // If model validation fails, return to the form with errors
    }
    public async Task<IActionResult> Index()
    {
        var authors = await _repository.GetUsers();
        return View(authors);
    }
    public IActionResult UserAdded()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
