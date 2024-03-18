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
    public class DormAccountsController : Controller
    {
        private readonly DbeStudentContext _context;

        public DormAccountsController(DbeStudentContext context)
        {
            _context = context;
        }

        // GET: DormAccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.DormAccounts.ToListAsync());
        }

        // GET: DormAccounts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormAccount = await _context.DormAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dormAccount == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "DormAccountTransactions", new { id = dormAccount.Id });
        }

        // GET: DormAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DormAccounts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CurrentBalance,Number")] DormAccount dormAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dormAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(dormAccount);
        }

        // GET: DormAccounts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormAccount = await _context.DormAccounts.FindAsync(id);
            if (dormAccount == null)
            {
                return NotFound();
            }
            return View(dormAccount);
        }

        // POST: DormAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CurrentBalance,Number")] DormAccount dormAccount)
        {
            if (id != dormAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dormAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DormAccountExists(dormAccount.Id))
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
            return View(dormAccount);
        }

        // GET: DormAccounts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormAccount = await _context.DormAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dormAccount == null)
            {
                return NotFound();
            }

            return View(dormAccount);
        }

        // POST: DormAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dormAccount = await _context.DormAccounts.FindAsync(id);
            if (dormAccount != null)
            {
                _context.DormAccounts.Remove(dormAccount);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DormAccountExists(int id)
        {
            return _context.DormAccounts.Any(e => e.Id == id);
        }
    }
}
