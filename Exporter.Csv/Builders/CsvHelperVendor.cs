using CsvHelper;
using Exporter.Abstract.Exporters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using Volo.Abp.DependencyInjection;

namespace Exporter.Csv.Builders
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(ICsvBuilder))]
    public class CsvHelperVendor : ICsvBuilder, ITransientDependency
    {
        protected List<IDictionary<string, object>> _datas;
        protected string _pathFolder;
        protected string _pathName;

        private bool _disposedValue;

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                _datas = null;
                _pathFolder = null;
                _pathName = null;
                _disposedValue = true;
            }
        }

        ~CsvHelperVendor()
        {
            Dispose(false);
        }

        public virtual void SetDatas(List<IDictionary<string, object>> datas)
        {
            this._datas = datas;
        }

        public virtual void SetOption(IOptionExporter opt)
        {
            var fmt = opt as OptionCsv;
            if(fmt != null)
            {
                _pathFolder = fmt.TempFolderPath;
            }
        }

        private void CreateFolder(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        private string GetFolderPath(string folderPath)
        {
            var folderName = System.Guid.NewGuid().ToString();
            var filePath = Path.Combine(folderPath, folderName);
            return filePath;
        }

        protected virtual string GetFilePath(string pathFolder)
        {
            var pathTempFolder = GetFolderPath(pathFolder);
            CreateFolder(pathTempFolder);
            var fileName = $"{System.Guid.NewGuid().ToString()}.csv";
            var filePath = Path.Combine(pathTempFolder, fileName);

            return filePath;
        }

        public virtual MemoryStream GetStreamDocument()
        {
            var bytes = File.ReadAllBytes(_pathName);
            var memoryStream = new MemoryStream(bytes);
            return memoryStream;
        }

        public virtual void BuildContent()
        {
            var fileName = GetFilePath(this._pathFolder);
            _pathName = fileName;
            using (var writer = new StreamWriter(fileName, false, System.Text.Encoding.UTF8))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                var keys = _datas[0].Keys.Select(x => x.Trim()).ToList();
                // write header
                foreach (var item in keys)
                {
                    csv.WriteField(item);
                }
                csv.NextRecord();
                // write details
                foreach (var item in _datas)
                {
                    foreach (var key in keys)
                    {
                        var value = item[key];
                        csv.WriteField(value);
                    }
                    csv.NextRecord();
                }
                //writer.Flush();
                //writer.Close();
            }
        }
    }
}
