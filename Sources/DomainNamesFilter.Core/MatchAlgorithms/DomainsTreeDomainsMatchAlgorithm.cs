namespace DomainNamesFilter.Core.MatchAlgorithms
{
    using System.Collections.Generic;
    using System.Linq;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Composite;

    /// <summary>
    /// Алгоритм фильтрации доменов использующий доменно дерево для поиска соответствия
    /// </summary>
    public sealed class DomainsTreeDomainsMatchAlgorithm : IDomainsMatchAlgorithm
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
            var domainsTree = new DomainsTree(blackListDomains);

            return sourceDomains.Where(sourceDomain => !domainsTree.Find(sourceDomain)).ToList();
        }

        #endregion
    }
}