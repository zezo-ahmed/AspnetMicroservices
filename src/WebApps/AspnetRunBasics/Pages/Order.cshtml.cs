using System;
using AspnetRunBasics.Models;
using System.Threading.Tasks;
using AspnetRunBasics.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspnetRunBasics
{
    public class OrderModel : PageModel
    {
        private readonly IOrderService _orderService;

        public OrderModel(IOrderService orderService)
        {
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        }

        public IEnumerable<OrderResponseModel> Orders { get; set; } = new List<OrderResponseModel>();

        public async Task<IActionResult> OnGetAsync()
        {
            Orders = await _orderService.GetOrdersByUserName("ahmed");

            return Page();
        }
    }
}