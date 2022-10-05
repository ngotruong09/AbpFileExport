using Exporter.Abstract.Builders;
using System;
using System.Collections.Generic;

namespace Exporter.Abstract.Exporters
{
    public interface IExporter: IDisposable
    {
        void SetBuilder(IBuilderBase builder, IOptionExporter option = null);
        IBuilderBase GetBuilder();
        void BuildDocument(List<IDictionary<string, object>> datas = null);
    }
}
