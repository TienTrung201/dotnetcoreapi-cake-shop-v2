using dotnetcoreapi.cake.shop.domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnetcoreapi.cake.shop.infrastructure
{
    public abstract class BaseRepository<TEntity> : BaseReadOnlyRepository<TEntity>, IBaseRepository<TEntity> where TEntity : class
    {
        #region Constructors
        public BaseRepository(CakeShopContext context) : base(context)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Thêm mới một bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần thêm</param>
        /// <returns>Số bản ghi được thêm thành công</returns>
        public virtual async Task<TEntity> CreateEntityAsync(TEntity entity)
        {
            await _context.AddAsync<TEntity>(entity);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("Không thể thêm bản ghi");
            }

            return entity;
        }

        /// <summary>
        /// Cập nhật một bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần cập nhật</param>
        /// <returns>Số bản ghi cập nhật thành công</returns>
        public virtual async Task<TEntity> UpdateEntityAsync(TEntity entity)
        {
            _context.Update<TEntity>(entity);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("Không thể cập nhật bản ghi");
            }

            return entity;
        }

        /// <summary>
        /// Xoá một bản ghi
        /// </summary>
        /// <param name="entity">Đối tượng cần xoá</param>
        /// <returns>Số bản ghi xoá thành công</returns>
        public virtual async Task<TEntity> DeleteEntityAsync(TEntity entity)
        {
            _context.Remove<TEntity>(entity);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
            {
                throw new Exception("Không thể xoá bản ghi");
            }

            return entity;
        } 
        #endregion
    }
}
