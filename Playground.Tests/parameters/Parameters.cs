using System.Runtime.InteropServices.JavaScript;
using Xunit.Abstractions;

namespace Playground.Tests.parameters;

public class Parameters
{
    private readonly ITestOutputHelper _testOutputHelper;
    private Int32 _n = 0;

    public Parameters(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private void M(
        Int32 x = 9,
        String s = "A",
        DateTime dt = default(DateTime),
        Guid guid = new Guid()
    )
    {
        _testOutputHelper.WriteLine("x={0}, s={1}, dt={2}, guid={3}", x, s, dt, guid);
    }

    [Fact]
    public void Test1()
    {
        // 1. Same as: M(9, "A", default(DateTime), new Guid());
        M();

        // 2. Same as: M(8, "X", default(DateTime), new Guid());
        M(8, "X");

        // 3. Same as: M(5, "A", DateTime.Now, Guid.NewGuid());
        M(5, guid: Guid.NewGuid(), dt: DateTime.Now);

        // 4. Same as: M(0, "1", default(DateTime), new Guid());
        M(_n++, _n++.ToString());

        // 5. Same as: String t1 = "2"; Int32 t2 = 3;
        //             M(t2, t1, default(DateTime), new Guid());
        M(s: (_n++).ToString(), x: _n++);
    }

    [Fact]
    public void Test2()
    {
        var name = "Jeff";
        Assert.Equal(typeof(String), GetVariableType(name)); // Displays: System.String

        // var n = null;           // Error: Cannot assign <null> to an implicitly-type
        var x = (String)null; // OK, but not much value
        Assert.Equal(typeof(String), GetVariableType(x)); // Displays: System.String

        var numbers = new[] { 1, 2, 3, 4 };
        Assert.Equal(typeof(int[]), GetVariableType(numbers)); // Displays: System.Int32[]

        // Less typing for complex types
        var collection = new Dictionary<String, Single> { { "Grant", 4.0f } };

        // Displays: System.Collections.Generic.Dictionary`2[System.String,System.Single]
        Assert.Equal(typeof(Dictionary<String, float>), GetVariableType(collection));

        foreach (var item in collection)
        {
            // Displays: System.Collections.Generic.KeyValuePair`2[System.String,System.Single]
            Assert.Equal(typeof(KeyValuePair<String, float>), GetVariableType(item));
        }
    }

    private Type GetVariableType<T>(T t)
    {
        return typeof(T);
    }
    
    [Fact]
    public void Test3()
    {
        Vararg(null); // passes null array, not null element
        Vararg(new object(), null);
    }

    private void Vararg(params object[] values)
    {
        if (values == null)
        {
            _testOutputHelper.WriteLine("Values is null");
        }
        else
        {
            _testOutputHelper.WriteLine($"Values is not null count {values.Length}");
        }
    }

    struct MyStruct
    {
        public int A;
    }

    [Fact]
    public void Test4()
    {
        int a = 2;
        GetVal(out a);
        
        Assert.Equal(10, a);
    }

    [Fact]
    public void Test5()
    {
        MyStruct a = new MyStruct();
        GetVal(ref a);
        
        Assert.Equal(3, a.A);
    }

    [Fact]
    public void Test6()
    {
        object a = new object();
        GetVal(ref a);
    }

    private void GetVal(out Int32 v) {
        v = 10;
    }
    private void GetVal(ref MyStruct v) {
        v.A = 3;
    }
    
    
    private void GetVal(ref object v)
    {
        v = new object();
    }    
    
    private void GetVal<T>(ref T? v)
    {
        v = default;
    }
        
    private void GetVal2<T>(out T? v)
    {
        v = default;
    }
    
    
}