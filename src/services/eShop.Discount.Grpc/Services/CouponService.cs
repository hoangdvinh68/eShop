using Dapper;
using Discount.API.Protos;
using eShop.Discount.Grpc.Domain.Entities;
using Grpc.Core;
using Npgsql;

namespace eShop.Discount.Grpc.Services;

public class CouponService : CouponGrpcService.CouponGrpcServiceBase
{
    private readonly string _connectionString;

    public CouponService(IConfiguration configuration)
    {
        _connectionString = configuration.GetValue<string>("ConnectionStrings:PostgresConnection");
    }

    public override async Task<ListCouponModel> List(ListCouponRequest request, ServerCallContext context)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        
        var query = await connection.QueryAsync<Coupon>("SELECT * FROM Coupon");
        
        var response = new ListCouponModel();
        
        response.CouponModel.AddRange(query.Select(x => new CouponModel()
        {
            Id = x.Id.ToString(),
            Description = x.Description,
            Amount = x.Amount,
            ProductName = x.ProductName
        }));
        
        return response;
    }

    public override async Task<CouponModel> Get(GetCouponRequest request, ServerCallContext context)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
            ("SELECT * FROM Coupon WHERE Id = @ProductName", new { ProductName = request.ProductName });

        if (coupon == null) return default!;
        
        return new CouponModel()
        {
            Id = coupon.Id.ToString(),
            Description = coupon.Description,
            Amount = coupon.Amount,
            ProductName = coupon.ProductName
        };
    }
}