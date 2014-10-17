namespace DomainNamesFilter.Core.Factories
{
    /// <summary>
    /// Интерфейс фабрики объектов
    /// </summary>
    /// <typeparam name="T">
    /// Тип создаваемого объекта фабрикой
    /// </typeparam>
    public interface IFactory<out T>
    {
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
        T Create();

        #endregion
    }
}