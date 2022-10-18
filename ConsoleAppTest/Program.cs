using Exporter;
using Exporter.Abstract.Managers;
using Exporter.Csv.Exporters;
using Exporter.Excel.Exporters;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace ConsoleAppTest
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            var collections = new ServiceCollection();
            collections.AddExporter(
                options => options
                .AddCsv()
                .AddExcel());

            var serviceProvider = collections.BuildServiceProvider();

            //========================================================================================

            List<IDictionary<string, object>> _datas = new List<IDictionary<string, object>>();
            _datas.Add(new Dictionary<string, object>() { { "Name", "A" }, { "Age", "22" } });
            _datas.Add(new Dictionary<string, object>() { { "Name", "B" }, { "Age", "23" } });
            _datas.Add(new Dictionary<string, object>() { { "Name", "C" }, { "Age", "24" } });
            _datas.Add(new Dictionary<string, object>() { { "Name", "D" }, { "Age", "25" } });

            // Save csv

            var _exporterManager = serviceProvider.GetService<IExporterManager>();

            using (var stream =
                await _exporterManager.GetStreamDocument(
                    ExportCsvType.GetExportType(), _datas, new OptionCsv { TempFolderPath = @"d:\MyGit\save2\save" }))
            using (var fileStream = new FileStream(@"d:\MyGit\save2\test.csv", FileMode.Create, FileAccess.Write))
            {
                stream.WriteTo(fileStream);
            }

            // Save excel
            using (var stream =
                await _exporterManager.GetStreamDocument(
                    ExportExcelType.GetExportType(), _datas, new OptionExcel { NumberRowPerSheet = 2 }))
            using (var fileStream = new FileStream(@"d:\MyGit\save2\test.xlsx", FileMode.Create, FileAccess.Write))
            {
                stream.WriteTo(fileStream);
            }

        }
    }
}
