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
    public class StudentsController : Controller
    {
        private readonly DbeStudentContext _context;

        public StudentsController(DbeStudentContext context)
        {
            _context = context;
        }

        // GET: Students

        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                ViewBag.GroupName = _context.Groups.FirstOrDefaultAsync(g => g.Id == id).Result.Name;
                var dbeStudentContext = _context.Students.Where(d => d.GroupId == id);
                return View(await dbeStudentContext.ToListAsync());
            }
            else
            {
                var dbeStudentContext = _context.Students.Include(d => d.Group);
                return View(await dbeStudentContext.ToListAsync());
            }
        }

        public async Task<IActionResult> BackToGroups()
        {
            return RedirectToAction("Index", "Groups");
        }

        public async Task<IActionResult> OpenUser(int? id)
        {
            return RedirectToAction("Details", "Users", new {_context.Users.FirstOrDefaultAsync(u => u.StudentId == id).Result.Id});
        }

        public async Task<IActionResult> OpenGroup(int? id)
        {
            return RedirectToAction("Details", "Groups", new { id });
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Name");
            return View();
        }

        // POST: Students/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Faculty,Course,GroupId,StudentNumber")] Student student)
        {
            student.Group = _context.Groups.FirstOrDefault(u => u.Id == student.GroupId);
            if (ModelState.IsValid || ModelState["Group"].AttemptedValue == null)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", student.GroupId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewBag.Id = id;
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", student.GroupId);
            return View(student);
        }

        // POST: Students/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Faculty,Course,GroupId,StudentNumber")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            student.Group= _context.Groups.FirstOrDefault(u => u.Id == student.GroupId);

            if (ModelState.IsValid || ModelState["Group"].AttemptedValue == null)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
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
            ViewData["GroupId"] = new SelectList(_context.Groups, "Id", "Id", student.GroupId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }
    }
}
