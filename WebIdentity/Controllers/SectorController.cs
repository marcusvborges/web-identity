using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebIdentity.Context;
using WebIdentity.Entities;

namespace WebIdentity.Controllers
{
    [Authorize]
    public class SectorController : Controller
    {
        private readonly MySQLDbContext _context;

        public SectorController(MySQLDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Sectors.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments,"Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,Name,Department")] Sector sector)
        {
            if (ModelState.IsValid)
            {
                _context.Sectors.Add(sector);
                await _context.SaveChangesAsync();
                return RedirectToAction();
;            }
            return View(sector);
        }


    }
}
