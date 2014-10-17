namespace DomainNamesFilter.Tests.Mocks
{
    using DomainNamesFilter.Core.Factories;
    using DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Composite;

    /// <summary>
    /// Заглушка фабрики доменных компонент
    /// </summary>
    public class DomainComponentFactoryMock : IDomainComponentFactory
    {
        #region Public Properties

        /// <summary>
        /// Признак листа дерева
        /// </summary>
        public bool IsLeaf { get; set; }

        /// <summary>
        /// Имя поддомена
        /// </summary>
        public string SubdomainName { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Создает объект
        /// </summary>
        /// <returns>
        /// Объект
        /// </returns>
        /// <remarks>
        /// Фабричный метод
        /// </remarks>
        public IDomainComponent Create()
        {
            return new DomainComponentMock(this, IsLeaf, SubdomainName);
        }

        #endregion
    }
}