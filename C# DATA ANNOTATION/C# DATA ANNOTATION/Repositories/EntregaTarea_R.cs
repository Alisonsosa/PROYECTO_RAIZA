using Microsoft.EntityFrameworkCore;
using RAIZA.Data;
using RAIZA.Interfaces;
using RAIZA.Models;
using RAIZA.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RAIZA.Repositories
{
    public class EntregaTarea_R : EntregaTarea_I
    {
        private readonly DatabaseService _context;

        public EntregaTarea_R(DatabaseService context)
        {
            this._context = context;
        }

        public async Task<List<EntregaTarea>> GetEntregaTareas() =>
            await _context.EntregaTarea.ToListAsync();

        public async Task<EntregaTarea> GetEntregaTareaById(int id) =>
            await _context.EntregaTarea.FirstOrDefaultAsync(e => e.Identregatarea == id);

        public async Task<bool> CreateEntregaTarea(EntregaTarea entregaTarea)
        {
            await _context.EntregaTarea.AddAsync(entregaTarea);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateEntregaTarea(EntregaTarea entregaTarea)
        {
            _context.EntregaTarea.Update(entregaTarea);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteEntregaTarea(int id)
        {
            var entregaTarea = await _context.EntregaTarea.FirstOrDefaultAsync(e => e.Identregatarea == id);
            if (entregaTarea == null) return false;

            _context.EntregaTarea.Remove(entregaTarea);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}