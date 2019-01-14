using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiAmor.Common
{
    public class SimpleViewItem : IComparable
    {
        public int Id { get; set; }
        public string Display { get; set; }


        public int CompareTo(object obj)
        {
            return this.Display.CompareTo((obj as SimpleViewItem).Display);
        }

        public override string ToString()
        {
            return this.Display;
        }

    }
}
