using Ardalis.GuardClauses;
using Ardalis.Specification;
using FoodStore.SharedKernal.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FoodStore.Infrastructure.Persistence
{
    public class EfReadRepository<T> : IReadRepository<T> where T : class, IAggregateRoot
    {
        public EfReadRepository(DbContext dbContext, ISpecificationEvaluator specificationEvaluator)
        {
            Guard.Against.Null(dbContext, nameof(dbContext));
            Guard.Against.Null(specificationEvaluator, nameof(specificationEvaluator));
            this.DbContext = dbContext;
            this.SpecificationEvaluator = specificationEvaluator;
        }

        protected DbContext DbContext { get; }
        protected ISpecificationEvaluator SpecificationEvaluator { get; }
        public virtual async Task<T?> GetByIdAsync<TId>(TId id, CancellationToken cancelationtoken)
        {
            return await DbContext.Set<T>().FindAsync(id, cancelationtoken);
        }

        public virtual async Task<T?> GetBySpecAsync<Spec>(Spec specification, CancellationToken cancelationtoken) where Spec : ISpecification<T>, ISingleResultSpecification
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }

        public virtual async Task<TResult> GetBySpecAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancelationtoken)
        {
            return await ApplySpecification(specification).FirstOrDefaultAsync();
        }


        public virtual async Task<List<T>> ListAsync(CancellationToken cancelationtoken)
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<List<T>> ListAsync(ISpecification<T> specification, CancellationToken cancelationtoken)
        {
            var queryResult = await ApplySpecification(specification).ToListAsync();

            return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
        }

        public virtual async Task<List<TResult>> ListAsync<TResult>(ISpecification<T, TResult> specification, CancellationToken cancelationtoken)
        {
            var queryResult = await ApplySpecification(specification).ToListAsync();

            return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
        }


        public virtual async Task<int> CountAsync(ISpecification<T> specification, CancellationToken cancelationtoken)
        {
            return await ApplySpecification(specification, true).CountAsync();
        }


        protected virtual IQueryable<T> ApplySpecification(ISpecification<T> specification, bool evaluateCriteriaOnly = false)
        {
            return SpecificationEvaluator.GetQuery(DbContext.Set<T>().AsQueryable(), specification, evaluateCriteriaOnly);
        }

        protected virtual IQueryable<TResult> ApplySpecification<TResult>(ISpecification<T, TResult> specification)
        {
            if (specification is null) throw new ArgumentNullException("Specification is required");
            if (specification.Selector is null) throw new SelectorNotFoundException();

            return SpecificationEvaluator.GetQuery(DbContext.Set<T>().AsQueryable(), specification);
        }
    }
}
