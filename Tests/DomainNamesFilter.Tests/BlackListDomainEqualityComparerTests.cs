namespace DomainNamesFilter.Tests
{
    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Comparers;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class BlackListDomainEqualityComparerTests
    {
        /// <summary>
        /// TODO
        /// </summary>
        private BlackListDomainEqualityComparer _comparer;

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
            _comparer = new BlackListDomainEqualityComparer();
        }

        /// <summary>
        /// Освобождает параметры тестового метода.
        /// Вызывается после запуска каждого теста
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
            _comparer = null;
        }

        #endregion

        #region Test Methods

        [Test]
        public void ShouldBeEqualityDomains()
        {
            // Arrange
            Domain domain = new Domain
                                {
                                    Name = "domain12.domain11"
                                };

            Domain subdomain = new Domain
                                   {
                                       Name = "domain13.domain12.domain11"
                                   };

            // Act
            bool isEquals = _comparer.Equals(domain, subdomain);

            // Assert 
            isEquals.Should().BeTrue();
        }

        [Test]
        public void ShouldNotBeEqualityDomains()
        {
            // Arrange
            Domain domain = new Domain
                                {
                                    Name = "domain13.domain12.domain11"
                                };

            Domain subdomain = new Domain
                                   {
                                       Name = "domain1x.domain12.domain11"
                                   };

            // Act
            bool isEquals = _comparer.Equals(domain, subdomain);

            // Assert 
            isEquals.Should().BeFalse();
        }

        #endregion
    }
}