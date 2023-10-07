using Microsoft.AspNetCore.Mvc;
using Skillfactory.Module32.MVCStartWebApp.Models;
using Skillfactory.Module32.MVCStartWebApp.Repositories;
using System.Diagnostics;

namespace Skillfactory.Module32.MVCStartWebApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IBlogRepository _repository;

    public HomeController(ILogger<HomeController> logger, IBlogRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    public async Task<IActionResult> Index()
    {
        // Добавим создание нового пользователя
        var newUser = new User()
        {
            Id = Guid.NewGuid(),
            FirstName = "Andrey",
            LastName = "Petrov",
            JoinDate = DateTime.Now
        };

        // Добавим в базу
        await _repository.AddUser(newUser);

        // Выведем результат
        Console.WriteLine($"User with id {newUser.Id}, named {newUser.FirstName} was successfully added on {newUser.JoinDate}");

        return View();
    }
    public async Task<IActionResult> Authors()
    {
        var authors = await _repository.GetUsers();
        return View(authors);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}