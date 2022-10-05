using System.IO;
using System;
using System.Collections.Generic;
using Exporter.Abstract.Exporters;

namespace Exporter.Abstract.Builders
{
    public interface IBuilderBase: IDisposable
    {
        void SetDatas(List<IDictionary<string, object>> datas);
        void SetOption(IOptionExporter opt);
        void BuildContent();
        MemoryStream GetStreamDocument();
    }
}
