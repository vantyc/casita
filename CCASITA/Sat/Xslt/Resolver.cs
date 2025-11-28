using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Reflection;
using System.Diagnostics;

namespace LaCasita.Sat.Xslt
{
    [DebuggerStepThrough]
    public class Resolver : XmlResolver
    {
        
        public override System.Net.ICredentials Credentials
        {
            set { throw new NotImplementedException(); }
        }

        public override Uri ResolveUri(Uri baseUri, string relativeUri)
        {
            return new Uri("tdb:" + relativeUri);
        }

        public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
        {
            var executingAssembly = Assembly.GetExecutingAssembly();
            return (from resourceName in executingAssembly.GetManifestResourceNames() 
            where resourceName.EndsWith("." + absoluteUri.LocalPath) 
            select executingAssembly.GetManifestResourceStream(resourceName) 
            into xslt where xslt != null 
            select XmlReader.Create(new StreamReader(xslt), null, absoluteUri.LocalPath)).FirstOrDefault();
        }

    }
}
