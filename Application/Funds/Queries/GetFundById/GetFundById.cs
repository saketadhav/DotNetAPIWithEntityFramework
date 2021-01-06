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

namespace Application.Funds.Queries.GetFundById
{
    public class GetFundByIdQuery: IRequest<FundDto>
    {
        public int Id { get; set; }
    }

    public class GetFundByIdQueryHandler : IRequestHandler<GetFundByIdQuery, FundDto>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GetFundByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<FundDto> Handle(GetFundByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.Funds.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Fund), request.Id);
            }

            return _mapper.Map<FundDto>(entity);
        }
    }
}
