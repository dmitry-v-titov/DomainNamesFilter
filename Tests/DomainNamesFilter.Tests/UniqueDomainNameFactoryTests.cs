namespace DomainNamesFilter.Tests
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using DomainNamesFilter.Core.Factories;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class UniqueDomainNameFactoryTests
    {
        private UniqueDomainNameFactory _uniqueDomainNameFactory;

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
            _uniqueDomainNameFactory = new UniqueDomainNameFactory();
        }

        /// <summary>
        /// Освобождает параметры тестового метода.
        /// Вызывается после запуска каждого теста
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
            _uniqueDomainNameFactory = null;
        }

        #endregion

        #region Test Methods

        [Test]
        public void ShouldCreateTopLevelDomain()
        {
            // Arrange
            const byte LEVEL = 1;
            const string PATERN = @"^\.[a-z]+\d*$";

            // Act
            string domainName = GetDomainName(LEVEL);

            // Assert 
            domainName.Should().MatchRegex(PATERN);
        }

        [Test]
        public void ShouldCreateLowerLevelDomain()
        {
            // Arrange
            const byte LEVEL = 3;
            const string PATERN = @"^([a-z]+\d*\.)+([a-z]+\d*)$";

            // Act
            string domainName = GetDomainName(LEVEL);

            // Assert 
            domainName.Should().MatchRegex(PATERN);
        }

        [Test]
        public void ShouldCreateUniqueDomainName()
        {
            // Arrange
            const int COUNT = 10;
            const byte LEVEL = 3;

            List<string> domainNameList = new List<string>();

            // Act
            for (int i = 0; i < COUNT; i++)
            {
                string domainName = GetDomainName(LEVEL);

                domainNameList.Add(domainName);
            }

            // Assert 
            domainNameList.Should().OnlyHaveUniqueItems();
        }

        #endregion

        #region Helper Methods

        private string GetDomainName(byte domainLevel)
        {
            _uniqueDomainNameFactory.DomainLevel = domainLevel;
            string domainName = _uniqueDomainNameFactory.Create();

            Trace.WriteLine(domainName);

            return domainName;
        }

        #endregion
    }
}