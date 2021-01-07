using Application.Common.Exceptions;
using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Funds.Commands.UpdateFund
{
    public class UpdateFundCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClientId { get; set; }
    }

    public class UpdateFundCommandHandler : IRequestHandler<UpdateFundCommand>
    {
        private readonly IApplicationDbContext _context;

        public UpdateFundCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateFundCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Funds.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Fund), request.Id);
            }

            entity.Name = request.Name;
            entity.ClientId = request.ClientId;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
