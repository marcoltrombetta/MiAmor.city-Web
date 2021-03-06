﻿using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;
using System.Collections.Generic;

namespace MiAmor.Services
{
    public interface IPriceListVendorCategoryService : IEntityService<PriceListVendorCategory>
    {
        PriceListVendorCategory GetById(int Id);

    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class PriceListVendorCategoryService : EntityService<PriceListVendorCategory>, IPriceListVendorCategoryService
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public PriceListVendorCategoryService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<PriceListVendorCategory>();

        }

        #endregion

        #region Methods

     
        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public PriceListVendorCategory GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        #endregion
        
    }

}
