namespace Playground.Tests;

public class Strings
{
    [Fact]
    public void Test1()
    {
        var s1 = new String("aaa");
        var s2 = "aaa";

        Assert.True(s1 == s2);
    }
}