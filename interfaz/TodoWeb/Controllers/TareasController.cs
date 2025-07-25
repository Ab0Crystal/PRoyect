using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TodoWeb.Datos;
using TodoWeb.Models;

namespace TodoWeb.Controllers
{
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
            var todoContext = _context.Tareas.Include(t => t.Usuario);
            return View(await todoContext.ToListAsync());
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
            return View();
        }
        // POST: Tareas/Create
        [HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("Titulo,Descripcion,FechaCreacion,Prioridad,FechaVencimiento,Completado,UsuarioId")] Tarea tarea)
{
    if (ModelState.IsValid)
    {
        _context.Add(tarea);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    ViewData["UsuarioId"] = new SelectList(_context.Set<Usuario>(), "Id", "Id", tarea.UsuarioId);
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
