namespace DomainNamesFilter.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using DomainNamesFilter.Core.DTO;
    using DomainNamesFilter.Core.MatchAlgorithms.HelperClasses.Composite;
    using DomainNamesFilter.Tests.Mocks;

    using FluentAssertions;

    using NUnit.Framework;

    [TestFixture]
    public class DomainComponentTests
    {
        /// <summary>
        /// Заглушка фабрики доменных компонент
        /// </summary>
        private DomainComponentFactoryMock _domainComponentFactory;

        #region Test Fixture Initialize

        /// <summary>
        /// Инициализирует параметры тестового класса.
        /// Вызывается перед запуском всех тестов
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            _domainComponentFactory = new DomainComponentFactoryMock();
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
            _domainComponentFactory = new DomainComponentFactoryMock();
        }

        /// <summary>
        /// Освобождает параметры тестового метода.
        /// Вызывается после запуска каждого теста
        /// </summary>
        [TearDown]
        public void TestCleanup()
        {
            _domainComponentFactory = null;
        }

        #endregion

        #region Test Methods

        [Test]
        public void ShouldNotBeNodesInDomainLeafComponent()
        {
            // Arrange

            // Act
            var domainComponentMock = new DomainComponentMock(_domainComponentFactory, true);

            // Assert 
            domainComponentMock.GetNodes().Should().BeNull();
        }

        [Test]
        public void ShouldBeNodesInDomainComponent()
        {
            // Arrange

            // Act
            var domainComponentMock = new DomainComponentMock(_domainComponentFactory);

            // Assert 
            domainComponentMock.GetNodes().Should().BeEmpty();
        }

        [Test]
        public void ShouldNotAddDomainIfThisLeaf()
        {
            // Arrange
            var domainComponent = new DomainComponentMock(_domainComponentFactory, true);

            var domain = new Domain
                             {
                                 Name = "domain.domain"
                             };

            // Act
            domainComponent.Add(domain);

            // Assert 
            domainComponent.GetNodes().Should().BeNull();
        }

        [Test]
        public void ShouldAddTopLevelDomain()
        {
            // Arrange
            const string DOMAIN_NAME = ".domain";

            var domainComponent = new DomainComponentMock(_domainComponentFactory);

            var domain = new Domain
                             {
                                 Name = DOMAIN_NAME
                             };

            // Act
            domainComponent.Add(domain);
            List<IDomainComponent> domainComponents = domainComponent.GetNodes().ToList();

            // Assert
            domainComponents.Should().HaveCount(1);
            domainComponents.ElementAt(0).SubdomainName.Should().Be(DOMAIN_NAME.Remove(0, 1));
            domainComponents.ElementAt(0).IsLeaf.Should().BeTrue();
        }

        [Test]
        public void ShouldAddLowLevelDomain()
        {
            // Arrange
            const string DOMAIN_NAME = "domain4.domain3.domain2.domain1";

            string[] subdomainNames = DOMAIN_NAME.Split(new[]
                                                            {
                                                                '.'
                                                            }).Reverse().ToArray();

            var domainComponent = new DomainComponentMock(_domainComponentFactory);

            var domain = new Domain
                             {
                                 Name = DOMAIN_NAME
                             };

            // Act
            domainComponent.Add(domain);
            List<IDomainComponent> domainComponents = domainComponent.GetNodes().ToList();

            // Assert
            domainComponents.Should().HaveCount(1);
            domainComponents.ElementAt(0).SubdomainName.Should().Be(subdomainNames[0]);
            domainComponents.ElementAt(0).IsLeaf.Should().BeFalse();

            for (int i = 1; i < subdomainNames.Length; i++)
            {
                var component = (DomainComponentMock)domainComponents.ElementAt(0);
                domainComponents = component.GetNodes().ToList();

                domainComponents.Should().HaveCount(1);
                domainComponents.ElementAt(0).SubdomainName.Should().Be(subdomainNames[i]);

                if (i == subdomainNames.Length - 1)
                {
                    domainComponents.ElementAt(0).IsLeaf.Should().BeTrue();
                }
                else
                {
                    domainComponents.ElementAt(0).IsLeaf.Should().BeFalse();
                }
            }
        }

        [Test]
        public void ShouldNotAddLowLevelDomainIfTopLevelDomainWasAdded()
        {
            // Arrange
            var topDomain = new Domain
                                {
                                    Name = "domain2.domain1"
                                };
            var lowDomain = new Domain
                                {
                                    Name = "domain3.domain2.domain1"
                                };

            var domainComponent = new DomainComponentMock(_domainComponentFactory);

            // Act
            domainComponent.Add(topDomain);
            domainComponent.Add(lowDomain);

            // Assert 
            List<IDomainComponent> domainComponents = domainComponent.GetNodes().ToList();

            domainComponents.Should().HaveCount(1);
            domainComponents.ElementAt(0).SubdomainName.Should().Be("domain1");
            domainComponents.ElementAt(0).IsLeaf.Should().BeFalse();

            var component = (DomainComponentMock)domainComponents.ElementAt(0);
            domainComponents = component.GetNodes().ToList();

            domainComponents.Should().HaveCount(1);
            domainComponents.ElementAt(0).SubdomainName.Should().Be("domain2");
            domainComponents.ElementAt(0).IsLeaf.Should().BeTrue();

            component = (DomainComponentMock)domainComponents.ElementAt(0);
            component.GetNodes().Should().BeNull();
        }

        [Test]
        public void ShouldCutLowLevelDomainIfTopLevelDomainAdd()
        {
            // Arrange
            var topDomain = new Domain
                                {
                                    Name = "domain2.domain1"
                                };

            var lowDomain = new Domain
                                {
                                    Name = "domain4.domain3.domain2.domain1"
                                };

            var domainComponent = new DomainComponentMock(_domainComponentFactory);

            // Act
            domainComponent.Add(lowDomain);
            domainComponent.Add(topDomain);

            // Assert 
            List<IDomainComponent> domainComponents = domainComponent.GetNodes().ToList();

            domainComponents.Should().HaveCount(1);
            domainComponents.ElementAt(0).SubdomainName.Should().Be("domain1");
            domainComponents.ElementAt(0).IsLeaf.Should().BeFalse();

            var component = (DomainComponentMock)domainComponents.ElementAt(0);
            domainComponents = component.GetNodes().ToList();

            domainComponents.Should().HaveCount(1);
            domainComponents.ElementAt(0).SubdomainName.Should().Be("domain2");
            domainComponents.ElementAt(0).IsLeaf.Should().BeTrue();

            component = (DomainComponentMock)domainComponents.ElementAt(0);
            component.GetNodes().Should().BeNull();
        }

        [Test]
        public void ShouldAddManyDomains()
        {
            var domains = new List<Domain>
                              {
                                  new Domain
                                      {
                                          Name = "domain12.domain11"
                                      }, 
                                  new Domain
                                      {
                                          Name = "domain1x.domain11"
                                      }, 
                                  new Domain
                                      {
                                          Name = "domain12"
                                      }
                              };

            var domainComponent = new DomainComponentMock(_domainComponentFactory);

            // Act
            domains.ForEach(domainComponent.Add);

            // Assert 
            List<IDomainComponent> domainComponents = domainComponent.GetNodes().ToList();

            domainComponents.Should().HaveCount(2);

            var component = (DomainComponentMock)domainComponents.ElementAt(0);
            component.GetNodes().Should().HaveCount(2);

            component = (DomainComponentMock)domainComponents.ElementAt(1);
            component.GetNodes().Should().BeNull();
        }

        [Test]
        public void ShouldFindEqualDomains()
        {
            var domains = new List<Domain>
                              {
                                  new Domain
                                      {
                                          Name = "domain13.domain12.domain11"
                                      }, 
                                  new Domain
                                      {
                                          Name = "domain1x.domain11"
                                      }, 
                                  new Domain
                                      {
                                          Name = "domain12"
                                      }
                              };

            var domainComponent = new DomainComponentMock(_domainComponentFactory);
            domains.ForEach(domainComponent.Add);

            // Act
            bool isFind = domainComponent.Find(domains.ElementAt(0));

            // Assert 
            isFind.Should().BeTrue();
        }

        [Test]
        public void ShouldNotFindDifferentDomains()
        {
            var domains = new List<Domain>
                              {
                                  new Domain
                                      {
                                          Name = "domain13.domain12.domain11"
                                      }, 
                                  new Domain
                                      {
                                          Name = "domain1x.domain11"
                                      }, 
                                  new Domain
                                      {
                                          Name = "domain12"
                                      }
                              };
            var domain = new Domain
                             {
                                 Name = "domain22.domain21"
                             };

            var domainComponent = new DomainComponentMock(_domainComponentFactory);
            domains.ForEach(domainComponent.Add);

            // Act
            bool isFind = domainComponent.Find(domain);

            // Assert 
            isFind.Should().BeFalse();
        }

        [Test]
        public void ShouldFindSubdomains()
        {
            var domains = new List<Domain>
                              {
                                  new Domain
                                      {
                                          Name = "domain13.domain12.domain11"
                                      }, 
                                  new Domain
                                      {
                                          Name = "domain1x.domain11"
                                      }, 
                                  new Domain
                                      {
                                          Name = "domain12"
                                      }
                              };
            var domain = new Domain
                             {
                                 Name = "domainxx.domain1x.domain11"
                             };

            var domainComponent = new DomainComponentMock(_domainComponentFactory);
            domains.ForEach(domainComponent.Add);

            // Act
            bool isFind = domainComponent.Find(domain);

            // Assert 
            isFind.Should().BeTrue();
        }

        [Test]
        public void ShouldNotFindDifferentSubdomains()
        {
            var domains = new List<Domain>
                              {
                                  new Domain
                                      {
                                          Name = "domain13.domain12.domain11"
                                      }, 
                                  new Domain
                                      {
                                          Name = "domain1x.domain11"
                                      }, 
                                  new Domain
                                      {
                                          Name = "domain12"
                                      }
                              };
            var domain = new Domain
                             {
                                 Name = "domain1x.domain12.domain11"
                             };

            var domainComponent = new DomainComponentMock(_domainComponentFactory);
            domains.ForEach(domainComponent.Add);

            // Act
            bool isFind = domainComponent.Find(domain);

            // Assert 
            isFind.Should().BeFalse();
        }

        #endregion
    }
}