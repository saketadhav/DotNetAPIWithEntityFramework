using Application.Common.Exceptions;
using Application.Funds.Queries.GetFunds;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Clients.Commands.CreateClient
{
    public class CreateClientCommand : IRequest<int>
    {
        public string Name { get; set; }
        public List<FundDto> Funds { get; set; }
    }

    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, int>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CreateClientCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            foreach(var item in request.Funds)
            {
                Fund fund = _context.Funds.FindAsync(item.Id).Result;
                if(fund != null && fund.ClientId != 0)
                {
                    throw new ArgumentException("Fund is already assigned to another Client.", fund.Id.ToString());
                }
            }

            List<Fund> funds = request.Funds.Select(x => new Fund { Id = x.Id, Name = x.Name}).ToList();

            var entity = new Client
            { 
                Name = request.Name,
                Funds = funds
            };

            _context.Clients.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
