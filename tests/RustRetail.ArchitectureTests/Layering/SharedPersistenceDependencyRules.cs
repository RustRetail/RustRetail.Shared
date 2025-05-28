using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;

namespace RustRetail.ArchitectureTests.Layering
{
    public class SharedPersistenceDependencyRules
    {
        private static readonly Assembly SharedPersistenceAssembly = typeof(SharedPersistence.Database.SpecificationEvaluator).Assembly;

        [Fact]
        public void SharedPersistence_Should_DependOn_SharedKernel()
        {
            // Arrange
            var assembly = SharedPersistenceAssembly;
            var sharedKernelAssemblyName = typeof(SharedKernel.Domain.Models.OutboxMessage).Assembly.GetName().Name;

            // Act
            var referencedAssemblies = assembly.GetReferencedAssemblies();
            var dependsOnSharedKernel = referencedAssemblies.Any(a => a.Name == sharedKernelAssemblyName);

            // Assert
            dependsOnSharedKernel.Should().BeTrue("SharedPersistence should depend on SharedKernel layer");
        }

        [Fact]
        public void SharedPersistence_ShouldNot_DependOn_SharedInfrastructure()
        {
            // Arrange
            var assembly = SharedPersistenceAssembly;

            // Act
            var result = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOn("RustRetail.SharedInfrastructure")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue("SharedPersistence should not depend on SharedInfrastructure layer");
        }

        [Fact]
        public void SharedPersistence_ShouldNot_DependOn_SharedApplication()
        {
            // Arrange
            var assembly = SharedPersistenceAssembly;

            // Act
            var result = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOn("RustRetail.SharedApplication")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue("SharedPersistence should not depend on SharedApplication layer");
        }
    }
}
