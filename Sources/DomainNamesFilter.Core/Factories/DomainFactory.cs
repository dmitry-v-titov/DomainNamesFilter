namespace DomainNamesFilter.Core.Factories
{
    using DomainNamesFilter.Core.DTO;

    /// <summary>
    /// Фабрика доменов
    /// </summary>
    internal class DomainFactory : IFactory<Domain>
    {
        #region Fields

        /// <summary>
        /// Фабрика доменных имен
        /// </summary>
        private readonly IFactory<string> _domainNameFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="domainNameFactory">
        /// Фабрика доменных имен
        /// </param>
        public DomainFactory(IFactory<string> domainNameFactory)
        {
            _domainNameFactory = domainNameFactory;
        }

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
        public Domain Create()
        {
            return new Domain
                       {
                           Name = _domainNameFactory.Create()
                       };
        }

        #endregion
    }
}