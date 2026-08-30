using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RAIZA.Interfaces;
using RAIZA.Models;
using RAIZA.Data;

namespace RAIZA.Repositories
{
    public class Usuario_R(DatabaseService context) : IUsuarioI
    {
        private readonly DatabaseService _context = context;

        public async Task<IEnumerable<Usuario>> GetUsuarios() =>
            await _context.Usuario.AsNoTracking().ToListAsync();

        public async Task<Usuario?> GetUsuarioById(int id) =>
            await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

        public async Task<Usuario?> GetUsuarioByCorreo(string correo) =>
            await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(u => u.Correo == correo);

        public async Task<IEnumerable<Usuario>> GetUsuariosByRol(string rol) =>
            await _context.Usuario
                .AsNoTracking()
                .Where(u => u.Rol.ToLower() == rol.ToLower())
                .ToListAsync();

        public async Task<bool> CreateUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Estado))
            {
                usuario.Estado = "Activo";
            }

            await _context.Usuario.AddAsync(usuario);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUsuario(Usuario usuario)
        {
            _context.Usuario.Update(usuario);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CambiarEstadoUsuario(int id, string nuevoEstado)
        {
            var usuario = await _context.Usuario.FirstOrDefaultAsync(u => u.Id == id);
            if (usuario == null) return false;

            usuario.Estado = nuevoEstado;
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