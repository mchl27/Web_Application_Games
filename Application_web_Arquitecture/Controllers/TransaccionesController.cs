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
    public class TransaccionesController : Controller
    {
        private readonly DbWebApplicationContext _context;

        public TransaccionesController(DbWebApplicationContext context)
        {
            _context = context;
        }

        // GET: Transacciones
        public async Task<IActionResult> Index()
        {
            var dbWebApplicationContext = _context.Transacciones.Include(t => t.IdUsuarioNavigation).Include(t => t.IdVideojuegoNavigation);
            return View(await dbWebApplicationContext.ToListAsync());
        }

        // GET: Transacciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccione = await _context.Transacciones
                .Include(t => t.IdUsuarioNavigation)
                .Include(t => t.IdVideojuegoNavigation)
                .FirstOrDefaultAsync(m => m.IdTransaccion == id);
            if (transaccione == null)
            {
                return NotFound();
            }

            return View(transaccione);
        }

        // GET: Transacciones/Create
        public IActionResult Create()
        {
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario");
            ViewData["IdVideojuego"] = new SelectList(_context.Videojuegos, "IdJuego", "IdJuego");
            return View();
        }

        // POST: Transacciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdTransaccion,IdUsuario,IdVideojuego,TipoTransaccion,Precio,Fecha")] Transaccione transaccione)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaccione);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", transaccione.IdUsuario);
            ViewData["IdVideojuego"] = new SelectList(_context.Videojuegos, "IdJuego", "IdJuego", transaccione.IdVideojuego);
            return View(transaccione);
        }

        // GET: Transacciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccione = await _context.Transacciones.FindAsync(id);
            if (transaccione == null)
            {
                return NotFound();
            }
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", transaccione.IdUsuario);
            ViewData["IdVideojuego"] = new SelectList(_context.Videojuegos, "IdJuego", "IdJuego", transaccione.IdVideojuego);
            return View(transaccione);
        }

        // POST: Transacciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdTransaccion,IdUsuario,IdVideojuego,TipoTransaccion,Precio,Fecha")] Transaccione transaccione)
        {
            if (id != transaccione.IdTransaccion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaccione);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransaccioneExists(transaccione.IdTransaccion))
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
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "IdUsuario", transaccione.IdUsuario);
            ViewData["IdVideojuego"] = new SelectList(_context.Videojuegos, "IdJuego", "IdJuego", transaccione.IdVideojuego);
            return View(transaccione);
        }

        // GET: Transacciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaccione = await _context.Transacciones
                .Include(t => t.IdUsuarioNavigation)
                .Include(t => t.IdVideojuegoNavigation)
                .FirstOrDefaultAsync(m => m.IdTransaccion == id);
            if (transaccione == null)
            {
                return NotFound();
            }

            return View(transaccione);
        }

        // POST: Transacciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaccione = await _context.Transacciones.FindAsync(id);
            if (transaccione != null)
            {
                _context.Transacciones.Remove(transaccione);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransaccioneExists(int id)
        {
            return _context.Transacciones.Any(e => e.IdTransaccion == id);
        }
    }
}
