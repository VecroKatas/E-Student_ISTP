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
    public class ExamSchedulesController : Controller
    {
        private readonly DbeStudentContext _context;

        public ExamSchedulesController(DbeStudentContext context)
        {
            _context = context;
        }

        // GET: ExamSchedules
        public async Task<IActionResult> Index()
        {
            var dbeStudentContext = _context.ExamSchedules.Include(e => e.Group);
            return View(await dbeStudentContext.ToListAsync());
        }

        // GET: ExamSchedules/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examSchedule = await _context.ExamSchedules
                .Include(e => e.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examSchedule == null)
            {
                return NotFound();
            }

            return View(examSchedule);
        }

        // GET: ExamSchedules/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name");
            return View();
        }

        // POST: ExamSchedules/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroupId,Year,File")] ExamSchedule examSchedule)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examSchedule);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", examSchedule.GroupId);
            return View(examSchedule);
        }

        // GET: ExamSchedules/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examSchedule = await _context.ExamSchedules.FindAsync(id);
            if (examSchedule == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", examSchedule.GroupId);
            return View(examSchedule);
        }

        // POST: ExamSchedules/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroupId,Year,File")] ExamSchedule examSchedule)
        {
            if (id != examSchedule.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examSchedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamScheduleExists(examSchedule.Id))
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
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name", examSchedule.GroupId);
            return View(examSchedule);
        }

        // GET: ExamSchedules/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examSchedule = await _context.ExamSchedules
                .Include(e => e.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examSchedule == null)
            {
                return NotFound();
            }

            return View(examSchedule);
        }

        // POST: ExamSchedules/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var examSchedule = await _context.ExamSchedules.FindAsync(id);
            if (examSchedule != null)
            {
                _context.ExamSchedules.Remove(examSchedule);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExamScheduleExists(int id)
        {
            return _context.ExamSchedules.Any(e => e.Id == id);
        }
    }
}
