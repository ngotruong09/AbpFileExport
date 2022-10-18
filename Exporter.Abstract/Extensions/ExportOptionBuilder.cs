namespace Microsoft.Extensions.DependencyInjection
{
    public class ExportOptionBuilder
    {
        public ExportOptionBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}
