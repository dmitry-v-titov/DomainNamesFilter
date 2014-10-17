namespace DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Composite
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.Extensions;
    using DomainNamesFilter.Core.Factories;

    /// <summary>
    /// ������� ����� �������� ���������� ����������
    /// </summary>
    internal abstract class BaseDomainComponent : IDomainComponent
    {
        #region Constructors and Destructors

        /// <summary>
        /// ����������� ������
        /// </summary>
        /// <param name="isLeaf">
        /// ������� ����� ������
        /// </param>
        /// <param name="subdomainName">
        /// ��� ���������
        /// </param>
        protected BaseDomainComponent(bool isLeaf, string subdomainName)
        {
            IsLeaf = isLeaf;
            SubdomainName = subdomainName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// ������� ����� ������
        /// </summary>
        public bool IsLeaf { get; private set; }

        /// <summary>
        /// ��� ���������
        /// </summary>
        public string SubdomainName { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        /// ��������� ������� �������� ���������
        /// </summary>
        protected IDomainComponentFactory ComponentFactory { get; set; }

        /// <summary>
        /// ������� �������� �����������
        /// </summary>
        /// <remarks>
        /// ���� ������
        /// </remarks>
        protected IDictionary<string, IDomainComponent> Nodes { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// ��������� ����� � ������ �������� ����
        /// </summary>
        /// <param name="domain">
        /// �����
        /// </param>
        public void Add(Domain domain)
        {
            if (IsLeaf)
            {
                return;
            }

            Domain resultDomain;
            ComponentFactory.IsLeaf = domain.IsTopLevelDomain();
            ComponentFactory.SubdomainName = domain.ExtractTopLevelDomain(out resultDomain);

            IDomainComponent domainComponent;
            if (Nodes.TryGetValue(ComponentFactory.SubdomainName, out domainComponent))
            {
                if (ComponentFactory.IsLeaf)
                {
                    domainComponent = ComponentFactory.Create();

                    Nodes[ComponentFactory.SubdomainName] = domainComponent;
                }
            }
            else
            {
                domainComponent = ComponentFactory.Create();

                Nodes.Add(domainComponent.SubdomainName, domainComponent);
            }

            domainComponent.Add(resultDomain);
        }

        /// <summary>
        /// ���� ����� � ������ �������� ����
        /// </summary>
        /// <param name="domain">
        /// �����
        /// </param>
        /// <returns>
        /// ������, ���� ����� ������
        /// </returns>
        public bool Find(Domain domain)
        {
            if (IsLeaf)
            {
                return true;
            }

            Domain resultDomain;
            string topLevelDomainName = domain.ExtractTopLevelDomain(out resultDomain);

            IDomainComponent domainComponent;
            return Nodes.TryGetValue(topLevelDomainName, out domainComponent) && domainComponent.Find(resultDomain);
        }

        #endregion
    }
}