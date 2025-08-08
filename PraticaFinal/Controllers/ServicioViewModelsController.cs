using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PraticaFinal.Models;

namespace PraticaFinal.Controllers
{
    public class ServicioViewModelsController : Controller
    {
        private readonly PraticaFinalContext _context;

        public ServicioViewModelsController(PraticaFinalContext context)
        {
            _context = context;
        }

        // GET: ServicioViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Servicios.ToListAsync());
        }

        // GET: ServicioViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicioViewModel = await _context.Servicios
                .FirstOrDefaultAsync(m => m.ServicioID == id);
            if (servicioViewModel == null)
            {
                return NotFound();
            }

            return View(servicioViewModel);
        }

        // GET: ServicioViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ServicioViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServicioID,Nombre,DuracionMin")] ServicioViewModel servicioViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(servicioViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(servicioViewModel);
        }

        // GET: ServicioViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicioViewModel = await _context.Servicios.FindAsync(id);
            if (servicioViewModel == null)
            {
                return NotFound();
            }
            return View(servicioViewModel);
        }

        // POST: ServicioViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServicioID,Nombre,DuracionMin")] ServicioViewModel servicioViewModel)
        {
            if (id != servicioViewModel.ServicioID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(servicioViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServicioViewModelExists(servicioViewModel.ServicioID))
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
            return View(servicioViewModel);
        }

        // GET: ServicioViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var servicioViewModel = await _context.Servicios
                .FirstOrDefaultAsync(m => m.ServicioID == id);
            if (servicioViewModel == null)
            {
                return NotFound();
            }

            return View(servicioViewModel);
        }

        // POST: ServicioViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var servicioViewModel = await _context.Servicios.FindAsync(id);
            if (servicioViewModel != null)
            {
                _context.Servicios.Remove(servicioViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServicioViewModelExists(int id)
        {
            return _context.Servicios.Any(e => e.ServicioID == id);
        }
        public IActionResult ExportToCSV()
        {
            var servicios = _context.Servicios.ToList();

            var sb = new StringBuilder();

            sb.AppendLine("ServicioID,Nombre,Descripción");

            foreach (var servicio in servicios)
            {
                sb.AppendLine($"{servicio.ServicioID},{servicio.Nombre},{servicio.DuracionMin}");
            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "Servicios.csv");
        }

    }
}
