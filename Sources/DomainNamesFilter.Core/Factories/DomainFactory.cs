namespace DomainNamesFilter.Core.Factories
{
    using DomainNamesFilter.Core.DTO;

    /// <summary>
    /// ������� �������
    /// </summary>
    internal class DomainFactory : IFactory<Domain>
    {
        #region Fields

        /// <summary>
        /// ������� �������� ����
        /// </summary>
        private readonly IFactory<string> _domainNameFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// ����������� ������
        /// </summary>
        /// <param name="domainNameFactory">
        /// ������� �������� ����
        /// </param>
        public DomainFactory(IFactory<string> domainNameFactory)
        {
            _domainNameFactory = domainNameFactory;
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
        public Domain Create()
        {
            return new Domain
                       {
                           Name = _domainNameFactory.Create()
                       };
        }

        #endregion
    }
}