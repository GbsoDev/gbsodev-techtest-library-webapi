using FluentValidation;
using GbsoDev.TechTest.Library.Bll.Contracts;
using GbsoDev.TechTest.Library.Bll.ValidationRules;
using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace GbsoDev.TechTest.Library.Bll
{
	public abstract class EntityBaseService<TEntity, TKey, TDal> : BaseService, IEntityBaseService<TEntity, TKey>
		where TEntity : class, IEntity<TKey>
		where TDal : class, IBaseDataAccesLayer<TEntity, TKey>
	{
		protected TDal MainDal => mainDal.Value;
		private Lazy<TDal> mainDal;
		protected IValidator<TEntity> MainVr { get; }
		
		protected EntityBaseService(IServiceProvider serviceProvider) : base(serviceProvider)
		{
			mainDal = ActivatorUtilities.GetServiceOrCreateInstance<Lazy<TDal>>(serviceProvider);
			MainVr = ActivatorUtilities.GetServiceOrCreateInstance<IValidator<TEntity>>(serviceProvider);
		}

		public virtual TEntity[] Get()
		{
			return MainDal.List();
		}
		
		public virtual TEntity? GetById(TKey id)
		{
			return MainDal.GetById(id);
		}
		
		public virtual TEntity Set(TEntity entity)
		{
			var validate = MainVr.Validate(entity, n => n.IncludeRuleSets(VrRuleSets.ALL, VrRuleSets.SET));
			if (!validate.IsValid)
			{
				throw new ValidationException(validate.Errors);
			}
			return MainDal.Register(entity);
		}
		
		public virtual TEntity Update(TEntity entity)
		{
			var validate = MainVr.Validate(entity, n => n.IncludeRuleSets(VrRuleSets.ALL, VrRuleSets.UPDATE));
			if (!validate.IsValid)
			{
				throw new ValidationException(validate.Errors);
			}
			return MainDal.Update(entity);
		}
		
		public virtual void Delete(TEntity entity)
		{
			var validate = MainVr.Validate(entity, n => n.IncludeRuleSets(VrRuleSets.DELETE));
			if (!validate.IsValid)
			{
				throw new ValidationException(validate.Errors);
			}
			MainDal.Delete(entity);
		}
		
		public virtual void DeleteById(TKey id)
		{
			var entity = MainDal.GetById(id);
			if (entity != null)
			{
				MainDal.Delete(entity);
			}
		}
	}
}
