using System.Collections;
using Xunit.Abstractions;

namespace Playground.Tests.parameters;

public class AnonymousClasses
{
    private readonly ITestOutputHelper _testOutputHelper;

    public AnonymousClasses(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        var a = 5;
        var s = "sss";

        var o1 = new { name = s, age = a };
        var o2 = new { name = s, age = a };
        
        Assert.Equal(o1, o2);

        IStructuralComparable o;
    }
}