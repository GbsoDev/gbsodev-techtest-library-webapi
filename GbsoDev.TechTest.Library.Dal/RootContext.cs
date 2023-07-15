using GbsoDev.TechTest.Library.El;
using Microsoft.EntityFrameworkCore;
using GbsoDev.TechTest.Library.Dal.Contracts;

namespace GbsoDev.TechTest.Library.Dal
{
	public sealed class RootContext : DbContext, IRootContext
	{
		public DbSet<Usuario> Usuarios { get; set; }
		public DbSet<Autor> Autores { get; set; }
		public DbSet<Libro> Libros { get; set; }
		public DbSet<AutorHasLibro> AutorHasLibros { get; set; }
		public DbSet<Editorial> Editoriales { get; set; }

		public RootContext(DbContextOptions<RootContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			ModelConfiguration.SetConfiguration(modelBuilder);
		}
	}
}
