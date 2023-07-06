using GbsoDev.TechTest.Library.El.Contracts;
using System.Linq.Expressions;

namespace GbsoDev.TechTest.Library.Dal.Contracts
{
	public interface IBaseDataAccesLayer<TEntity, TKey>
		where TEntity : class, IEntity<TKey>
	{
		TEntity Register(TEntity entity);
		TEntity? GetById(TKey id);
		TEntity[] List();
		TEntity[] Where(Expression<Func<TEntity, bool>> expression);
		/// <summary>
		/// Update all properies from entity
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		TEntity Update(TEntity entity);
		/// <summary>
		/// Update explicit properies of anonymus type
		/// </summary>
		/// <param name="id">Entity id key</param>
		/// <param name="object">Properties object</param>
		/// <returns></returns>
		TEntity? Update(TEntity entity, Expression<Func<TEntity, object>> @object);
		void Delete(TEntity entity);
	}
}
