using System.Collections.Generic;
using System.Threading.Tasks;
using RAIZA.Models;

namespace RAIZA.Interfaces
{
    public interface Usuario_I
    {
        Task<List<Usuario>> GetUsuarios();
        Task<Usuario> GetUsuarioById(int id);
        Task<bool> CreateUsuario(Usuario usuario);
        Task<bool> UpdateUsuario(Usuario usuario);
        Task<bool> DeleteUsuario(int id);
    }
}