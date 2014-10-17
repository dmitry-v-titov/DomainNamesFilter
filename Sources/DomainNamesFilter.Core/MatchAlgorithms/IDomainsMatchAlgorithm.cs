namespace DomainNamesFilter.Core.MatchAlgorithms
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.DTO;

    /// <summary>
    /// Интерфейс алгоритма фильтрации доменов
    /// </summary>
    public interface IDomainsMatchAlgorithm
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
        IEnumerable<Domain> Match(IEnumerable<Domain> sourceDomains, IEnumerable<Domain> blackListDomains);

        #endregion
    }
}