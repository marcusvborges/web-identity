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
            var sectors = await _context.Sectors
                .Include(s => s.Department)
                .ToListAsync();

            return View(sectors);
        }

        [Authorize(Policy = "RequireUserAdminSuperAdminRole")]
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments,"Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,DepartmentId")] Sector sector)
        {
            if (ModelState.IsValid)
            {
                _context.Sectors.Add(sector);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
;            }
            return View(sector);
        }

        [Authorize(Policy = "RequireUserAdminSuperAdminRole")]
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var sector = await _context.Sectors
                .Include(s => s.Department)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (sector == null)
            {
                return NotFound();
            }

            return View(sector);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sector = await _context.Sectors.FindAsync(id);
            if (sector == null)
            {
                return NotFound();
            }
            return View(sector);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            var sector = await _context.Sectors.FindAsync(id);
            if (sector != null)
            {
                _context.Sectors.Remove(sector);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
