using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;

namespace RustRetail.ArchitectureTests.Layering
{
    public class SharedApplicationDependencyRules
    {
        private static readonly Assembly SharedApplicationAssembly = typeof(SharedApplication.Behaviors.Event.MediatRDomainEventDispatcher).Assembly;

        [Fact]
        public void SharedApplication_Should_DependOn_SharedKernel()
        {
            // Arrange
            var assembly = SharedApplicationAssembly;
            var sharedKernelAssemblyName = typeof(SharedKernel.Domain.Models.OutboxMessage).Assembly.GetName().Name;

            // Act
            var referencedAssemblies = assembly.GetReferencedAssemblies();
            var dependsOnSharedKernel = referencedAssemblies.Any(a => a.Name == sharedKernelAssemblyName);

            // Assert
            dependsOnSharedKernel.Should().BeTrue("SharedApplication should depend on SharedKernel layer");
        }

        [Fact]
        public void SharedApplication_ShouldNot_DependOn_SharedInfrastructure()
        {
            // Arrange
            var assembly = SharedApplicationAssembly;

            // Act
            var result = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOn("RustRetail.SharedInfrastructure")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue("SharedApplication should not depend on SharedInfrastructure layer");
        }

        [Fact]
        public void SharedApplication_ShouldNot_DependOn_SharedPersistence()
        {
            // Arrange
            var assembly = SharedApplicationAssembly;

            // Act
            var result = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOn("RustRetail.SharedPersistence")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue("SharedApplication should not depend on SharedPersistence layer");
        }
    }
}
