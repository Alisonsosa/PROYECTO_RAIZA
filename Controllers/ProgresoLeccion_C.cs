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
    public class ProgresoLeccionController : ControllerBase
    {
        private readonly IProgresoLeccionI _progresoLeccionRepository;

        public ProgresoLeccionController(IProgresoLeccionI progresoLeccionRepository)
        {
            _progresoLeccionRepository = progresoLeccionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProgresoLecciones()
        {
            try
            {
                var progresos = await _progresoLeccionRepository.GetProgresoLecciones();
                return Ok(progresos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar los progresos de lecciones.", detalle = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgresoLeccionById([FromRoute] int id)
        {
            try
            {
                var progreso = await _progresoLeccionRepository.GetProgresoLeccionById(id);

                if (progreso == null)
                {
                    return NotFound(new { mensaje = "No se encontró el progreso de lección solicitado." });
                }

                return Ok(progreso);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar el progreso de lección.", detalle = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProgresoLeccion([FromBody] ProgresoLeccion progresoLeccion)
        {
            if (progresoLeccion == null)
            {
                return BadRequest(new { mensaje = "Los datos del progreso de lección son obligatorios." });
            }

            try
            {
                var resultado = await _progresoLeccionRepository.CreateProgresoLeccion(progresoLeccion);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible registrar el progreso de lección." });
                }

                return CreatedAtAction(
                    nameof(GetProgresoLeccionById),
                    new { id = progresoLeccion.Idprogresoleccion },
                    progresoLeccion
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al registrar el progreso de lección.", detalle = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProgresoLeccion([FromRoute] int id, [FromBody] ProgresoLeccion progresoLeccion)
        {
            if (progresoLeccion == null)
            {
                return BadRequest(new { mensaje = "Los datos del progreso de lección son obligatorios." });
            }

            if (id != progresoLeccion.Idprogresoleccion)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del progreso enviado." });
            }

            try
            {
                var progresoExistente = await _progresoLeccionRepository.GetProgresoLeccionById(id);

                if (progresoExistente == null)
                {
                    return NotFound(new { mensaje = "No se encontró el progreso de lección a actualizar." });
                }

                var resultado = await _progresoLeccionRepository.UpdateProgresoLeccion(progresoLeccion);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible actualizar el progreso de lección." });
                }

                return Ok(new { mensaje = "Progreso de lección actualizado correctamente.", progresoLeccion });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al actualizar el progreso de lección.", detalle = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgresoLeccion([FromRoute] int id)
        {
            try
            {
                var progresoExistente = await _progresoLeccionRepository.GetProgresoLeccionById(id);

                if (progresoExistente == null)
                {
                    return NotFound(new { mensaje = "No se encontró el progreso de lección a eliminar." });
                }

                var resultado = await _progresoLeccionRepository.DeleteProgresoLeccion(id);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible eliminar el progreso de lección." });
                }

                return Ok(new { mensaje = "Progreso de lección eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al eliminar el progreso de lección.", detalle = ex.Message });
            }
        }
    }
}