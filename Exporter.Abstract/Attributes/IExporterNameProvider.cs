using System;

namespace Exporter.Abstract.Attributes
{
    public interface IExporterNameProvider
    {
        string GetExporterName(Type eventType);
    }
}
