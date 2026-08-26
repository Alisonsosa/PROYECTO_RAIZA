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
    public class CertificadoController : ControllerBase
    {
        private readonly CertificadoI _certificadoRepository;

        public CertificadoController(CertificadoI certificadoRepository)
        {
            _certificadoRepository = certificadoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCertificados()
        {
            try
            {
                var certificados = await _certificadoRepository.GetCertificados();
                return Ok(certificados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al obtener certificados.", detalle = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCertificadoById(int id)
        {
            try
            {
                var certificado = await _certificadoRepository.GetCertificadoById(id);

                if (certificado == null)
                {
                    return NotFound(new { mensaje = "No se encontró el certificado." });
                }

                return Ok(certificado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al buscar el certificado.", detalle = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCertificado([FromBody] Certificado certificado)
        {
            if (certificado == null)
            {
                return BadRequest(new { mensaje = "Los datos del certificado son obligatorios." });
            }

            try
            {
                var resultado = await _certificadoRepository.CreateCertificado(certificado);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible emitir el certificado." });
                }

                return CreatedAtAction(
                    nameof(GetCertificadoById),
                    new { id = certificado.idcertificado },
                    certificado
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al emitir el certificado.", detalle = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCertificado(int id, [FromBody] Certificado certificado)
        {
            if (certificado == null)
            {
                return BadRequest(new { mensaje = "Los datos del certificado son obligatorios." });
            }

            if (id != certificado.idcertificado)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del certificado." });
            }

            try
            {
                var certificadoExistente = await _certificadoRepository.GetCertificadoById(id);

                if (certificadoExistente == null)
                {
                    return NotFound(new { mensaje = "No se encontró el certificado." });
                }

                var resultado = await _certificadoRepository.UpdateCertificado(certificado);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible actualizar el certificado." });
                }

                return Ok(new { mensaje = "Certificado actualizado correctamente.", certificado });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al actualizar el certificado.", detalle = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertificado(int id)
        {
            try
            {
                var certificado = await _certificadoRepository.GetCertificadoById(id);

                if (certificado == null)
                {
                    return NotFound(new { mensaje = "No se encontró el certificado." });
                }

                var resultado = await _certificadoRepository.DeleteCertificado(id);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible eliminar el certificado." });
                }

                return Ok(new { mensaje = "Certificado eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al eliminar el certificado.", detalle = ex.Message });
            }
        }
    }
}
