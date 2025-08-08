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
    public class TerapeutaViewModelsController : Controller
    {
        private readonly PraticaFinalContext _context;

        public TerapeutaViewModelsController(PraticaFinalContext context)
        {
            _context = context;
        }

        // GET: TerapeutaViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Terapeutas.ToListAsync());
        }

        // GET: TerapeutaViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terapeutaViewModel = await _context.Terapeutas
                .FirstOrDefaultAsync(m => m.TerapeutaID == id);
            if (terapeutaViewModel == null)
            {
                return NotFound();
            }

            return View(terapeutaViewModel);
        }

        // GET: TerapeutaViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TerapeutaViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TerapeutaID,Nombre,Especialidad")] TerapeutaViewModel terapeutaViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(terapeutaViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(terapeutaViewModel);
        }

        // GET: TerapeutaViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terapeutaViewModel = await _context.Terapeutas.FindAsync(id);
            if (terapeutaViewModel == null)
            {
                return NotFound();
            }
            return View(terapeutaViewModel);
        }

        // POST: TerapeutaViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TerapeutaID,Nombre,Especialidad")] TerapeutaViewModel terapeutaViewModel)
        {
            if (id != terapeutaViewModel.TerapeutaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(terapeutaViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TerapeutaViewModelExists(terapeutaViewModel.TerapeutaID))
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
            return View(terapeutaViewModel);
        }

        // GET: TerapeutaViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var terapeutaViewModel = await _context.Terapeutas
                .FirstOrDefaultAsync(m => m.TerapeutaID == id);
            if (terapeutaViewModel == null)
            {
                return NotFound();
            }

            return View(terapeutaViewModel);
        }

        // POST: TerapeutaViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var terapeutaViewModel = await _context.Terapeutas.FindAsync(id);
            if (terapeutaViewModel != null)
            {
                _context.Terapeutas.Remove(terapeutaViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TerapeutaViewModelExists(int id)
        {
            return _context.Terapeutas.Any(e => e.TerapeutaID == id);
        }
        public IActionResult ExportToCSV()
        {
            var terapeutas = _context.Terapeutas.ToList();

            var sb = new StringBuilder();

            sb.AppendLine("TerapeutaID,Nombre,Especialidad,Teléfono");

            foreach (var terapeuta in terapeutas)
            {
                sb.AppendLine($"{terapeuta.TerapeutaID},{terapeuta.Nombre},{terapeuta.Especialidad}");
            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "Terapeutas.csv");
        }

    }
}
