namespace DomainNamesFilter.Core.Repositories
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.DTO;

    /// <summary>
    /// Репозиторий черного списка доменов
    /// </summary>
    public sealed class BlackListDomainsRepository : DomainsRepository
    {
        #region Constructors and Destructors

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="domains">
        /// Коллекция доменов
        /// </param>
        public BlackListDomainsRepository(IEnumerable<Domain> domains)
            : base(domains)
        {
        }

        #endregion
    }
}