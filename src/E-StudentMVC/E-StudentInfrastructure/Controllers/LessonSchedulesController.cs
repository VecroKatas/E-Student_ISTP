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
    public class LessonSchedulesController : Controller
    {
        private readonly DbeStudentContext _context;

        public LessonSchedulesController(DbeStudentContext context)
        {
            _context = context;
        }

        // GET: LessonSchedules
        public async Task<IActionResult> Index()
        {
            var dbeStudentContext = _context.LessonSchedules.Include(l => l.Group);
            return View(await dbeStudentContext.ToListAsync());
        }

        // GET: LessonSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessonSchedule = await _context.LessonSchedules
                .Include(l => l.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lessonSchedule == null)
            {
                return NotFound();
            }

            return View(lessonSchedule);
        }

        // GET: LessonSchedules/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name");
            return View();
        }

        // POST: LessonSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroupId,Year,File")] LessonSchedule lessonSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lessonSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", lessonSchedule.GroupId);
            return View(lessonSchedule);
        }

        // GET: LessonSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessonSchedule = await _context.LessonSchedules.FindAsync(id);
            if (lessonSchedule == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", lessonSchedule.GroupId);
            return View(lessonSchedule);
        }

        // POST: LessonSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroupId,Year,File")] LessonSchedule lessonSchedule)
        {
            if (id != lessonSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lessonSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonScheduleExists(lessonSchedule.Id))
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
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", lessonSchedule.GroupId);
            return View(lessonSchedule);
        }

        // GET: LessonSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessonSchedule = await _context.LessonSchedules
                .Include(l => l.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lessonSchedule == null)
            {
                return NotFound();
            }

            return View(lessonSchedule);
        }

        // POST: LessonSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lessonSchedule = await _context.LessonSchedules.FindAsync(id);
            if (lessonSchedule != null)
            {
                _context.LessonSchedules.Remove(lessonSchedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonScheduleExists(int id)
        {
            return _context.LessonSchedules.Any(e => e.Id == id);
        }
    }
}
