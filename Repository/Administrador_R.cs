using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RAIZA.Interfaces;
using RAIZA.Models;
using RAIZA.Data;

namespace RAIZA.Repositories
{
    public class Administrador_R : AdministradorI
    {
        private readonly DatabaseService _context;

        public Administrador_R(DatabaseService context)
        {
            this._context = context;
        }

        public async Task<List<Administrador>> GetAdministradores() =>
            await _context.Administrador.ToListAsync();

        public async Task<Administrador?> GetAdministradorById(int id) =>
            await _context.Administrador.FirstOrDefaultAsync(a => a.idadministrador == id);

        public async Task<bool> CreateAdministrador(Administrador administrador)
        {
            await _context.Administrador.AddAsync(administrador);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAdministrador(Administrador administrador)
        {
            _context.Administrador.Update(administrador);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAdministrador(int id)
        {
            var administrador = await _context.Administrador.FirstOrDefaultAsync(a => a.idadministrador == id);
            if (administrador == null) return false;

            _context.Administrador.Remove(administrador);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}