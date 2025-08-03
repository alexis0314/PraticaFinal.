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
    public class CitasController : Controller
    {
        private readonly PraticaFinalContext _context;

        public CitasController(PraticaFinalContext context)
        {
            _context = context;
        }

        // GET: Citas
        public async Task<IActionResult> Index()
        {
            var praticaFinalContext = _context.Citas.Include(c => c.Paciente).Include(c => c.Servicio).Include(c => c.Terapeuta);
            return View(await praticaFinalContext.ToListAsync());
        }

        // GET: Citas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Servicio)
                .Include(c => c.Terapeuta)
                .FirstOrDefaultAsync(m => m.CitaID == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // GET: Citas/Create
        public IActionResult Create()
        {
            ViewData["PacienteID"] = new SelectList(_context.Pacientes, "PacienteID", "Nombre");
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "Nombre");
            ViewData["TerapeutaID"] = new SelectList(_context.Terapeutas, "TerapeutaID", "Especialidad");
            return View();
        }

        // POST: Citas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CitaID,PacienteID,ServicioID,TerapeutaID,Fecha,Hora")] Cita cita)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cita);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PacienteID"] = new SelectList(_context.Pacientes, "PacienteID", "Nombre", cita.PacienteID);
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "Nombre", cita.ServicioID);
            ViewData["TerapeutaID"] = new SelectList(_context.Terapeutas, "TerapeutaID", "Especialidad", cita.TerapeutaID);
            return View(cita);
        }

        // GET: Citas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return NotFound();
            }
            ViewData["PacienteID"] = new SelectList(_context.Pacientes, "PacienteID", "Nombre", cita.PacienteID);
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "Nombre", cita.ServicioID);
            ViewData["TerapeutaID"] = new SelectList(_context.Terapeutas, "TerapeutaID", "Especialidad", cita.TerapeutaID);
            return View(cita);
        }

        // POST: Citas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CitaID,PacienteID,ServicioID,TerapeutaID,Fecha,Hora")] Cita cita)
        {
            if (id != cita.CitaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cita);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CitaExists(cita.CitaID))
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
            ViewData["PacienteID"] = new SelectList(_context.Pacientes, "PacienteID", "Nombre", cita.PacienteID);
            ViewData["ServicioID"] = new SelectList(_context.Servicios, "ServicioID", "Nombre", cita.ServicioID);
            ViewData["TerapeutaID"] = new SelectList(_context.Terapeutas, "TerapeutaID", "Especialidad", cita.TerapeutaID);
            return View(cita);
        }

        // GET: Citas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cita = await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Servicio)
                .Include(c => c.Terapeuta)
                .FirstOrDefaultAsync(m => m.CitaID == id);
            if (cita == null)
            {
                return NotFound();
            }

            return View(cita);
        }

        // POST: Citas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita != null)
            {
                _context.Citas.Remove(cita);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CitaExists(int id)
        {
            return _context.Citas.Any(e => e.CitaID == id);
        }
    }
}
