namespace DomainNamesFilter.Console
{
    using System;

    using DomainNamesFilter.Console;

    /// <summary>
    /// Реализация консольного приложения
    /// </summary>
    internal class Program
    {
        #region Methods

        /// <summary>
        /// Точка входа для консольного приложения
        /// </summary>
        private static void Main()
        {
            Console.WriteLine("Инициализация исходных данных...\n");
            var parser = new DomainsFilter();

            Console.WriteLine("Количество элементов в исходном списке доменов: {0}", parser.WorkingDomainsCount);
            Console.WriteLine("Количество элементов в черном списке доменов: {0}\n", parser.BlackListDomainsCount);

            Console.WriteLine("Выполнение фильтрации...\n");
            parser.Match();

            Console.WriteLine("Количество элементов в результирующем списке доменов: {0}\n", parser.ResultDomainsCount);

            Console.WriteLine("Для выхода нажмите любую клавишу...");
            Console.ReadKey();
        }

        #endregion
    }
}