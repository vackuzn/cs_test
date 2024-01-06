namespace Playground.Tests;

public class Strings
{
    [Fact]
    public void StringNewVsCompileTime()
    {
        var s1 = new String("aaa");
        var s2 = "aaa";

        Assert.True(s1 == s2);
    }
    [Fact]
    public void EqualsVsRefEquals()
    {
        string hello = "hello";
        string helloWorld = "hello world";
        string helloWorld2 = "hello world";
        string helloWorld3 = hello + " world";

        Assert.Equal(helloWorld, helloWorld2);
        Assert.True(ReferenceEquals(helloWorld, helloWorld2));

        Assert.Equal(helloWorld, helloWorld3);
        Assert.False(ReferenceEquals(helloWorld, helloWorld3));
    }
}