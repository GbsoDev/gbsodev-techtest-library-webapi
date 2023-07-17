using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace GbsoDev.TechTest.Library.Dal
{
	public abstract class BaseDataAccesLayer<TEntity, TKey> : IBaseDataAccesLayer<TEntity, TKey>
		where TEntity : class, IEntity<TKey>
	{
		protected IRootContext RootContext => rootContext.Value;
		private Lazy<IRootContext> rootContext;

		public BaseDataAccesLayer(IServiceProvider serviceProvider)
		{
			rootContext = ActivatorUtilities.GetServiceOrCreateInstance<Lazy<IRootContext>>(serviceProvider);
		}

		public virtual TEntity Register(TEntity entity)
		{
			RootContext.Set<TEntity>().Add(entity);
			RootContext.SaveChanges();
			return entity;
		}

		public virtual TEntity? GetById(TKey id)
		{
			var result = RootContext.Find<TEntity>(id);
			return result;
		}

		public virtual TEntity[] List()
		{
			var result = RootContext.Set<TEntity>().ToArray();
			return result;
		}

		public virtual TEntity Update(TEntity entity)
		{
			var entityResult = RootContext.Find<TEntity>(entity.Id);
			if (entityResult != null)
			{
				entity.CreatedDate = entityResult.CreatedDate;
				RootContext.Entry(entityResult).CurrentValues.SetValues(entity);
				RootContext.SaveChanges();
			}
			return entityResult;
		}

		public virtual TEntity? Update(TEntity entity, Expression<Func<TEntity, object>> @object)
		{
			var entityResult = RootContext.Find<TEntity>(entity.Id);
			var objectResult = @object?.Compile()?.Invoke(entity);
			if (entityResult != null && objectResult != null)
				RootContext.Entry(entity).CurrentValues.SetValues(objectResult);
			return entityResult;
		}

		public virtual void Delete(TEntity entity)
		{
			RootContext.Remove(entity);
			RootContext.SaveChanges();
		}
	}
}
