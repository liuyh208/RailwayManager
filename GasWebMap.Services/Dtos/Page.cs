using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GasWebMap.Services.Dtos
{
    public class PageData<TEntity>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the  class.
        /// </summary>
        /// <param name="entites">提取的分页数据</param>
        /// <param name="totalCount">实体的总数</param>
        public PageData(IEnumerable<TEntity> entites, int totalCount)
        {
            rows = entites;
            total = totalCount;
        }

        /// <summary>
        /// </summary>
        /// <param name="entites">提取的分页数据</param>
        public PageData(IEnumerable<TEntity> entites)
            : this(entites, entites.Count())
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        ///     分页数据
        /// </summary>
        /// <value>The result.</value>
        public IEnumerable<TEntity> rows { get; private set; }

        /// <summary>
        ///     结果总数
        /// </summary>
        /// <value>The total count.</value>
        public int total { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        ///     计算页数
        /// </summary>
        /// <param name="pageSize">页大小</param>
        /// <returns>System.Int32.</returns>
        public int TotalPages(int pageSize)
        {
            return (int)Math.Ceiling(Convert.ToDouble(total) / pageSize);
        }

        #endregion Methods
    }
}
