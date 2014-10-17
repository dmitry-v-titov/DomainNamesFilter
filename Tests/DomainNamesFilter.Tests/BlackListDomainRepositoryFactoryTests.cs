namespace DomainNamesFilter.Tests
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.Factories;
    using DomainNamesFilter.Core.Repositories;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class BlackListDomainRepositoryFactoryTests
    {
        private const byte MAX_DOMAIN_LEVEL = 3;

        private const int DOMAINS_COUNT = 10;

        private BlackListDomainsRepositoryFactory _blackListDomainsRepositoryFactory;

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
            _blackListDomainsRepositoryFactory = new BlackListDomainsRepositoryFactory(MAX_DOMAIN_LEVEL, DOMAINS_COUNT);
        }

        /// <summary>
        /// Освобождает параметры тестового метода.
        /// Вызывается после запуска каждого теста
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
            _blackListDomainsRepositoryFactory = null;
        }

        #endregion

        #region Test Methods

        [Test]
        public void ShouldCreateDomainsForBlackListDomainRepository()
        {
            // Arrange

            // Act
            IEnumerable<Domain> domains = GetDomains();

            // Assert 
            domains.Should().HaveCount(DOMAINS_COUNT).And.ContainItemsAssignableTo<Domain>();
        }

        [Test]
        public void ShouldOnlyHaveUniqueItemsForBlackListDomainRepository()
        {
            // Arrange

            // Act
            IEnumerable<Domain> domains = GetDomains();

            // Assert 
            domains.Should().OnlyHaveUniqueItems();
        }

        #endregion

        #region Helper Methods

        private IEnumerable<Domain> GetDomains()
        {
            BlackListDomainsRepository blackListDomainsRepository = _blackListDomainsRepositoryFactory.Create();

            return blackListDomainsRepository.GetAllEntities();
        }

        #endregion
    }
}