using Exporter.Abstract.Attributes;
using Exporter.Abstract.Exporters;
using Exporter.Abstract.Factories;
using Exporter.Csv.Builders;
using Exporter.Csv.Exporters;
using System;
using Volo.Abp.DependencyInjection;

namespace Exporter.Factories
{
    [ExporterName(nameof(ExportCsvType.CSV))]
    public class CsvFactory : IExportFactory, ISingletonDependency
    {
        private readonly IServiceProvider _serviceProvider;
        public CsvFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public virtual IExporter GetExporter(IOptionExporter options = null)
        {
            var exporter = _serviceProvider.GetService(typeof(CsvExporter)) as IExporter;
            var buidler = _serviceProvider.GetService(typeof(ICsvBuilder)) as ICsvBuilder;
            exporter.SetBuilder(buidler, options);
            return exporter;
        }
    }
}
