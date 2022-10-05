using Exporter.Abstract;
using Volo.Abp.Modularity;

namespace Exporter.Excel
{
    [DependsOn(
        typeof(HwlExporterAbstractModule)
     )]
    public class HwlExporterExcelModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
        }
    }
}
