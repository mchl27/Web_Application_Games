using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Application_web_Arquitecture.Models;

namespace Application_web_Arquitecture.Controllers
{
    public class VideojuegosController : Controller
    {
        private readonly DbWebApplicationContext _context;

        public VideojuegosController(DbWebApplicationContext context)
        {
            _context = context;
        }

        // GET: Videojuegos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Videojuegos.ToListAsync());
        }

        // GET: Videojuegos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videojuego = await _context.Videojuegos
                .FirstOrDefaultAsync(m => m.IdJuego == id);
            if (videojuego == null)
            {
                return NotFound();
            }

            return View(videojuego);
        }

        // GET: Videojuegos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Videojuegos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdJuego,Titulo,Genero,Precio,Disponibilidad")] Videojuego videojuego)
        {
            if (ModelState.IsValid)
            {
                _context.Add(videojuego);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(videojuego);
        }

        // GET: Videojuegos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videojuego = await _context.Videojuegos.FindAsync(id);
            if (videojuego == null)
            {
                return NotFound();
            }
            return View(videojuego);
        }

        // POST: Videojuegos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdJuego,Titulo,Genero,Precio,Disponibilidad")] Videojuego videojuego)
        {
            if (id != videojuego.IdJuego)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(videojuego);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VideojuegoExists(videojuego.IdJuego))
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
            return View(videojuego);
        }

        // GET: Videojuegos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var videojuego = await _context.Videojuegos
                .FirstOrDefaultAsync(m => m.IdJuego == id);
            if (videojuego == null)
            {
                return NotFound();
            }

            return View(videojuego);
        }

        // POST: Videojuegos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var videojuego = await _context.Videojuegos.FindAsync(id);
            if (videojuego != null)
            {
                _context.Videojuegos.Remove(videojuego);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VideojuegoExists(int id)
        {
            return _context.Videojuegos.Any(e => e.IdJuego == id);
        }
    }
}
