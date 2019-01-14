using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;
using System.Collections.Generic;

namespace MiAmor.Services
{
    public interface IEventPostTagService : IEntityService<EventPostTag>
    {
        EventPostTag GetById(int Id);
        PagedDataResponse<EventPostTag> GetFilteredEventPostTags(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions); 
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class EventPostTagService : EntityService<EventPostTag>, IEventPostTagService
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public EventPostTagService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<EventPostTag>();

        }

        #endregion

        #region Methods

     
        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public EventPostTag GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<EventPostTag> GetFilteredEventPostTags(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<EventPostTag> EventPostTags;
            int TotalItems = 0;
            if (FilterElements != null)
            {
                IQueryable<EventPostTag> query = _dbset;
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
                            case "EventPostCount":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.EventPostCount).Contains(filter.MyValue));
                                break;
                            default:
                                EventPostTags = (from c in _dbset
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
                EventPostTags = query.ToList();
            }
            else
            {
                EventPostTags = (from c in _dbset
                              select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<EventPostTag> PagedDataResponse = new PagedDataResponse<EventPostTag>(TotalItems, EventPostTags.Count(), EventPostTags, 0);
            return PagedDataResponse;
        }

        #endregion
        
    }

}
