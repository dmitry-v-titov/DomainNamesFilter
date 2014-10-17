namespace DomainNamesFilter.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.MatchAlgorithms;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class DomainsMatchAlgorithmTests
    {
        private readonly IEnumerable _algorithms = new[]
                                                       {
                                                           new TestCaseData(new StringBasedComparisonDomainsMatchAlgorithm()), 
                                                           new TestCaseData(new ParallelStringBasedComparisonDomainsMatchAlgorithm()), 
                                                           new TestCaseData(new BinarySearchDomainsMatchAlgorithm()), 
                                                           new TestCaseData(new ParallelBinarySearchDomainsMatchAlgorithm()), 
                                                           new TestCaseData(new DomainsTreeDomainsMatchAlgorithm()),
                                                           new TestCaseData(new ParallelDomainsTreeDomainsMatchAlgorithm()) 
                                                       };

        private IEnumerable<Domain> _sourceDomains;

        private IEnumerable<Domain> _blackListDomains;

        private IEnumerable<Domain> _resultDomains;

        #region Test Fixture Initialize

        /// <summary>
        /// Инициализирует параметры тестового класса.
        /// Вызывается перед запуском всех тестов
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            _blackListDomains = new List<Domain>
                                    {
                                        new Domain
                                            {
                                                Name = "testDomain6.testDomain2.testDomain1"
                                            },
                                        new Domain
                                            {
                                                Name = "testDomain5.testDomain5.testDomain5"
                                            },
                                        new Domain
                                            {
                                                Name = "testDomain7.testDomain6.testDomain6"
                                            },
                                        new Domain
                                            {
                                                Name = ".testDomain0"
                                            },
                                        new Domain
                                            {
                                                Name = "testDomain1.testDomain1"
                                            },
                                        new Domain
                                            {
                                                Name = ".testDomain2"
                                            },
                                        new Domain
                                            {
                                                Name = "testDomain3.testDomain3.testDomain3"
                                            },
                                        new Domain
                                            {
                                                Name = ".testDomain4"
                                            },
                                        new Domain
                                            {
                                                Name = "testDomain2.testDomain1"
                                            },
                                        new Domain
                                            {
                                                Name = "testDomain4.testDomain3.testDomain3"
                                            },
                                        new Domain
                                            {
                                                Name = "testDomain6.testDomain6.testDomain6"
                                            },
                                        new Domain
                                            {
                                                Name = "testDomain3.testDomain2.testDomain1"
                                            }
                                    };

            var sourceDomains = new List<Domain>
                                    {
                                        new Domain
                                            {
                                                Name = ".testDomain4"
                                            }, 
                                        new Domain
                                            {
                                                Name = "testDomain0.testDomain0"
                                            }, 
                                        new Domain
                                            {
                                                Name = "testDomain3.testDomain3.testDomain3"
                                            }, 
                                        new Domain
                                            {
                                                Name = "testDomain1.testDomain1.testDomain1"
                                            }, 
                                        new Domain
                                            {
                                                Name = "testDomain2.testDomain2.testDomain2"
                                            },
                                            new Domain
                                            {
                                                Name = "testDomain3.testDomain2.testDomain1"
                                            },
                                            new Domain
                                            {
                                                Name = "testDomain2.testDomain1"
                                            }
                                    };

            _resultDomains = new List<Domain>
                                 {
                                     new Domain
                                         {
                                             Name = ".testDomain0x"
                                         }, 
                                     new Domain
                                         {
                                             Name = "testDomain1x.testDomain1"
                                         }, 
                                     new Domain
                                         {
                                             Name = ".testDomain2x"
                                         }, 
                                     new Domain
                                         {
                                             Name = "testDomain3.testDomain3x.testDomain3"
                                         }, 
                                     new Domain
                                         {
                                             Name = ".testDomain4x"
                                         }
                                 };

            _sourceDomains = new List<Domain>(sourceDomains.Concat(_resultDomains));
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

        /// <param name="algorithm">
        /// Алгоритм фильтрации доменов
        /// </param>
        [Test]
        [TestCaseSource("_algorithms")]
        public void TestMethodName(IDomainsMatchAlgorithm algorithm)
        {
            // Arrange

            // Act
            IEnumerable<Domain> domains = algorithm.Match(_sourceDomains, _blackListDomains);

            // Assert 
            domains.Select(o => o.Name).Should().BeEquivalentTo(_resultDomains.Select(o => o.Name));
        }

        #endregion
    }
}