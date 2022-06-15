﻿using Onebrb.Core.Interfaces;

namespace Onebrb.Infrastructure
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
