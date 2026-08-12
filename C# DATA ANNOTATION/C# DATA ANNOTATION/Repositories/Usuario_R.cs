using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RAIZA.Interfaces;
using RAIZA.Models;
using RAIZA.Data;

namespace RAIZA.Repositories
{
    public class Usuario_R : Usuario_I
    {
        private readonly DatabaseService _context;

        public Usuario_R(DatabaseService context)
        {
            this._context = context;
        }

        public async Task<List<Usuario>> GetUsuarios() =>
            await _context.Usuario.ToListAsync();

        public async Task<Usuario> GetUsuarioById(int id) =>
            await _context.Usuario.FirstOrDefaultAsync(u => u.Id == id);

        public async Task<bool> CreateUsuario(Usuario usuario)
        {
            await _context.Usuario.AddAsync(usuario);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUsuario(Usuario usuario)
        {
            _context.Usuario.Update(usuario);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteUsuario(int id)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null) return false;

            _context.Usuario.Remove(usuario);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}