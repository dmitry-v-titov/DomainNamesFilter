namespace DomainNamesFilter.Core
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.DTO;

    /// <summary>
    /// Фильтр доменных имен
    /// </summary>
    public sealed class DomainsMatch
    {
        #region Fields

        /// <summary>
        /// Параметры сравнения списков доменных имен
        /// </summary>
        private readonly MatchParameters _parameters;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="parameters">
        /// Параметры сравнения списков доменных имен
        /// </param>
        public DomainsMatch(MatchParameters parameters)
        {
            _parameters = parameters;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Выполнение фильтрации
        /// </summary>
        /// <returns>
        /// Отфильтрованный список доменных имен
        /// </returns>
        public IEnumerable<Domain> Execute()
        {
            IEnumerable<Domain> sourceDomains = _parameters.WorkingDomainsesRepository.GetAllEntities();
            IEnumerable<Domain> blackListDomains = _parameters.BlackListDomainsesRepository.GetAllEntities();

            return _parameters.Algorithm.Match(sourceDomains, blackListDomains);
        }

        #endregion
    }
}