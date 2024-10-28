using CsvHelper;
using Microsoft.Extensions.Options;
using ProductImporter.Logic.Shared;
using ProductImporter.Model;
using System;
using System.Globalization;
using System.IO;

namespace ProductImporter.Logic.Target
{
    public class CsvProductTarget : IProductTarget, IDisposable
    {
        private readonly IOptions<CsvProductTargetOptions> _productTargetOptions;
        private readonly IWriteImportStatistics _importStatistics;

        private CsvWriter _csvWriter;

        public CsvProductTarget(
            IOptions<CsvProductTargetOptions> productTargetOptions,
            IWriteImportStatistics importStatistics)
        {
            _productTargetOptions = productTargetOptions;
            _importStatistics = importStatistics;
        }

        public void Open()
        {
            var streamWriter = new StreamWriter(_productTargetOptions.Value.TargetCsvPath);
            _csvWriter = new CsvWriter(streamWriter, CultureInfo.CurrentCulture, leaveOpen: false);

            _csvWriter.WriteHeader<Product>();
            _csvWriter.NextRecord();
        }

        public void AddProduct(Product product)
        {
            if (_csvWriter == null)
                throw new InvalidOperationException("Cannot add products to a target that is not yet open");

            _csvWriter.WriteRecord(product);
            _csvWriter.NextRecord();

            _importStatistics.IncrementOutputCount();
        }

        public void Close()
        {
            if (_csvWriter == null)
                throw new InvalidOperationException("Cannot close a target that is not yet open");

            _csvWriter.Flush();
        }

        public void Dispose()
        {
            _csvWriter?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
