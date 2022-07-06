using Onebrb.Domain.Interfaces;

namespace Onebrb.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly OnebrbDbContext onebrbDbContext;

        public UnitOfWork(OnebrbDbContext onebrbDbContext)
        {
            this.onebrbDbContext = onebrbDbContext;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await onebrbDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            onebrbDbContext.Dispose();
        }
    }
}
