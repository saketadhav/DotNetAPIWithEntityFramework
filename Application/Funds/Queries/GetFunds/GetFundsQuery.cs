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

namespace Application.Funds.Queries.GetFunds
{
    public class GetFundsQuery : IRequest<List<FundDto>>
    {
    }

    public class GetFundsQueryHandler : IRequestHandler<GetFundsQuery, List<FundDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFundsQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FundDto>> Handle(GetFundsQuery request, CancellationToken cancellationToken)
        {
            return await _context.Funds
                    .AsNoTracking()
                    .ProjectTo<FundDto>(_mapper.ConfigurationProvider)
                    .OrderBy(c => c.Name)
                    .ToListAsync(cancellationToken);
        }
    }
}
