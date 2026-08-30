using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RAIZA.Interfaces;
using RAIZA.Models;
using RAIZA.Data;

namespace RAIZA.Repositories
{
    public class ProgresoLeccion_R : IProgresoLeccionI
    {
        private readonly DatabaseService _context;

        public ProgresoLeccion_R(DatabaseService context)
        {
            this._context = context;
        }

        public async Task<List<ProgresoLeccion>> GetProgresoLecciones() =>
            await _context.ProgresoLeccion.ToListAsync();

        public async Task<ProgresoLeccion?> GetProgresoLeccionById(int id) =>
            await _context.ProgresoLeccion.FirstOrDefaultAsync(predicate: p => p.Idprogresoleccion == id);

        public async Task<bool> CreateProgresoLeccion(ProgresoLeccion progresoLeccion)
        {
            await _context.ProgresoLeccion.AddAsync(progresoLeccion);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProgresoLeccion(ProgresoLeccion progresoLeccion)
        {
            _context.ProgresoLeccion.Update(progresoLeccion);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProgresoLeccion(int id)
        {
            var progresoLeccion = await _context.ProgresoLeccion.FirstOrDefaultAsync(p => p.Idprogresoleccion == id);
            if (progresoLeccion == null) return false;

            _context.ProgresoLeccion.Remove(progresoLeccion);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}