using Microsoft.EntityFrameworkCore;
using RAIZA.Data;
using RAIZA.Interfaces;
using RAIZA.Models;
using RAIZA.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RAIZA.Repositories
{
    public class ClaseParticipante_R : ClaseParticipante_I
    {
        private readonly DatabaseService _context;

        public ClaseParticipante_R(DatabaseService context)
        {
            this._context = context;
        }

        public async Task<List<ClaseParticipante>> GetClaseParticipantes() =>
            await _context.ClaseParticipante.ToListAsync();

        public async Task<ClaseParticipante> GetClaseParticipanteById(int idclase, int idestudiante) =>
            await _context.ClaseParticipante.FirstOrDefaultAsync(cp => cp.idclase == idclase && cp.idestudiante == idestudiante);

        public async Task<bool> CreateClaseParticipante(ClaseParticipante claseParticipante)
        {
            await _context.ClaseParticipante.AddAsync(claseParticipante);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateClaseParticipante(ClaseParticipante claseParticipante)
        {
            _context.ClaseParticipante.Update(claseParticipante);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteClaseParticipante(int idclase, int idestudiante)
        {
            var claseParticipante = await _context.ClaseParticipante.FirstOrDefaultAsync(cp => cp.idclase == idclase && cp.idestudiante == idestudiante);
            if (claseParticipante == null) return false;

            _context.ClaseParticipante.Remove(claseParticipante);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}