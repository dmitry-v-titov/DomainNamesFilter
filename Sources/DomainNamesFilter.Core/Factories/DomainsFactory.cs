namespace DomainNamesFilter.Core.Factories
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.DTO;

    /// <summary>
    /// Фабрика коллекций доменов
    /// </summary>
    internal sealed class DomainsFactory : IFactory<IEnumerable<Domain>>
    {
        #region Fields

        /// <summary>
        /// Фабрика доменов
        /// </summary>
        private readonly IFactory<Domain> _domainFactory;

        /// <summary>
        /// Количество доменов
        /// </summary>
        private readonly int _domainsCount;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="domainFactory">
        /// Фабрика доменов
        /// </param>
        /// <param name="domainsCount">
        /// Количество доменов
        /// </param>
        public DomainsFactory(IFactory<Domain> domainFactory, int domainsCount)
        {
            _domainFactory = domainFactory;
            _domainsCount = domainsCount;
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
        public IEnumerable<Domain> Create()
        {
            var domains = new List<Domain>();

            for (int i = 0; i < _domainsCount; i++)
            {
                domains.Add(_domainFactory.Create());
            }

            return domains;
        }

        #endregion
    }
}