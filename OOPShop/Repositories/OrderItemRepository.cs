using OOPShop.Repositories.Interfaces;
using OOPShop.Models;
using OOPShop.Data;

namespace OOPShop.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        AbstractApplicationDbContext db;

        // DI
        public OrderItemRepository(AbstractApplicationDbContext db)
        {
            this.db = db;
        }

        public void Add(OrderItem entity)
        {
            db.OrderItems.Add(entity);
            db.SaveChanges();
        }

        public bool Delete(int id)
        {
            bool deleted = false;
            var orderItem = db.OrderItems.FirstOrDefault(o => o.Id == id);

            if (orderItem != null)
            {
                db.OrderItems.Remove(orderItem);
                db.SaveChanges();
                deleted = true;
            }
            return deleted;
        }

        public List<OrderItem> GetAll()
        {
            return db.OrderItems.ToList();
        }

        public OrderItem? GetById(int id)
        {
            OrderItem? orderItem = db.OrderItems.FirstOrDefault(o => o.Id == id);
            return orderItem;
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public void Save(OrderItem orderItem)
        {
            if (GetById(orderItem.Id) == null)
                db.Add(orderItem);
            db.SaveChanges();
        }
    }
}
