using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Client> Clients { get; set; }
        DbSet<Fund> Funds { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
