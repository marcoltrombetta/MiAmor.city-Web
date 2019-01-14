using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiAmor.Core;
using MiAmor.Data;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity;

namespace MiAmor.Services
{
    public interface IEventPostService : IEntityService<EventPost>
    {
        EventPost GetById(int Id);
        PagedDataResponse<EventPost> GetFilteredEventPosts(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions);
        List<EventPost> GetEventPostByPartialName(string PartialName, int NumOfRows);

    }
    /// <summary>
    /// Affiliate service
    /// </summary>
    public partial class EventPostService : EntityService<EventPost>, IEventPostService
    {
        #region Fields
        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="AddressRepository">Address repository</param>
        /// <param name="eventPublisher">Event published</param>
        public EventPostService(IContext context)
            : base(context)
        {
            _context = context;
            _dbset = _context.Set<EventPost>();

        }

        #endregion

        #region Methods

     
        /// <summary>
        /// Gets an Address by Address identifier
        /// </summary>
        /// <param name="AddressId">Address identifier</param>
        /// <returns>Address</returns>
        public EventPost GetById(int Id)
        {
            return _dbset.FirstOrDefault(x => x.Id == Id);
        }

        public PagedDataResponse<EventPost> GetFilteredEventPosts(List<FilterKeyValue> FilterElements, GridPageOptions PageOptions)
        {
            List<EventPost> EventPosts;
            int TotalItems = 0;
            if (FilterElements != null)
            {
                IQueryable<EventPost> query = _dbset;
                foreach (FilterKeyValue filter in FilterElements)
                {
                    if (filter.MyKey != null && filter.MyValue != null)
                    {
                        switch (filter.MyKey)
                        {
                            case "Id":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.Id).Contains(filter.MyValue));
                                break;
                            case "Title":
                                query = query.Where(c => c.Title.Contains(filter.MyValue));
                                break;
                            case "CommentCount":
                                query = query.Where(c => System.Data.Entity.SqlServer.SqlFunctions.StringConvert((double)c.CommentCount).Contains(filter.MyValue));
                                break;
                            case "StartDateUtc":
                                try
                                {
                                    DateTime dt = DateTime.ParseExact(filter.MyValue, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                                    query = query.Where(c => EntityFunctions.TruncateTime(c.StartDateUtc) == EntityFunctions.TruncateTime(dt));
                                }
                                catch (Exception e)
                                { 
                                
                                }
                                break;
                            default:
                                EventPosts = (from c in _dbset
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
                EventPosts = query.ToList();
            }
            else
            {
                EventPosts = (from c in _dbset
                                select c).OrderBy(c => c.Id)
                                .Skip((PageOptions.PageNumber - 1) * PageOptions.PageSize).Take(PageOptions.PageSize).ToList();
                TotalItems = _dbset.Count();
            }
            PagedDataResponse<EventPost> PagedDataResponse = new PagedDataResponse<EventPost>(TotalItems, EventPosts.Count(), EventPosts, 0);
            return PagedDataResponse;
        }

        public List<EventPost> GetEventPostByPartialName(string PartialName, int NumOfRows)
        {
            List<EventPost> EventPosts = (from v in _dbset
                                 where v.Title.Contains(PartialName)
                                 select v).OrderBy(c => c.Title).Take(NumOfRows).ToList();
            return EventPosts;
        }

        #endregion
        
    }

}
