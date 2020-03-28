using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FormExample.Data;
using FormExample.Models;

namespace FormExample.Controllers
{
    public class SecondaryFindingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SecondaryFindingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SecondaryFindings
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.SecondaryFinding.Include(s => s.PrimaryFinding);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: SecondaryFindings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secondaryFinding = await _context.SecondaryFinding
                .Include(s => s.PrimaryFinding)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (secondaryFinding == null)
            {
                return NotFound();
            }

            return View(secondaryFinding);
        }

        // GET: SecondaryFindings/Create
        public IActionResult Create()
        {
            ViewData["PrimaryFindingId"] = new SelectList(_context.Set<PrimaryFinding>(), "Id", "Id");
            return View();
        }

        // POST: SecondaryFindings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,PrimaryFindingId")] SecondaryFinding secondaryFinding)
        {
            if (ModelState.IsValid)
            {
                _context.Add(secondaryFinding);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PrimaryFindingId"] = new SelectList(_context.Set<PrimaryFinding>(), "Id", "Id", secondaryFinding.PrimaryFindingId);
            return View(secondaryFinding);
        }

        // GET: SecondaryFindings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secondaryFinding = await _context.SecondaryFinding.FindAsync(id);
            if (secondaryFinding == null)
            {
                return NotFound();
            }
            ViewData["PrimaryFindingId"] = new SelectList(_context.Set<PrimaryFinding>(), "Id", "Id", secondaryFinding.PrimaryFindingId);
            return View(secondaryFinding);
        }

        // POST: SecondaryFindings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PrimaryFindingId")] SecondaryFinding secondaryFinding)
        {
            if (id != secondaryFinding.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(secondaryFinding);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SecondaryFindingExists(secondaryFinding.Id))
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
            ViewData["PrimaryFindingId"] = new SelectList(_context.Set<PrimaryFinding>(), "Id", "Id", secondaryFinding.PrimaryFindingId);
            return View(secondaryFinding);
        }

        // GET: SecondaryFindings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var secondaryFinding = await _context.SecondaryFinding
                .Include(s => s.PrimaryFinding)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (secondaryFinding == null)
            {
                return NotFound();
            }

            return View(secondaryFinding);
        }

        // POST: SecondaryFindings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var secondaryFinding = await _context.SecondaryFinding.FindAsync(id);
            _context.SecondaryFinding.Remove(secondaryFinding);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SecondaryFindingExists(int id)
        {
            return _context.SecondaryFinding.Any(e => e.Id == id);
        }
    }
}
