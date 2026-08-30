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
    public class AdministradorController : ControllerBase
    {
        private readonly AdministradorI _administradorRepository;

        public AdministradorController(AdministradorI administradorRepository)
        {
            _administradorRepository = administradorRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAdministradores()
        {
            try
            {
                var administradores = await _administradorRepository.GetAdministradores();
                return Ok(administradores);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar los administradores.", detalle = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAdministradorById(int id)
        {
            try
            {
                var administrador = await _administradorRepository.GetAdministradorById(id);

                if (administrador == null)
                {
                    return NotFound(new { mensaje = "No se encontró el administrador." });
                }

                return Ok(administrador);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar el administrador.", detalle = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAdministrador([FromBody] Administrador administrador)
        {
            if (administrador == null)
            {
                return BadRequest(new { mensaje = "Los datos del administrador son obligatorios." });
            }

            try
            {
                var resultado = await _administradorRepository.CreateAdministrador(administrador);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible crear el administrador." });
                }

                return CreatedAtAction(
                    nameof(GetAdministradorById),
                    new { id = administrador.idadministrador },
                    administrador
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al guardar el administrador.", detalle = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdministrador(int id, [FromBody] Administrador administrador)
        {
            if (administrador == null)
            {
                return BadRequest(new { mensaje = "Los datos del administrador son obligatorios." });
            }

            if (id != administrador.idadministrador)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del administrador." });
            }

            try
            {
                var administradorExistente = await _administradorRepository.GetAdministradorById(id);

                if (administradorExistente == null)
                {
                    return NotFound(new { mensaje = "No se encontró el administrador a actualizar." });
                }

                var resultado = await _administradorRepository.UpdateAdministrador(administrador);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible actualizar el administrador." });
                }

                return Ok(new { mensaje = "Administrador actualizado correctamente.", administrador });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al actualizar el administrador.", detalle = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrador(int id)
        {
            try
            {
                var administrador = await _administradorRepository.GetAdministradorById(id);

                if (administrador == null)
                {
                    return NotFound(new { mensaje = "No se encontró el administrador a eliminar." });
                }

                var resultado = await _administradorRepository.DeleteAdministrador(id);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible eliminar el administrador." });
                }

                return Ok(new { mensaje = "Administrador eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al eliminar el administrador.", detalle = ex.Message });
            }
        }
    }
}