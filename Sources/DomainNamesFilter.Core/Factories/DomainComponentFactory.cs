namespace DomainNamesFilter.Core.Factories
{
    using DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Composite;

    /// <summary>
    /// Фабрика доменных компонент
    /// </summary>
    internal sealed class DomainComponentFactory : IDomainComponentFactory
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
            if (IsLeaf)
            {
                return new SubdomainLeaf(SubdomainName);
            }

            return new Subdomain(SubdomainName);
        }

        #endregion
    }
}