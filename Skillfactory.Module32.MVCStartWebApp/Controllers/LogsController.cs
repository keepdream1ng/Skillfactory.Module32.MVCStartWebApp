using Microsoft.AspNetCore.Mvc;
using Skillfactory.Module32.MVCStartWebApp.Models;
using System.Diagnostics;

namespace Skillfactory.Module32.MVCStartWebApp.Controllers;

public class LogsController : Controller
{
	private readonly ILogger<LogsController> _logger;
	private readonly IRequestsRepository _repository;

	public LogsController(ILogger<LogsController> logger, IRequestsRepository repository)
    {
		_logger = logger;
		_repository = repository;
	}

    public async Task<IActionResult> Index()
    {
        var requests = await _repository.GetAllRequests();
        return View(requests);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
