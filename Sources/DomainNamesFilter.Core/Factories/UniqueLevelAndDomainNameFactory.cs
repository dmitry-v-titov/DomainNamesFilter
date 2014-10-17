namespace DomainNamesFilter.Core.Factories
{
    using System;

    /// <summary>
    /// Фабрика доменных имен.
    /// Создает уникальные доменные имена со случайным уровнем домена
    /// </summary>
    internal sealed class UniqueLevelAndDomainNameFactory : IFactory<string>
    {
        #region Static Fields

        /// <summary>
        /// Генератор случайных чисел
        /// </summary>
        private static readonly Random __random = new Random();

        #endregion

        #region Fields

        /// <summary>
        /// Максимальный уровень домена
        /// </summary>
        private readonly byte _maxDomainLevel;

        /// <summary>
        /// Фабрика доменных имен
        /// </summary>
        private readonly UniqueDomainNameFactory _uniqueDomainNameFactory;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="maxDomainLevel">
        /// Максимальный уровень домена
        /// </param>
        public UniqueLevelAndDomainNameFactory(byte maxDomainLevel)
        {
            _maxDomainLevel = maxDomainLevel;

            _uniqueDomainNameFactory = new UniqueDomainNameFactory();
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
            _uniqueDomainNameFactory.DomainLevel = (byte)__random.Next(1, _maxDomainLevel + 1);

            return _uniqueDomainNameFactory.Create();
        }

        #endregion
    }
}