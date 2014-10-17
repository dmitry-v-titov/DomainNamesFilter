namespace DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Comparers
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.Extensions;

    /// <summary>
    /// Определяет методы для поддержки операций сравнения объектов типа <see cref="Domain" /> в отношении равенства
    /// </summary>
    internal sealed class BlackListDomainEqualityComparer : EqualityComparer<Domain>
    {
        #region Public Methods and Operators

        /// <summary>
        /// Определяет, равны ли два указанных объекта
        /// </summary>
        /// <param name="x">
        /// Первый сравниваемый объект
        /// </param>
        /// <param name="y">
        /// Второй сравниваемый объект
        /// </param>
        /// <returns>
        /// true , если указанные объекты равны; в противном случае — false
        /// </returns>
        public override bool Equals(Domain x, Domain y)
        {
            return y.IsSubDomain(x);
        }

        /// <summary>
        /// Возвращает хэш-код указанного объекта
        /// </summary>
        /// <param name="obj">
        /// Объект для которого необходимо возвратить хэш-код
        /// </param>
        /// <returns>
        /// Хэш-код указанного объекта
        /// </returns>
        public override int GetHashCode(Domain obj)
        {
            return obj != null ? obj.Name.GetHashCode() : 0;
        }

        #endregion
    }
}