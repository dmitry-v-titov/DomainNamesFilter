namespace DomainNamesFilter.Core.Factories
{
    using DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Composite;

    /// <summary>
    /// Интерфейс фабрики доменных компонент
    /// </summary>
    public interface IDomainComponentFactory : IFactory<IDomainComponent>
    {
        #region Public Properties

        /// <summary>
        /// Признак листа дерева
        /// </summary>
        bool IsLeaf { get; set; }

        /// <summary>
        /// Имя поддомена
        /// </summary>
        string SubdomainName { get; set; }

        #endregion
    }
}