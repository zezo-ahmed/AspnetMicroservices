﻿using MediatR;
using AutoMapper;
using Ordering.Domain.Entities;
using Ordering.Application.Models;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Contracts.Infrastructure;

namespace Ordering.Application.Features.Orders.Commands.ChechoutOrder
{
    public class ChechoutOrderCommandHandler : IRequestHandler<ChechoutOrderCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<ChechoutOrderCommandHandler> _logger;

        public ChechoutOrderCommandHandler(IMapper mapper, IEmailService emailService, IOrderRepository orderRepository, ILogger<ChechoutOrderCommandHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<int> Handle(ChechoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);
            var newOrder = await _orderRepository.AddAsync(orderEntity);

            _logger.LogInformation($"Order {newOrder.Id} is successfully created.");

            await SendEmail(newOrder);

            return newOrder.Id;
        }
        private async Task SendEmail(Order order)
        {
            var email = new Email() { To = "ezozkme@gmail.com", Body = "Order was created.", Subject = "Order was created." };
            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Order {order.Id} failed due to an error with the mail service {ex.Message}");
            }
        }
    }
}
