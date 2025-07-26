using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoWeb.Datos;
using TodoWeb.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TodoWeb.Controllers

{
    [Authorize]
    public class TareasController : Controller
    {
        private readonly TodoContext _context;

        public TareasController(TodoContext context)
        {
            _context = context;
        }

        // GET: Tareas
public async Task<IActionResult> Index()
{
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    if (userIdClaim == null) return Unauthorized();

    int userId = int.Parse(userIdClaim);
    var tareas = await _context.Tareas
                          .Include(t => t.Usuario)
                          .Where(t => t.UsuarioId == userId)
                          .ToListAsync();
    return View(tareas);
}




        // GET: Tareas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // GET: Tareas/Create
        public IActionResult Create()
        {
            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id");

            ViewBag.Prioridades = new SelectList(new[]
            {
        new { Value = 1, Text = "Baja" },
        new { Value = 2, Text = "Media" },
        new { Value = 3, Text = "Alta" }
    }, "Value", "Text");

            // ✅ Asignar la fecha de creación por defecto
            var nuevaTarea = new Tarea
            {
                FechaCreacion = DateTime.Now
            };

            return View(nuevaTarea);
        }
        // POST: Tareas/Create
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create(Tarea tarea)
{
    if (ModelState.IsValid)
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null)
        {
            return Unauthorized();
        }
        tarea.UsuarioId = int.Parse(userIdClaim);
        _context.Tareas.Add(tarea);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    return View(tarea);
}



        // GET: Tareas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea == null)
                return NotFound();

            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id", tarea.UsuarioId);
            return View(tarea);
        }


        // POST: Tareas/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descripcion,FechaCreacion,Prioridad,FechaVencimiento,Completado,UsuarioId")] Tarea tarea)
        {
            if (id != tarea.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tarea);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TareaExists(tarea.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id", tarea.UsuarioId);
            return View(tarea);
        }


        // GET: Tareas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tarea = await _context.Tareas
                .Include(t => t.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tarea == null)
            {
                return NotFound();
            }

            return View(tarea);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tarea = await _context.Tareas.FindAsync(id);
            if (tarea != null)
            {
                _context.Tareas.Remove(tarea);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TareaExists(int id)
        {
            return _context.Tareas.Any(e => e.Id == id);
        }
    }
}
