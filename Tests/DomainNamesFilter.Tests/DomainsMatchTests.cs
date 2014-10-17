namespace DomainNamesFilter.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using DomainNamesFilter.Core;
    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.MatchAlgorithms;
    using DomainNamesFilter.Core.Repositories;

    using FluentAssertions;

    using Moq;

    using NUnit.Framework;

    [TestFixture]
    public class DomainsMatchTests
    {
        private DomainsMatch _domainsMatch;

        private IEnumerable<Domain> _resultDomains;

        private MockRepository _mockRepository;

        #region Test Fixture Initialize

        /// <summary>
        /// Инициализирует параметры тестового класса.
        /// Вызывается перед запуском всех тестов
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            _resultDomains = new List<Domain>
                                 {
                                     new Domain
                                         {
                                             Name = ".testDomain0"
                                         }, 
                                     new Domain
                                         {
                                             Name = ".testDomain1"
                                         }, 
                                     new Domain
                                         {
                                             Name = ".testDomain2"
                                         }
                                 };

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
            Mock<IDomainsMatchAlgorithm> mockMatchAlgorithm = _mockRepository.Create<IDomainsMatchAlgorithm>();
            mockMatchAlgorithm.Setup(m => m.Match(It.IsAny<IEnumerable<Domain>>(), It.IsAny<IEnumerable<Domain>>())).Returns(() => _resultDomains);

            Mock<IRepository<Domain>> mockDomainsRepository = _mockRepository.Create<IRepository<Domain>>();
            mockDomainsRepository.Setup(m => m.GetAllEntities()).Returns(Enumerable.Empty<Domain>);

            MatchParameters parameters = new MatchParameters
                                             {
                                                 Algorithm = mockMatchAlgorithm.Object,
                                                 WorkingDomainsesRepository = mockDomainsRepository.Object,
                                                 BlackListDomainsesRepository = mockDomainsRepository.Object
                                             };

            _domainsMatch = new DomainsMatch(parameters);
        }

        /// <summary>
        /// Освобождает параметры тестового метода.
        /// Вызывается после запуска каждого теста
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
            _domainsMatch = null;
        }

        #endregion

        #region Test Methods

        [Test]
        public void TestMethodName()
        {
            // Arrange

            // Act
            IEnumerable<Domain> domains = _domainsMatch.Execute();

            // Assert 
            domains.Select(o => o.Name).Should().BeEquivalentTo(_resultDomains.Select(o => o.Name));
        }

        #endregion
    }
}