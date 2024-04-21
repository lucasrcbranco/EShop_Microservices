namespace Discount.Grpc.Services;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName.ToUpper() == request.ProductName.ToUpper());

        if (coupon is null)
        {
            return new CouponModel { ProductName = "No Discount", Amount = 0, Description = "No Discount Available" };
        }

        logger.LogInformation("Discount is retrieved for ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var couponExistsByName = await dbContext.Coupons.AnyAsync(c => c.ProductName.ToUpper() == request.Coupon.ProductName.ToUpper());
        if (couponExistsByName)
        {
            throw new RpcException(new Status(StatusCode.AlreadyExists, "ProductName already taken"));
        }

        var coupon = request.Coupon.Adapt<Coupon>();
        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request Object"));
        }

        await dbContext.Coupons.AddAsync(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully created for ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);

        return coupon.Adapt<CouponModel>();
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var couponExistsById = await dbContext.Coupons.AnyAsync(c => c.Id == request.Coupon.Id);
        if (!couponExistsById)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Invalid Request Object"));
        }

        var couponExistsByName = await dbContext.Coupons.AnyAsync(c => c.ProductName.ToUpper() == request.Coupon.ProductName.ToUpper() && c.Id != request.Coupon.Id);
        if (couponExistsByName)
        {
            throw new RpcException(new Status(StatusCode.AlreadyExists, "ProductName already taken"));
        }

        var updatedCoupon = request.Coupon.Adapt<Coupon>();
        if (updatedCoupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid Request Object"));
        }


        dbContext.Coupons.Update(updatedCoupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully updated for ProductName : {productName}, Amount : {amount}", updatedCoupon.ProductName, updatedCoupon.Amount);

        return updatedCoupon.Adapt<CouponModel>();
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.ProductName.ToUpper() == request.ProductName.ToUpper());

        if (coupon is null)
        {
            throw new RpcException(new Status(StatusCode.NotFound, "Invalid Request Object"));
        }


        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully removed for ProductName : {productName}, Amount : {amount}", coupon.ProductName, coupon.Amount);

        return new DeleteDiscountResponse { Success = true };
    }
}
