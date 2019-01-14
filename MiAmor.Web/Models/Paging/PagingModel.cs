using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Models
{
    public abstract class PagingModel
    {
        private int PageNumber;
        private int p1;
        private long p2;
        private int CategoryId1;

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }    
        public int TotalPages {
            get { 
            int _TotalPages=0;
            _TotalPages = TotalCount / PageSize;
            return _TotalPages; 
            }
        }
        public bool HasPreviousPage
        {
            get { return (PageIndex > 1); }
        }
        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }
        public List<int> PagesNum 
        {
        get { 
             //TODO add paging to page
            List<int> PagingList= new List<int> {};
            int i = 0;
            if (PageIndex == 1)
            {
                i = 1;
            }
            else if (PageIndex > 1 && PageIndex < TotalPages)
            {
                if (PageIndex == 2)
                {
                    i = PageIndex - 1;
                }
                else {
                    i = PageIndex - 2;
                }
            }
            else
            {
                i = TotalPages - 4;
            }
            for (int j=i ;j<= i+4; j++)
            {
                PagingList.Add(j);
            }
            return  PagingList;
            }
        }

        
    }
}