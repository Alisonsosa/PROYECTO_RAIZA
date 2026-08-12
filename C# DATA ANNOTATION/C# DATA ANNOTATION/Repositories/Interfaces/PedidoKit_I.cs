using System.Collections.Generic;
using System.Threading.Tasks;
using RAIZA.Models;

namespace RAIZA.Interfaces
{
    public interface PedidoKit_I
    {
        Task<List<PedidoKit>> GetPedidoKits();
        Task<PedidoKit> GetPedidoKitById(int id);
        Task<bool> CreatePedidoKit(PedidoKit pedidoKit);
        Task<bool> UpdatePedidoKit(PedidoKit pedidoKit);
        Task<bool> DeletePedidoKit(int id);
    }
}