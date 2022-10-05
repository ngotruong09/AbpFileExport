using Exporter.Abstract.Builders;
using Exporter.Abstract.Exporters;
using Exporter.Csv.Builders;
using System;
using System.Collections.Generic;
using Volo.Abp.DependencyInjection;

namespace Exporter.Csv.Exporters
{
    public class CsvExporter : IExporter, ITransientDependency
    {
        public ICsvBuilder Builder { get; protected set; }
        private bool _disposedValue;

        public virtual void SetBuilder(IBuilderBase builder, IOptionExporter option = null)
        {
            Builder = builder as ICsvBuilder;
            Builder.SetOption(option);
        }

        public virtual IBuilderBase GetBuilder()
        {
            return Builder;
        }

        public virtual void BuildDocument(List<IDictionary<string, object>> datas = null)
        {
            Builder.SetDatas(datas);
            Builder.BuildContent();
        }
        ~CsvExporter()
        {
            Dispose(false);
        }
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    Builder?.Dispose();
                }
                Builder = null;
                _disposedValue = true;
            }
        }
    }
}
