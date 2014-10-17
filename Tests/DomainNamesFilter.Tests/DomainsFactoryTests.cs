namespace DomainNamesFilter.Tests
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.Factories;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class DomainsFactoryTests
    {
        private const int DOMAINS_COUNT = 10;

        private const string DOMAIN_NAME = "domain";

        private MockRepository _mockRepository;

        private IFactory<IEnumerable<Domain>> _domainsFactory;

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
            Mock<IFactory<Domain>> mockDomainFactory = _mockRepository.Create<IFactory<Domain>>();
            mockDomainFactory.Setup(m => m.Create()).Returns(
                () => new Domain
                          {
                              Name = DOMAIN_NAME
                          });

            _domainsFactory = new DomainsFactory(mockDomainFactory.Object, DOMAINS_COUNT);
        }

        /// <summary>
        /// Освобождает параметры тестового метода.
        /// Вызывается после запуска каждого теста
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
            _domainsFactory = null;
        }

        #endregion

        #region Test Methods

        [Test]
        public void ShouldCreateDomains()
        {
            // Arrange

            // Act
            IEnumerable<Domain> domains = _domainsFactory.Create();

            // Assert 
            domains.Should().NotBeEmpty().And.ContainItemsAssignableTo<Domain>();
        }

        #endregion
    }
}