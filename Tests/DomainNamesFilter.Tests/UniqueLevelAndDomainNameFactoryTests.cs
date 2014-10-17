namespace DomainNamesFilter.Tests
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.Extensions;
    using DomainNamesFilter.Core.Factories;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class UniqueLevelAndDomainNameFactoryTests
    {
        private const byte MAX_DOMAIN_LEVEL = 3;

        private UniqueLevelAndDomainNameFactory _domainNameFactory;

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
            _domainNameFactory = new UniqueLevelAndDomainNameFactory(MAX_DOMAIN_LEVEL);
        }

        /// <summary>
        /// Освобождает параметры тестового метода.
        /// Вызывается после запуска каждого теста
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
            _domainNameFactory = null;
        }

        #endregion

        #region Test Methods

        [Test]
        public void ShouldCreateRandomLevelDomain()
        {
            // Arrange
            const int COUNT = 10;

            DomainFactory domainFactory = new DomainFactory(_domainNameFactory);
            List<int> domainsLevel = new List<int>();

            // Act
            for (int i = 0; i < COUNT; i++)
            {
                Domain domain = domainFactory.Create();
                domainsLevel.Add(domain.GetLevel());
            }

            // Assert 
            domainsLevel.Should().Contain(l => l != domainsLevel[0]);
        }

        #endregion
    }
}