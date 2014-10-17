namespace DomainNamesFilter.Core.MatchAlgorithms
{
    using System.Collections.Generic;
    using System.Linq;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Comparers;

    /// <summary>
    /// Алгоритм фильтрации доменов использующий поочередное сравнение того, что домен является поддоменом.
    /// Используются параллельные запросы поиска соответствия доменов
    /// </summary>
    public sealed class ParallelStringBasedComparisonDomainsMatchAlgorithm : IDomainsMatchAlgorithm
    {
        #region Public Methods and Operators

        /// <summary>
        /// Формирует коллекцию доменов не входящих в черный список
        /// </summary>
        /// <param name="sourceDomains">
        /// Исходная коллекция доменов
        /// </param>
        /// <param name="blackListDomains">
        /// Черный список доменов
        /// </param>
        /// <returns>
        /// Отфильтрованная коллекция доменов
        /// </returns>
        public IEnumerable<Domain> Match(IEnumerable<Domain> sourceDomains, IEnumerable<Domain> blackListDomains)
        {
            var comparer = new BlackListDomainEqualityComparer();

            return sourceDomains.AsParallel().Where(sourceDomain => !blackListDomains.Contains(sourceDomain, comparer)).ToList();
        }

        #endregion
    }
}