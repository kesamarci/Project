using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Models.Attributumos
{
    public class Attri
    {
        [AttributeUsage(AttributeTargets.Class)]
        public class ToExportAttribute : Attribute
        {
            public string ElementName { get; }

            
            public ToExportAttribute(string elementName)
            {
                ElementName = elementName;
            }
        }
    }
}
