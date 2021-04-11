using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vaccinator.Models;

namespace Vaccinator.Controllers
{
    public class InjectionsController : Controller
    {
        private readonly ContexteBDD _context = new ContexteBDD();

        // GET: Injections
        public async Task<IActionResult> Index(int searchedPerson)
        {
           ViewData["listeDesPersonnes"] = new SelectList(_context.Personnes, "Id", "Nom");

           if (searchedPerson > 0)
            {
                return View(await _context.Injections.Include(injection => injection.Personne).Include(injection => injection.Vaccin).Where(i => i.Personne.Id == searchedPerson).ToListAsync());
            }

            return View(await _context.Injections.Include(injection => injection.Personne).Include(injection => injection.Vaccin).ToListAsync());
        }

        // GET: Injections/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var injection = await _context.Injections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (injection == null)
            {
                return NotFound();
            }

            return View(injection);
        }

        // GET: Injections/Create
        public IActionResult Create()
        {
            ViewData["listeDesPersonnes"] = new SelectList(_context.Personnes, "Id", "Nom");
            ViewData["listeDesVaccins"] = new SelectList(_context.Vaccins, "Id", "Nom");

            return View();
        }

        // POST: Injections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Marque,Lot,Date,DateRappel")] Injection injection, int Personne, int Vaccin)
        {

            var personne = await _context.Personnes.FindAsync(Personne);

            injection.Personne = personne;

            var vaccin = await _context.Vaccins.FindAsync(Vaccin);

            injection.Vaccin = vaccin;

            ModelState.Clear();
            TryValidateModel(injection);

            if (ModelState.IsValid)
            {
                _context.Add(injection);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["listeDesPersonnes"] = new SelectList(_context.Personnes, "Id", "Nom");
            ViewData["listeDesVaccins"] = new SelectList(_context.Vaccins, "Id", "Nom");

            return View(injection);
        }

        // GET: Injections/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewData["listeDesPersonnes"] = new SelectList(_context.Personnes, "Id", "Nom");
            ViewData["listeDesVaccins"] = new SelectList(_context.Vaccins, "Id", "Nom");

            if (id == null)
            {
                return NotFound();
            }

            var injection = await _context.Injections.FindAsync(id);
            if (injection == null)
            {
                return NotFound();
            }
            return View(injection);
        }

        // POST: Injections/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Marque,Lot,Date,DateRappel")] Injection injection, int Personne, int Vaccin)
        {
            if (id != injection.Id)
            {
                return NotFound();
            }

            var personne = await _context.Personnes.FindAsync(Personne);

            injection.Personne = personne;

            var vaccin = await _context.Vaccins.FindAsync(Vaccin);

            injection.Vaccin = vaccin;

            ModelState.Clear();
            TryValidateModel(injection);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(injection);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InjectionExists(injection.Id))
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
            return View(injection);
        }

        // GET: Injections/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var injection = await _context.Injections
                .FirstOrDefaultAsync(m => m.Id == id);
            if (injection == null)
            {
                return NotFound();
            }

            return View(injection);
        }

        // POST: Injections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var injection = await _context.Injections.FindAsync(id);
            _context.Injections.Remove(injection);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InjectionExists(int id)
        {
            return _context.Injections.Any(e => e.Id == id);
        }
    }
}
