using OOPShop.Models;

namespace OOPShop.Repositories.Interfaces
{
    public interface IOrderRepository : IRepository<Order>
    {
        List<OrderItem> GetAllItems(Order order);
        List<Order> GetAllOrders(User user);
        Order? GetOpenOrder(User user);
    }
}
