using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Data.Enums
{
    public enum BookingDocumentType : int
    {
        [Description("Bill of Lading")]
        BillOfLading = 0,
        [Description("Photos")]
        Photos = 1,
        [Description("Documents")]
        Documents = 2,
        [Description("Packing List")]
        PackingList = 3,
        [Description("Commercial Invoice")]
        CommercialInvoice = 4,
        [Description("Other")]
        Other = 5
    }
}
