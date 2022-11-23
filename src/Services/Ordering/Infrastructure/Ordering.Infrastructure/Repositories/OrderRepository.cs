using Ordering.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Ordering.Infrastructure.Persistence;
using Ordering.Application.Contracts.Persistence;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(OrderContext context) : base(context)
        {

        }
        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            return await _context.Orders.Where(o => o.UserName == userName).ToListAsync();
        }
    }
}
