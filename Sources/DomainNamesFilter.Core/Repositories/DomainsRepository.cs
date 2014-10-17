namespace DomainNamesFilter.Core.Repositories
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.DTO;

    /// <summary>
    /// Базовый класс репозитория доменов
    /// </summary>
    public abstract class DomainsRepository : IRepository<Domain>
    {
        /// <summary>
        /// Коллекция доменов
        /// </summary>
        private readonly IEnumerable<Domain> _domains;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="domains">
        /// Коллекция доменов
        /// </param>
        protected DomainsRepository(IEnumerable<Domain> domains)
        {
            _domains = domains;
        }

        /// <summary>
        /// Возвращает все элементы репозитория
        /// </summary>
        /// <returns>
        /// Коллекция всех элементов репозитория
        /// </returns>
        public IEnumerable<Domain> GetAllEntities()
        {
            return _domains;
        }
    }
}