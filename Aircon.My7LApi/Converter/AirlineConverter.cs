using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.My7LApi.Converter
{
    public class AirlineConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Airline);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            var firstProperty = (JProperty)jo.First;
            var firstPropertyName = firstProperty.Name;

            //var array = (AirlineInfo[])jo["DFW-IAD"].ToObject(typeof(AirlineInfo[]));

            var array = (AirlineInfo[])jo[firstPropertyName].ToObject(typeof(AirlineInfo[]));

            Airline req = new Airline
            {
                AirlineInfo = array,
            };
            return req;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

}
