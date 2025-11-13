using System.Runtime.Serialization.Json;

namespace AgentClient.Infrastructure.Extensions
{
    public static class SerializerExtension
    {
        public static byte[] Serialise<T>(this T data)
        {
            var serialiser = new DataContractJsonSerializer(typeof(T));

            using (var ms = new MemoryStream())
            {
                serialiser.WriteObject(ms, data);
                return ms.ToArray();
            }
        }

        public static T Deserialise<T>(this byte[] data)
        {
            var deserialiser = new DataContractJsonSerializer(typeof(T));

            using (var ms = new MemoryStream(data))
            {
               return (T)deserialiser.ReadObject(ms);
            }
        }
    }
}
