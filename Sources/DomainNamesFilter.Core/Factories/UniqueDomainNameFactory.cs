namespace DomainNamesFilter.Core.Factories
{
    using System.Text;

    /// <summary>
    /// Фабрика доменных имен.
    /// Создает уникальные доменные имена
    /// </summary>
    internal sealed class UniqueDomainNameFactory : IFactory<string>
    {
        #region Constants

        /// <summary>
        /// Шаблонное имя домена
        /// </summary>
        private const string DOMAIN_NAME = "domain";

        #endregion

        #region Static Fields

        /// <summary>
        /// Счетчик доменного имени
        /// </summary>
        private static int __id;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="domainLevel">
        /// Уровень домена
        /// </param>
        public UniqueDomainNameFactory(byte domainLevel = 1)
        {
            DomainLevel = domainLevel;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Уровень домена
        /// </summary>
        public byte DomainLevel { get; set; }

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
            const int CAPACITY = 255;

            int id = GetId();

            if (DomainLevel < 1)
            {
                return null;
            }

            if (DomainLevel == 1)
            {
                return string.Format(".{0}{1}", DOMAIN_NAME, id);
            }

            var domainName = new StringBuilder(string.Empty, CAPACITY);

            for (int i = 0; i < DomainLevel; i++)
            {
                domainName.AppendFormat("{0}{1}.", DOMAIN_NAME, id);
            }

            return domainName.Remove(domainName.Length - 1, 1).ToString();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Возвращает текущее значение счетчика доменного имени
        /// </summary>
        /// <returns>
        /// Значение счетчика
        /// </returns>
        private static int GetId()
        {
            return __id++;
        }

        #endregion
    }
}