namespace DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Composite
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.DTO;

    /// <summary>
    /// Дерево доменов
    /// </summary>
    internal sealed class DomainsTree
    {
        #region Fields

        /// <summary>
        /// Коллекция доменов
        /// </summary>
        private readonly IEnumerable<Domain> _domains;

        /// <summary>
        /// Корень дерева доменов
        /// </summary>
        private readonly IDomainComponent _treeRoot;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="domains">
        /// Коллекция доменов
        /// </param>
        public DomainsTree(IEnumerable<Domain> domains)
        {
            _domains = domains;
            _treeRoot = new Subdomain();

            InitializationDomainsTree();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Поиск домена в дереве доменов
        /// </summary>
        /// <param name="domain">
        /// Домен
        /// </param>
        /// <returns>
        /// Истина, если домен найден
        /// </returns>
        public bool Find(Domain domain)
        {
            return _treeRoot.Find(domain);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Инициализация дерева доменов
        /// </summary>
        private void InitializationDomainsTree()
        {
            foreach (Domain domain in _domains)
            {
                _treeRoot.Add(domain);
            }
        }

        #endregion
    }
}