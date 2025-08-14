using Domain.Entities;
using Domain.IRepository;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class Repository<T>(AppDbContext dbContext) : IRepository<T> where T : Entity
    {
        protected readonly AppDbContext _context = dbContext;
        protected readonly DbSet<T> _entities = dbContext.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
            => await _entities.AnyAsync(predicate);

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
            => await _entities.Where(predicate).ToListAsync();
    }
}
