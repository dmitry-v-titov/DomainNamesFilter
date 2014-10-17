namespace DomainNamesFilter.Core.Factories
{
    using System.Collections.Generic;
    using System.Linq;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.Repositories;

    /// <summary>
    /// ������� ����������� ����������� �������
    /// </summary>
    public sealed class WorkingDomainsRepositoryFactory : IFactory<WorkingDomainsRepository>
    {
        #region Fields

        /// <summary>
        /// ������� ��������� ������� (������ �����)
        /// </summary>
        private readonly IFactory<IEnumerable<Domain>> _domainsFirstFactory;

        /// <summary>
        /// ������� ��������� ������� (������ �����)
        /// </summary>
        private readonly IFactory<IEnumerable<Domain>> _domainsSecondFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// ����������� ������
        /// </summary>
        /// <param name="blackListDomains">
        /// ��������� ������ ������ �������
        /// </param>
        /// <param name="blackListCount">
        /// ���������� ��������� � ������ ������ �������
        /// </param>
        /// <param name="maxNewDomainLevel">
        /// ������������ ������� ������� ������ �����
        /// </param>
        /// <param name="newDomainsCount">
        /// ���������� ������� ������ �����
        /// </param>
        public WorkingDomainsRepositoryFactory(IEnumerable<Domain> blackListDomains, int blackListCount, byte maxNewDomainLevel, int newDomainsCount)
        {
            IFactory<string> domainNameFirstFactory = new RandomSelectDomainNameFactory(blackListDomains);
            IFactory<Domain> domainFirstFactory = new DomainFactory(domainNameFirstFactory);

            _domainsFirstFactory = new DomainsFactory(domainFirstFactory, blackListCount);

            IFactory<string> domainNameFactory = new UniqueLevelAndDomainNameFactory(maxNewDomainLevel);
            IFactory<Domain> domainFactory = new DomainFactory(domainNameFactory);

            _domainsSecondFactory = new DomainsFactory(domainFactory, newDomainsCount);
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// ������� ������
        /// </summary>
        /// <returns>
        /// ������
        /// </returns>
        /// <remarks>
        /// ��������� �����
        /// </remarks>
        public WorkingDomainsRepository Create()
        {
            IEnumerable<Domain> domainsFirstPart = _domainsFirstFactory.Create();
            IEnumerable<Domain> domainsSecondPart = _domainsSecondFactory.Create();

            IEnumerable<Domain> domains = domainsFirstPart.Concat(domainsSecondPart);

            return new WorkingDomainsRepository(domains);
        }

        #endregion
    }
}