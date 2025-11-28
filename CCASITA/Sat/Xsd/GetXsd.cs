using System.Xml;
using System.Reflection;

namespace LaCasita.Sat.Xsd
{


    public class GetXsd
    {
        public GetXsd(string resourceName)
        {
            var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("LaCasita.Sat.Xsd." + resourceName);
            if (stream == null) return;
            XmlReader = XmlReader.Create(stream);
            while (XmlReader.Read())
            {
                if (!XmlReader.IsStartElement()) continue;
                if (XmlReader.Name != "xs:schema") continue;
                NameSpace = XmlReader["targetNamespace"];
                break;
            }
        }
        public string NameSpace { get; private set; }
        public XmlReader XmlReader { get; private set; }
    }



}
