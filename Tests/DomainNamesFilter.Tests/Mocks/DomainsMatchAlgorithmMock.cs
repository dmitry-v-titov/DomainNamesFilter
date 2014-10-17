namespace DomainNamesFilter.Tests.Mocks
{
    using System.Collections.Generic;
    using System.Linq;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.MatchAlgorithms;

    /// <summary>
    /// Заглушка алгоритма фильтрации доменов
    /// </summary>
    internal sealed class DomainsMatchAlgorithmMock : IDomainsMatchAlgorithm
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
            return Enumerable.Empty<Domain>();
        }

        #endregion
    }
}