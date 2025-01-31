﻿using ITEC.Backend.Domain.Models.Base;

namespace ITEC.Backend.Persistence.Repositories.Abstractions
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<int> AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task<T> GetById(int id);
    }
}
