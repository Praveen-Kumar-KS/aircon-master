using System.IO;
using System.Runtime.Serialization.Json;

namespace Aircon.Helper
{
    public static class JsonSerializer
    {
        public static T Deserialize<T>(Stream stream)
        {
            return (T)(new DataContractJsonSerializer(typeof(T))).ReadObject(stream);
        }

        public static void Serialize<T>(Stream stream, T root)
        {
            (new DataContractJsonSerializer(typeof(T))).WriteObject(stream, root);
        }
    }
}
