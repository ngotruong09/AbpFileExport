using Exporter.Abstract.Factories;
using Exporter.Csv.Builders;
using Exporter.Csv.Exporters;
using Exporter.Factories;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static ExportOptionBuilder AddCsv(this ExportOptionBuilder options)
        {
            options.Services.AddTransient<CsvExporter>();
            options.Services.AddTransient<ICsvBuilder, CsvHelperVendor>();
            options.Services.AddSingleton<CsvFactory>();
            return options;
        }
    }
}
