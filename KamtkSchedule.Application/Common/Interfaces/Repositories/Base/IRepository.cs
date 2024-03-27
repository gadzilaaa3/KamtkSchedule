using KamtkSchedule.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace KamtkSchedule.Application.Common.Interfaces.Repositories.Base
{
    public interface IRepository<T, TDto> : IDisposable
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        TDto? FindOne(int? id);
        TDto? FindOne(Func<TDto, bool> predicate);
        Task<TDto?> FindOneAsync(int? id, 
            CancellationToken cancellationToken = default);
        Task<TDto?> FindOneAsync(Expression<Func<TDto, bool>> predicate,
            CancellationToken cancellationToken = default);

        IEnumerable<TDto> FindMany(Func<TDto, bool> predicate);
        Task<List<TDto>> FindManyAsync(Func<TDto, bool> predicate, 
            CancellationToken cancellationToken = default);

        PagedList<TDto> FindManyWithPaginate(
            Expression<Func<TDto, bool>> predicate,
            PaginatedParameters parameters);
        PagedList<TDto> FindManyWithPaginate(
            PaginatedParameters parameters);

        void ExecuteQuery(string sql, object[] sqlParametersObjects);
        Task ExecuteQueryAsync(string sql, object[] sqlParametersObjects,
            CancellationToken cancellationToken = default);

        int Add(T entity, bool persist = true);
        Task<int> AddAsync(T entity, bool persist = true,
            CancellationToken cancellationToken = default);

        int AddRange(IEnumerable<T> entities,
            bool persist = true);
        Task<int> AddRangeAsync(IEnumerable<T> entities,
            bool persist = true, CancellationToken cancellationToken = default);

        IQueryable<TDto> SelectDto();
    }
}
