using System;
using System.Collections.Generic;

namespace Exporter.Abstract.Options
{
    public class ExportFactoryOptions
    {
        public Dictionary<string, Type> Factories { get; }
        public ExportFactoryOptions()
        {
            Factories = new Dictionary<string, Type>();
        }
    }
}
