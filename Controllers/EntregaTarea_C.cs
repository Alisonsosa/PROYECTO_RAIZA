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
    public class EntregaTareaController : ControllerBase
    {
        private readonly IEntregaTareaI _entregaTareaRepository;

        public EntregaTareaController(IEntregaTareaI entregaTareaRepository)
        {
            _entregaTareaRepository = entregaTareaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetEntregaTareas()
        {
            try
            {
                var entregas = await _entregaTareaRepository.GetEntregaTareas();
                return Ok(entregas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar las entregas de tareas.", detalle = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEntregaTareaById([FromRoute] int id)
        {
            try
            {
                var entrega = await _entregaTareaRepository.GetEntregaTareaById(id);

                if (entrega == null)
                {
                    return NotFound(new { mensaje = "No se encontró la entrega de tarea solicitada." });
                }

                return Ok(entrega);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar la entrega de tarea.", detalle = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntregaTarea([FromBody] EntregaTarea entregaTarea)
        {
            if (entregaTarea == null)
            {
                return BadRequest(new { mensaje = "Los datos de la entrega son obligatorios." });
            }

            try
            {
                var resultado = await _entregaTareaRepository.CreateEntregaTarea(entregaTarea);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible guardar la entrega de tarea." });
                }

                return CreatedAtAction(
                    nameof(GetEntregaTareaById),
                    new { id = entregaTarea.Identregatarea },
                    entregaTarea
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al registrar la entrega de tarea.", detalle = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEntregaTarea([FromRoute] int id, [FromBody] EntregaTarea entregaTarea)
        {
            if (entregaTarea == null)
            {
                return BadRequest(new { mensaje = "Los datos de la entrega son obligatorios." });
            }

            if (id != entregaTarea.Identregatarea)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID de la entrega enviada." });
            }

            try
            {
                var entregaExistente = await _entregaTareaRepository.GetEntregaTareaById(id);

                if (entregaExistente == null)
                {
                    return NotFound(new { mensaje = "No se encontró la entrega de tarea a actualizar." });
                }

                var resultado = await _entregaTareaRepository.UpdateEntregaTarea(entregaTarea);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible actualizar la entrega de tarea." });
                }

                return Ok(new { mensaje = "Entrega de tarea actualizada correctamente.", entregaTarea });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al actualizar la entrega de tarea.", detalle = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEntregaTarea([FromRoute] int id)
        {
            try
            {
                var entregaExistente = await _entregaTareaRepository.GetEntregaTareaById(id);

                if (entregaExistente == null)
                {
                    return NotFound(new { mensaje = "No se encontró la entrega de tarea a eliminar." });
                }

                var resultado = await _entregaTareaRepository.DeleteEntregaTarea(id);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible eliminar la entrega de tarea." });
                }

                return Ok(new { mensaje = "Entrega de tarea eliminada correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al eliminar la entrega de tarea.", detalle = ex.Message });
            }
        }
    }
}