using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PraticaFinal.Models;

namespace PraticaFinal.Controllers
{
    public class CitaViewModelsController : Controller
    {
        private readonly PraticaFinalContext _context;

        public CitaViewModelsController(PraticaFinalContext context)
        {
            _context = context;
        }

        // GET: CitaViewModels
        public async Task<IActionResult> Index()
        {
            var praticaFinalContext = _context.Citas.Include(c => c.Paciente).Include(c => c.Servicio).Include(c => c.Terapeuta);
            return View(await praticaFinalContext.ToListAsync());
        }

        // GET: CitaViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citaViewModel = await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Servicio)
                .Include(c => c.Terapeuta)
                .FirstOrDefaultAsync(m => m.CitaID == id);
            if (citaViewModel == null)
            {
                return NotFound();
            }

            return View(citaViewModel);
        }

        // GET: CitaViewModels/Create
        public IActionResult Create()
        {
            ViewData["PacienteID"] = new SelectList(_context.Pacientes, "PacienteID", "Nombre");
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "Nombre");
            ViewData["TerapeutaID"] = new SelectList(_context.Terapeutas, "TerapeutaID", "Nombre");
            ViewBag.Horas = new SelectList(Enumerable.Range(8, 11));

            return View();
        }

        // POST: CitaViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitaID,PacienteID,ServicioID,TerapeutaID,Fecha,Hora")] CitaViewModel citaViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(citaViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PacienteID"] = new SelectList(_context.Pacientes, "PacienteID", "Nombre", citaViewModel.PacienteID);
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "Nombre", citaViewModel.ServicioID);
            ViewData["TerapeutaID"] = new SelectList(_context.Terapeutas, "TerapeutaID", "Nombre", citaViewModel.TerapeutaID);
           ViewBag.Horas = new SelectList(Enumerable.Range(8, 11));

            return View(citaViewModel);
        }

        // GET: CitaViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citaViewModel = await _context.Citas.FindAsync(id);
            if (citaViewModel == null)
            {
                return NotFound();
            }
            ViewData["PacienteID"] = new SelectList(_context.Pacientes, "PacienteID", "Nombre", citaViewModel.PacienteID);
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "Nombre", citaViewModel.ServicioID);
            ViewData["TerapeutaID"] = new SelectList(_context.Terapeutas, "TerapeutaID", "Nombre", citaViewModel.TerapeutaID);
            return View(citaViewModel);
        }

        // POST: CitaViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CitaID,PacienteID,ServicioID,TerapeutaID,Fecha,Hora")] CitaViewModel citaViewModel)
        {
            if (id != citaViewModel.CitaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(citaViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaViewModelExists(citaViewModel.CitaID))
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
            ViewData["PacienteID"] = new SelectList(_context.Pacientes, "PacienteID", "Email", citaViewModel.PacienteID);
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "Nombre", citaViewModel.ServicioID);
            ViewData["TerapeutaID"] = new SelectList(_context.Terapeutas, "TerapeutaID", "Especialidad", citaViewModel.TerapeutaID);
            return View(citaViewModel);
        }

        // GET: CitaViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var citaViewModel = await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Servicio)
                .Include(c => c.Terapeuta)
                .FirstOrDefaultAsync(m => m.CitaID == id);
            if (citaViewModel == null)
            {
                return NotFound();
            }

            return View(citaViewModel);
        }

        // POST: CitaViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var citaViewModel = await _context.Citas.FindAsync(id);
            if (citaViewModel != null)
            {
                _context.Citas.Remove(citaViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaViewModelExists(int id)
        {
            return _context.Citas.Any(e => e.CitaID == id);
        }
    }
}
