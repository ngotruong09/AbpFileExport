using Exporter.Abstract;
using Volo.Abp.Modularity;

namespace Exporter.Csv
{
    [DependsOn(
        typeof(HwlExporterAbstractModule)
     )]
    public class HwlExporterCsvModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}
