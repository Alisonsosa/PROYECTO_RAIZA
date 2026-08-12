using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RAIZA.Interfaces;
using RAIZA.Models;
using RAIZA.Data;

namespace RAIZA.Repositories
{
    public class PedidoKit_R : PedidoKit_I
    {
        private readonly DatabaseService _context;

        public PedidoKit_R(DatabaseService context)
        {
            this._context = context;
        }

        public async Task<List<PedidoKit>> GetPedidoKits() =>
            await _context.PedidoKit.ToListAsync();

        public async Task<PedidoKit> GetPedidoKitById(int id) =>
            await _context.PedidoKit.FirstOrDefaultAsync(p => p.idPedidoKit == id);

        public async Task<bool> CreatePedidoKit(PedidoKit pedidoKit)
        {
            await _context.PedidoKit.AddAsync(pedidoKit);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePedidoKit(PedidoKit pedidoKit)
        {
            _context.PedidoKit.Update(pedidoKit);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeletePedidoKit(int id)
        {
            var pedidoKit = await _context.PedidoKit.FirstOrDefaultAsync(p => p.idPedidoKit == id);
            if (pedidoKit == null) return false;

            _context.PedidoKit.Remove(pedidoKit);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}