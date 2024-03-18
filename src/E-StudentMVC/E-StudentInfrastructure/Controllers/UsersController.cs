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
    public class UsersController : Controller
    {
        private readonly DbeStudentContext _context;

        public UsersController(DbeStudentContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var dbeStudentContext = _context.Users.Include(u => u.DormResident).Include(u => u.Student);
            return View(await dbeStudentContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.DormResident)
                .Include(u => u.DormResident.Pass)
                .Include(u => u.DormResident.Account)
                .Include(u => u.Student)
                .Include(u => u.Student.Group)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            ViewData["DormResidentId"] = new SelectList(_context.DormResidents, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,Name,Password,IsDormResident,CreatedAt,DormResidentId")] User user)
        {
            user.Student = _context.Students.FirstOrDefault(u => u.Id == user.StudentId);
            user.DormResident = _context.DormResidents.FirstOrDefault(u => u.Id == user.DormResidentId);

            if (ModelState.IsValid || ModelState["Student"].AttemptedValue == null)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DormResidentId"] = new SelectList(_context.DormResidents, "Id", "Id", user.DormResidentId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", user.StudentId);
            return View(user);
        }

        public async Task<IActionResult> OpenGroup(int? id)
        {
            return RedirectToAction("Details", "Groups", new {id});
        }

        public async Task<IActionResult> OpenStudent(int? id)
        {
            return RedirectToAction("Details", "Students", new { id });
        }

        public async Task<IActionResult> OpenResident(int? id)
        {
            return RedirectToAction("Details", "DormResidents", new { id });
        }

        public async Task<IActionResult> OpenDormAccount(int? id)
        {
            return RedirectToAction("Details", "DormAccounts", new { id });
        }

        public async Task<IActionResult> OpenDormPass(int? id)
        {
            return RedirectToAction("Details", "DormPasses", new { id });
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            ViewBag.Id = id;
            ViewBag.IsDormResident = user.IsDormResident;
            ViewData["DormResidentId"] = (new SelectList(_context.DormResidents, "Id", "Id", user.DormResidentId));
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", user.StudentId);
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,Name,Password,IsDormResident,CreatedAt,DormResidentId")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            user.Student = _context.Students.FirstOrDefault(u => u.Id == user.StudentId);
            user.DormResident = _context.DormResidents.FirstOrDefault(u => u.Id == user.DormResidentId);

            if (ModelState.IsValid || ModelState["Student"].AttemptedValue == null)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Users");
            }
            ViewData["id"] = id; 
            ViewData["DormResidentId"] = new SelectList(_context.DormResidents, "Id", "Id", user.DormResidentId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", user.StudentId);
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.DormResident)
                .Include(u => u.DormResident.Pass)
                .Include(u => u.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
