using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace OMS.Entities
{
    public static class EntityHelper
    {
        public static XElement SqlSerializeEntity<T>(T objEntity)
        {
            return EntityHelper.SqlSerializeEntityWithFormat<T>(objEntity, "yyyy-MM-dd hh:mm:ss tt");
        }
        public static XElement SqlSerializeEntityWithFormat<T>(T objEntity, string dateFormat)
        {
            XElement xElement = JsonConvert.DeserializeXmlNode(JsonConvert.SerializeObject(new JObject(new JProperty(objEntity.GetType().Name, JObject.FromObject(objEntity, new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore,
                Converters =
                {
                    new IsoDateTimeConverter
                    {
                        DateTimeFormat = dateFormat
                    },
                    new JsonBooleanTypeConverter(),
                    new JsonNullableStringConverter()
                }
            }))), Newtonsoft.Json.Formatting.None, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            }), "Root").ToXElement();             
            return xElement;
        }
        public static XElement ToXElement(this XmlNode node)
        {
            return node.ToXElement(LoadOptions.None);
        }
        public static XElement ToXElement(this XmlNode node, LoadOptions options)
        {
            XElement result;
            using (XmlNodeReader xmlNodeReader = new XmlNodeReader(node))
            {
                result = XElement.Load(xmlNodeReader, options);
            }
            return result;
        }

    }
}
