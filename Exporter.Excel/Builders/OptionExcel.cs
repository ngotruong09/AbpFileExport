using Exporter.Abstract.Exporters;

namespace Exporter
{
    public class OptionExcel : IOptionExporter
    {
        public int NumberRowPerSheet { get; set; }
    }
}
