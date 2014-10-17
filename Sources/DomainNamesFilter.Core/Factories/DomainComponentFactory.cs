namespace DomainNamesFilter.Core.Factories
{
    using DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Composite;

    /// <summary>
    /// ������� �������� ���������
    /// </summary>
    internal sealed class DomainComponentFactory : IDomainComponentFactory
    {
        #region Public Properties

        /// <summary>
        /// ������� ����� ������
        /// </summary>
        public bool IsLeaf { get; set; }

        /// <summary>
        /// ��� ���������
        /// </summary>
        public string SubdomainName { get; set; }

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
        public IDomainComponent Create()
        {
            if (IsLeaf)
            {
                return new SubdomainLeaf(SubdomainName);
            }

            return new Subdomain(SubdomainName);
        }

        #endregion
    }
}