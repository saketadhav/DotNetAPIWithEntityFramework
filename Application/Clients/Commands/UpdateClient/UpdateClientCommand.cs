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

namespace Application.Clients.Commands.UpdateClient
{
    public class UpdateClientCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UpdateClientCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Clients.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Client), request.Id);
            }

            entity.Name = request.Name;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
