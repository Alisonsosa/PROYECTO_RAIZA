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
    public class EstudianteController : ControllerBase
    {
        private readonly IEstudiante_I _estudianteRepository;

        public EstudianteController(IEstudiante_I estudianteRepository)
        {
            _estudianteRepository = estudianteRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetEstudiantes()
        {
            try
            {
                var estudiantes = await _estudianteRepository.GetEstudiantes();
                return Ok(estudiantes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar estudiantes.", detalle = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEstudianteById(int id)
        {
            try
            {
                var estudiante = await _estudianteRepository.GetEstudianteById(id);

                if (estudiante == null)
                {
                    return NotFound(new { mensaje = "No se encontró el estudiante." });
                }

                return Ok(estudiante);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar el estudiante.", detalle = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEstudiante([FromBody] Estudiante estudiante)
        {
            if (estudiante == null)
            {
                return BadRequest(new { mensaje = "Los datos del estudiante son obligatorios." });
            }

            try
            {
                var resultado = await _estudianteRepository.CreateEstudiante(estudiante);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible crear el estudiante." });
                }

                return CreatedAtAction(
                    nameof(GetEstudianteById),
                    new { id = estudiante.idestudiante },
                    estudiante
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al registrar el estudiante.", detalle = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEstudiante(int id, [FromBody] Estudiante estudiante)
        {
            if (estudiante == null)
            {
                return BadRequest(new { mensaje = "Los datos del estudiante son obligatorios." });
            }

            if (id != estudiante.idestudiante)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del estudiante." });
            }

            try
            {
                var estudianteExistente = await _estudianteRepository.GetEstudianteById(id);

                if (estudianteExistente == null)
                {
                    return NotFound(new { mensaje = "No se encontró el estudiante a actualizar." });
                }

                var resultado = await _estudianteRepository.UpdateEstudiante(estudiante);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible actualizar el estudiante." });
                }

                return Ok(new { mensaje = "Estudiante actualizado correctamente.", estudiante });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al actualizar el estudiante.", detalle = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstudiante(int id)
        {
            try
            {
                var estudiante = await _estudianteRepository.GetEstudianteById(id);

                if (estudiante == null)
                {
                    return NotFound(new { mensaje = "No se encontró el estudiante a eliminar." });
                }

                var resultado = await _estudianteRepository.DeleteEstudiante(id);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible eliminar el estudiante." });
                }

                return Ok(new { mensaje = "Estudiante eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al eliminar el estudiante.", detalle = ex.Message });
            }
        }
    }
}