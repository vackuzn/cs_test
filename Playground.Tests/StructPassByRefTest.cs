namespace Playground.Tests;

public class StructPassByRefTest
{
    [Fact]
    public void Test1()
    {
        var a = new Aa
        {
            a = 5
        };

        Aaa(ref a);
        
        Assert.Equal(6, a.a);
    }

    private void Aaa(ref Aa a)
    {
        a.a++;
    }
}

internal struct Aa
{
    public int a;
};