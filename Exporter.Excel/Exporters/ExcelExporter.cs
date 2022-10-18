using Exporter.Abstract.Builders;
using Exporter.Abstract.Exporters;
using Exporter.Excel.Builders;
using System;
using System.Collections.Generic;

namespace Exporter.Excel.Exporters
{
    public class ExcelExporter: IExporter
    {
        public IExcelBuilder Builder { get; protected set; }
        private bool _disposedValue;

        public virtual void SetBuilder(IBuilderBase builder, IOptionExporter option = null)
        {
            Builder = builder as IExcelBuilder;
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
        ~ExcelExporter()
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
