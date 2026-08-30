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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioI _usuarioRepository;

        public UsuarioController(IUsuarioI usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsuarios()
        {
            try
            {
                var usuarios = await _usuarioRepository.GetUsuarios();
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar los usuarios.", detalle = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUsuarioById([FromRoute] int id)
        {
            try
            {
                var usuario = await _usuarioRepository.GetUsuarioById(id);

                if (usuario == null)
                {
                    return NotFound(new { mensaje = "No se encontró el usuario solicitado." });
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar el usuario.", detalle = ex.Message });
            }
        }

        [HttpGet("correo/{correo}")]
        public async Task<IActionResult> GetUsuarioByCorreo([FromRoute] string correo)
        {
            try
            {
                var usuario = await _usuarioRepository.GetUsuarioByCorreo(correo);

                if (usuario == null)
                {
                    return NotFound(new { mensaje = "No se encontró un usuario con ese correo electrónico." });
                }

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar el usuario por correo.", detalle = ex.Message });
            }
        }

        [HttpGet("rol/{rol}")]
        public async Task<IActionResult> GetUsuariosByRol([FromRoute] string rol)
        {
            try
            {
                var usuarios = await _usuarioRepository.GetUsuariosByRol(rol);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar usuarios por rol.", detalle = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest(new { mensaje = "Los datos del usuario son obligatorios." });
            }

            try
            {
                var resultado = await _usuarioRepository.CreateUsuario(usuario);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible registrar el usuario." });
                }

                return CreatedAtAction(
                    nameof(GetUsuarioById),
                    new { id = usuario.Id },
                    usuario
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al registrar el usuario.", detalle = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUsuario([FromRoute] int id, [FromBody] Usuario usuario)
        {
            if (usuario == null)
            {
                return BadRequest(new { mensaje = "Los datos del usuario son obligatorios." });
            }

            if (id != usuario.Id)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del usuario enviado." });
            }

            try
            {
                var usuarioExistente = await _usuarioRepository.GetUsuarioById(id);

                if (usuarioExistente == null)
                {
                    return NotFound(new { mensaje = "No se encontró el usuario a actualizar." });
                }

                var resultado = await _usuarioRepository.UpdateUsuario(usuario);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible actualizar el usuario." });
                }

                return Ok(new { mensaje = "Usuario actualizado correctamente.", usuario });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al actualizar el usuario.", detalle = ex.Message });
            }
        }

        [HttpPatch("{id:int}/estado")]
        public async Task<IActionResult> CambiarEstadoUsuario([FromRoute] int id, [FromBody] string nuevoEstado)
        {
            if (string.IsNullOrWhiteSpace(nuevoEstado))
            {
                return BadRequest(new { mensaje = "El nuevo estado es requerido." });
            }

            try
            {
                var resultado = await _usuarioRepository.CambiarEstadoUsuario(id, nuevoEstado);

                if (!resultado)
                {
                    return NotFound(new { mensaje = "No fue posible actualizar el estado. Verifica si el usuario existe." });
                }

                return Ok(new { mensaje = "Estado del usuario actualizado correctamente.", estado = nuevoEstado });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al cambiar el estado del usuario.", detalle = ex.Message });
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUsuario([FromRoute] int id)
        {
            try
            {
                var usuarioExistente = await _usuarioRepository.GetUsuarioById(id);

                if (usuarioExistente == null)
                {
                    return NotFound(new { mensaje = "No se encontró el usuario a eliminar." });
                }

                var resultado = await _usuarioRepository.DeleteUsuario(id);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible eliminar el usuario." });
                }

                return Ok(new { mensaje = "Usuario eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al eliminar el usuario.", detalle = ex.Message });
            }
        }
    }
}