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
    public class DormsController : Controller
    {
        private readonly DbeStudentContext _context;

        public DormsController(DbeStudentContext context)
        {
            _context = context;
        }

        // GET: Dorms
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dorms.ToListAsync());
        }

        // GET: Dorms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dorm = await _context.Dorms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dorm == null)
            {
                return NotFound();
            }

            return View(dorm);
        }

        public async Task<IActionResult> OpenPasses(int? id)
        {
            return RedirectToAction("Index", "DormPasses", new { id});
        }

        public async Task<IActionResult> OpenRooms(int? id)
        {
            return RedirectToAction("Index", "DormRooms", new { id });
        }

        public async Task<IActionResult> OpenInspections(int? id)
        {
            return RedirectToAction("Index", "DormInspections", new { id, _context.Dorms.FirstOrDefault(d => d.Id == id).Number});
        }

        // GET: Dorms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dorms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,Address")] Dorm dorm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dorm);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dorm);
        }

        // GET: Dorms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dorm = await _context.Dorms.FindAsync(id);
            if (dorm == null)
            {
                return NotFound();
            }
            ViewBag.Id = id;
            return View(dorm);
        }

        // POST: Dorms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Address")] Dorm dorm)
        {
            if (id != dorm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dorm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DormExists(dorm.Id))
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
            return View(dorm);
        }

        // GET: Dorms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dorm = await _context.Dorms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dorm == null)
            {
                return NotFound();
            }

            return View(dorm);
        }

        // POST: Dorms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dorm = await _context.Dorms.FindAsync(id);
            if (dorm != null)
            {
                _context.Dorms.Remove(dorm);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DormExists(int id)
        {
            return _context.Dorms.Any(e => e.Id == id);
        }
    }
}
