using Exporter;
using Exporter.Abstract.Managers;
using Exporter.Csv.Exporters;
using Exporter.Excel.Exporters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplicationTest.Controllers
{
    [ApiController]
    [Route("test")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IExporterManager _exporterManager;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IExporterManager exporterManager)
        {
            _logger = logger;
            _exporterManager = exporterManager;
        }

        [HttpGet]
        public async Task<string> ExportFileAsync()
        {
            List<IDictionary<string, object>> _datas = new List<IDictionary<string, object>>();
            _datas.Add(new Dictionary<string, object>() { { "Name", "A" }, { "Age", "22" } });
            _datas.Add(new Dictionary<string, object>() { { "Name", "B" }, { "Age", "23" } });
            _datas.Add(new Dictionary<string, object>() { { "Name", "C" }, { "Age", "24" } });
            _datas.Add(new Dictionary<string, object>() { { "Name", "D" }, { "Age", "25" } });

            // Save csv
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

            return "OK";
        }
    }
}
