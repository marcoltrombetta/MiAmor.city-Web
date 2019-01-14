using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiAmor.Web.Area
{
    public static class EfFilterHelper
    {
        public static object GetValueByColType(string Value,string ColType)
        {
            switch (ColType)
            {
                case "Int32":
                    return Convert.ToInt32(Value);

                case "decimal":
                    return Convert.ToDecimal(Value);

                case "double":
                    return Convert.ToDouble(Value);

                case "DateTime":
                    return Convert.ToDateTime(Value);

            }
            return Value;

        }
    }
}