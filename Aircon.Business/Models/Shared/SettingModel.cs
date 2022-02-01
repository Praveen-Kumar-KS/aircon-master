using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.Shared
{
    public class SettingModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public int UserId { get; set; }
    }

    public interface ISettings { }

}
