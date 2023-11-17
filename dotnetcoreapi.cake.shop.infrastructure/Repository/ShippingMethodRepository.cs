using dotnetcoreapi.cake.shop.domain;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi.cake.shop.infrastructure
{
    public class ShippingMethodRepository : BaseRepository<ShippingMethod>, IShippingMethodRepository
    {
        public ShippingMethodRepository(CakeShopContext context) : base(context)
        {
        }

        /// <summary>
        /// Lấy đơn vị vận chuyển mặc định
        /// </summary>
        /// <returns></returns>
        public async Task<List<ShippingMethod>> GetDefaultShippingMethodsAsync()
        {
            var defaultShippingMethods = await _context.ShippingMethods
                                                .Where(s => s.IsDefault == true)
                                                .ToListAsync();

            return defaultShippingMethods;
        }

        /// <summary>
        /// Cập nhật danh sách đơn vị vận chuyển
        /// </summary>
        /// <param name="shippingMethods">Danh sách bản ghi cần cập nhật</param>
        /// <returns></returns>
        public async Task<List<ShippingMethod>> UpdateShippingMethodsAsync(List<ShippingMethod> shippingMethods)
        {
            _context.ShippingMethods.UpdateRange(shippingMethods);
            var result = await _context.SaveChangesAsync();

            if(result == 0)
            {
                throw new Exception("Không thể cập nhật bản ghi");
            }

            return shippingMethods;
        }
    }
}
