using HeatExchangeApp.Data;
using HeatExchangeApp.Models;
using HeatExchangeApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace HeatExchangeApp.Controllers;

public class HomeController : Controller
{
    private readonly HeatExchangeService _service;
    private readonly AppDbContext _context;

    public HomeController(HeatExchangeService service, AppDbContext context)
    {
        _service = service;
        _context = context;
    }

    public IActionResult Index()
    {
        return View(new InputData());
    }

    [HttpPost]
    public IActionResult Calculate(InputData model)
    {
        if (!ModelState.IsValid)
            return View("Index", model);

        var results = _service.Calculate(model);
        ViewBag.Input = model;
        return View("Results", results);
    }

    [HttpPost]
    public async Task<IActionResult> Save([FromForm] InputData input, [FromForm] string scenarioName)
    {
        var results = _service.Calculate(input);

        var scenario = new SavedScenario
        {
            Name = string.IsNullOrWhiteSpace(scenarioName) ? "Сценарий" : scenarioName.Trim(),
            InputJson = JsonSerializer.Serialize(input),
            ResultsJson = JsonSerializer.Serialize(results)
        };

        await _context.SavedScenarios.AddAsync(scenario);
        await _context.SaveChangesAsync();

        return RedirectToAction("Saved");
    }

    public async Task<IActionResult> Saved()
    {
        var list = await _context.SavedScenarios
            .OrderByDescending(s => s.CreatedAt)
            .ToListAsync();
        return View(list);
    }

    public async Task<IActionResult> Load(int id)
    {
        var s = await _context.SavedScenarios.FindAsync(id);
        if (s == null) return NotFound();

        var input = JsonSerializer.Deserialize<InputData>(s.InputJson) ?? new InputData();
        var results = JsonSerializer.Deserialize<List<LayerPoint>>(s.ResultsJson) ?? new();

        ViewBag.Input = input;
        ViewBag.ScenarioName = s.Name;
        return View("Results", results);
    }

    [HttpPost]
    public IActionResult Recalculate(InputData input)
    {
        if (!ModelState.IsValid)
            return View("Index", input);

        var results = _service.Calculate(input);
        ViewBag.Input = input;
        return View("Results", results);
    }

    public IActionResult Privacy() => View();
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View();
}
