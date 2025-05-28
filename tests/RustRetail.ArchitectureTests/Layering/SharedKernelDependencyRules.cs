using FluentAssertions;
using NetArchTest.Rules;
using System.Reflection;

namespace RustRetail.ArchitectureTests.Layering
{
    public class SharedKernelDependencyRules
    {
        private static readonly Assembly SharedKernelAssembly = typeof(SharedKernel.Domain.Models.OutboxMessage).Assembly;

        [Fact]
        public void SharedKernel_ShouldNot_DependOn_SharedApplication()
        {
            // Arrange
            var assembly = SharedKernelAssembly;

            // Act
            var result = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOn("RustRetail.SharedApplication")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue("SharedKernel should not depend on SharedApplication layer");
        }

        [Fact]
        public void SharedKernel_ShouldNot_DependOn_SharedInfrastructure()
        {
            // Arrange
            var assembly = SharedKernelAssembly;

            // Act
            var result = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOn("RustRetail.SharedInfrastructure")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue("SharedKernel should not depend on SharedInfrastructure layer");
        }

        [Fact]
        public void SharedKernel_ShouldNot_DependOn_SharedPersistence()
        {
            // Arrange
            var assembly = SharedKernelAssembly;

            // Act
            var result = Types.InAssembly(assembly)
                .ShouldNot()
                .HaveDependencyOn("RustRetail.SharedPersistence")
                .GetResult();

            // Assert
            result.IsSuccessful.Should().BeTrue("SharedKernel should not depend on SharedPersistence layer");
        }
    }
}
