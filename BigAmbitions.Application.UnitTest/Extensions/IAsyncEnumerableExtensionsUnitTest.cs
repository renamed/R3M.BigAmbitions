using BigAmbitions.Application.Extensions;
using FluentAssertions;

namespace BigAmbitions.Application.UnitTest.Extensions;

public class IAsyncEnumerableExtensionsUnitTest
{
    static class IAsyncEnumerableExtensionsUnitTestHelper
    {
        public static async IAsyncEnumerable<T> Get<T>(T[] elements = default)
        {
            elements ??= [];
            foreach (var elem in elements)
            {
                await Task.CompletedTask;
                yield return elem;
            }
        }
    }

    [Fact]
    public async Task ToListAsync_ShouldReturnAList()
    {
        // Arrange
        IAsyncEnumerable<int> data = IAsyncEnumerableExtensionsUnitTestHelper.Get<int>();

        // Act
        var list = await data.ToListAsync();

        // Assert
        list.GetType().FullName.Should().Be(typeof(List<int>).FullName);
    }

    [Fact]
    public async Task ToListAsync_ShouldReturnEmptyList_WhenEnumerableHasNoElements()
    {
        // Arrange
        IAsyncEnumerable<string> data = IAsyncEnumerableExtensionsUnitTestHelper.Get<string>();

        // Act
        var list = await data.ToListAsync();

        // Assert
        list.Should().BeEmpty();
    }

    [Fact]
    public async Task ToListAsync_ShouldReturnNonEmptyList_WhenEnumerableHaslements()
    {
        // Arrange
        bool[] elemens = [true, true, false, false, false];
        IAsyncEnumerable<bool> data = IAsyncEnumerableExtensionsUnitTestHelper.Get(elemens);

        // Act
        var list = await data.ToListAsync();

        // Assert
        list.Should().HaveCount(5);
        list.Should().EndWith(elemens);
    }
}

