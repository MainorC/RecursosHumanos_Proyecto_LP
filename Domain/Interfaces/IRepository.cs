namespace RecursosHumanos.Domain.Interfaces
{
    /// <summary>
    /// Interfaz genérica para repositorios (Patrón Repository)
    /// </summary>
    /// <typeparam name="T">Tipo de entidad</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Obtiene todas las entidades
        /// </summary>
        List<T> GetAll();

        /// <summary>
        /// Obtiene una entidad por su ID
        /// </summary>
        T? GetById(int id);

        /// <summary>
        /// Inserta una nueva entidad
        /// </summary>
        bool Insert(T entity);

        /// <summary>
        /// Actualiza una entidad existente
        /// </summary>
        bool Update(T entity);

        /// <summary>
        /// Elimina una entidad (soft delete)
        /// </summary>
        bool Delete(int id);
    }
}

