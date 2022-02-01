using Aircon.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.Shared
{
    public class GenericAttributeModel : BaseModel
    {
        public int EntityId { get; set; }
        public string KeyGroup { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
