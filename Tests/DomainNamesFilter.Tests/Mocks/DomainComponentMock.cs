namespace DomainNamesFilter.Tests.Mocks
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.Factories;
    using DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Composite;

    /// <summary>
    /// Заглушка доменной компоненты композиции
    /// </summary>
    internal sealed class DomainComponentMock : BaseDomainComponent
    {
        #region Constructors and Destructors

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="componentFactory">
        /// Интерфейс фабрики доменных компонент
        /// </param>
        /// <param name="isLeaf">
        /// Признак листа дерева
        /// </param>
        /// <param name="subdomainName">
        /// Имя поддомена
        /// </param>
        public DomainComponentMock(IDomainComponentFactory componentFactory, bool isLeaf = false, string subdomainName = "")
            : base(isLeaf, subdomainName)
        {
            Nodes = IsLeaf ? null : new Dictionary<string, IDomainComponent>();
            ComponentFactory = componentFactory;
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Возвращает узлы компоненты
        /// </summary>
        /// <returns>
        /// Коллекция доменных компонентов
        /// </returns>
        public IEnumerable<IDomainComponent> GetNodes()
        {
            return Nodes != null ? Nodes.Values : null;
        }

        #endregion
    }
}