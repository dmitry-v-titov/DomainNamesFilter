namespace DomainNamesFilter.Core.Repositories
{
    using System.Collections.Generic;

    /// <summary>
    /// Интерфейс репозитория
    /// </summary>
    /// <typeparam name="TEntity">
    /// Тип элементов репозитория
    /// </typeparam>
    public interface IRepository<out TEntity>
    {
        /// <summary>
        /// Возвращает все элементы репозитория
        /// </summary>
        /// <returns>
        /// Коллекция всех элементов репозитория
        /// </returns>
        IEnumerable<TEntity> GetAllEntities();
    }
}