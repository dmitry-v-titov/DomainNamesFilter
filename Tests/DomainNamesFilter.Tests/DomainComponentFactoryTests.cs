namespace DomainNamesFilter.Tests
{
    using DomainNamesFilter.Core.Factories;
    using DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Composite;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class DomainComponentFactoryTests
    {
        private DomainComponentFactory _factory;

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
            _factory = new DomainComponentFactory();
        }

        /// <summary>
        /// Освобождает параметры тестового метода.
        /// Вызывается после запуска каждого теста
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
            _factory = null;
        }

        #endregion

        #region Test Methods

        [Test]
        public void ShouldCreateSubdomainLeaf()
        {
            // Arrange
            const string SUBDOMAIN_NAME = "subdomain";

            _factory.IsLeaf = true;
            _factory.SubdomainName = SUBDOMAIN_NAME;

            // Act
            IDomainComponent domainComponent = _factory.Create();

            // Assert 
            domainComponent.IsLeaf.Should().BeTrue();
            domainComponent.SubdomainName.Should().Be(SUBDOMAIN_NAME);
        }

        [Test]
        public void ShouldCreateSubdomain()
        {
            // Arrange
            const string SUBDOMAIN_NAME = "subdomain";

            _factory.IsLeaf = false;
            _factory.SubdomainName = SUBDOMAIN_NAME;

            // Act
            IDomainComponent domainComponent = _factory.Create();

            // Assert 
            domainComponent.IsLeaf.Should().BeFalse();
            domainComponent.SubdomainName.Should().Be(SUBDOMAIN_NAME);
        }

        #endregion
    }
}