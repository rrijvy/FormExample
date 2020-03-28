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
    public class PrimaryFindingsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PrimaryFindingsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PrimaryFindings
        public async Task<IActionResult> Index()
        {
            return View(await _context.PrimaryFinding.ToListAsync());
        }

        // GET: PrimaryFindings/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var primaryFinding = await _context.PrimaryFinding
                .FirstOrDefaultAsync(m => m.Id == id);
            if (primaryFinding == null)
            {
                return NotFound();
            }

            return View(primaryFinding);
        }

        // GET: PrimaryFindings/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PrimaryFindings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PrimaryFinding primaryFinding)
        {
            if (ModelState.IsValid)
            {
                _context.Add(primaryFinding);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(primaryFinding);
        }

        // GET: PrimaryFindings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var primaryFinding = await _context.PrimaryFinding.FindAsync(id);
            if (primaryFinding == null)
            {
                return NotFound();
            }
            return View(primaryFinding);
        }

        // POST: PrimaryFindings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PrimaryFinding primaryFinding)
        {
            if (id != primaryFinding.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(primaryFinding);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrimaryFindingExists(primaryFinding.Id))
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
            return View(primaryFinding);
        }

        // GET: PrimaryFindings/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var primaryFinding = await _context.PrimaryFinding
                .FirstOrDefaultAsync(m => m.Id == id);
            if (primaryFinding == null)
            {
                return NotFound();
            }

            return View(primaryFinding);
        }

        // POST: PrimaryFindings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var primaryFinding = await _context.PrimaryFinding.FindAsync(id);
            _context.PrimaryFinding.Remove(primaryFinding);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrimaryFindingExists(int id)
        {
            return _context.PrimaryFinding.Any(e => e.Id == id);
        }


        public JsonResult GetAll()
        {
            var obejct = new List<Object>();

            var list = _context.SecondaryFinding
                .GroupBy(x => x.PrimaryFindingId)
                .Select(x => x.Key)
                .ToList();

            foreach (var item in list)
            {
                var secondaryList = _context.SecondaryFinding.Where(x => x.PrimaryFindingId == item).ToList();
                obejct.Add(new { Id = item, SecondaryFindings = secondaryList });
            }


            return Json(obejct);
        }
    }
}
