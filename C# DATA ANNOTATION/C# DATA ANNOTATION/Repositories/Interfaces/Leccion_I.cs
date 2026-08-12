using System.Collections.Generic;
using System.Threading.Tasks;
using RAIZA.Models;

namespace RAIZA.Interfaces
{
    public interface Leccion_I
    {
        Task<List<Leccion>> GetLecciones();
        Task<Leccion> GetLeccionById(int id);
        Task<bool> CreateLeccion(Leccion leccion);
        Task<bool> UpdateLeccion(Leccion leccion);
        Task<bool> DeleteLeccion(int id);
    }
}