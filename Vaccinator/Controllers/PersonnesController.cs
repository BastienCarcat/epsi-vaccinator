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
    public class PersonnesController : Controller
    {
        private readonly ContexteBDD _context = new ContexteBDD();


        private async Task<List<Personne>> LatePerson()
        {
            var injectionsLate = await _context.Injections.Include(injection => injection.Personne).Where(i => i.DateRappel < DateTime.Now).ToListAsync();
            List<Personne> personnesRetard = new List<Personne>();
            foreach (var item in injectionsLate)
            {
                if (!personnesRetard.Contains(item.Personne))
                {
                    personnesRetard.Add(item.Personne);
                }
            }
            return personnesRetard;
        }

        // GET: Personnes
        public async Task<IActionResult> Index(int searchedVaccin, bool currentYear, bool late)
        {
            ViewData["listeDesVaccins"] = new SelectList(_context.Vaccins, "Id", "Nom");
            var allPerson = await _context.Personnes.ToListAsync();
            if (searchedVaccin > 0)
            {
                if (late)
                {
                    allPerson = await LatePerson();
                }

                var injectionsByVaccine = await _context.Injections.Include(injection => injection.Vaccin).Include(injection => injection.Personne).Where(i => i.Vaccin.Id == searchedVaccin).ToListAsync();

                if (currentYear)
                {
                    var injectionsInCurrentYear = await _context.Injections.Include(injection => injection.Personne).Where(i => i.Date.Year != DateTime.Now.Year).ToListAsync();

                    if (injectionsInCurrentYear.Count != 0)
                    {
                        foreach (var item in injectionsInCurrentYear)
                        {
                            if (injectionsByVaccine.Contains(item))
                            {
                                injectionsByVaccine.Remove(injectionsByVaccine.Single(i => i.Id == item.Id));
                            }
                        }

                    }
                }

                if (injectionsByVaccine.Count != 0)
                {
                    foreach (var item in injectionsByVaccine)
                    {
                        if (allPerson.Contains(item.Personne))
                        {
                            allPerson.Remove(allPerson.Single(p => p.Id == item.Personne.Id));
                        }
                    }

                    return View(allPerson);
                }
            }
            if (currentYear && searchedVaccin == 0)
            {
                if (late)
                {
                    allPerson = await LatePerson();
                }

                var injectionsInCurrentYear = await _context.Injections.Include(injection => injection.Vaccin).Include(injection => injection.Personne).Where(i => i.Date.Year == DateTime.Now.Year).ToListAsync();

                if (injectionsInCurrentYear.Count != 0)
                {
                    foreach (var item in injectionsInCurrentYear)
                    {
                        if (allPerson.Contains(item.Personne))
                        {
                            allPerson.Remove(allPerson.Single(p => p.Id == item.Personne.Id));
                        }
                    }

                    
                    return View(allPerson);
                }
            }
            if (late && searchedVaccin == 0)
            {
                var latePerson = await LatePerson();
                return View(latePerson);
            }

            return View(await _context.Personnes.ToListAsync());
        }

        // GET: Personnes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personne = await _context.Personnes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personne == null)
            {
                return NotFound();
            }

            return View(personne);
        }

        // GET: Personnes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Personnes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prenom,Sexe,DateNaissance,Statut")] Personne personne)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personne);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personne);
        }

        // GET: Personnes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personne = await _context.Personnes.FindAsync(id);
            if (personne == null)
            {
                return NotFound();
            }
            return View(personne);
        }

        // POST: Personnes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nom,Prenom,Sexe,DateNaissance,Statut")] Personne personne)
        {
            if (id != personne.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personne);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonneExists(personne.Id))
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
            return View(personne);
        }

        // GET: Personnes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personne = await _context.Personnes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (personne == null)
            {
                return NotFound();
            }

            return View(personne);
        }

        // POST: Personnes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personne = await _context.Personnes.FindAsync(id);
            _context.Personnes.Remove(personne);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonneExists(int id)
        {
            return _context.Personnes.Any(e => e.Id == id);
        }
    }
}
