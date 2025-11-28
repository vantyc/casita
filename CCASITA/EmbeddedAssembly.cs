using System.Reflection;
using System.Collections.Generic;

namespace LaCasita
{
    public class EmbeddedAssembly
    {
        static Dictionary<string, Assembly> _dictionary;

        public static void Load(string embeddedResource, string fileName)
        {
            if (_dictionary == null) _dictionary = new Dictionary<string, Assembly>();

            var executingAssembly = Assembly.GetExecutingAssembly();

            using (var stream = executingAssembly.GetManifestResourceStream(embeddedResource))
            {
                if (stream == null) return;

                var bytes = new byte[(int)stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                try
                {
                    var assembly = Assembly.Load(bytes);

                    _dictionary.Add(assembly.FullName, assembly);
                }
                catch { }
            }
        }

        public static Assembly Get(string assemblyFullName)
        {
            if (_dictionary == null || _dictionary.Count == 0) return null;

            return _dictionary.ContainsKey(assemblyFullName) ? _dictionary[assemblyFullName] : null;
        }
    }
}