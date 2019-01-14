using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Common
{
    public abstract class SelectableItemBase
    {
        public TypeEnum Id { get; set; }
        public string Display { get; set; }

        protected List<SimpleViewItem> _items;
        public enum TypeEnum { };


        public SelectableItemBase()
        {
            _items = new List<SimpleViewItem>();
        }


        protected void AddItem(int index, string display)
        {
            _items.Add(new SimpleViewItem() { Id = index, Display = display });
        }


        public IEnumerable<SimpleViewItem> GetViewItemsList(string placeholderDisplay)
        {
            var result = GetViewItemsList();
            if (result != null && result.Count() > 0)
            {
                // Insert placeholder item:
                var list = result.ToList();
                list.Insert(0, new SimpleViewItem() { Id = -1, Display = placeholderDisplay });

                result = list;

            }

            return result;
        }


        public IEnumerable<SimpleViewItem> GetViewItemsList()
        {
            return _items;
        }

        public SimpleViewItem GetItemById(int id)
        {
            return _items.Where(i => (int)(i.Id) == id).FirstOrDefault();
        }

        public string GetDisplayById(int id)
        {
            var item = GetItemById(id);
            if (item != null)
            {
                return item.Display;
            }
            return null;

        }



    }
}
