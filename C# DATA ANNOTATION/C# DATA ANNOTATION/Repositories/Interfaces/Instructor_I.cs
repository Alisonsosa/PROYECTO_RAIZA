using System.Collections.Generic;
using System.Threading.Tasks;
using RAIZA.Models;

namespace RAIZA.Interfaces
{
    public interface Instructor_I
    {
        Task<List<Instructor>> GetInstructores();
        Task<Instructor> GetInstructorById(int id);
        Task<bool> CreateInstructor(Instructor instructor);
        Task<bool> UpdateInstructor(Instructor instructor);
        Task<bool> DeleteInstructor(int id);
    }
}