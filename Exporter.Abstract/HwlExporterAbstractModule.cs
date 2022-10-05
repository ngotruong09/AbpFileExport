using Exporter.Abstract.Attributes;
using Exporter.Abstract.Factories;
using Exporter.Abstract.Options;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using Volo.Abp.Modularity;

namespace Exporter.Abstract
{
    public class HwlExporterAbstractModule: AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            AddEventHandlers(context.Services);
        }

        private static void AddEventHandlers(IServiceCollection services)
        {
            var factories = new Dictionary<string, Type>();
            services.OnRegistred(ctx =>
            {
                if (IsIExportFactory(ctx.ImplementationType))
                {
                    var exporterName = ExporterNameAttribute.GetExporterNameOrDefault(ctx.ImplementationType);
                    if (!factories.ContainsKey(exporterName))
                    {
                        factories.Add(exporterName, ctx.ImplementationType);
                    }
                }
            });
            services.Configure<ExportFactoryOptions>(options =>
            {
                options.Factories.AddIfNotContains(factories);
            });
        }

        private static bool IsIExportFactory(Type givenType)
        {
            var interfaces = givenType.GetInterfaces();
            foreach (var @interface in interfaces)
            {
                if (typeof(IExportFactory).GetTypeInfo().IsAssignableFrom(@interface))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
