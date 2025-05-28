using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;

namespace RustRetail.ArchitectureTests.Layering
{
    public class SharedInfrastructureDependencyRules
    {
        private static readonly Assembly SharedInfrastructureAssembly = typeof(SharedInfrastructure.MinimalApi.EndpointExtensions).Assembly;

        [Fact(Skip = "Achieve dependency through SharedApplication")]
        public void SharedInfrastructure_Should_DependOn_SharedKernel()
        {
            // Arrange
            var assembly = SharedInfrastructureAssembly;
            var sharedKernelAssemblyName = typeof(SharedKernel.Domain.Models.OutboxMessage).Assembly.GetName().Name;

            // Act
            var referencedAssemblies = assembly.GetReferencedAssemblies();
            var dependsOnSharedKernel = referencedAssemblies.Any(a => a.Name == sharedKernelAssemblyName);

            // Assert
            dependsOnSharedKernel.Should().BeTrue("SharedInfrastructure should depend on SharedKernel layer");
        }

        [Fact(Skip = "Can depend on SharedApplication")]
        public void SharedInfrastructure_ShouldNot_DependOn_SharedApplication()
        {
            // Arrange
            var assembly = SharedInfrastructureAssembly;

            // Act
            var result = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOn("RustRetail.SharedApplication")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue("SharedInfrastructure should not depend on SharedApplication layer");
        }

        [Fact]
        public void SharedInfrastructure_ShouldNot_DependOn_SharedPersistence()
        {
            // Arrange
            var assembly = SharedInfrastructureAssembly;

            // Act
            var result = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOn("RustRetail.SharedPersistence")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue("SharedInfrastructure should not depend on SharedPersistence layer");
        }
    }
}
