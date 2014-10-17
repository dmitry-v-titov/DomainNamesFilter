namespace DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Composite
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.Factories;

    /// <summary>
    /// Доменная компонента композиции
    /// </summary>
    internal sealed class Subdomain : BaseDomainComponent
    {
        #region Constructors and Destructors

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="subdomainName">
        /// Имя поддомена
        /// </param>
        public Subdomain(string subdomainName = "")
            : base(false, subdomainName)
        {
            Nodes = new Dictionary<string, IDomainComponent>();
            ComponentFactory = new DomainComponentFactory();
        }

        #endregion
    }
}