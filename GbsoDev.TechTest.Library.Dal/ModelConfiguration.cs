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
			libroEntity.Property(x => x.Id).HasColumnType("numeric(10, 0)").ValueGeneratedNever().HasColumnName("ISBN");
			libroEntity.HasKey(x => x.Id);
			libroEntity.HasOne(x=> x.Editorial).WithMany(x=> x.Libros).HasForeignKey(x=>x.EditorialId).IsRequired();
			libroEntity.Property(x => x.Titulo).IsRequired().HasMaxLength(45);
			libroEntity.Property(x => x.Sinopsis).IsRequired();
			libroEntity.Property(x => x.NPaginas).IsRequired().HasMaxLength(45);
			libroEntity.HasMany(l => l.Autores)
			.WithMany(a => a.Libros)
			.UsingEntity<Dictionary<string, object>>(
				"autores_has_libros",
				x => x.HasOne<Autor>().WithMany().HasForeignKey("autores_id"),
				x => x.HasOne<Libro>().WithMany().HasForeignKey("libros_ISBN"),
				x =>
				{
					x.ToTable("autores_has_libros");
					x.HasKey("autores_id", "libros_ISBN");
				}
			);

			#endregion

			#region editorialEntity
			var editorialEntity = modelBuilder.Entity<Editorial>();
			editorialEntity.ToTable("Editoriales");
			editorialEntity.HasKey(x => x.Id);
			editorialEntity.Property(x => x.Nombre).IsRequired().HasMaxLength(45);
			editorialEntity.Property(x => x.Sede).IsRequired().HasMaxLength(45);
			autorEntity.Property(x => x.CreatedDate).IsRequired().HasDefaultValue(DateTime.Now);
			#endregion
		}
	}
}
