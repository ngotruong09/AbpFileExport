using System;
using System.Collections.Generic;

namespace Exporter.Abstract.Options
{
    public class ExportFactoryOptions
    {
        public Dictionary<string, Type> Factories { get; private set; }
        public ExportFactoryOptions(Dictionary<string, Type> factories)
        {
            Factories = factories;
        }
        public ExportFactoryOptions()
        {
            Factories = new Dictionary<string, Type>();
        }
        public void SetFactorys(Dictionary<string, Type> factories)
        {
            Factories = factories;
        }
    }
}
