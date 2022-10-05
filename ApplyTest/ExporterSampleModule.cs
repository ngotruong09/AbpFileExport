using Exporter.Csv;
using Exporter.Excel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace ApplyTest
{
    [DependsOn(
      typeof(AbpAutofacModule),
      typeof(HwlExporterCsvModule),
      typeof(HwlExporterExcelModule)
    )]
    public class ExporterSampleModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostEnvironment = context.Services.GetSingletonInstance<IHostEnvironment>();

            context.Services.AddHostedService<ExporterSampleHostedService>();
        }
    }
}
