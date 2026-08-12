using System.Collections.Generic;
using System.Threading.Tasks;
using RAIZA.Models;

namespace RAIZA.Interfaces
{
    public interface EntregaTarea_I
    {
        Task<List<EntregaTarea>> GetEntregaTareas();
        Task<EntregaTarea> GetEntregaTareaById(int id);
        Task<bool> CreateEntregaTarea(EntregaTarea entregaTarea);
        Task<bool> UpdateEntregaTarea(EntregaTarea entregaTarea);
        Task<bool> DeleteEntregaTarea(int id);
    }
}