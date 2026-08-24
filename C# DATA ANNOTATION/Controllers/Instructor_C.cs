using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using RAIZA.Interfaces;
using RAIZA.Models;



namespace RAIZA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructor_I _instructorRepository;

        public InstructorController(IInstructor_I instructorRepository)
        {
            _instructorRepository = instructorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetInstructores()
        {
            try
            {
                var instructores = await _instructorRepository.GetInstructores();
                return Ok(instructores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar los instructores.", detalle = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetInstructorById([FromRoute] int id)
        {
            try
            {
                var instructor = await _instructorRepository.GetInstructorById(id);

                if (instructor == null)
                {
                    return NotFound(new { mensaje = "No se encontró el instructor solicitado." });
                }

                return Ok(instructor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar el instructor.", detalle = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateInstructor([FromBody] Instructor instructor)
        {
            if (instructor == null)
            {
                return BadRequest(new { mensaje = "Los datos del instructor son obligatorios." });
            }

            try
            {
                var resultado = await _instructorRepository.CreateInstructor(instructor);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible registrar el instructor." });
                }

                return CreatedAtAction(
                    nameof(GetInstructorById),
                    new { id = instructor.idinstructor },
                    instructor
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al guardar el instructor.", detalle = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInstructor([FromRoute] int id, [FromBody] Instructor instructor)
        {
            if (instructor == null)
            {
                return BadRequest(new { mensaje = "Los datos del instructor son obligatorios." });
            }

            if (id != instructor.idinstructor)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del instructor." });
            }

            try
            {
                var instructorExistente = await _instructorRepository.GetInstructorById(id);

                if (instructorExistente == null)
                {
                    return NotFound(new { mensaje = "No se encontró el instructor a actualizar." });
                }

                var resultado = await _instructorRepository.UpdateInstructor(instructor);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible actualizar el instructor." });
                }

                return Ok(new { mensaje = "Instructor actualizado correctamente.", instructor });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al actualizar el instructor.", detalle = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInstructor([FromRoute] int id)
        {
            try
            {
                var instructorExistente = await _instructorRepository.GetInstructorById(id);

                if (instructorExistente == null)
                {
                    return NotFound(new { mensaje = "No se encontró el instructor a eliminar." });
                }

                var resultado = await _instructorRepository.DeleteInstructor(id);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible eliminar el instructor." });
                }

                return Ok(new { mensaje = "Instructor eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al eliminar el instructor.", detalle = ex.Message });
            }
        }
    }
}