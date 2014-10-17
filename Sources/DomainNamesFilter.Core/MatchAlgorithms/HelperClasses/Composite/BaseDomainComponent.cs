namespace DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Composite
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.Extensions;
    using DomainNamesFilter.Core.Factories;

    /// <summary>
    /// Базовый класс доменной компоненты композиции
    /// </summary>
    internal abstract class BaseDomainComponent : IDomainComponent
    {
        #region Constructors and Destructors

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="isLeaf">
        /// Признак листа дерева
        /// </param>
        /// <param name="subdomainName">
        /// Имя поддомена
        /// </param>
        protected BaseDomainComponent(bool isLeaf, string subdomainName)
        {
            IsLeaf = isLeaf;
            SubdomainName = subdomainName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Признак листа дерева
        /// </summary>
        public bool IsLeaf { get; private set; }

        /// <summary>
        /// Имя поддомена
        /// </summary>
        public string SubdomainName { get; private set; }

        #endregion

        #region Properties

        /// <summary>
        /// Интерфейс фабрики доменных компонент
        /// </summary>
        protected IDomainComponentFactory ComponentFactory { get; set; }

        /// <summary>
        /// Словарь доменных компонентов
        /// </summary>
        /// <remarks>
        /// Узлы дерева
        /// </remarks>
        protected IDictionary<string, IDomainComponent> Nodes { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Добавляет домен в дерево доменных имен
        /// </summary>
        /// <param name="domain">
        /// Домен
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
        /// Ищет домен в дереве доменных имен
        /// </summary>
        /// <param name="domain">
        /// Домен
        /// </param>
        /// <returns>
        /// Истина, если домен найден
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