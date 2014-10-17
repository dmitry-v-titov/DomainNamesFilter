namespace DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Composite
{
    using DomainNamesFilter.Core.Factories;

    /// <summary>
    /// Доменная компонента листа композиции
    /// </summary>
    internal sealed class SubdomainLeaf : BaseDomainComponent
    {
        #region Constructors and Destructors

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="subdomainName">
        /// Имя поддомена
        /// </param>
        public SubdomainLeaf(string subdomainName)
            : base(true, subdomainName)
        {
            ComponentFactory = new DomainComponentFactory();
        }

        #endregion
    }
}