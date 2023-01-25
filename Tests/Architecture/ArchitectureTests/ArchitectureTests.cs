using FluentAssertions;
using NetArchTest.Rules;

namespace ArchitectureTests
{
    public class ArchitectureTests
    {
        private const string DomainNamespace = "Domain";
        private const string ApplicationNamspace = "Application";
        private const string InfrastructureNamespace = "Infrastructure";
        private const string PresentationNamespace = "Presentation";
        private const string WebNamespace = "Web";

        [Fact]
        public void Domain_Should_Not_HaveDependencyOnOtherProjects()
        {
            //Arrange

            var assembly = typeof(Domain.AssemblyReference).Assembly;
            var otherProjects = new[]
            {
                ApplicationNamspace,
                InfrastructureNamespace,
                PresentationNamespace,
                WebNamespace
            };

            //Act

            var testResults = Types
                            .InAssembly(assembly)
                            .ShouldNot()
                            .HaveDependencyOnAll(otherProjects)
                            .GetResult();

            //Assert

            testResults.IsSuccessful.Should().BeTrue();
        }
        [Fact]
        public void Application_Should_Not_HaveDependencyOnOtherProjects()
        {
            //Arrange

            var assembly = typeof(Application.AssemblyReference).Assembly;
            var otherProjects = new[]
            {   
                InfrastructureNamespace,
                PresentationNamespace,
                WebNamespace
            };

            //Act

            var testResults = Types
                            .InAssembly(assembly)
                            .ShouldNot()
                            .HaveDependencyOnAll(otherProjects)
                            .GetResult();

            //Assert

            testResults.IsSuccessful.Should().BeTrue();
        }
        // Currently some handlers does not have dependencies such as SMSService , so
        // Do not want to enforce this rule but it is better to know that this can be 
        // done also.
        //[Fact]
        //public void Handlers_Should_HaveDependencyOnDomain()
        //{
        //    //Arrange

        //    var assembly = typeof(Application.AssemblyReference).Assembly;

        //    //Act

        //    var testResults = Types
        //                    .InAssembly(assembly)
        //                    .That()
        //                    .HaveNameEndingWith("Handler")
        //                    .Should()
        //                    .HaveDependencyOn(DomainNamespace)
        //                    .GetResult();

        //    //Assert

        //    testResults.IsSuccessful.Should().BeTrue();
        //}
        [Fact]
        public void Infrastructure_Should_Not_HaveDependencyOnOtherProjects()
        {
            //Arrange

            var assembly = typeof(Infrastructure.AssemblyReference).Assembly;
            var otherProjects = new[]
            {   
                PresentationNamespace,
                WebNamespace
            };

            //Act

            var testResults = Types
                            .InAssembly(assembly)
                            .ShouldNot()
                            .HaveDependencyOnAll(otherProjects)
                            .GetResult();

            //Assert

            testResults.IsSuccessful.Should().BeTrue();
        }
        [Fact]
        public void Presentation_Should_Not_HaveDependencyOnOtherProjects()
        {
            //Arrange

            var assembly = typeof(Presentation.AssemblyReference).Assembly;
            var otherProjects = new[]
            {
                InfrastructureNamespace,
                WebNamespace
            };

            //Act

            var testResults = Types
                            .InAssembly(assembly)
                            .ShouldNot()
                            .HaveDependencyOnAll(otherProjects)
                            .GetResult();

            //Assert

            testResults.IsSuccessful.Should().BeTrue();
        }

        // Some of the controller does not have dependencies in the MediatR
        // So does not want to enforce but good to know this.
        //[Fact]
        //public void Controllers_Should_HaveDependencyOnMediatR()
        //{
        //    //Arrange

        //    var assembly = typeof(Presentation.AssemblyReference).Assembly;
           
        //    //Act

        //    var testResults = Types
        //                    .InAssembly(assembly)
        //                    .That()
        //                    .HaveNameEndingWith("Controller")
        //                    .Should()
        //                    .HaveDependencyOn("MediatR")
        //                    .GetResult();

        //    //Assert

        //    testResults.IsSuccessful.Should().BeTrue();
        //}
    }
}