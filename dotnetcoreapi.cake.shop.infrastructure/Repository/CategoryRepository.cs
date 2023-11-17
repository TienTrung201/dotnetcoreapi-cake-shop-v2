using dotnetcoreapi.cake.shop.domain;
using dotnetcoreapi.cake.shop.infrastructure;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreapi.cake.shop.infrastructure
{
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(CakeShopContext context) : base(context)
        {
        }
    }
}
