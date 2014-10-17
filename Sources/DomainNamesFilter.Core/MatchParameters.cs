namespace DomainNamesFilter.Core
{
    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.MatchAlgorithms;
    using DomainNamesFilter.Core.Repositories;

    /// <summary>
    /// Параметры сравнения списков доменных имен
    /// </summary>
    public sealed class MatchParameters
    {
        #region Public Properties

        /// <summary>
        /// Интерфейс алгоритма фильтрации доменов
        /// </summary>
        public IDomainsMatchAlgorithm Algorithm { get; set; }

        /// <summary>
        /// Репозиторий черного списка имен доменов
        /// </summary>
        public IRepository<Domain> BlackListDomainsesRepository { get; set; }

        /// <summary>
        /// Репозиторий проверяемых доменов
        /// </summary>
        public IRepository<Domain> WorkingDomainsesRepository { get; set; }

        #endregion
    }
}