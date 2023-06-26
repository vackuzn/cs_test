using Xunit.Abstractions;

namespace Playground.Tests;

public class DynamicTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public DynamicTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        Object o1 = 123;        // OK: Implicit cast from Int32 to Object (boxing)
        //Int32 n1 = o1;          // Error: No implicit cast from Object to Int32
        Int32 n2 = (Int32) o1;  // OK: Explicit cast from Object to Int32 (unboxing)

        dynamic d1 = 123;       // OK: Implicit cast from Int32 to dynamic (boxing)
        Int32 n3 = d1;          // OK: Implicit cast from dynamic to Int32 (unboxing)
    }
    
    [Fact]
    public void Test2()
    {
        dynamic a = 1;
        Assert.Equal(typeof(int), a.GetType());
        
        a = "asda";
        Assert.Equal(typeof(String), a.GetType());
    }
    
    [Fact]
    public void Test3()
    {
        dynamic value;
        for (Int32 demo = 0; demo < 2; demo++)
        {
            value = (demo == 0) ? (dynamic)5 : (dynamic)"A";
            value = value + value;
            var res = M(_testOutputHelper, value);
        }
    }

    private static void M(ITestOutputHelper testOutputHelper, Int32 n)
    {
        testOutputHelper.WriteLine("M(Int32): " + n);
    }

    private static void M(ITestOutputHelper testOutputHelper, String s)
    {
        testOutputHelper.WriteLine("M(String): " + s);
    }
}