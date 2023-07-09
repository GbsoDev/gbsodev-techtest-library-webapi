using GbsoDev.TechTest.Library.El;
using Microsoft.EntityFrameworkCore;

namespace GbsoDev.TechTest.Library.Dal
{
	internal static class ModelConfiguration
	{
		internal static void SetConfiguration(ModelBuilder modelBuilder)
		{
			#region autorEntity
			var autorEntity = modelBuilder.Entity<Autor>();
			autorEntity.ToTable("autores");
			autorEntity.HasKey(x => x.Id);
			autorEntity.Property(x => x.Nombre).IsRequired().HasMaxLength(45);
			autorEntity.Property(x => x.Apellidos).IsRequired().HasMaxLength(45);
			autorEntity.Property(x => x.CreatedDate).IsRequired().HasDefaultValue(DateTime.Now);
			#endregion

			#region libroEntity
			var libroEntity = modelBuilder.Entity<Libro>();
			libroEntity.ToTable("libros");
			libroEntity.Property(x => x.Id).HasColumnType("numeric(13, 0)").ValueGeneratedNever().HasColumnName("ISBN");
			libroEntity.HasKey(x => x.Id);
			libroEntity.HasOne(x => x.Editorial).WithMany(x => x.Libros).HasForeignKey(x => x.EditorialId).HasPrincipalKey(x => x.Id).IsRequired();
			libroEntity.Property(x => x.Titulo).IsRequired().HasMaxLength(45);
			libroEntity.Property(x => x.Sinopsis).IsRequired();
			libroEntity.Property(x => x.NPaginas).IsRequired().HasMaxLength(45);
			#endregion

			#region relación muchos a muchos autor libro
			var autorHasLibroEntity = modelBuilder.Entity<AutorHasLibro>();
			autorHasLibroEntity.ToTable("autores_has_libros");
			autorHasLibroEntity.HasKey(x => x.Id);
			autorHasLibroEntity.Property(x => x.AutorId).HasColumnName("autores_id");
			autorHasLibroEntity.HasOne(x => x.Autor).WithMany(x => x.AutorHasLibros)
				.HasForeignKey(x => x.AutorId)
				.IsRequired();
			autorHasLibroEntity.Property(x => x.LibroId).HasColumnName("libros_ISBN");
			autorHasLibroEntity.HasOne(x => x.Libro).WithMany(x => x.LibroHasAutores)
				.HasForeignKey(x => x.LibroId)
				.IsRequired();
			autorHasLibroEntity.HasIndex(x => new { x.AutorId, x.LibroId }).IsUnique();
			autorHasLibroEntity.Ignore(x => x.CreatedDate);
			#endregion

			#region editorialEntity
			var editorialEntity = modelBuilder.Entity<Editorial>();
			editorialEntity.ToTable("Editoriales");
			editorialEntity.HasKey(x => x.Id);
			editorialEntity.Property(x => x.Nombre).IsRequired().HasMaxLength(45);
			editorialEntity.Property(x => x.Sede).IsRequired().HasMaxLength(45);
			editorialEntity.HasMany(x => x.Libros).WithOne(x => x.Editorial).HasPrincipalKey(x => x.Id).HasForeignKey(x => x.EditorialId);
			autorEntity.Property(x => x.CreatedDate).IsRequired().HasDefaultValue(DateTime.Now);
			#endregion

			#region auth
			var authEntity = modelBuilder.Entity<Usuario>();
			authEntity.ToTable("Usuarios");
			authEntity.HasKey(x => x.Id);
			authEntity.Property(x => x.Nombre).IsRequired().HasMaxLength(20);
			authEntity.Property(x => x.UserName).IsRequired().HasMaxLength(20);
			authEntity.Property(x => x.Password).IsRequired();
			autorEntity.Property(x => x.CreatedDate).IsRequired().HasDefaultValue(DateTime.Now);
			#endregion
		}
	}
}
