namespace DomainNamesFilter.Core.Factories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using DomainNamesFilter.Core.DTO;

    /// <summary>
    /// Фабрика доменных имен.
    /// Создает доменное имя на основе случайно выбранного доменного имени из коллекции доменов
    /// </summary>
    internal sealed class RandomSelectDomainNameFactory : IFactory<string>
    {
        #region Static Fields

        /// <summary>
        /// Генератор случайных чисел 
        /// </summary>
        private static readonly Random __random = new Random();

        #endregion

        #region Fields

        /// <summary>
        /// Коллекция доменов 
        /// </summary>
        private readonly IEnumerable<Domain> _domains;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Конструктор класса 
        /// </summary>
        /// <param name="domains">
        /// Коллекция доменов  
        /// </param>
        public RandomSelectDomainNameFactory(IEnumerable<Domain> domains)
        {
            _domains = domains;
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
        public string Create()
        {
            return _domains.ElementAt(__random.Next(_domains.Count())).Name;
        }

        #endregion
    }
}