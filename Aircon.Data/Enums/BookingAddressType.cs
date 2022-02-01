using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Enums
{
    public enum BookingAddressType 
    {
        [Description("Shipper/Exporter")]
        ShipperOrExporter = 0,
        [Description("Consignee/Importer")]
        ConsigneeOrImporter = 1,
        [Description("PickUp")]
        IsPickUp = 2
    }
}
