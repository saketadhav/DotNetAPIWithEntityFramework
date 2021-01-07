using Application.Common.Interfaces;
using CsvHelper;
using Infrastructure.Files.Maps;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace Infrastructure.Files
{
    public class CsvFileBuilder<T> : ICsvFileBuilder<T>
    {
        public byte[] BuildFile(IEnumerable<T> records, string recordType)
        {
            using var memoryStream = new MemoryStream();
            using (var streamWriter = new StreamWriter(memoryStream))
            {
                using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);
                if (recordType == "clientrecord")
                {
                    csvWriter.Configuration.RegisterClassMap<ClientRecordMap>();

                }
                else if(recordType == "fundrecord")
                {
                    csvWriter.Configuration.RegisterClassMap<FundRecordMap>();
                }
                csvWriter.WriteRecords(records);
            }

            return memoryStream.ToArray();
        }
    }
}
