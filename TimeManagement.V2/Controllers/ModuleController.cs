using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TimeManagement.V2.Models;

public class ModuleController : Controller
{
    private readonly AppDbContext _dbContext;

    public ModuleController(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult Index()
    {
        var modules = _dbContext.Modules.Include(m => m.StudySessions).ToList();
        return View(modules);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Module module)
    {
        if (ModelState.IsValid)
        {
            // Add logic to validate and create a new module in the database
            _dbContext.Modules.Add(module);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(module);
    }

    public IActionResult Edit(int id)
    {
        var module = _dbContext.Modules.Find(id);
        if (module == null)
        {
            return NotFound(); // Return 404 if module is not found
        }

        return View(module);
    }

    [HttpPost]
    public IActionResult Edit(Module module)
    {
        if (ModelState.IsValid)
        {
            // Add logic to validate and update the module in the database
            _dbContext.Modules.Update(module);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(module);
    }

    public IActionResult Delete(int id)
    {
        var module = _dbContext.Modules.Find(id);
        if (module == null)
        {
            return NotFound(); // Return 404 if module is not found
        }

        return View(module);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        // Add logic to delete the module from the database
        var module = _dbContext.Modules.Find(id);
        if (module == null)
        {
            return NotFound(); // Return 404 if module is not found
        }

        _dbContext.Modules.Remove(module);
        _dbContext.SaveChanges();

        return RedirectToAction("Index");
    }

    public IActionResult StudySessions(int id)
    {
        var module = _dbContext.Modules.Include(m => m.StudySessions).FirstOrDefault(m => m.ID == id);
        if (module == null)
        {
            return NotFound(); // Return 404 if module is not found
        }

        return View(module);
    }
}
