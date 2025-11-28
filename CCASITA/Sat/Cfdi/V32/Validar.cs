using System;
using System.IO;
using System.Xml;
using System.Linq;
using System.Xml.Schema;
using System.Collections.Generic;

namespace LaCasita.Sat.Cfdi.V32
{

    public class Validar
    {

        private readonly TextReader _textReader;

        private readonly string _stringXml;


        private static List<string> _estructura = new List<string>();


        private static List<string> _totales = new List<string>();


        private static Comprobante _comprobante;

        public List<string> ErroresEstructura
        {
            get
            {
                return _estructura;
            }
        }

        public string ErroresTotales
        {
            get
            {
                return _totales.ToString();
            }
        }

        public Validar(string stringXml)
        {
            
            _stringXml = stringXml;
            _textReader = new StringReader(stringXml);
           
            Inicializar();
        }

        public Validar(FileSystemInfo fileInfo)
        {
            
            _stringXml = File.ReadAllText(fileInfo.FullName);
            _textReader = new StringReader(_stringXml);
            Inicializar();
        }

        public Validar(TextReader textReader)
        {
            
            _textReader = textReader;
            Inicializar();
        }

        public Validar(Comprobante comprobante)
        {

            _comprobante = comprobante;


        }

        private void Inicializar()
        {

            _estructura = new List<string>();

            _totales = new List<string>();

            _comprobante = Comprobante.Deserialize(_stringXml);

            ValidarEstructura();
            ValidarTotales();

        }

        private void ValidarTotales()
        {

            var subTotal = decimal.Round(_comprobante.Conceptos.Sum(x => x.Importe), 6);

            var importes = String.Empty;

            if (subTotal != _comprobante.SubTotal)
            {
                importes = _comprobante.Conceptos.Aggregate(importes, (current, a) => current + (a.Importe + " "));


                _totales.Add(importes + subTotal + " " + _comprobante.SubTotal);
            }

        }

        private void ValidarEstructura()
        {
            var xmlReaderSettings = new XmlReaderSettings { ValidationType = ValidationType.Schema };
            xmlReaderSettings.ValidationEventHandler += ValidationEventHandler;
            foreach (var xmlSchemaDefinition in GetXmlSchemaDefinitions32())
            {
                xmlReaderSettings.Schemas.Add(xmlSchemaDefinition.NameSpace, xmlSchemaDefinition.XmlReader);
            }
            var cfdi = XmlReader.Create(_textReader, xmlReaderSettings);
            while (cfdi.Read()) { }
        }

        private static void ValidationEventHandler(object sender, ValidationEventArgs validationEventArgs)
        {
            switch (validationEventArgs.Severity)
            {
                case XmlSeverityType.Warning:
                    _estructura.Add("Warning: " + validationEventArgs.Message);
                    break;
                case XmlSeverityType.Error:
                    _estructura.Add("Error: " + validationEventArgs.Message);
                    break;
            }
        }

        private static IEnumerable<Xsd.GetXsd> GetXmlSchemaDefinitions32()
        {
            return new List<Xsd.GetXsd>
                {
                    new Xsd.GetXsd("cfdv32.xsd"),
                    new Xsd.GetXsd("ecc.xsd"),
                    new Xsd.GetXsd("psgecfd.xsd"),
                    new Xsd.GetXsd("donat11.xsd"),
                    new Xsd.GetXsd("divisas.xsd"),
                    new Xsd.GetXsd("ecb.xsd"),
                    new Xsd.GetXsd("detallista.xsd"),
                    new Xsd.GetXsd("implocal.xsd"),
                    new Xsd.GetXsd("terceros11.xsd"),
                    new Xsd.GetXsd("iedu.xsd"),
                    new Xsd.GetXsd("ventavehiculos11.xsd"),
                    new Xsd.GetXsd("pfic.xsd"),
                    new Xsd.GetXsd("TuristaPasajeroExtranjero.xsd"),
                    new Xsd.GetXsd("leyendasFisc.xsd"),
                    new Xsd.GetXsd("spei.xsd"),
                    new Xsd.GetXsd("nomina11.xsd"),
                    new Xsd.GetXsd("cfdiregistrofiscal.xsd"),
                    new Xsd.GetXsd("pagoenespecie.xsd"),
                    new Xsd.GetXsd("consumodecombustibles.xsd"),
                    new Xsd.GetXsd("valesdedespensa.xsd"),
                    new Xsd.GetXsd("aerolineas.xsd"),
                    new Xsd.GetXsd("notariospublicos.xsd"),
                    new Xsd.GetXsd("TimbreFiscalDigital.xsd")
                };
        }

    }
}
