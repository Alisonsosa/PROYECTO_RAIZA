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
        public class CompraController : ControllerBase
        {
            private readonly CompraI _compraRepository;

            public CompraController(CompraI compraRepository)
            {
                _compraRepository = compraRepository;
            }

            [HttpGet]
            public async Task<IActionResult> GetCompras()
            {
                try
                {
                    var compras = await _compraRepository.GetCompras();
                    return Ok(compras);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { mensaje = "Error interno al obtener compras.", detalle = ex.Message });
                }
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetCompraById(int id)
            {
                try
                {
                    var compra = await _compraRepository.GetCompraById(id);

                    if (compra == null)
                    {
                        return NotFound(new { mensaje = "No se encontró la compra." });
                    }

                    return Ok(compra);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { mensaje = "Error interno al consultar la compra.", detalle = ex.Message });
                }
            }

            [HttpPost]
            public async Task<IActionResult> CreateCompra([FromBody] Compra compra)
            {
                if (compra == null)
                {
                    return BadRequest(new { mensaje = "Los datos de la compra son obligatorios." });
                }

                try
                {
                    var resultado = await _compraRepository.CreateCompra(compra);

                    if (!resultado)
                    {
                        return BadRequest(new { mensaje = "No fue posible registrar la compra." });
                    }

                    return CreatedAtAction(
                        nameof(GetCompraById),
                        new { id = compra.idcompra },
                        compra
                    );
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { mensaje = "Error interno al crear la compra.", detalle = ex.Message });
                }
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateCompra(int id, [FromBody] Compra compra)
            {
                if (compra == null)
                {
                    return BadRequest(new { mensaje = "Los datos de la compra son obligatorios." });
                }

                if (id != compra.idcompra)
                {
                    return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID de la compra." });
                }

                try
                {
                    var compraExistente = await _compraRepository.GetCompraById(id);

                    if (compraExistente == null)
                    {
                        return NotFound(new { mensaje = "No se encontró la compra." });
                    }

                    var resultado = await _compraRepository.UpdateCompra(compra);

                    if (!resultado)
                    {
                        return BadRequest(new { mensaje = "No fue posible actualizar la compra." });
                    }

                    return Ok(new { mensaje = "Compra actualizada correctamente.", compra });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { mensaje = "Error interno al actualizar la compra.", detalle = ex.Message });
                }
            }

            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteCompra(int id)
            {
                try
                {
                    var compra = await _compraRepository.GetCompraById(id);

                    if (compra == null)
                    {
                        return NotFound(new { mensaje = "No se encontró la compra." });
                    }

                    var resultado = await _compraRepository.DeleteCompra(id);

                    if (!resultado)
                    {
                        return BadRequest(new { mensaje = "No fue posible eliminar la compra." });
                    }

                    return Ok(new { mensaje = "Compra eliminada correctamente." });
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { mensaje = "Error interno al eliminar la compra.", detalle = ex.Message });
                }
            }
        }
    }