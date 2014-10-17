namespace DomainNamesFilter.Core.Factories
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.Repositories;

    /// <summary>
    /// ������� ����������� ������� ������ �������
    /// </summary>
    public sealed class BlackListDomainsRepositoryFactory : IFactory<BlackListDomainsRepository>
    {
        #region Fields

        /// <summary>
        /// ������� ��������� �������
        /// </summary>
        private readonly IFactory<IEnumerable<Domain>> _domainsFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// ����������� ������
        /// </summary>
        /// <param name="maxDomainLevel">
        /// ������������ ������� �������
        /// </param>
        /// <param name="domainsCount">
        /// ���������� �������
        /// </param>
        public BlackListDomainsRepositoryFactory(byte maxDomainLevel, int domainsCount)
        {
            IFactory<string> domainNameFactory = new UniqueLevelAndDomainNameFactory(maxDomainLevel);
            IFactory<Domain> domainFactory = new DomainFactory(domainNameFactory);

            _domainsFactory = new DomainsFactory(domainFactory, domainsCount);
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
        public BlackListDomainsRepository Create()
        {
            IEnumerable<Domain> domains = _domainsFactory.Create();

            return new BlackListDomainsRepository(domains);
        }

        #endregion
    }
}