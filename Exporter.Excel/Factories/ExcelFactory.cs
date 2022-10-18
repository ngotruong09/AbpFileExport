using Exporter.Abstract.Attributes;
using Exporter.Abstract.Exporters;
using Exporter.Abstract.Factories;
using Exporter.Excel.Builders;
using Exporter.Excel.Exporters;
using System;

namespace Exporter.Excel.Factories
{
    [ExporterName(nameof(ExportExcelType.EXCEL))]
    public class ExcelFactory : IExportFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public ExcelFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public virtual IExporter GetExporter(IOptionExporter options = null)
        {
            var exporter = _serviceProvider.GetService(typeof(ExcelExporter)) as IExporter;
            var buidler = _serviceProvider.GetService(typeof(IExcelBuilder)) as IExcelBuilder;
            exporter.SetBuilder(buidler, options);
            return exporter;
        }
    }
}