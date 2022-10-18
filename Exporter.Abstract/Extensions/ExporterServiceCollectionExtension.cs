using Exporter.Abstract.Attributes;
using Exporter.Abstract.Factories;
using Exporter.Abstract.Managers;
using Exporter.Abstract.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ExporterServiceCollectionExtension
    {
        public static IServiceCollection AddExporter(this IServiceCollection services,
            Action<ExportOptionBuilder> configure = default)
        {
            var optBuidler = new ExportOptionBuilder(services);
            configure?.Invoke(optBuidler);

            var factories = GetExportFactorys();
            services.Configure<ExportFactoryOptions>(options =>
            {
                options.SetFactorys(factories);
            });

            services
                .AddTransient<IExporterManager, ExporterManager>();

            return services;
        }

        public static Dictionary<string, Type> GetExportFactorys()
        {
            var factories = new Dictionary<string, Type>();
            var type = typeof(IExportFactory);
            var types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));

            foreach (var item in types)
            {
                if (item.IsInterface == false)
                {
                    var exporterName = ExporterNameAttribute.GetExporterNameOrDefault(item);
                    if (!factories.ContainsKey(exporterName))
                    {
                        factories.Add(exporterName, item);
                    }
                }
            }

            return factories;
        }

    }
}
