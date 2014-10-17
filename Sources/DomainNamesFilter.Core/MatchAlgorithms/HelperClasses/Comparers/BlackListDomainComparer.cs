namespace DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Comparers
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.Extensions;

    /// <summary>
    /// Предоставляет метод для сравнения двух объектов типа <see cref="Domain" /> с проверкой на то, что один домен является
    /// поддоменом второго
    /// </summary>
    internal sealed class BlackListDomainComparer : Comparer<Domain>
    {
        #region Public Methods and Operators

        /// <summary>
        /// Сравнение двух объектов и возврат значения, указывающего, является ли один объект меньшим, равным или большим другого
        /// </summary>
        /// <param name="x">
        /// Первый сравниваемый объект
        /// </param>
        /// <param name="y">
        /// Второй сравниваемый объект
        /// </param>
        /// <returns>
        /// Знаковое целое число, которое определяет относительные значения параметров x и y
        /// </returns>
        public override int Compare(Domain x, Domain y)
        {
            if (ReferenceEquals(x, null))
            {
                return -1;
            }

            if (ReferenceEquals(y, null))
            {
                return 1;
            }

            return y.IsSubDomain(x) ? 0 : x.CompareDomainName(y);
        }

        #endregion
    }
}