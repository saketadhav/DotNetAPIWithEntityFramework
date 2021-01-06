using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Funds.Commands.CreateFund
{
    public class CreateFundCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateFundCommandHandler : IRequestHandler<CreateFundCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateFundCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateFundCommand request, CancellationToken cancellationToken)
        {
            var entity = new Fund
            {
                Name = request.Name
            };

            _context.Funds.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
