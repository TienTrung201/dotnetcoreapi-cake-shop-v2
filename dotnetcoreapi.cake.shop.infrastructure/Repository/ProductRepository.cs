using dotnetcoreapi.cake.shop.domain;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi.cake.shop.infrastructure
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(CakeShopContext context) : base(context)
        {
        }

        /// <summary>
        /// Lấy toàn bộ danh sách
        /// </summary>
        /// <returns></returns>
        public override IQueryable<Product> GetAllEntities()
        {
            var allProducts = _context.Products
                                .Include(p => p.Category)
                                .Include(p => p.Images)
                                .AsQueryable();

            return allProducts;
        }

        /// <summary>
        /// Lấy một sản phẩm theo ID
        /// </summary>
        /// <param name="productId">ID của sản phẩm</param>
        /// <returns></returns>
        public override async Task<Product> GetEntityByIdAsync(int productId)
        {
            var product = await _context.Products
                                .Include(p => p.Category)
                                .Include(p => p.Images)
                                .FirstOrDefaultAsync(p => p.ProductId == productId);

            if (product == null)
            {
                throw new NotFoundException("Sản phẩm không tồn tại", ErrorCode.NotFound);
            }

            return product;
        }

        /// <summary>
        /// Kiểm tra một sản phẩm đã từng được bán chưa
        /// </summary>
        /// <param name="productId">ID sản phẩm</param>
        /// <returns></returns>
        public async Task<int> HasOrders(int productId)
        {
            var hasOrders = await _context.OrderItems.CountAsync(oi => oi.ProductId == productId);

            return hasOrders;
        }
    }
}
