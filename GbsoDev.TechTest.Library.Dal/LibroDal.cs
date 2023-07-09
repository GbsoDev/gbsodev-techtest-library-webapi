using GbsoDev.TechTest.Library.Dal.Contracts;
using GbsoDev.TechTest.Library.El;
using GbsoDev.TechTest.Library.El.Contracts;
using Microsoft.EntityFrameworkCore;

namespace GbsoDev.TechTest.Library.Dal
{
	internal sealed class LibroDal : BaseDataAccesLayer<Libro, long>, ILibroDal
	{

		public LibroDal(IServiceProvider serviceProvider) : base(serviceProvider)
		{
		}

		public override Libro Register(Libro entity)
		{
			RootContext.Libros.Add(entity);
			RootContext.SaveChanges();
			return entity;
		}

		public override Libro? GetById(long id)
		{
			var result = RootContext.Libros
				.Include(x => x.LibroHasAutores)
				.ThenInclude(x => x.Autor)
				.FirstOrDefault(x=> x.Id == id);
			return result;
		}

		public override Libro Update(Libro entity)
		{
			var entityResult = RootContext.Libros
				.Include(x => x.LibroHasAutores).FirstOrDefault(x => x.Id == entity.Id);
			if (entityResult != null)
			{
				entityResult.LibroHasAutores.Clear();
				RootContext.SaveChanges();

				entity.CreatedDate = entityResult.CreatedDate;
				RootContext.Entry(entityResult).CurrentValues.SetValues(entity);
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
