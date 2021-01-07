using Application.Common.Exceptions;
using Application.Funds.Queries.GetFunds;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Funds.Queries.GetFundsByClientId
{
    public class GetFundsByClientIdQuery : IRequest<List<FundDto>>
    {
        public int ClientId { get; set; }
    }

    public class GetFundsByClientIdQueryHandler : IRequestHandler<GetFundsByClientIdQuery, List<FundDto>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFundsByClientIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<FundDto>> Handle(GetFundsByClientIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Funds.Where(x => x.ClientId == request.ClientId).ToListAsync();

            if (entity == null)
            {
                throw new NotFoundException(nameof(Fund), request.ClientId);
            }

            return entity.Select(x => new FundDto { Id = x.Id, Name = x.Name }).ToList();
        }
    }
}
