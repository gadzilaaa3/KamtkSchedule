using KamtkSchedule.Application.Common.Interfaces.Repositories.Base;
using KamtkSchedule.Domain.Dtos.Api;
using KamtkSchedule.Domain.Dtos.Api.Base;
using KamtkSchedule.Domain.Entities.Base;
using KamtkSchedule.Infrastructure.Data;
using KamtkSchedule.Infrastructure.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace KamtkSchedule.Infrastructure.Repositories.Database.Base
{
    public abstract class BaseRepository<T, TDto> : IRepository<T, TDto> 
        where T : BaseEntity, new() where TDto : BaseEntityDto, new()
    {
        private readonly bool _disposeContext;
        public DbSet<T> Table { get; }
        public ApplicationDbContext Context { get; }
        protected BaseRepository(ApplicationDbContext context)
        {
            Context = context;
            Table = Context.Set<T>();
            _disposeContext = false;
        }
        protected BaseRepository(DbContextOptions<ApplicationDbContext> options)
            : this(new ApplicationDbContext(options))
        {
            _disposeContext = true;
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken 
            = default)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }
        
        public virtual TDto? FindOne(int? id)
        {
            return SelectDto().FirstOrDefault(e => e.Id == id);
        }
        public virtual Task<TDto?> FindOneAsync(int? id, 
            CancellationToken cancellationToken = default)
        {
            return SelectDto().FirstOrDefaultAsync(e => e.Id == id, 
                cancellationToken);
        }
        public virtual TDto? FindOne(Func<TDto, bool> predicate)
        {
            return SelectDto().FirstOrDefault(predicate);
        }
        public virtual Task<TDto?> FindOneAsync(
            Expression<Func<TDto, bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            return SelectDto().FirstOrDefaultAsync(predicate, cancellationToken);
        }
        
        public virtual IEnumerable<TDto> FindMany(Func<TDto, bool> predicate)
        {
            return SelectDto().Where(predicate);
        }
        public virtual Task<List<TDto>> FindManyAsync(
            Func<TDto, bool> predicate, 
            CancellationToken cancellationToken = default)
        {
            return SelectDto()
                .Where(predicate)
                .AsQueryable()
                .ToListAsync(cancellationToken);
        }

        public virtual PagedList<TDto> FindManyWithPaginate(
            Expression<Func<TDto, bool>> predicate, 
            PaginatedParameters parameters)
        {
            var query = SelectDto().Where(predicate);

            return PagedList<TDto>.ToPagedList(query,
                parameters.PageNumber,
                parameters.PageSize);
        }
        public virtual PagedList<TDto> FindManyWithPaginate(
            PaginatedParameters parameters)
        {
            return PagedList<TDto>.ToPagedList(SelectDto(),
                parameters.PageNumber,
                parameters.PageSize);
        }

        public void ExecuteQuery(string sql, object[] sqlParametersObjects)
            => Context.Database.ExecuteSqlRaw(sql, sqlParametersObjects);
        public Task ExecuteQueryAsync(string sql, object[] sqlParametersObjects,
            CancellationToken cancellationToken = default)
            => Context.Database.ExecuteSqlRawAsync(sql, sqlParametersObjects,
                cancellationToken);

        public virtual int Add(T entity, bool persist = true)
        {
            Table.Add(entity);
            return persist ? SaveChanges() : 0;
        }
        public virtual Task<int> AddAsync(T entity, bool persist = true,
            CancellationToken cancellationToken = default)
        {
            return Task.Run(async () =>
            {
                await Table.AddAsync(entity);
                return persist ? await SaveChangesAsync() : 0;
            }, cancellationToken);
        }

        public virtual int AddRange(IEnumerable<T> entities, 
            bool persist = true)
        {
            Table.AddRange(entities);
            return persist ? SaveChanges() : 0;
        }
        public virtual Task<int> AddRangeAsync(IEnumerable<T> entities,
            bool persist = true, CancellationToken cancellationToken = default)
        {
            return Task.Run(async () =>
            {
                await Table.AddRangeAsync(entities);
                return persist ? await SaveChangesAsync() : 0;
            }, cancellationToken);
        }

        public abstract IQueryable<TDto> SelectDto();

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private bool _isDisposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }
            if (disposing)
            {
                if (_disposeContext)
                {
                    Context.Dispose();
                }
            }
            _isDisposed = true;
        }
        ~BaseRepository()
        {
            Dispose(false);
        }
    }
}
