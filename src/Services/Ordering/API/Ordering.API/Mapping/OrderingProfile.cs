using AutoMapper;
using EventBus.Messages.Events;
using Ordering.Application.Features.Orders.Commands.ChechoutOrder;

namespace Ordering.API.Mapping
{
    public class OrderingProfile : Profile
    {
        public OrderingProfile()
        {
            CreateMap<ChechoutOrderCommand, BasketCheckoutEvent>().ReverseMap();
        }
    }
}
