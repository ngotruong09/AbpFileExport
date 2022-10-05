using Exporter.Abstract.Exporters;
using Exporter.Abstract.Factories;
using Exporter.Abstract.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Exporter.Abstract.Managers
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IExporterManager))]
    public class ExporterManager: IExporterManager, ITransientDependency
    {
        protected ExportFactoryOptions ExportFactoryOptions { get; }
        protected IServiceProvider ServiceProvider { get; }

        public ExporterManager(IOptions<ExportFactoryOptions> exportFactoryOptions, IServiceProvider serviceProvider)
        {
            ExportFactoryOptions = exportFactoryOptions.Value;
            ServiceProvider = serviceProvider;
        }

        public Task<MemoryStream> GetStreamDocument(ExportType exporterType, List<IDictionary<string, object>> datas, IOptionExporter option = null)
        {
            var stream = new MemoryStream();
            var factories = ExportFactoryOptions.Factories;
            var exporterName = exporterType.GetExportName();
            if (factories.ContainsKey(exporterName))
            {
                var factoryType = factories[exporterName];
                var factory = ServiceProvider.GetService(factoryType) as IExportFactory;
                using (var exporter = factory.GetExporter(option))
                {
                    exporter.BuildDocument(datas);
                    var builder = exporter.GetBuilder();
                    stream = builder.GetStreamDocument();
                }
            }
            else
            {
                throw new Exception($"Not found provider for {exporterName}");
            }

            return Task.FromResult(stream);
        }
    }
}
