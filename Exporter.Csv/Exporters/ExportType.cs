using Exporter.Abstract.Managers;

namespace Exporter.Csv.Exporters
{
    public class ExportCsvType : ExportType
    {
        public const string CSV = "CSV";
        public override string GetExportName()
        {
            return CSV;
        }

        public static ExportType GetExportType()
        {
            var exportType = new ExportCsvType();
            return exportType;
        }
    }
}
