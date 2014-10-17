namespace DomainNamesFilter.Core.MatchAlgorithms
{
    using System.Collections.Generic;
    using System.Linq;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Comparers;

    /// <summary>
    /// �������� ���������� ������� ������������ ����������� ��������� ����, ��� ����� �������� ����������
    /// </summary>
    public sealed class StringBasedComparisonDomainsMatchAlgorithm : IDomainsMatchAlgorithm
    {
        #region Public Methods and Operators

        /// <summary>
        /// ��������� ��������� ������� �� �������� � ������ ������
        /// </summary>
        /// <param name="sourceDomains">
        /// �������� ��������� �������
        /// </param>
        /// <param name="blackListDomains">
        /// ������ ������ �������
        /// </param>
        /// <returns>
        /// ��������������� ��������� �������
        /// </returns>
        public IEnumerable<Domain> Match(IEnumerable<Domain> sourceDomains, IEnumerable<Domain> blackListDomains)
        {
            var comparer = new BlackListDomainEqualityComparer();

            return sourceDomains.Where(sourceDomain => !blackListDomains.Contains(sourceDomain, comparer)).ToList();
        }

        #endregion
    }
}