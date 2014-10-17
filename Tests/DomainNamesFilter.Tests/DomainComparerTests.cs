namespace DomainNamesFilter.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Comparers;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class DomainComparerTests
    {
        private readonly IEnumerable _comparers = new[]
                                                      {
                                                          new TestCaseData(new SortingDomainComparer()), 
                                                          new TestCaseData(new BlackListDomainComparer())
                                                      };

        private IEnumerable<Domain> _domains;

        private IEnumerable<int> _compareTemplate;

        private Domain _compareDomain;

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
                                       Name = "domain0.domain0"
                                   }, 
                               new Domain
                                   {
                                       Name = ".domain1"
                                   }, 
                               new Domain
                                   {
                                       Name = "domain2.domain2.domain2"
                                   }
                           };

            _compareTemplate = new[]
                                   {
                                       1, 
                                       0, 
                                       -1
                                   };

            _compareDomain = new Domain
                                 {
                                     Name = ".domain1"
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
        [TestCaseSource("_comparers")]
        public void ShouldCompareDomain(IComparer<Domain> domainComparer)
        {
            // Arrange

            // Act
            var results = _domains.Select(domain => domainComparer.Compare(domain, _compareDomain)).ToList();

            // Assert 
            results.Should().BeEquivalentTo(_compareTemplate);
        }

        #endregion
    }
}