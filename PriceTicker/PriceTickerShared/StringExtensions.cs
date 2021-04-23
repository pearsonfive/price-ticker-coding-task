using System.IO;
using System.Xml.Serialization;

namespace PriceTickerShared
{
    public static class StringExtensions
    {
        public static T Deserialise<T>(this string xml)
        {
            var serialiser = new XmlSerializer(typeof(T));
            using (var reader = new StringReader(xml))
            {
                return (T)serialiser.Deserialize(reader);
            }
        }

        public static string Serialise<T>(this T obj)
        {
            var serialiser = new XmlSerializer(typeof(T));
            using (var writer = new StringWriter())
            {
                serialiser.Serialize(writer, obj);
                return writer.ToString();
            }
        }
    }
}