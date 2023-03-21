using Discount.API.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Threading.Tasks;
using Dapper;

namespace Discount.API.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        // Note:
        // Here all CRUD queries written in my old ways; except for GetDiscount method(this is Mehmet(Tutor) way)

        private readonly IConfiguration _configuration;

        public DiscountRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Coupon> GetDiscount(string prodcutName)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetConnectionString("DiscountDbConn"));
            //("Server=discountdb;Port=5432;Database=Discountdb;User Id=admin;Password=Newuser@1234;");
            //(_configuration.GetValue<string>("DatabaseSettings.ConnectionString")); // very previous connection


            var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
                ("Select * from coupon where productname = @ProdcutName", new { ProdcutName = prodcutName });

            if (coupon == null)
                return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc"};
            
            return coupon;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetConnectionString("DiscountDbConn"));
            
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            var id = coupon.Id;
            var productName = coupon.ProductName;
            var productDesc = coupon.Description;
            var amount = coupon.Amount;

            var affected= await connection.ExecuteAsync
                ("insert into Coupon(id, productname, description, amount) values ('" + id + "', '" + productName + "', '" + productDesc + "', '" + amount + "')");

            if (affected == 0)
                return false;
            
            return true;
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetConnectionString("DiscountDbConn"));

            var id = coupon.Id;
            var productName = coupon.ProductName;
            var productDesc = coupon.Description;
            var amount = coupon.Amount;

            var affected = await connection.ExecuteAsync
                ("Update Coupon Set ProductName='" + productName + "', Description= '" + productDesc + "', Amount= '"+ amount +"' where Id = '"+ id +"'");

            if (affected == 0)
                return false;

            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var connection = new NpgsqlConnection
                (_configuration.GetConnectionString("DiscountDbConn"));
            
            var affected = await connection.ExecuteAsync
                ("delete from coupon where productname = '" + productName + "'");

            if (affected == 0)
                return false;

            return true;
        }
    }
}
