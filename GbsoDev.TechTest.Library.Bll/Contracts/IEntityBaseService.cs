using GbsoDev.TechTest.Library.El.Contracts;

namespace GbsoDev.TechTest.Library.Bll.Contracts
{
	public interface IEntityBaseService<TEntity, TKey>
		where TEntity : class, IEntity<TKey>
	{
		public TEntity[] Get();
		public TEntity? GetById(TKey id);
		public TEntity Set(TEntity entity);
		public TEntity Update(TEntity entity);
		public void Delete(TEntity entity);
		public void DeleteById(TKey id);
	}
}
