using Core.Entities;
using Core.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly AppDbContext _context;
		private Hashtable _repositories;

		public UnitOfWork(AppDbContext context)
		{
			_context = context;
		}

		public async Task<int> Complete()
		{
			return await _context.SaveChangesAsync();
		}

		public void Dispose()
		{
			_context.Dispose();
		}

		public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
		{
			if (_repositories != null) _repositories = new Hashtable();

			string type = typeof(TEntity).Name;

			if (!_repositories.Contains(type))
			{
				Type repositoryType = typeof(GenericRepository<>);
				object respositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

				_repositories.Add(repositoryType, respositoryInstance);
			}

			return (IGenericRepository<TEntity>)_repositories[type];
		}
	}
}
