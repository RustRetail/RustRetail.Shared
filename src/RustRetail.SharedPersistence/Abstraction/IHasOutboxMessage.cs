using Microsoft.EntityFrameworkCore;
using RustRetail.SharedKernel.Domain.Models;

namespace RustRetail.SharedPersistence.Abstraction
{
    public interface IHasOutboxMessage
    {
        public DbSet<OutboxMessage> OutboxMessage { get; set; }
    }
}
