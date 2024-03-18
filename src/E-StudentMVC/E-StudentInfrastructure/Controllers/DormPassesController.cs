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
    public class DormPassesController : Controller
    {
        private readonly DbeStudentContext _context;

        public DormPassesController(DbeStudentContext context)
        {
            _context = context;
        }

        // GET: DormPasses
        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                ViewBag.DormNumber = _context.Dorms.FirstOrDefaultAsync(g => g.Id == id).Result.Number;
                var dbeStudentContext = _context.DormPasses.Where(d => d.DormId == id).Include(d => d.Room);
                return View(await dbeStudentContext.ToListAsync());
            }
            else
            {
                var dbeStudentContext = _context.DormPasses.Include(d => d.Dorm).Include(d => d.Room);
                return View(await dbeStudentContext.ToListAsync());
            }
        }

        // GET: DormPasses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormPass = await _context.DormPasses
                .Include(d => d.Dorm)
                .Include(d => d.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dormPass == null)
            {
                return NotFound();
            }

            return View(dormPass);
        }

        public async Task<IActionResult> OpenRoom(int? id)
        {
            return RedirectToAction("Details", "DormRooms", new { id });
        }

        public async Task<IActionResult> OpenDorm(int? id)
        {
            return RedirectToAction("Details", "Dorms", new { id });
        }

        public async Task<IActionResult> OpenResident(int? id)
        {
            return RedirectToAction("Details", "DormResidents", new { _context.DormResidents.FirstOrDefaultAsync(u => u.PassId == id).Result.Id });
        }

        // GET: DormPasses/Create
        public IActionResult Create()
        {
            ViewData["DormId"] = new SelectList(_context.Dorms, "Id", "Id");
            ViewData["RoomId"] = new SelectList(_context.DormRooms, "Id", "Id");
            return View();
        }

        // POST: DormPasses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,Name,DormId,RoomId,Issued,Expires")] DormPass dormPass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dormPass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DormId"] = new SelectList(_context.Dorms, "Id", "Id", dormPass.DormId);
            ViewData["RoomId"] = new SelectList(_context.DormRooms, "Id", "Id", dormPass.RoomId);
            return View(dormPass);
        }

        // GET: DormPasses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormPass = await _context.DormPasses.FindAsync(id);
            if (dormPass == null)
            {
                return NotFound();
            }
            ViewData["DormId"] = new SelectList(_context.Dorms, "Id", "Id", dormPass.DormId);
            ViewData["RoomId"] = new SelectList(_context.DormRooms, "Id", "Id", dormPass.RoomId);
            return View(dormPass);
        }

        // POST: DormPasses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,Name,DormId,RoomId,Issued,Expires")] DormPass dormPass)
        {
            if (id != dormPass.Id)
            {
                return NotFound();
            }

            dormPass.Dorm = _context.Dorms.FirstOrDefault(d => d.Id == dormPass.DormId);
            dormPass.Room = _context.DormRooms.FirstOrDefault(d => d.Id == dormPass.RoomId);

            if (ModelState.IsValid || ModelState["Dorm"].AttemptedValue == null)
            {
                try
                {
                    _context.Update(dormPass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DormPassExists(dormPass.Id))
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
            ViewData["DormId"] = new SelectList(_context.Dorms, "Id", "Id", dormPass.DormId);
            ViewData["RoomId"] = new SelectList(_context.DormRooms, "Id", "Id", dormPass.RoomId);
            return View(dormPass);
        }

        // GET: DormPasses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormPass = await _context.DormPasses
                .Include(d => d.Dorm)
                .Include(d => d.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dormPass == null)
            {
                return NotFound();
            }

            return View(dormPass);
        }

        // POST: DormPasses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dormPass = await _context.DormPasses.FindAsync(id);
            if (dormPass != null)
            {
                _context.DormPasses.Remove(dormPass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DormPassExists(int id)
        {
            return _context.DormPasses.Any(e => e.Id == id);
        }
    }
}
