namespace DomainNamesFilter.Core.Extensions
{
    using System.Linq;

    using DomainNamesFilter.Core.DTO;

    /// <summary>
    /// Расширения для класса <see cref="Domain" />
    /// </summary>
    internal static class DomainExtensions
    {
        #region Public Methods and Operators

        /// <summary>
        /// Сравнение доменных имен
        /// </summary>
        /// <param name="currentDomain">
        /// Текущий домен
        /// </param>
        /// <param name="sourceDomain">
        /// Исходный домен
        /// </param>
        /// <returns>
        /// 0 - если домены равны, 1 - если текущий домен больше результирующего, -1 - если текущий домен меньше результирующего
        /// </returns>
        public static int CompareDomainName(this Domain currentDomain, Domain sourceDomain)
        {
            var currentDomainName = new string(currentDomain.Name.Reverse().ToArray());
            var sourceDomainName = new string(sourceDomain.Name.Reverse().ToArray());

            return string.CompareOrdinal(currentDomainName, sourceDomainName);
        }

        /// <summary>
        /// Извлечение старшего доменного имени из домена.
        /// В resultDomain содержится поддомен домена domain
        /// </summary>
        /// <param name="domain">
        /// Текущий домен
        /// </param>
        /// <param name="resultDomain">
        /// Поддомен текущего домена
        /// </param>
        /// <returns>
        /// Старшее доменное имя
        /// </returns>
        public static string ExtractTopLevelDomain(this Domain domain, out Domain resultDomain)
        {
            string subdomainName;
            int indexOf = domain.Name.LastIndexOf('.');

            resultDomain = new Domain();

            if (indexOf == 0)
            {
                subdomainName = domain.Name.Remove(0, 1);
                resultDomain.Name = domain.Name;
            }
            else if (indexOf < 0)
            {
                subdomainName = domain.Name;
                resultDomain.Name = domain.Name;
            }
            else
            {
                subdomainName = domain.Name.Substring(indexOf + 1, domain.Name.Length - indexOf - 1);
                resultDomain.Name = domain.Name.Substring(0, indexOf);
            }

            return subdomainName;
        }

        /// <summary>
        /// Возвращает уровень домена
        /// </summary>
        /// <param name="domain">
        /// Домен
        /// </param>
        /// <returns>
        /// Уровень домена
        /// </returns>
        public static int GetLevel(this Domain domain)
        {
            const int TOP_LEVEL_DOMAIN = 1;

            if (domain.IsTopLevelDomain())
            {
                return TOP_LEVEL_DOMAIN;
            }

            return domain.Name.Count(ch => ch.Equals('.')) + 1;
        }

        /// <summary>
        /// Проверка что текущий домен является поддоменом исходного
        /// </summary>
        /// <param name="currentDomain">
        /// Текущий домен
        /// </param>
        /// <param name="sourceDomain">
        /// Исходный домен
        /// </param>
        /// <returns>
        /// Истина, если текущий домен является поддоменом исходного
        /// </returns>
        public static bool IsSubDomain(this Domain currentDomain, Domain sourceDomain)
        {
            if (currentDomain.Name.Length < sourceDomain.Name.Length)
            {
                return false;
            }

            return currentDomain.Name.Length == sourceDomain.Name.Length
                       ? currentDomain.Name.Equals(sourceDomain.Name)
                       : currentDomain.Name.EndsWith(sourceDomain.Name.IndexOf('.') == 0
                                                         ? sourceDomain.Name
                                                         : string.Format(".{0}", sourceDomain.Name));
        }

        /// <summary>
        /// Проверка что домен является доменом верхнего уровня
        /// </summary>
        /// <param name="domain">
        /// Домен
        /// </param>
        /// <returns>
        /// Истина, если домен является доменом верхнего уровня
        /// </returns>
        public static bool IsTopLevelDomain(this Domain domain)
        {
            return domain.Name.IndexOf('.') <= 0;
        }

        #endregion
    }
}