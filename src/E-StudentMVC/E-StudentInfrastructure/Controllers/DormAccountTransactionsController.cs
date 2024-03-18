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
    public class DormAccountTransactionsController : Controller
    {
        private readonly DbeStudentContext _context;

        public DormAccountTransactionsController(DbeStudentContext context)
        {
            _context = context;
        }

        // GET: DormAccountTransactions
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null) return RedirectToAction("DormAccountTransactions", "Index");

            ViewBag.AccountNumber = _context.DormAccounts.FirstOrDefaultAsync(d => d.Id == id).Result.Number;
            var dbeStudentContext = _context.DormAccountTransactions.Where(d => d.AccountId == id).Include(d => d.Account);
            return View(await dbeStudentContext.ToListAsync());
        }

        // GET: DormAccountTransactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormAccountTransaction = await _context.DormAccountTransactions
                .Include(d => d.Account)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dormAccountTransaction == null)
            {
                return NotFound();
            }

            return View(dormAccountTransaction);
        }

        // GET: DormAccountTransactions/Create
        public IActionResult Create()
        {
            ViewData["AccountId"] = new SelectList(_context.DormAccounts, "Id", "Id");
            return View();
        }

        // POST: DormAccountTransactions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccountId,Amount,Balance,SecondParty,Date")] DormAccountTransaction dormAccountTransaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dormAccountTransaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AccountId"] = new SelectList(_context.DormAccounts, "Id", "Id", dormAccountTransaction.AccountId);
            return View(dormAccountTransaction);
        }

        // GET: DormAccountTransactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormAccountTransaction = await _context.DormAccountTransactions.FindAsync(id);
            if (dormAccountTransaction == null)
            {
                return NotFound();
            }
            ViewData["AccountId"] = new SelectList(_context.DormAccounts, "Id", "Id", dormAccountTransaction.AccountId);
            return View(dormAccountTransaction);
        }

        // POST: DormAccountTransactions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AccountId,Amount,Account,Balance,SecondParty,Date")] DormAccountTransaction dormAccountTransaction)
        {
            if (id != dormAccountTransaction.Id)
            {
                return NotFound();
            }

            dormAccountTransaction.Account = _context.DormAccounts.FirstOrDefault(a => a.Id == dormAccountTransaction.AccountId);

            if (ModelState.IsValid || ModelState["Account"].AttemptedValue == null)
            {
                try
                {
                    _context.Update(dormAccountTransaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DormAccountTransactionExists(dormAccountTransaction.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "DormAccountTransactions", new { id = dormAccountTransaction.AccountId});
            }
            ViewData["AccountId"] = new SelectList(_context.DormAccounts, "Id", "Id", dormAccountTransaction.AccountId);
            return View(dormAccountTransaction);
        }

        // GET: DormAccountTransactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dormAccountTransaction = await _context.DormAccountTransactions
                .Include(d => d.Account)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dormAccountTransaction == null)
            {
                return NotFound();
            }

            return View(dormAccountTransaction);
        }

        // POST: DormAccountTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dormAccountTransaction = await _context.DormAccountTransactions.FindAsync(id);
            if (dormAccountTransaction != null)
            {
                _context.DormAccountTransactions.Remove(dormAccountTransaction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DormAccountTransactionExists(int id)
        {
            return _context.DormAccountTransactions.Any(e => e.Id == id);
        }
    }
}
