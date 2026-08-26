using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RAIZA.Interfaces;
using RAIZA.Models;
using RAIZA.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RAIZA.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TareaController : ControllerBase
    {
        private readonly ITarea_I _tareaRepository;

        public TareaController(ITarea_I tareaRepository)
        {
            _tareaRepository = tareaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetTareas()
        {
            try
            {
                var tareas = await _tareaRepository.GetTareas();
                return Ok(tareas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar las tareas.", detalle = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTareaById([FromRoute] int id)
        {
            try
            {
                var tarea = await _tareaRepository.GetTareaById(id);

                if (tarea == null)
                {
                    return NotFound(new { mensaje = "No se encontró la tarea solicitada." });
                }

                return Ok(tarea);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar la tarea.", detalle = ex.Message });
            }
        }

        [HttpGet("modulo/{idModulo}")]
        public async Task<IActionResult> GetTareasByModuloId([FromRoute] int idModulo)
        {
            try
            {
                var tareas = await _tareaRepository.GetTareasByModuloId(idModulo);
                return Ok(tareas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar las tareas por módulo.", detalle = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTarea([FromBody] Tarea tarea)
        {
            if (tarea == null)
            {
                return BadRequest(new { mensaje = "Los datos de la tarea son obligatorios." });
            }

            try
            {
                var resultado = await _tareaRepository.CreateTarea(tarea);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible registrar la tarea." });
                }

                return CreatedAtAction(
                    nameof(GetTareaById),
                    new { id = tarea.idtarea },
                    tarea
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al registrar la tarea.", detalle = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTarea([FromRoute] int id, [FromBody] Tarea tarea)
        {
            if (tarea == null)
            {
                return BadRequest(new { mensaje = "Los datos de la tarea son obligatorios." });
            }

            if (id != tarea.idtarea)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID de la tarea enviada." });
            }

            try
            {
                var tareaExistente = await _tareaRepository.GetTareaById(id);

                if (tareaExistente == null)
                {
                    return NotFound(new { mensaje = "No se encontró la tarea a actualizar." });
                }

                var resultado = await _tareaRepository.UpdateTarea(tarea);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible actualizar la tarea." });
                }

                return Ok(new { mensaje = "Tarea actualizada correctamente.", tarea });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al actualizar la tarea.", detalle = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarea([FromRoute] int id)
        {
            try
            {
                var tareaExistente = await _tareaRepository.GetTareaById(id);

                if (tareaExistente == null)
                {
                    return NotFound(new { mensaje = "No se encontró la tarea a eliminar." });
                }

                var resultado = await _tareaRepository.DeleteTarea(id);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible eliminar la tarea." });
                }

                return Ok(new { mensaje = "Tarea eliminada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al eliminar la tarea.", detalle = ex.Message });
            }
        }
    }
}