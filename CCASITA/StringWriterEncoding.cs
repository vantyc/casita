using System;
using System.IO;
using System.Text;
using System.Diagnostics;

namespace LaCasita
{
    [DebuggerStepThrough]
    public class StringWriterEncoding : StringWriter
    {
        private readonly Encoding _encoding;

        public StringWriterEncoding()
        {
        }

        public StringWriterEncoding(IFormatProvider formatProvider)
            : base(formatProvider)
        {
        }

        public StringWriterEncoding(StringBuilder stringBuilder)
            : base(stringBuilder)
        {
        }

        public StringWriterEncoding(StringBuilder stringBuilder, IFormatProvider formatProvider)
            : base(stringBuilder, formatProvider)
        {
        }

        public StringWriterEncoding(Encoding encoding)
        {
            _encoding = encoding;
        }

        public StringWriterEncoding(IFormatProvider formatProvider, Encoding encoding)
            : base(formatProvider)
        {
            _encoding = encoding;
        }

        public StringWriterEncoding(StringBuilder stringBuilder, Encoding encoding)
            : base(stringBuilder)
        {
            _encoding = encoding;
        }

        public StringWriterEncoding(StringBuilder stringBuilder, IFormatProvider formatProvider, Encoding encoding)
            : base(stringBuilder, formatProvider)
        {
            _encoding = encoding;
        }

        public override Encoding Encoding
        {
            get { return _encoding ?? base.Encoding; }
        }
    }
}
