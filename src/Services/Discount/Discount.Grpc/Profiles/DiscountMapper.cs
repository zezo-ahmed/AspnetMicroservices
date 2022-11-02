using AutoMapper;
using Discount.Grpc.Protos;
using Discount.Grpc.Entities;

namespace Discount.Grpc.Profiles
{
    public class DiscountMapper : Profile
    {
        public DiscountMapper()
        {
            CreateMap<Coupon, CouponModel>().ReverseMap();
        }
    }
}
