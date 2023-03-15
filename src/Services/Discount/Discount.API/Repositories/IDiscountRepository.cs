using Discount.API.Entities;
using System.Threading.Tasks;

namespace Discount.API.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string ProdcutName);

        Task<bool> CreateDiscount(Coupon coupon);

        Task<bool> UpdateDiscount(Coupon coupon);

        Task<bool> DeleteDiscount(string ProdcutName);
    }
}
