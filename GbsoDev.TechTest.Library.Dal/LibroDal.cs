using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace GbsoDev.TechTest.Library.Dal
{
	public sealed class LibroDal : BaseDataAccesLayer<Libro, long>, ILibroDal
	{

		public LibroDal(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		public override Libro? GetById(long id)
		{
			var result = base.GetById(id);
			if (result != null)
			{
				var entry = RootContext.Entry(result);
				entry.Reference(x => x.Editorial).Load();
				entry.Collection(x => x.LibroHasAutores).Query().Include(x => x.Autor).Load();
			}
			return result;
		}

		public override Libro Update(Libro entity)
		{
			var entityResult = base.GetById(entity.Id);
			if (entityResult != null)
			{
				var entry = RootContext.Entry(entityResult);
				entry.Collection(x => x.LibroHasAutores).Load();
				entityResult.LibroHasAutores.Clear();
				entity.CreatedDate = entityResult.CreatedDate;
				entry.CurrentValues.SetValues(entity);
				entityResult.LibroHasAutores.AddRange(entity.LibroHasAutores);
				RootContext.SaveChanges();
			};
			return entityResult ?? entity;
		}

		public List<Libro> ListLibrosByAutorId(long id)
		{
			return RootContext.AutorHasLibros
				.Include(x => x.Libro)
				.Where(x => x.LibroId == id)
				.Select(x=> x.Libro!).ToList();
		}
	}
}
