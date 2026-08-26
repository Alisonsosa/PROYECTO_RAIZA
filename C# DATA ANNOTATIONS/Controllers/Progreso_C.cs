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
    public class ProgresoController : ControllerBase
    {
        private readonly IProgresoI _progresoRepository;

        public ProgresoController(IProgresoI progresoRepository)
        {
            _progresoRepository = progresoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProgresos()
        {
            try
            {
                var progresos = await _progresoRepository.GetProgresos();
                return Ok(progresos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar los registros de progreso.", detalle = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProgresoById([FromRoute] int id)
        {
            try
            {
                var progreso = await _progresoRepository.GetProgresoById(id);

                if (progreso == null)
                {
                    return NotFound(new { mensaje = "No se encontró el registro de progreso solicitado." });
                }

                return Ok(progreso);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar el registro de progreso.", detalle = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProgreso([FromBody] Progreso progreso)
        {
            if (progreso == null)
            {
                return BadRequest(new { mensaje = "Los datos del progreso son obligatorios." });
            }

            try
            {
                var resultado = await _progresoRepository.CreateProgreso(progreso);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible registrar el progreso." });
                }

                return CreatedAtAction(
                    nameof(GetProgresoById),
                    new { id = progreso.Idprogreso },
                    progreso
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al registrar el progreso.", detalle = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProgreso([FromRoute] int id, [FromBody] Progreso progreso)
        {
            if (progreso == null)
            {
                return BadRequest(new { mensaje = "Los datos del progreso son obligatorios." });
            }

            if (id != progreso.Idprogreso)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del progreso enviado." });
            }

            try
            {
                var progresoExistente = await _progresoRepository.GetProgresoById(id);

                if (progresoExistente == null)
                {
                    return NotFound(new { mensaje = "No se encontró el registro de progreso a actualizar." });
                }

                var resultado = await _progresoRepository.UpdateProgreso(progreso);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible actualizar el progreso." });
                }

                return Ok(new { mensaje = "Progreso actualizado correctamente.", progreso });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al actualizar el progreso.", detalle = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProgreso([FromRoute] int id)
        {
            try
            {
                var progresoExistente = await _progresoRepository.GetProgresoById(id);

                if (progresoExistente == null)
                {
                    return NotFound(new { mensaje = "No se encontró el registro de progreso a eliminar." });
                }

                var resultado = await _progresoRepository.DeleteProgreso(id);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible eliminar el registro de progreso." });
                }

                return Ok(new { mensaje = "Registro de progreso eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al eliminar el registro de progreso.", detalle = ex.Message });
            }
        }
    }
}