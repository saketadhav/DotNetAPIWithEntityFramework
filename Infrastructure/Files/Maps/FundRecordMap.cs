using Application.Funds.Queries.ExportFunds;
using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Files.Maps
{
    public class FundRecordMap : ClassMap<FundRecord>
    {
        public FundRecordMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
        }
    }
}
