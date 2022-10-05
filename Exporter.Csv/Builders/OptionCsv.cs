using Exporter.Abstract.Exporters;

namespace Exporter
{
    public class OptionCsv : IOptionExporter
    {
        public string TempFolderPath { get; set; }
    }
}
