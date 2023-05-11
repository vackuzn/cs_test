namespace Playground.Tests;

public class LockTest
{
    private int i = 0;
    private List<int> a = new() {1,2,3};

    [Fact]
    public void Test1()
    {
        Assert.Throws<ArgumentNullException>(() => Monitor.Enter(null));
    }
    [Fact]
    public void Test2()
    {
        // Runs but implicitly casted to a new object every time 
        Monitor.Enter(i);
    }
    [Fact]
    public void Test3()
    {
        // Same as above
        Monitor.Enter(a[1]);
    }
}