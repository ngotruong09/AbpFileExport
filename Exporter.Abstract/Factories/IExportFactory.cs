using Exporter.Abstract.Exporters;

namespace Exporter.Abstract.Factories
{
    public interface IExportFactory
    {
        IExporter GetExporter(IOptionExporter options = null);
    }
}
