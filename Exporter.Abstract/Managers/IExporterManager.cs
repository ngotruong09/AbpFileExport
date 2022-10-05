using Exporter.Abstract.Exporters;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Exporter.Abstract.Managers
{
    public interface IExporterManager
    {
        Task<MemoryStream> GetStreamDocument(ExportType exportType, List<IDictionary<string, object>> datas, IOptionExporter option = null);
    }
}
