namespace DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Composite
{
    using DomainNamesFilter.Core.DTO;

    /// <summary>
    /// Интерфейс доменной компоненты
    /// </summary>
    public interface IDomainComponent
    {
        #region Public Properties

        /// <summary>
        /// Признак листа дерева
        /// </summary>
        bool IsLeaf { get; }

        /// <summary>
        /// Имя поддомена
        /// </summary>
        string SubdomainName { get; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Добавляет домен в дерево доменных имен
        /// </summary>
        /// <param name="domain">
        /// Домен
        /// </param>
        void Add(Domain domain);

        /// <summary>
        /// Ищет домен в дереве доменных имен
        /// </summary>
        /// <param name="domain">
        /// Домен
        /// </param>
        /// <returns>
        /// Истина, если домен найден
        /// </returns>
        bool Find(Domain domain);

        #endregion
    }
}