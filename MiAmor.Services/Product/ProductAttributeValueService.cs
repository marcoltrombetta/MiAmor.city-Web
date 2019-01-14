﻿using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;

namespace MiAmor.Services
{
    public interface IProductAttributeValueService : IEntityService<ProductAttributeValue>
    {
        ProductAttributeValue GetById(int Id);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class ProductAttributeValueService : EntityService<ProductAttributeValue>, IProductAttributeValueService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public ProductAttributeValueService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<ProductAttributeValue>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public ProductAttributeValue GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        #endregion

    }

}