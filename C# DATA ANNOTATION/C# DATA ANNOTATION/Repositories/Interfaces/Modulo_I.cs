using System.Collections.Generic;
using System.Threading.Tasks;
using RAIZA.Models;

namespace RAIZA.Interfaces
{
    public interface Modulo_I
    {
        Task<List<Modulo>> GetModulos();
        Task<Modulo> GetModuloById(int id);
        Task<bool> CreateModulo(Modulo modulo);
        Task<bool> UpdateModulo(Modulo modulo);
        Task<bool> DeleteModulo(int id);
    }
}