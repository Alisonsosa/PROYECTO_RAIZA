using System.Collections.Generic;
using System.Threading.Tasks;
using RAIZA.Models;

namespace RAIZA.Interfaces
{
    public interface Progreso_I
    {
        Task<List<Progreso>> GetProgresos();
        Task<Progreso> GetProgresoById(int id);
        Task<bool> CreateProgreso(Progreso progreso);
        Task<bool> UpdateProgreso(Progreso progreso);
        Task<bool> DeleteProgreso(int id);
    }
}