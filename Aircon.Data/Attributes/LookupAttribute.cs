using System;
using System.Collections.Generic;
using System.Text;

namespace Aircon.Data.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class LookupAttribute : Attribute
    {
        public LookupAttribute(string route)
        {
            Route = route;
        }
        public string Route { get; set; }
    }
}
