namespace DomainNamesFilter.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.Factories;
    using DomainNamesFilter.Core.Repositories;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class WorkingDomainRepositoryTests
    {
        private const int BLACK_DOMAINS_COUNT = 15;

        private const int CHOOSED_BLACK_DOMAINS_COUNT = 5;

        private const byte MAX_NEW_DOMAIN_LEVEL = 3;

        private const int NEW_DOMAINS_COUNT = 5;

        private MockRepository _mockRepository;

        private WorkingDomainsRepositoryFactory _domainsRepositoryFactory;

        private IEnumerable<Domain> _blackListDomains;

        #region Test Fixture Initialize

        /// <summary>
        /// Инициализирует параметры тестового класса.
        /// Вызывается перед запуском всех тестов
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            _mockRepository = new MockRepository(MockBehavior.Strict)
                                  {
                                      DefaultValue = DefaultValue.Mock
                                  };
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
            int id = 0;

            Mock<IFactory<Domain>> mockDomainFactory = _mockRepository.Create<IFactory<Domain>>();
            mockDomainFactory.Setup(m => m.Create()).Returns(
                () => new Domain
                          {
                              Name = string.Format(".BlackDomain{0}", id)
                          }).Callback(() => id++);

            IFactory<IEnumerable<Domain>> blackDomainsFactory = new DomainsFactory(mockDomainFactory.Object, BLACK_DOMAINS_COUNT);
            _blackListDomains = blackDomainsFactory.Create();

            _domainsRepositoryFactory = new WorkingDomainsRepositoryFactory(
                _blackListDomains, 
                CHOOSED_BLACK_DOMAINS_COUNT, 
                MAX_NEW_DOMAIN_LEVEL, 
                NEW_DOMAINS_COUNT);
        }

        /// <summary>
        /// Освобождает параметры тестового метода.
        /// Вызывается после запуска каждого теста
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
            _domainsRepositoryFactory = null;
        }

        #endregion

        #region Test Methods

        [Test]
        public void ShouldCreateDomainsForWorkingDomainsesRepository()
        {
            // Arrange

            // Act
            IEnumerable<Domain> domains = GetDomains();

            // Assert 
            domains.Should().HaveCount(CHOOSED_BLACK_DOMAINS_COUNT + NEW_DOMAINS_COUNT).And.ContainItemsAssignableTo<Domain>();
        }

        [Test]
        public void ShouldInsertBlackListIntoWorkingDomainsesRepository()
        {
            // Arrange

            // Act
            IEnumerable<string> sourceBlackDomainsName = _blackListDomains.Select(o => o.Name);
            IEnumerable<string> resultBlackDomainsName = GetDomains().Take(CHOOSED_BLACK_DOMAINS_COUNT).Select(o => o.Name);

            // Assert 
            resultBlackDomainsName.Should().BeSubsetOf(sourceBlackDomainsName);
        }

        [Test]
        public void ShouldInsertUniqueDomainsIntoWorkingDomainsesRepository()
        {
            // Arrange

            // Act
            IEnumerable<string> sourceBlackDomainsName = _blackListDomains.Select(o => o.Name);
            IEnumerable<string> uniqueDomainsName = GetDomains().Skip(CHOOSED_BLACK_DOMAINS_COUNT).Select(o => o.Name);

            // Assert 
            uniqueDomainsName.Should().NotBeSubsetOf(sourceBlackDomainsName); 
        }

        #endregion

        #region Helper Methods

        private IEnumerable<Domain> GetDomains()
        {
            WorkingDomainsRepository workingDomainsRepository = _domainsRepositoryFactory.Create();

            return workingDomainsRepository.GetAllEntities();
        }

        #endregion
    }
}