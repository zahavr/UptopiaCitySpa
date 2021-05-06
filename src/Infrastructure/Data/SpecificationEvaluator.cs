using Core.Entities;
using Core.Specification;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Data
{
	public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
	{
		public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
			ISpecification<TEntity> specification)
		{
			IQueryable<TEntity> query = inputQuery;

			if (specification.Criteria != null)
			{
				query = query.Where(specification.Criteria);
			}

			if (specification.OrderBy != null)
			{
				query = query.OrderBy(specification.OrderBy);
			}

			if (specification.OrderByDesc != null)
			{
				query = query.OrderByDescending(specification.OrderBy);
			}

			if (specification.IsPagingEnabled)
			{
				query = query.Skip(specification.Skip).Take(specification.Take);
			}

			query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

			return query;
		}
	}
}
