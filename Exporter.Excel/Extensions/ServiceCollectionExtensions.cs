using Exporter.Abstract.Factories;
using Exporter.Excel.Builders;
using Exporter.Excel.Exporters;
using Exporter.Excel.Factories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static ExportOptionBuilder AddExcel(this ExportOptionBuilder options)
        {
            options.Services.AddTransient<ExcelExporter>();
            options.Services.AddTransient<IExcelBuilder, EPPlusVendor>();
            options.Services.AddSingleton<ExcelFactory>();

            return options;
        }
    }
}
