using GbsoDev.TechTest.Library.El;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GbsoDev.TechTest.Library.Dal.Contracts
{
	public interface IRootContext
	{
		DbSet<Usuario> Usuarios { get; set; }
		DbSet<Autor> Autores { get; set; }
		DbSet<Libro> Libros { get; set; }
		DbSet<AutorHasLibro> AutorHasLibros { get; set; }
		DbSet<Editorial> Editoriales { get; set; }
		
		int SaveChanges(bool acceptAllChangesOnSuccess);
		int SaveChanges();
		TEntity? Find<TEntity>(params object?[]? keyValues) where TEntity : class;
		DbSet<TEntity> Set<TEntity>() where TEntity : class;
		EntityEntry Entry(object entity);
		EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
		EntityEntry<TEntity> Remove<TEntity>(TEntity entity) where TEntity: class;
		void RemoveRange(IEnumerable<object> entities);
	}
}
