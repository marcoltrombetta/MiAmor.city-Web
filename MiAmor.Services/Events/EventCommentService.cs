using System;
using System.Linq;
using MiAmor.Core;
using MiAmor.Services;
using MiAmor.Data;
using System.Collections.Generic;

namespace MiAmor.Services
{
    public interface IEventCommentService : IEntityService<EventComment>
    {
        EventComment GetById(int Id);
        PagedDataResponse<EventComment> GetFilteredEventComments(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions); 
    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class EventCommentService : EntityService<EventComment>, IEventCommentService
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public EventCommentService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<EventComment>();

        }

        #endregion

        #region Methods

     
        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public EventComment GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<EventComment> GetFilteredEventComments(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<EventComment> EventComments;
            int TotalItems = 0;
            if (FilterElements != null)
            {
                IQueryable<EventComment> query = _dbset;
                foreach (FilterKeyValue filter in FilterElements)
                {
                    if (filter.MyKey != null && filter.MyValue != null)
                    {
                        switch (filter.MyKey)
                        {
                            case "Id":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.Id).Contains(filter.MyValue));
                                break;
                            case "CustomerId":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.CustomerId).Contains(filter.MyValue));
                                break;
                            case "CommentText":
                                query = query.Where(c => c.CommentText.Contains(filter.MyValue));
                                break;
                            case "EventPostId":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.EventPostId).Contains(filter.MyValue));
                                break;
                            default:
                                EventComments = (from c in _dbset
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
                EventComments = query.ToList();
            }
            else
            {
                EventComments = (from c in _dbset
                              select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<EventComment> PagedDataResponse = new PagedDataResponse<EventComment>(TotalItems, EventComments.Count(), EventComments, 0);
            return PagedDataResponse;
        }


        #endregion
        
    }

}
