namespace DomainNamesFilter.Tests
{
    using System.Collections.Generic;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Composite;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class DomainsTreeTests
    {
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
        public void ShouldWorkDomainsTree()
        {
            // Arrange
            IEnumerable<Domain> domains = new[]
                                              {
                                                  new Domain
                                                      {
                                                          Name = "domain0.domain0"
                                                      }, 
                                                  new Domain
                                                      {
                                                          Name = "domain1.domain0"
                                                      }, 
                                                  new Domain
                                                      {
                                                          Name = "domain1.domain1.domain1"
                                                      }, 
                                                  new Domain
                                                      {
                                                          Name = ".domain2"
                                                      }
                                              };

            var domain1 = new Domain
                              {
                                  Name = "domain1.domain2"
                              };
            var domain2 = new Domain
                              {
                                  Name = "domain6.domain5"
                              };

            // Act
            var domainsTree = new DomainsTree(domains);
            bool isFound1 = domainsTree.Find(domain1);
            bool isFound2 = domainsTree.Find(domain2);

            // Assert 
            isFound1.Should().BeTrue();
            isFound2.Should().BeFalse();
        }

        #endregion
    }
}