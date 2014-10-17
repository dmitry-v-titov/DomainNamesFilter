namespace DomainNamesFilter.Core.Repositories
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.DTO;

    /// <summary>
    /// Репозиторий проверяемых доменов
    /// </summary>
    public sealed class WorkingDomainsRepository : DomainsRepository
    {
        #region Constructors and Destructors

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="domains">
        /// Коллекция доменов
        /// </param>
        public WorkingDomainsRepository(IEnumerable<Domain> domains)
            : base(domains)
        {
        }

        #endregion
    }
}