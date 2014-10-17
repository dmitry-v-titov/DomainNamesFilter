namespace DomainNamesFilter.Core.Factories
{
    using System.Text;

    /// <summary>
    /// ������� �������� ����.
    /// ������� ���������� �������� �����
    /// </summary>
    internal sealed class UniqueDomainNameFactory : IFactory<string>
    {
        #region Constants

        /// <summary>
        /// ��������� ��� ������
        /// </summary>
        private const string DOMAIN_NAME = "domain";

        #endregion

        #region Static Fields

        /// <summary>
        /// ������� ��������� �����
        /// </summary>
        private static int __id;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// ����������� ������
        /// </summary>
        /// <param name="domainLevel">
        /// ������� ������
        /// </param>
        public UniqueDomainNameFactory(byte domainLevel = 1)
        {
            DomainLevel = domainLevel;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// ������� ������
        /// </summary>
        public byte DomainLevel { get; set; }

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
        public string Create()
        {
            const int CAPACITY = 255;

            int id = GetId();

            if (DomainLevel < 1)
            {
                return null;
            }

            if (DomainLevel == 1)
            {
                return string.Format(".{0}{1}", DOMAIN_NAME, id);
            }

            var domainName = new StringBuilder(string.Empty, CAPACITY);

            for (int i = 0; i < DomainLevel; i++)
            {
                domainName.AppendFormat("{0}{1}.", DOMAIN_NAME, id);
            }

            return domainName.Remove(domainName.Length - 1, 1).ToString();
        }

        #endregion

        #region Methods

        /// <summary>
        /// ���������� ������� �������� �������� ��������� �����
        /// </summary>
        /// <returns>
        /// �������� ��������
        /// </returns>
        private static int GetId()
        {
            return __id++;
        }

        #endregion
    }
}