using Exporter.Abstract.Managers;

namespace Exporter.Excel.Exporters
{
    public class ExportExcelType : ExportType
    {
        public const string EXCEL = "EXCEL";
        public override string GetExportName()
        {
            return EXCEL;
        }
        public static ExportType GetExportType()
        {
            var exportType = new ExportExcelType();
            return exportType;
        }
    }
}
