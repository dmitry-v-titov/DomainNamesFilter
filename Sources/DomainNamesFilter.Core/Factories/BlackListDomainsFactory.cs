namespace DomainNamesFilter.Core.Factories
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.Repositories;

    /// <summary>
    /// Фабрика репозитория черного списка доменов
    /// </summary>
    public sealed class BlackListDomainsRepositoryFactory : IFactory<BlackListDomainsRepository>
    {
        #region Fields

        /// <summary>
        /// Фабрика коллекции доменов
        /// </summary>
        private readonly IFactory<IEnumerable<Domain>> _domainsFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="maxDomainLevel">
        /// Максимальный уровень доменов
        /// </param>
        /// <param name="domainsCount">
        /// Количество доменов
        /// </param>
        public BlackListDomainsRepositoryFactory(byte maxDomainLevel, int domainsCount)
        {
            IFactory<string> domainNameFactory = new UniqueLevelAndDomainNameFactory(maxDomainLevel);
            IFactory<Domain> domainFactory = new DomainFactory(domainNameFactory);

            _domainsFactory = new DomainsFactory(domainFactory, domainsCount);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Создает объект
        /// </summary>
        /// <returns>
        /// Объект
        /// </returns>
        /// <remarks>
        /// Фабричный метод
        /// </remarks>
        public BlackListDomainsRepository Create()
        {
            IEnumerable<Domain> domains = _domainsFactory.Create();

            return new BlackListDomainsRepository(domains);
        }

        #endregion
    }
}