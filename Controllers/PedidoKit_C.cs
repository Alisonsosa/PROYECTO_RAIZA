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
    public class PedidoKitController : ControllerBase
    {
        private readonly IPedidoKit_I _pedidoKitRepository;

        public PedidoKitController(IPedidoKit_I pedidoKitRepository)
        {
            _pedidoKitRepository = pedidoKitRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetPedidoKits()
        {
            try
            {
                var pedidos = await _pedidoKitRepository.GetPedidoKits();
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar los pedidos de kits.", detalle = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoKitById([FromRoute] int id)
        {
            try
            {
                var pedido = await _pedidoKitRepository.GetPedidoKitById(id);

                if (pedido == null)
                {
                    return NotFound(new { mensaje = "No se encontró el pedido de kit solicitado." });
                }

                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al consultar el pedido de kit.", detalle = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePedidoKit([FromBody] PedidoKit pedidoKit)
        {
            if (pedidoKit == null)
            {
                return BadRequest(new { mensaje = "Los datos del pedido son obligatorios." });
            }

            try
            {
                var resultado = await _pedidoKitRepository.CreatePedidoKit(pedidoKit);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible registrar el pedido de kit." });
                }

                return CreatedAtAction(
                    nameof(GetPedidoKitById),
                    new { id = pedidoKit.idPedidoKit },
                    pedidoKit
                );
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al registrar el pedido de kit.", detalle = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePedidoKit([FromRoute] int id, [FromBody] PedidoKit pedidoKit)
        {
            if (pedidoKit == null)
            {
                return BadRequest(new { mensaje = "Los datos del pedido son obligatorios." });
            }

            if (id != pedidoKit.idPedidoKit)
            {
                return BadRequest(new { mensaje = "El ID de la URL no coincide con el ID del pedido enviado." });
            }

            try
            {
                var pedidoExistente = await _pedidoKitRepository.GetPedidoKitById(id);

                if (pedidoExistente == null)
                {
                    return NotFound(new { mensaje = "No se encontró el pedido de kit a actualizar." });
                }

                var resultado = await _pedidoKitRepository.UpdatePedidoKit(pedidoKit);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible actualizar el pedido de kit." });
                }

                return Ok(new { mensaje = "Pedido de kit actualizado correctamente.", pedidoKit });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al actualizar el pedido de kit.", detalle = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePedidoKit([FromRoute] int id)
        {
            try
            {
                var pedidoExistente = await _pedidoKitRepository.GetPedidoKitById(id);

                if (pedidoExistente == null)
                {
                    return NotFound(new { mensaje = "No se encontró el pedido de kit a eliminar." });
                }

                var resultado = await _pedidoKitRepository.DeletePedidoKit(id);

                if (!resultado)
                {
                    return BadRequest(new { mensaje = "No fue posible eliminar el pedido de kit." });
                }

                return Ok(new { mensaje = "Pedido de kit eliminado correctamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = "Error interno al eliminar el pedido de kit.", detalle = ex.Message });
            }
        }
    }
}