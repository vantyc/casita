/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/
using System.Xml;
using System.Text;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace LaCasita
{
    public static class ExtensionMethods
    {

        /// <summary>
        /// verifica si byte es par
        /// </summary>
        /// <param name="numero">numero</param>
        /// <returns>true false</returns>
        [DebuggerStepThrough]
        public static bool EsPar(this byte numero)
        {
            return numero % 2 == 0;
        }

        /// <summary>
        /// verifica si int es par
        /// </summary>
        /// <param name="numero">numero</param>
        /// <returns>true false</returns>
        [DebuggerStepThrough]
        public static bool EsPar(this int numero)
        {
            return numero % 2 == 0;
        }

        /// <summary>
        /// Indenta y regresa string de XmlDocument
        /// </summary>
        /// <param name="xmlDocument">Documento Xml XmlDocument</param>
        /// <returns>string de xml indentado</returns>
        [DebuggerStepThrough]
        public static string ToIndentedString(this XmlDocument xmlDocument)
        {
            using (var stringWriter = new StringWriterEncoding(new StringBuilder(), Encoding.UTF8))
            {
                using (var xmlTextWriter = new XmlTextWriter(stringWriter))
                {
                    xmlTextWriter.Formatting = Formatting.Indented;
                    xmlDocument.Save(xmlTextWriter);
                    return stringWriter.ToString();
                }
            }
        }

        /// <summary>
        /// verifica si String es un Rfc valido
        /// </summary>
        /// <param name="String">String</param>
        /// <returns>true false</returns>
        [DebuggerStepThrough]
        public static bool EsRfc(this string String)
        {
            if (string.IsNullOrEmpty(String)) return false;
            const string pattern = @"^([A-Z|a-z|&amp;]{3})(([0-9]{2})([0][13456789]|[1][012])([0][1-9]|[12][\d]|[3][0])|([0-9]{2})([0][13578]|[1][02])([0][1-9]|[12][\d]|[3][01])|([02468][048]|[13579][26])([0][2])([0][1-9]|[12][\d])|([1-9]{2})([0][2])([0][1-9]|[12][0-8]))(\w{2}[A|a|0-9]{1})$|^([A-Z|a-z]{4})(([0-9]{2})([0][13456789]|[1][012])([0][1-9]|[12][\d]|[3][0])|([0-9]{2})([0][13578]|[1][02])([0][1-9]|[12][\d]|[3][01])|([02468][048]|[13579][26])([0][2])([0][1-9]|[12][\d])|([1-9]{2})([0][2])([0][1-9]|[12][0-8]))((\w{2})([A|a|0-9]{1})){0,3}$";
            return Regex.IsMatch(String, pattern);
        }

    }
}
