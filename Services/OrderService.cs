using OOPShop.Models;
using OOPShop.Repositories.Interfaces;
using OOPShop.Services.Interfaces;

namespace OOPShop.Services
{
    public class OrderService : IOrderService
    {
        IOrderItemRepository orderItemRepository;
        IOrderRepository orderRepository;
        AuthUser authUser;

        public OrderService(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository, AuthUser authUser)
        {
            this.authUser = authUser;
            this.orderRepository = orderRepository;
            this.orderItemRepository = orderItemRepository;
        }

        public void Add(Order order)
        {
            orderRepository.Add(order);
        }

        public void Cancel(Order order)
        {
            order.Status = OrderStatus.Cancelled;
            orderRepository.Save();
        }

        public void Complete(Order order)
        {
            order.Status = OrderStatus.Completed;
            orderRepository.Save();
        }

        public bool Delete(int id)
        {
            return orderRepository.Delete(id);
        }

        public List<Order> GetAll()
        {
            return orderRepository.GetAll();
        }

        public List<OrderItem> GetAllItems(Order order)
        {
            return orderRepository.GetAllItems(order);
        }

        public bool Order(Product product, int quantity)
        {
            // price of the last added products
            double totalItemsPrice = product.Price * quantity;

            // creating a new order / getting already existing open order of the current user
            // add total price of the products which are passed as an argument
            Order order = GetOpenOrder(authUser.User);
            double totalSum = order.TotalSum + totalItemsPrice;

            if (totalSum > authUser.User.Balance)
            {
                return false;
            }

            order.TotalSum = totalSum;
            orderRepository.Save(order);

            // creating order item for the ordered mentioned above
            OrderItem orderItem = new OrderItem();
            orderItem.Quantity = quantity;
            orderItem.OrderId = order.Id;
            orderItem.ProductId = product.Id;
            orderItemRepository.Save(orderItem);

            return true;
        }

        public List<Order> GetAllOrders(User user)
        {
            return orderRepository.GetAllOrders(user);
        }

        public Order GetOpenOrder(User user)
        {
            Order? order = orderRepository.GetOpenOrder(user);
            if (order is null)
            {
                order = new Order();
                order.Status = OrderStatus.Open;
                order.UserId = user.Id;
            }
            return order;
        }

        public Order? GetById(int id)
        {
            return orderRepository.GetById(id);
        }

        public void Save()
        {
            orderRepository.Save();
        }
    }
}
