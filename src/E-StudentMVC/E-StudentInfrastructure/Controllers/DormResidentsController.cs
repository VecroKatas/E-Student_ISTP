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
    public class DormResidentsController : Controller
    {
        private readonly DbeStudentContext _context;

        public DormResidentsController(DbeStudentContext context)
        {
            _context = context;
        }

        // GET: DormResidents
        public async Task<IActionResult> Index(int? id)
        {
            if (id != null)
            {
                int passId = _context.DormResidents.FirstOrDefaultAsync(r => r.Id == id).Result.PassId;
                ViewBag.DormNumber = _context.DormPasses.FirstOrDefaultAsync(g => g.Id == passId).Result.Dorm.Number;
                var tmpContext = _context.DormPasses.Where(d => d.DormId == id).ToList();
                var list = new List<int>();
                foreach (var pass in tmpContext)
                {
                    list.Add(pass.Id);
                }
                var dbeStudentContext = _context.DormResidents.Where(d => list.Contains(d.PassId)).Include(d => d.Pass).Include(d => d.Pass.Dorm);
                return View(await dbeStudentContext.ToListAsync());
            }
            else
            {
                var dbeStudentContext = _context.DormResidents.Include(d => d.Account).Include(d => d.Pass).Include(d => d.Pass.Dorm);
                return View(await dbeStudentContext.ToListAsync());
            }
        }

        // GET: DormResidents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormResident = await _context.DormResidents
                .Include(d => d.Account)
                .Include(d => d.Pass)
                .Include(d => d.Pass.Dorm)
                .Include(d => d.Pass.Room)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dormResident == null)
            {
                return NotFound();
            }

            return View(dormResident);
        }

        // GET: DormResidents/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.DormAccounts, "Id", "Id");
            ViewData["PassId"] = new SelectList(_context.DormPasses, "Id", "Id");
            return View();
        }

        // POST: DormResidents/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PassId,AccountId")] DormResident dormResident)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dormResident);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.DormAccounts, "Id", "Id", dormResident.AccountId);
            ViewData["PassId"] = new SelectList(_context.DormPasses, "Id", "Id", dormResident.PassId);
            return View(dormResident);
        }

        public async Task<IActionResult> OpenAccount(int? id)
        {
            return RedirectToAction("Details", "DormAccounts", new { id });
        }

        public async Task<IActionResult> OpenPass(int? id)
        {
            return RedirectToAction("Details", "DormPasses", new { id });
        }

        public async Task<IActionResult> OpenDorm(int? id)
        {
            return RedirectToAction("Details", "Dorms", new { id });
        }

        public async Task<IActionResult> OpenTransactions(int? id)
        {
            var account = await _context.DormAccounts.FirstOrDefaultAsync(m => m.Id == id);
            return RedirectToAction("Index", "DormAccountTransactions", new { id = account.Id });;
        }

        public async Task<IActionResult> OpenUser(int? id)
        {
            return RedirectToAction("Details", "Users", new { _context.Users.FirstOrDefaultAsync(u => u.DormResidentId == id).Result.Id });
        }

        // GET: DormResidents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormResident = await _context.DormResidents.FindAsync(id);
            if (dormResident == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.DormAccounts, "Id", "Id", dormResident.AccountId);
            ViewData["PassId"] = new SelectList(_context.DormPasses, "Id", "Id", dormResident.PassId);
            return View(dormResident);
        }

        // POST: DormResidents/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PassId,AccountId")] DormResident dormResident)
        {
            if (id != dormResident.Id)
            {
                return NotFound();
            }

            dormResident.Pass = _context.DormPasses.FirstOrDefault(d => d.Id == dormResident.PassId);
            dormResident.Account = _context.DormAccounts.FirstOrDefault(d => d.Id == dormResident.AccountId);

            if (ModelState.IsValid || ModelState["Pass"].AttemptedValue == null)
            {
                try
                {
                    _context.Update(dormResident);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DormResidentExists(dormResident.Id))
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
            ViewData["AccountId"] = new SelectList(_context.DormAccounts, "Id", "Id", dormResident.AccountId);
            ViewData["PassId"] = new SelectList(_context.DormPasses, "Id", "Id", dormResident.PassId);
            return View(dormResident);
        }

        // GET: DormResidents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormResident = await _context.DormResidents
                .Include(d => d.Account)
                .Include(d => d.Pass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dormResident == null)
            {
                return NotFound();
            }

            return View(dormResident);
        }

        // POST: DormResidents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dormResident = await _context.DormResidents.FindAsync(id);
            if (dormResident != null)
            {
                _context.DormResidents.Remove(dormResident);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DormResidentExists(int id)
        {
            return _context.DormResidents.Any(e => e.Id == id);
        }
    }
}
