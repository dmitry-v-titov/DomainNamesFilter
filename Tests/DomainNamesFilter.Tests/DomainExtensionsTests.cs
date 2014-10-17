namespace DomainNamesFilter.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.Extensions;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class DomainExtensionsTests
    {
        #region Fields

        private IEnumerable<Domain> _domains;

        private IEnumerable<int> _domainsLevel;

        #endregion

        #region Test Fixture Initialize

        /// <summary>
        /// Инициализирует параметры тестового класса.
        /// Вызывается перед запуском всех тестов
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            _domains = new[]
                           {
                               new Domain
                                   {
                                       Name = ".domain11"
                                   }, 
                               new Domain
                                   {
                                       Name = "domain22.domain21"
                                   }, 
                               new Domain
                                   {
                                       Name = "domain33.domain32.domain31"
                                   }
                           };

            _domainsLevel = new[]
                                {
                                    1, 
                                    2, 
                                    3
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
        }

        /// <summary>
        /// Освобождает параметры тестового метода.
        /// Вызывается после запуска каждого теста
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
        }

        #endregion

        #region Test Methods

        [Test]
        public void ShouldGetDomainLevel()
        {
            // Arrange

            // Act
            List<int> levels = _domains.Select(domain => domain.GetLevel()).ToList();

            // Assert 
            levels.Should().BeEquivalentTo(_domainsLevel);
        }

        [Test]
        public void ShouldExtractTopLevelDomainNameForTopLevelDomainWithDot()
        {
            // Arrange
            const string DOMAIN_NAME = ".domain1";

            var domain = new Domain
                             {
                                 Name = DOMAIN_NAME
                             };

            // Act
            Domain resultDomain;
            string domainName = domain.ExtractTopLevelDomain(out resultDomain);

            // Assert 
            domainName.Should().Be(DOMAIN_NAME.Remove(0, 1));
            resultDomain.Name.Should().Be(DOMAIN_NAME);
        }

        [Test]
        public void ShouldExtractTopLevelDomainNameForTopLevelDomainWithoutDot()
        {
            // Arrange
            const string DOMAIN_NAME = "domain1";

            var domain = new Domain
                             {
                                 Name = DOMAIN_NAME
                             };

            // Act
            Domain resultDomain;
            string domainName = domain.ExtractTopLevelDomain(out resultDomain);

            // Assert 
            domainName.Should().Be(DOMAIN_NAME);
            resultDomain.Name.Should().Be(DOMAIN_NAME);
        }

        [Test]
        public void ShouldExtractTopLevelDomainNameForLowLevelDomain()
        {
            // Arrange
            const string DOMAIN_NAME = "domain2.domain1";

            var domain = new Domain
                             {
                                 Name = DOMAIN_NAME
                             };

            // Act
            Domain resultDomain;
            string domainName = domain.ExtractTopLevelDomain(out resultDomain);

            // Assert 
            domainName.Should().Be("domain1");
            resultDomain.Name.Should().Be("domain2");
        }

        [Test]
        public void ShouldIsTopLevelDomainWithDot()
        {
            // Arrange

            // Act
            bool isTopLevelDomain = _domains.ElementAt(0).IsTopLevelDomain();

            // Assert 
            isTopLevelDomain.Should().BeTrue();
        }

        [Test]
        public void ShouldIsTopLevelDomainWithoutDot()
        {
            // Arrange
            var domain = new Domain
                             {
                                 Name = "domain"
                             };

            // Act
            bool isTopLevelDomain = domain.IsTopLevelDomain();

            // Assert 
            isTopLevelDomain.Should().BeTrue();
        }

        [Test]
        public void ShouldNotIsTopLevelDomain()
        {
            // Arrange

            // Act
            bool isTopLevelDomain = _domains.ElementAt(1).IsTopLevelDomain();

            // Assert 
            isTopLevelDomain.Should().BeFalse();
        }

        [Test]
        public void ShouldIsSubDomain()
        {
            // Arrange
            var domain = new Domain
                             {
                                 Name = "domain12.domain11"
                             };

            // Act
            bool isSubDomain = domain.IsSubDomain(_domains.ElementAt(0));

            // Assert 
            isSubDomain.Should().BeTrue();
        }

        [Test]
        public void ShouldNotIsSubDomain()
        {
            // Arrange
            var domain = new Domain
                             {
                                 Name = "xxxdomain33.domain32.domain31"
                             };

            // Act
            bool isSubDomain = domain.IsSubDomain(_domains.ElementAt(2));

            // Assert 
            isSubDomain.Should().BeFalse();
        }

        [Test]
        public void ShouldCompareDomainName()
        {
            // Arrange
            var compareDomain = new Domain
                                    {
                                        Name = "domain22.domain21"
                                    };
            int[] compareTemplate =
                {
                    1, 
                    0, 
                    -1
                };

            // Act
            List<int> compareResult = _domains.Select(compareDomain.CompareDomainName).ToList();

            // Assert 
            compareResult.Should().BeEquivalentTo(compareTemplate);
        }

        #endregion
    }
}