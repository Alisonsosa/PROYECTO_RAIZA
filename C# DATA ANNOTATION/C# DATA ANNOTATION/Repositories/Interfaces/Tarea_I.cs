using System.Collections.Generic;
using System.Threading.Tasks;
using RAIZA.Models;

namespace RAIZA.Interfaces
{
    public interface Tarea_I
    {
        Task<List<Tarea>> GetTareas();
        Task<Tarea> GetTareaById(int id);
        Task<bool> CreateTarea(Tarea tarea);
        Task<bool> UpdateTarea(Tarea tarea);
        Task<bool> DeleteTarea(int id);
    }
}