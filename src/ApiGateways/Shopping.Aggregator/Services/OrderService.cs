﻿using Shopping.Aggregator.Models;
using Shopping.Aggregator.Extensions;

namespace Shopping.Aggregator.Services
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
            var response = await _client.GetAsync($"api/v1/Order/userName/{userName}");

            return await response.ReadContentAs<List<OrderResponseModel>>();
        }
    }
}
