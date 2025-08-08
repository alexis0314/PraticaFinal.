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
    public class PacienteViewModelsController : Controller
    {
        private readonly PraticaFinalContext _context;

        public PacienteViewModelsController(PraticaFinalContext context)
        {
            _context = context;
        }

        // GET: PacienteViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.Pacientes.ToListAsync());
        }

        // GET: PacienteViewModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacienteViewModel = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.PacienteID == id);
            if (pacienteViewModel == null)
            {
                return NotFound();
            }

            return View(pacienteViewModel);
        }

        // GET: PacienteViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PacienteViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PacienteID,Nombre,Telefono,Email")] PacienteViewModel pacienteViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pacienteViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pacienteViewModel);
        }

        // GET: PacienteViewModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacienteViewModel = await _context.Pacientes.FindAsync(id);
            if (pacienteViewModel == null)
            {
                return NotFound();
            }
            return View(pacienteViewModel);
        }

        // POST: PacienteViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PacienteID,Nombre,Telefono,Email")] PacienteViewModel pacienteViewModel)
        {
            if (id != pacienteViewModel.PacienteID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pacienteViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PacienteViewModelExists(pacienteViewModel.PacienteID))
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
            return View(pacienteViewModel);
        }

        // GET: PacienteViewModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pacienteViewModel = await _context.Pacientes
                .FirstOrDefaultAsync(m => m.PacienteID == id);
            if (pacienteViewModel == null)
            {
                return NotFound();
            }

            return View(pacienteViewModel);
        }

        // POST: PacienteViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pacienteViewModel = await _context.Pacientes.FindAsync(id);
            if (pacienteViewModel != null)
            {
                _context.Pacientes.Remove(pacienteViewModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PacienteViewModelExists(int id)
        {
            return _context.Pacientes.Any(e => e.PacienteID == id);
        }
        public IActionResult ExportToCSV()
        {
            var pacientes = _context.Pacientes.ToList();

            var sb = new StringBuilder();

            sb.AppendLine("ID,Nombre,Apellido,Teléfono,Email");

            foreach (var paciente in pacientes)
            {
                sb.AppendLine($"{paciente.PacienteID},{paciente.Nombre},{paciente.Telefono},{paciente.Email}");
            }

            return File(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv", "Pacientes.csv");
        }
    }
}
