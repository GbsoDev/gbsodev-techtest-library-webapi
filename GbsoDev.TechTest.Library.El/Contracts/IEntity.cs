
namespace GbsoDev.TechTest.Library.El.Contracts
{
	/// <summary>
	/// Interfaz para entidades
	/// </summary>
	/// <typeparam name="TKey"></typeparam>
	public interface IEntity<TKey> 
	{
		/// <summary>
		/// Clave de identidad de la entidad
		/// </summary>
		public TKey Id { get; set; }
		/// <summary>
		/// Registro para auditoría
		/// </summary>
		DateTime? CreatedDate { get; set; }
	}
}
