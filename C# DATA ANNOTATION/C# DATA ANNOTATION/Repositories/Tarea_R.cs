using Microsoft.EntityFrameworkCore;
using RAIZA.Data;
using RAIZA.Interfaces;
using RAIZA.Models;
using RAIZA.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RAIZA.Repositories
{
    public class Tarea_R : Tarea_I
    {
        private readonly DatabaseService _context;

        public Tarea_R(DatabaseService context)
        {
            this._context = context;
        }

        public async Task<List<Tarea>> GetTareas() =>
            await _context.Tarea.ToListAsync();

        public async Task<Tarea> GetTareaById(int id) =>
            await _context.Tarea.FirstOrDefaultAsync(t => t.idtarea == id);

        public async Task<bool> CreateTarea(Tarea tarea)
        {
            await _context.Tarea.AddAsync(tarea);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateTarea(Tarea tarea)
        {
            _context.Tarea.Update(tarea);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTarea(int id)
        {
            var tarea = await _context.Tarea.FirstOrDefaultAsync(t => t.idtarea == id);
            if (tarea == null) return false;

            _context.Tarea.Remove(tarea);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}