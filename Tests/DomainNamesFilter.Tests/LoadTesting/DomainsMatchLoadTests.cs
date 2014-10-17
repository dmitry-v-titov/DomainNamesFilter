namespace DomainNamesFilter.Tests.LoadTesting
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Linq;

    using DomainNamesFilter.Core;
    using DomainNamesFilter.Core.Factories;
    using DomainNamesFilter.Core.MatchAlgorithms;
    using DomainNamesFilter.Core.Repositories;
    using DomainNamesFilter.Tests.Mocks;

    using NUnit.Framework;

    /// <summary>
    /// Нагрузочное тестирование фильтрации доменов
    /// </summary>
    [TestFixture]
    public class DomainsMatchLoadTests
    {
        #region Constants

        /// <summary>
        /// Количество элементов из черного списка доменов
        /// </summary>
        private const int BLACK_LIST_COUNT = 1000;

        /// <summary>
        /// Количество элементов в черном списке доменов
        /// </summary>
        private const int DOMAINS_COUNT = 20000;

        /// <summary>
        /// Максимальный уровень домена
        /// </summary>
        private const byte MAX_DOMAIN_LEVEL = 3;

        /// <summary>
        /// Максимальный уровень создаваемых доменов
        /// </summary>
        private const byte MAX_NEW_DOMAIN_LEVEL = 3;

        /// <summary>
        /// Количество элементов в рабочем списке доменов
        /// </summary>
        private const int NEW_DOMAINS_COUNT = 1000;

        #endregion

        #region Fields

        /// <summary>
        /// Коллекция параметров теста
        /// </summary>
        private readonly IEnumerable _algorithms = new[]
                                                       {
                                                           new TestCaseData(new DomainsMatchAlgorithmMock()), 
                                                           new TestCaseData(new StringBasedComparisonDomainsMatchAlgorithm()), 
                                                           new TestCaseData(new ParallelStringBasedComparisonDomainsMatchAlgorithm()), 
                                                           new TestCaseData(new BinarySearchDomainsMatchAlgorithm()), 
                                                           new TestCaseData(new ParallelBinarySearchDomainsMatchAlgorithm()), 
                                                           new TestCaseData(new DomainsTreeDomainsMatchAlgorithm()), 
                                                           new TestCaseData(new ParallelDomainsTreeDomainsMatchAlgorithm())
                                                       };

        #endregion

        #region Test Fixture Initialize

        /// <summary>
        /// Инициализирует параметры тестового класса.
        /// Вызывается перед запуском всех тестов
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
        }

        /// <summary>
        /// Освобождает параметры тестового класса.
        /// Вызывается после запуска всех тестов
        /// </summary>
        [TestFixtureTearDown]
        public void Cleanup()
        {
        }

        #endregion

        #region Test Initialize

        /// <summary>
        /// Инициализирует параметры тестового метода.
        /// Вызывается перед запуском каждого теста
        /// </summary>
        [SetUp]
        public void TestInitialize()
        {
            GC.Collect();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        /// <summary>
        /// Освобождает параметры тестового метода.
        /// Вызывается после запуска каждого теста
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
            // _parameters = null;
        }

        #endregion

        #region Test Methods

        /// <summary>
        /// Вычисление среднего времени выполнения фильтрации доменов
        /// </summary>
        /// <param name="algorithm">
        /// Параметры теста
        /// </param>
        [Test]
        [TestCaseSource("_algorithms")]
        public void AlgorithmTime(IDomainsMatchAlgorithm algorithm)
        {
            const int TEST_COUNT = 100;

            var stopwatch = new Stopwatch();
            var millisecondsArray = new long[TEST_COUNT];

            for (int i = 0; i < TEST_COUNT; i++)
            {
                DomainsMatch domainsMatch = CreateDomainsMatch(algorithm);

                stopwatch.Restart();

                domainsMatch.Execute();

                stopwatch.Stop();

                millisecondsArray[i] = stopwatch.ElapsedMilliseconds;
            }

            Trace.WriteLine(string.Format("Average time {0}: {1} ms", algorithm, millisecondsArray.Average()));
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Создает фильтр доменных имен
        /// </summary>
        /// <param name="algorithm">
        /// Алгоритм фильтрации доменов
        /// </param>
        /// <returns>
        /// Фильтр доменных имен
        /// </returns>
        private DomainsMatch CreateDomainsMatch(IDomainsMatchAlgorithm algorithm)
        {
            BlackListDomainsRepositoryFactory blackListDomainsRepositoryFactory = new BlackListDomainsRepositoryFactory(MAX_DOMAIN_LEVEL, 
                                                                                                                        DOMAINS_COUNT);
            BlackListDomainsRepository blackListDomainsRepository = blackListDomainsRepositoryFactory.Create();

            WorkingDomainsRepositoryFactory workingDomainsRepositoryFactory =
                new WorkingDomainsRepositoryFactory(blackListDomainsRepository.GetAllEntities(), 
                                                    BLACK_LIST_COUNT, 
                                                    MAX_NEW_DOMAIN_LEVEL, 
                                                    NEW_DOMAINS_COUNT);
            WorkingDomainsRepository workingDomainsRepository = workingDomainsRepositoryFactory.Create();

            var parameters = new MatchParameters
                                 {
                                     Algorithm = algorithm, 
                                     BlackListDomainsesRepository = blackListDomainsRepository, 
                                     WorkingDomainsesRepository = workingDomainsRepository
                                 };

            return new DomainsMatch(parameters);
        }

        #endregion
    }
}