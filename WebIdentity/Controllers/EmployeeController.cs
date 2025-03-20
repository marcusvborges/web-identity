using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebIdentity.Context;
using WebIdentity.Entities;
using WebIdentity.Enums;

namespace WebIdentity.Controllers
{
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly MySQLDbContext _context;

        public EmployeeController(MySQLDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var employees = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Sector)
                .ToListAsync();
            return View(employees);
        }

        [Authorize(Policy = "RequireUserAdminSuperAdminRole")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .Include(e => e.Department)
                .Include(e => e.Sector)
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [Authorize(Policy = "RequireUserAdminSuperAdminRole")]
        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
            ViewBag.Sectors = new SelectList(_context.Sectors, "Id", "Name");
            ViewBag.HierarchicalLevels = Enum
                .GetValues(typeof(HierarchicalLevel))                                            
                .Cast<HierarchicalLevel>()                                            
                .Select(e => new SelectListItem                                           
                {                                            
                    Text = e.ToString(),                                               
                    Value = e.ToString()                                      
                })                                         
                .ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(
            "EmployeeId,Name,Email,BirthDate, PhoneNumber," +
            "DateAdmission,SectorId,DepartmentId,HierarchicalLevel"
        )] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            ViewBag.Sectors = new SelectList(_context.Sectors, "Id", "Name", employee.SectorId);
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name", employee.DepartmentId);
            ViewBag.HierarchicalLevels = new SelectList(
                Enum.GetValues(typeof(HierarchicalLevel))
                    .Cast<HierarchicalLevel>()
                    .Select(e => new { Id = e.ToString(), Name = e.ToString() }),
                "Id",
                "Name",
                employee.HierarchicalLevel.ToString() // Define o valor selecionado
            );
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind(
            "EmployeeId,Name,Email,BirthDate, PhoneNumber," +
            "DateAdmission,SectorId,DepartmentId,HierarchicalLevel"
        )] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!employeeExists(employee.EmployeeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return NotFound();
            }

            var employee = await _context.Employees
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool employeeExists(int id)
        {
            return _context.Employees.Any(e => e.EmployeeId == id);
        }
    }
}
