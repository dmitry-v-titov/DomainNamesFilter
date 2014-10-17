namespace DomainNamesFilter.Core.MatchAlgorithms
{
    using System.Collections.Generic;
    using System.Linq;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Comparers;

    /// <summary>
    /// �������� ���������� ������� ������������ �������� ����� �������.
    /// ������������ ������������ ������� ������ ������������ �������
    /// </summary>
    public sealed class ParallelBinarySearchDomainsMatchAlgorithm : IDomainsMatchAlgorithm
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
            var sortingComparer = new SortingDomainComparer();
            var comparer = new BlackListDomainComparer();
            List<Domain> domains = blackListDomains.ToList();

            domains.Sort(sortingComparer);

            return sourceDomains.AsParallel().Where(sourceDomain => domains.BinarySearch(sourceDomain, comparer) < 0).ToList();
        }

        #endregion
    }
}