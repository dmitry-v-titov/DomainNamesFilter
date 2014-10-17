namespace DomainNamesFilter.Core.Factories
{
    using System.Collections.Generic;
    using System.Linq;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.Repositories;

    /// <summary>
    /// Фабрика репозитория проверяемых доменов
    /// </summary>
    public sealed class WorkingDomainsRepositoryFactory : IFactory<WorkingDomainsRepository>
    {
        #region Fields

        /// <summary>
        /// Фабрика коллекций доменов (первая часть)
        /// </summary>
        private readonly IFactory<IEnumerable<Domain>> _domainsFirstFactory;

        /// <summary>
        /// Фабрика коллекций доменов (вторая часть)
        /// </summary>
        private readonly IFactory<IEnumerable<Domain>> _domainsSecondFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="blackListDomains">
        /// Коллекция списка черных доменов
        /// </param>
        /// <param name="blackListCount">
        /// Количество элементов в черном списке доменов
        /// </param>
        /// <param name="maxNewDomainLevel">
        /// Максимальный уровень доменов второй части
        /// </param>
        /// <param name="newDomainsCount">
        /// Количество доменов второй части
        /// </param>
        public WorkingDomainsRepositoryFactory(IEnumerable<Domain> blackListDomains, int blackListCount, byte maxNewDomainLevel, int newDomainsCount)
        {
            IFactory<string> domainNameFirstFactory = new RandomSelectDomainNameFactory(blackListDomains);
            IFactory<Domain> domainFirstFactory = new DomainFactory(domainNameFirstFactory);

            _domainsFirstFactory = new DomainsFactory(domainFirstFactory, blackListCount);

            IFactory<string> domainNameFactory = new UniqueLevelAndDomainNameFactory(maxNewDomainLevel);
            IFactory<Domain> domainFactory = new DomainFactory(domainNameFactory);

            _domainsSecondFactory = new DomainsFactory(domainFactory, newDomainsCount);
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
        public WorkingDomainsRepository Create()
        {
            IEnumerable<Domain> domainsFirstPart = _domainsFirstFactory.Create();
            IEnumerable<Domain> domainsSecondPart = _domainsSecondFactory.Create();

            IEnumerable<Domain> domains = domainsFirstPart.Concat(domainsSecondPart);

            return new WorkingDomainsRepository(domains);
        }

        #endregion
    }
}