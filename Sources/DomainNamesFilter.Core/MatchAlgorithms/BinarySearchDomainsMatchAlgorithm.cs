namespace DomainNamesFilter.Core.MatchAlgorithms
{
    using System.Collections.Generic;
    using System.Linq;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Comparers;

    /// <summary>
    /// Алгоритм фильтрации доменов использующий бинарный поиск доменов
    /// </summary>
    public sealed class BinarySearchDomainsMatchAlgorithm : IDomainsMatchAlgorithm
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
            var sortingComparer = new SortingDomainComparer();
            var comparer = new BlackListDomainComparer();
            List<Domain> domains = blackListDomains.ToList();

            domains.Sort(sortingComparer);

            return sourceDomains.Where(sourceDomain => domains.BinarySearch(sourceDomain, comparer) < 0).ToList();
        }

        #endregion
    }
}