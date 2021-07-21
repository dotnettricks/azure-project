
using ePizzaHub.Entities;
using ePizzaHub.Repositories.Models;
using ePizzaHub.Repositories.Models.cosmos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ePizzaHub.Repositories.Interfaces
{
    public interface ICartRepositoryCosmos : IRepositoryCosmos<Cart>
    {
        Cart GetCart(Guid CartId);
        CartModelCosmos GetCartDetails(Guid CartId);
        int DeleteItem(Guid cartId, int itemId);
        int UpdateQuantity(Guid cartId, int itemId, int Quantity);
        int UpdateCart(Guid cartId, int userId);
    }
}