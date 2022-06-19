using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Jalgratta_Eksam.Data;
using Jalgratta_Eksam.Models;

namespace Jalgratta_Eksam.Controllers
{
    public class EksamidController : Controller
    {
        private readonly EksamidDBContext _context;

        public EksamidController(EksamidDBContext context)
        {
            _context = context;
        }

        // GET: Eksams/Registreeri
        public IActionResult Registreeri()
        {
            return View();
        }

        // POST: Eksams/Registreeri
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registreeri([Bind("ID,Eesnimi,Perekonnanimi")] Eksam eksam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eksam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eksam);
        }


        // GET: Eksams/Teooria
        public async Task<IActionResult> Teooria()
        {
            var model = _context.Eksamid.Where(e => e.Teooria == -1);

            return View(await model.ToListAsync());
        }


        // GET: Eksams/slaalom
        public async Task<IActionResult> Slaalom()
        {
            var model = _context.Eksamid.Where(e => e.Teooria >= 9 && e.Slaalom == -1);

            return View(await model.ToListAsync());
        }



        // GET: Eksams/ring
        public async Task<IActionResult> Ring()
        {
            var model = _context.Eksamid.Where(e => e.Teooria >= 9 && e.Ring == -1);

            return View(await model.ToListAsync());
        }



        // GET: Eksams/tanav
        public async Task<IActionResult> Tanav()
        {
            var model = _context.Eksamid.Where(e => e.Ring == 1 && e.Slaalom == 1 && e.Tanav == -1);

            return View(await model.ToListAsync());
        }

        // GET: Eksams/PassFail/Id
        public async Task<IActionResult> PassFail(int Id, string Osa, int Tulemus)
        {
            var eksam = await _context.Eksamid.FindAsync(Id);
            if (eksam == null)
            {
                return NotFound();
            }
            switch (Osa)
            {
                case nameof(eksam.Slaalom):
                    {
                        eksam.Slaalom = Tulemus;
                        break;
                    }
                case nameof(eksam.Ring):
                    {
                        eksam.Ring = Tulemus;
                        break;
                    }
                case nameof(eksam.Tanav):
                    {
                        eksam.Tanav = Tulemus;
                        break;
                    }
                default:
                    {
                        return NotFound();
                    }


            }
            try
            {
                _context.Update(eksam);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EksamExists(eksam.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }

            return RedirectToAction(Osa);
        }

        // GET: Eksams/Luba
        public async Task<IActionResult> Luba()
        {
            var model = _context.Eksamid.Select(e => new LubaViewModel()
            {
                ID = e.ID,
                Eesnimi = e.Eesnimi,
                Perekonnanimi = e.Perekonnanimi,
                Teooria = e.Teooria == -1 ? "Tegemata" : e.Teooria + "/10",
                Slaalom = e.Slaalom == -1 ? "." : e.Slaalom == 1 ? "Õnnestus" : "Põrus",
                Ring = e.Ring == -1 ? "." : e.Ring == 1 ? "Õnnestus" : "Põrus",
                Tanav = e.Tanav == -1 ? "." : e.Tanav == 1 ? "Õnnestus" : "Põrus",
                Luba = e.Luba == 1 ? "Väljastatud" : e.Tanav == 1 ? "Väljasta" : "."
            });

            return View(await model.ToListAsync());
        }




        // GET: Eksams/tänav
        public async Task<IActionResult> VäljastaLuba(int Id)
        {
            var eksam = await _context.Eksamid.FindAsync(Id);
            if (eksam == null)
            {
                return NotFound();
            }
            eksam.Luba = 1;
            try
            {
                _context.Update(eksam);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EksamExists(eksam.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }

            return RedirectToAction("Luba");
        }



        // POST: Eksams/TeooriaTulemus
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TeooriaTulemus([Bind("ID,Teooria")] Eksam tulemus)
        {

            var eksam = await _context.Eksamid.FindAsync(tulemus.ID);
            if (eksam == null)
            {
                return NotFound();
            }
            eksam.Teooria = tulemus.Teooria;

            try
            {
                _context.Update(eksam);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EksamExists(eksam.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

            }
            return RedirectToAction(nameof(Teooria));

        }





        // GET: Eksams
        public async Task<IActionResult> Index()
        {
            var model = _context.Eksamid.Select(e => new LubaViewModel()
            {
                ID = e.ID,
                Eesnimi = e.Eesnimi,
                Perekonnanimi = e.Perekonnanimi,
                Teooria = e.Teooria == -1 ? "Tegemata" : e.Teooria + "/10",
                Slaalom = e.Slaalom == -1 ? "Tegemata" : e.Slaalom == 1 ? "Õnnestus" : "Põrus",
                Ring = e.Ring == -1 ? "Tegemata" : e.Ring == 1 ? "Õnnestus" : "Põrus",
                Tanav = e.Tanav == -1 ? "Tegemata" : e.Tanav == 1 ? "Õnnestus" : "Põrus",
                Luba = e.Luba == 1 ? "Väljastatud" : "Väljastamata"
            });            
            
            return View(await model.ToListAsync());
        }

        // GET: Eksams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eksam = await _context.Eksamid
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eksam == null)
            {
                return NotFound();
            }

            return View(eksam);
        }

        // GET: Eksams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Eksams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Eesnimi,Perekonnanimi,Teooria,Slaalom,Ring,Tänav,Luba")] Eksam eksam)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eksam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eksam);
        }

        // GET: Eksams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eksam = await _context.Eksamid.FindAsync(id);
            if (eksam == null)
            {
                return NotFound();
            }
            return View(eksam);
        }

        // POST: Eksams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Eesnimi,Perekonnanimi,Teooria,Slaalom,Ring,Tänav,Luba")] Eksam eksam)
        {
            if (id != eksam.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eksam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EksamExists(eksam.ID))
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
            return View(eksam);
        }

        // GET: Eksams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eksam = await _context.Eksamid
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eksam == null)
            {
                return NotFound();
            }

            return View(eksam);
        }

        // POST: Eksams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eksam = await _context.Eksamid.FindAsync(id);
            _ = _context.Eksamid.Remove(eksam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EksamExists(int id)
        {
            return _context.Eksamid.Any(e => e.ID == id);
        }
    }
}
