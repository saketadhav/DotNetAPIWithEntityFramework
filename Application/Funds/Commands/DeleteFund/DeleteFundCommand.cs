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

namespace Application.Funds.Commands.DeleteFund
{
    public class DeleteFundCommand : IRequest
    {
        public int Id { get; set; }
    }

    public class DeleteFundCommandHandler : IRequestHandler<DeleteFundCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteFundCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteFundCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Funds.FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Fund), request.Id);
            }

            _context.Funds.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
