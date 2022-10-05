using Exporter;
using Exporter.Abstract.Managers;
using Exporter.Csv.Exporters;
using Exporter.Excel.Exporters;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp;

namespace ApplyTest
{
    public class ExporterSampleHostedService : IHostedService
    {
        private readonly IAbpApplicationWithExternalServiceProvider _application;
        private readonly IServiceProvider _serviceProvider;
        private readonly IExporterManager _exporterManager;

        public ExporterSampleHostedService(IAbpApplicationWithExternalServiceProvider application, IServiceProvider serviceProvider, IExporterManager exporterManager)
        {
            _application = application;
            _serviceProvider = serviceProvider;
            _exporterManager = exporterManager;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _application.Initialize(_serviceProvider);

            List<IDictionary<string, object>> _datas = new List<IDictionary<string, object>>();
            _datas.Add(new Dictionary<string, object>() { { "Name", "truong" }, { "Age", "22" } });
            _datas.Add(new Dictionary<string, object>() { { "Name", "truong1" }, { "Age", "23" } });
            _datas.Add(new Dictionary<string, object>() { { "Name", "truong2" }, { "Age", "24" } });
            _datas.Add(new Dictionary<string, object>() { { "Name", "truong3" }, { "Age", "25" } });


            // Save csv
            using (var stream = await _exporterManager.GetStreamDocument(ExportCsvType.GetExportType(), _datas, new OptionCsv { TempFolderPath = @"d:\MyGit\save2\save" }))
            using (var fileStream = new FileStream(@"d:\MyGit\save2\test.csv", FileMode.Create, FileAccess.Write))
            {
                stream.WriteTo(fileStream);
            }

            // Save excel
            using (var stream = await _exporterManager.GetStreamDocument(ExportExcelType.GetExportType(), _datas, new OptionExcel { NumberRowPerSheet = 2 }))
            using (var fileStream = new FileStream(@"d:\MyGit\save2\test.xlsx", FileMode.Create, FileAccess.Write))
            {
                stream.WriteTo(fileStream);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _application.Shutdown();

            return Task.CompletedTask;
        }
    }
}
