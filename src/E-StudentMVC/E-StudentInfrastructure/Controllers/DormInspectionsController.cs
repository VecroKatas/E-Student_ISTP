using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_StudentDomain.Model;
using E_StudentInfrastructure;

namespace E_StudentInfrastructure.Controllers
{
    public class DormInspectionsController : Controller
    {
        private readonly DbeStudentContext _context;

        public DormInspectionsController(DbeStudentContext context)
        {
            _context = context;
        }

        // GET: DormInspections

        public async Task<IActionResult> Index(int? id, string? number)
        {
            if (id == null) return RedirectToAction("Dorms", "Index");
            ViewBag.DormId = id;
            ViewBag.DormNumber = number;
            var inspectionByDorm = _context.DormInspections.Where(i => i.DormId == id).Include(i => i.Dorm);

            return View(await inspectionByDorm.ToListAsync());
        }

        // GET: DormInspections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormInspection = await _context.DormInspections
                .Include(d => d.Dorm)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dormInspection == null)
            {
                return NotFound();
            }

            return View(dormInspection);
        }

        // GET: DormInspections/Create
        public IActionResult Create()
        {
            ViewData["DormId"] = new SelectList(_context.Dorms, "Id", "Address");
            return View();
        }

        // POST: DormInspections/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DormId,Date")] DormInspection dormInspection)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dormInspection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DormId"] = new SelectList(_context.Dorms, "Id", "Number", dormInspection.DormId);
            return View(dormInspection);
        }

        // GET: DormInspections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormInspection = await _context.DormInspections.FindAsync(id);
            if (dormInspection == null)
            {
                return NotFound();
            }
            ViewData["DormId"] = new SelectList(_context.Dorms, "Id", "Number", dormInspection.DormId);
            return View(dormInspection);
        }

        // POST: DormInspections/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DormId,Date")] DormInspection dormInspection)
        {
            if (id != dormInspection.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dormInspection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DormInspectionExists(dormInspection.Id))
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
            ViewData["DormId"] = new SelectList(_context.Dorms, "Id", "Number", dormInspection.DormId);
            return View(dormInspection);
        }

        // GET: DormInspections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormInspection = await _context.DormInspections
                .Include(d => d.Dorm)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dormInspection == null)
            {
                return NotFound();
            }

            return View(dormInspection);
        }

        // POST: DormInspections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dormInspection = await _context.DormInspections.FindAsync(id);
            if (dormInspection != null)
            {
                _context.DormInspections.Remove(dormInspection);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DormInspectionExists(int id)
        {
            return _context.DormInspections.Any(e => e.Id == id);
        }
    }
}
