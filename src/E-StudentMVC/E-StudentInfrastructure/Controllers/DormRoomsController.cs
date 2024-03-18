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
    public class DormRoomsController : Controller
    {
        private readonly DbeStudentContext _context;

        public DormRoomsController(DbeStudentContext context)
        {
            _context = context;
        }

        // GET: DormRooms
        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                ViewBag.DormNumber = _context.Dorms.FirstOrDefaultAsync(g => g.Id == id).Result.Number;
                var dbeStudentContext = _context.DormRooms.Where(d => d.DormId == id);
                return View(await dbeStudentContext.ToListAsync());
            }
            else
            {
                var dbeStudentContext = _context.DormRooms.Include(d => d.Dorm);
                return View(await dbeStudentContext.ToListAsync());
            }
        }

        // GET: DormRooms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormRoom = await _context.DormRooms
                .Include(d => d.Dorm)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dormRoom == null)
            {
                return NotFound();
            }

            return View(dormRoom);
        }

        // GET: DormRooms/Create
        public IActionResult Create()
        {
            ViewData["DormId"] = new SelectList(_context.Dorms, "Id", "Number");
            return View();
        }

        // POST: DormRooms/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Number,DormId")] DormRoom dormRoom)
        {
            dormRoom.Dorm = _context.Dorms.FirstOrDefault(d => d.Id == dormRoom.DormId);

            if (ModelState.IsValid || ModelState["Dorm"].AttemptedValue == null)
            {
                _context.Add(dormRoom);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DormId"] = new SelectList(_context.Dorms, "Id", "Number", dormRoom.DormId);
            return View(dormRoom);
        }

        // GET: DormRooms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormRoom = await _context.DormRooms.FindAsync(id);
            if (dormRoom == null)
            {
                return NotFound();
            }
            ViewBag.Id = id;
            ViewData["DormId"] = new SelectList(_context.Dorms, "Id", "Number", dormRoom.DormId);
            return View(dormRoom);
        }

        // POST: DormRooms/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Number,DormId")] DormRoom dormRoom)
        {
            if (id != dormRoom.Id)
            {
                return NotFound();
            }

            dormRoom.Dorm = _context.Dorms.FirstOrDefault(d => d.Id == dormRoom.DormId);
                
            if (ModelState.IsValid || ModelState["Dorm"].AttemptedValue == null)
            {
                try
                {
                    _context.Update(dormRoom);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DormRoomExists(dormRoom.Id))
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
            ViewData["DormId"] = new SelectList(_context.Dorms, "Id", "Number", dormRoom.DormId);
            return View(dormRoom);
        }

        // GET: DormRooms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormRoom = await _context.DormRooms
                .Include(d => d.Dorm)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dormRoom == null)
            {
                return NotFound();
            }

            return View(dormRoom);
        }

        // POST: DormRooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dormRoom = await _context.DormRooms.FindAsync(id);
            if (dormRoom != null)
            {
                _context.DormRooms.Remove(dormRoom);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DormRoomExists(int id)
        {
            return _context.DormRooms.Any(e => e.Id == id);
        }
    }
}
