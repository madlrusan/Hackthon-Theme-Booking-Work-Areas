using ITEC.Backend.Domain.Models.Base;
using ITEC.Backend.Persistence.Database;
using ITEC.Backend.Persistence.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITEC.Backend.Persistence.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly ApplicationDbContext _dbContext;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAsync(T entity)
        {
            var entry = await _dbContext.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entry.Entity.Id;
        }

        public async Task AddRangeAsync(List<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
