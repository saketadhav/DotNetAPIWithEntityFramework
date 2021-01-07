using Application.Clients.Queries.ExportClients;
using Application.Common.Interfaces;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Funds.Queries.ExportFunds
{
    public class ExportFundsQuery : IRequest<ExportFundsVm>
    {
    }

    public class ExportFundsQueryHandler : IRequestHandler<ExportFundsQuery, ExportFundsVm>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICsvFileBuilder<FundRecord> _fileBuilder;

        public ExportFundsQueryHandler(IApplicationDbContext context, IMapper mapper, ICsvFileBuilder<FundRecord> fileBuilder)
        {
            _context = context;
            _mapper = mapper;
            _fileBuilder = fileBuilder;
        }

        public async Task<ExportFundsVm> Handle(ExportFundsQuery request, CancellationToken cancellationToken)
        {
            var vm = new ExportFundsVm();

            var records = await _context.Funds
                .ProjectTo<FundRecord>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            vm.Content = _fileBuilder.BuildFile(records, "fundrecord");
            vm.ContextType = "text/csv";
            vm.FileName = "Fund.csv";

            return await Task.FromResult(vm);
        }
    }
}
