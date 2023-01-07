using System;
using System.Net.Http;
using AspnetRunBasics.Models;
using System.Threading.Tasks;
using AspnetRunBasics.Extensions;
using System.Collections.Generic;

namespace AspnetRunBasics.Services
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _client;

        public OrderService(HttpClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<IEnumerable<OrderResponseModel>> GetOrdersByUserName(string userName)
        {
            var response = await _client.GetAsync($"/Order/userName/{userName}");

            return await response.ReadContentAs<List<OrderResponseModel>>();
        }
    }
}
