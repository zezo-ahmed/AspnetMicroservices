using Grpc.Core;
using AutoMapper;
using Discount.Grpc.Protos;
using Discount.Grpc.Entities;
using Discount.Grpc.Repositories;

namespace Discount.Grpc.Services
{
    public class DiscountService : DiscountProtoService.DiscountProtoServiceBase
    {
        private readonly IMapper _mapper;
        private readonly IDiscountRepository _repository;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IMapper mapper, IDiscountRepository repository, ILogger<DiscountService> logger)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async override Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetDiscount(request.ProductName);

            if(coupon == null)
            {
                _logger.LogError($"Discount with ProductName: {request.ProductName} is not found");

                throw new RpcException(new Status(StatusCode.NotFound, $"Discount with ProductName: {request.ProductName} is not found"));
            }

            _logger.LogInformation($"Discount is retrieved for ProductName : {coupon.ProductName}, Amount : {coupon.Amount}");

            return _mapper.Map<CouponModel>(coupon);
        }

        public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _repository.CreateDiscount(coupon);
            _logger.LogInformation($"Discount is successfully created. ProductName : {coupon.ProductName}");

            return _mapper.Map<CouponModel>(coupon);
        }

        public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request.Coupon);

            await _repository.UpdateDiscount(coupon);
            _logger.LogInformation($"Discount is successfully updated. ProductName : {coupon.ProductName}");

            return _mapper.Map<CouponModel>(coupon);
        }

        public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var deleted = await _repository.DeleteDiscount(request.ProductName);
            _logger.LogInformation($"Discount is successfully deleted. ProductName : {request.ProductName}");

            var response = new DeleteDiscountResponse { Success = deleted };

            return response;
        }
    }
}
