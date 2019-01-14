using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;
using System.Collections.Generic;

namespace MiAmor.Services
{
    public interface IManufacturerService : IEntityService<Manufacturer>
    {
        Manufacturer GetById(int Id);
        PagedDataResponse<Manufacturer> GetFilteredManufacturers(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class ManufacturerService : EntityService<Manufacturer>, IManufacturerService
    {
        #region Fields


        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public ManufacturerService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<Manufacturer>();

        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public Manufacturer GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<Manufacturer> GetFilteredManufacturers(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<Manufacturer> Manufacturers;
            int TotalItems = 0;
            if (FilterElements != null)
            {

                IQueryable<Manufacturer> query = _dbset;
                foreach (FilterKeyValue filter in FilterElements)
                {
                    if (filter.MyKey != null && filter.MyValue != null)
                    {
                        switch (filter.MyKey)
                        {
                            case "Id":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.Id).Contains(filter.MyValue));
                                break;
                            case "Name":
                                query = query.Where(c => c.Name.Contains(filter.MyValue));
                                break;
                            case "Description":
                                query = query.Where(c => c.Description.Contains(filter.MyValue));
                                break;
                            case "MetaKeywords":
                                query = query.Where(c => c.MetaKeywords.Contains(filter.MyValue));
                                break;
                            case "MetaDescription":
                                query = query.Where(c => c.MetaDescription.Contains(filter.MyValue));
                                break;
                            case "MetaTitle":
                                query = query.Where(c => c.MetaTitle.Contains(filter.MyValue));
                                break;
                            case "PageSize":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.PageSize).Contains(filter.MyValue));
                                break;
                            case "Published":
                                bool boolPublished;
                                if (filter.MyValue == "1")
                                {
                                    boolPublished = true;
                                }
                                else
                                {
                                    boolPublished = false;
                                }
                                query = query.Where(c => c.Published == boolPublished);
                                break;
                            case "DisplayOrder":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.DisplayOrder).Contains(filter.MyValue));
                                break;

                            default:
                                Manufacturers = (from c in _dbset
                                            select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                                break;
                        }
                    }
                }
                query = query
                    .OrderBy(c => c.Id)
                    .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize);
                TotalItems = _dbset.Count();
                Manufacturers = query.ToList();
            }
            else
            {
                Manufacturers = (from c in _dbset
                            select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<Manufacturer> PagedDataResponse = new PagedDataResponse<Manufacturer>(TotalItems, Manufacturers.Count(), Manufacturers, 0);
            return PagedDataResponse;
        }
        #endregion

    }

}
