using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WebScrapingPOC.Utility
{
    public class XMLUtility
    {
        public static string ObjectToXML<T>(T vehicleSearchResult)
        {
            XmlSerializer xsSubmit = new XmlSerializer(typeof(T));

            using var sw = new StringWriter();
            using var writer = XmlWriter.Create(sw, new XmlWriterSettings { OmitXmlDeclaration = true });
            xsSubmit.Serialize(writer, vehicleSearchResult);
            return sw.ToString(); 
        }
    }
}
