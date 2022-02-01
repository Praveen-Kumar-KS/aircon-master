
using Aircon.My7LApi.Converter;
using Newtonsoft.Json;

public class Rootobject
{
    public Data data { get; set; }
}

public class Data
{
    public Results results { get; set; }
    public string message { get; set; }
}

public class Results
{
    public Vendors Vendors { get; set; }
    public Routing[] Routing { get; set; }
}

public class Vendors
{
    public object[] Pickup { get; set; }
    public object[] Delivery { get; set; }
    public Airline Airline { get; set; }
}

[JsonConverter(typeof(AirlineConverter))]
public class Airline
{
    [JsonProperty("AirlineInfo")]
    public AirlineInfo[] AirlineInfo { get; set; }
}
public class AirlineInfo
{
    //[JsonProperty("DFW-IAD")]
    //public DFWIAD[] DFWIAD { get; set; }

    public string AirlineName { get; set; }
    public string AirlineCode { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public bool ContractRates { get; set; }
    public bool CargoOnly { get; set; }
    public bool AllIn { get; set; }
    public Tariffinformation TariffInformation { get; set; }
    //public Widebodyrfsinformation[] WidebodyRFSInformation { get; set; }
    public Serviceinformation ServiceInformation { get; set; }
    public Totalcharges TotalCharges { get; set; }

}




public class DFWIAD
{
    public string AirlineName { get; set; }
    public string AirlineCode { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public bool ContractRates { get; set; }
    public bool CargoOnly { get; set; }
    public bool AllIn { get; set; }
    public Tariffinformation TariffInformation { get; set; }
    //public Widebodyrfsinformation[] WidebodyRFSInformation { get; set; }
    public Serviceinformation ServiceInformation { get; set; }
    public Totalcharges TotalCharges { get; set; }
}

public class Tariffinformation
{
    public string ValidFrom { get; set; }
    public string ValidTo { get; set; }
    public int PromoRate { get; set; }
    public int DimFactor { get; set; }
    public string Remarks { get; set; }
    public int Min { get; set; }
    public int Fixed { get; set; }
    public float Rate { get; set; }
}

public class Serviceinformation
{
    public string ServiceLevel { get; set; }
    public string ServiceType { get; set; }
    public string ServiceCode { get; set; }
    public string RateType { get; set; }
    public bool ZoneRate { get; set; }
    public string MaxWeight { get; set; }
}

public class Totalcharges
{
    public float Subtotal { get; set; }
    public float FuelAmount { get; set; }
    public float Security { get; set; }
    public float Screening { get; set; }
    public float Tax { get; set; }
    public float Total { get; set; }
}

//public class Widebodyrfsinformation
//{
//    public Connection[] Connections { get; set; }
//}

//public class Connection
//{
//    public string Origin { get; set; }
//    public string Destination { get; set; }
//    public Flight[] Flights { get; set; }
//}

//public class Flight
//{
//    public int Flight { get; set; }
//    public string Type { get; set; }
//    public Days Days { get; set; }
//}

//public class Days
//{
//    public bool Mon { get; set; }
//    public bool Tue { get; set; }
//    public bool Wed { get; set; }
//    public bool Thu { get; set; }
//    public bool Fri { get; set; }
//    public bool Sat { get; set; }
//    public bool Sun { get; set; }
//}

public class Routing
{
    public string OriginAirport { get; set; }
    public string DestinationAirport { get; set; }
    public float CheapestCost { get; set; }
    public string Remarks { get; set; }
}
