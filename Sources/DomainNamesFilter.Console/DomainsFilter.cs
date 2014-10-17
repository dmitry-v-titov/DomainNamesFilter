namespace DomainNamesFilter.Console
{
    using System.Collections.Generic;
    using System.Linq;

    using DomainNamesFilter.Core;
    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.Factories;
    using DomainNamesFilter.Core.MatchAlgorithms;
    using DomainNamesFilter.Core.Repositories;

    /// <summary>
    /// Демонстрационный фильтр доменных имен
    /// </summary>
    /// <remarks>
    /// Фасадный класс
    /// </remarks>
    internal sealed class DomainsFilter
    {
        #region Constants

        /// <summary>
        /// Количество элементов из черного списка доменов
        /// </summary>
        private const int BLACK_LIST_COUNT = 1000;

        /// <summary>
        /// Количество элементов в черном списке доменов
        /// </summary>
        private const int DOMAINS_COUNT = 20000;

        /// <summary>
        /// Максимальный уровень домена
        /// </summary>
        private const byte MAX_DOMAIN_LEVEL = 3;

        /// <summary>
        /// Максимальный уровень создаваемых доменов
        /// </summary>
        private const byte MAX_NEW_DOMAIN_LEVEL = 3;

        /// <summary>
        /// Количество элементов в рабочем списке доменов
        /// </summary>
        private const int NEW_DOMAINS_COUNT = 1000;

        #endregion

        #region Fields

        /// <summary>
        /// Параметры сравнения списков доменных имен
        /// </summary>
        private MatchParameters _parameters;

        /// <summary>
        /// Коллекция отфильтрованных доменов
        /// </summary>
        private IEnumerable<Domain> _resultDomains;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public DomainsFilter()
        {
            this.InitializeData();
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Количество элементов в черном списке доменов
        /// </summary>
        public int BlackListDomainsCount
        {
            get
            {
                return this._parameters.BlackListDomainsesRepository.GetAllEntities().Count();
            }
        }

        /// <summary>
        /// Количество элементов в отфильтрованном списке доменов
        /// </summary>
        public int ResultDomainsCount
        {
            get
            {
                return this._resultDomains.Count();
            }
        }

        /// <summary>
        /// Количество элементов в исходном списке доменов
        /// </summary>
        public int WorkingDomainsCount
        {
            get
            {
                return this._parameters.WorkingDomainsesRepository.GetAllEntities().Count();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Выполняет фильтрацию доменов
        /// </summary>
        public void Match()
        {
            var domainsMatch = new DomainsMatch(this._parameters);

            this._resultDomains = domainsMatch.Execute();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Инициализация исходных данных
        /// </summary>
        private void InitializeData()
        {
            var blackListDomainsRepositoryFactory = new BlackListDomainsRepositoryFactory(MAX_DOMAIN_LEVEL, DOMAINS_COUNT);
            BlackListDomainsRepository blackListDomainsRepository = blackListDomainsRepositoryFactory.Create();

            var workingDomainsRepositoryFactory = new WorkingDomainsRepositoryFactory(blackListDomainsRepository.GetAllEntities(), 
                                                                                      BLACK_LIST_COUNT, 
                                                                                      MAX_NEW_DOMAIN_LEVEL, 
                                                                                      NEW_DOMAINS_COUNT);
            WorkingDomainsRepository workingDomainsRepository = workingDomainsRepositoryFactory.Create();

            this._parameters = new MatchParameters
                              {
                                  Algorithm = new DomainsTreeDomainsMatchAlgorithm(), 
                                  BlackListDomainsesRepository = blackListDomainsRepository, 
                                  WorkingDomainsesRepository = workingDomainsRepository
                              };

            this._resultDomains = Enumerable.Empty<Domain>();
        }

        #endregion
    }
}