using Exporter.Abstract.Exporters;
using Exporter.Abstract.Helpers;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Volo.Abp.DependencyInjection;

namespace Exporter.Excel.Builders
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IExcelBuilder))]
    public class EPPlusVendor : IExcelBuilder, ITransientDependency
    {
        protected List<IDictionary<string, object>> _datas;
        protected int _numRowPerSheet;
        protected ExcelPackage _excelPackage;

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
                if (disposing)
                {
                    _excelPackage?.Dispose();
                }
                _datas = null;
                _excelPackage = null;
                _disposedValue = true;
            }
        }

        ~EPPlusVendor()
        {
            Dispose(false);
        }

        public EPPlusVendor()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            _excelPackage = new ExcelPackage();
            _numRowPerSheet = 1000000;
        }

        public virtual void SetOption(IOptionExporter format)
        {
            var opt = format as OptionExcel;
            if (opt != null)
            {
                _numRowPerSheet = opt.NumberRowPerSheet;
            }
        }
        
        public virtual void SetDatas(List<IDictionary<string, object>> datas)
        {
            this._datas = datas;
        }
        
        public virtual void BuildContent()
        {
            var listRows = Common.SplitArray(this._datas, this._numRowPerSheet);
            for (int sheetIndex = 0; sheetIndex < listRows.Count; sheetIndex++)
            {
                var dataExport = listRows[sheetIndex];
                var sheetName = $"Sheet{sheetIndex + 1}";
                var sheet = _excelPackage.Workbook.Worksheets.Add(sheetName);
                // create header          
                var keys = dataExport[0].Keys.Select(x => x.Trim()).ToList();
                for (int i = 0; i < keys.Count; i++)
                {
                    var row = 1;
                    var col = i + 1;
                    sheet.Cells[row, col].Value = keys[i];
                }

                // create detail rows
                int rowDetailIndex = 2;
                foreach (var item in dataExport)
                {
                    for (int i = 0; i < keys.Count; i++)
                    {
                        var colName = keys[i];
                        var col = i + 1;
                        sheet.Cells[rowDetailIndex, col].Value = item[colName];
                    }
                    rowDetailIndex++;
                }
                sheet.Row(1).Style.Font.Bold = true;
                sheet.Columns.AutoFit();
            }
        }

        public virtual MemoryStream GetStreamDocument()
        {
            var stream = new MemoryStream();
            _excelPackage.SaveAs(stream);
            return stream;
        }  
    }
}
