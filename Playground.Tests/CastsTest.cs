using System.Runtime.InteropServices.JavaScript;
using Xunit.Abstractions;

namespace Playground.Tests;

public class CastsTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public CastsTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private class A
    {
        public A()
        {
        }

        public A(ITestOutputHelper testOutputHelper)
        {
            testOutputHelper.WriteLine("CTR A");
            testOutputHelper.WriteLine(DoVirtualStuff1());
        }

        public virtual string DoVirtualStuff1()
        {
            return "A";
        }

        public virtual string DoVirtualStuff2()
        {
            return "A";
        }

        public string DoStuff()
        {
            return "A";
        }
    }

    private class VirtualCallInCtr : A
    {
        private String PrintMe = "aaa";
        
        public VirtualCallInCtr()
        {
            PrintMe += "d";
        }
        
        public VirtualCallInCtr(ITestOutputHelper testOutputHelper): base(testOutputHelper)
        {
            PrintMe += "d";
            testOutputHelper.WriteLine("CTR VirtualCallInCtr");
        }

        
        public override string DoVirtualStuff1()
        {
            return PrintMe;
        }
    }

    private class B : A
    {
        public B()
        {
        }

        public B(ITestOutputHelper testOutputHelper): base(testOutputHelper)
        {
            testOutputHelper.WriteLine("CTR B");
        }
        
        public override string DoVirtualStuff1()
        {
            return "B";
        }

        public new virtual string DoVirtualStuff2()
        {
            return "B";
        }

        public new Type GetType()
        {
            return typeof(C);
        }
    }


    private class C : B
    {
        public C()
        {
        }

        public C(ITestOutputHelper testOutputHelper): base(testOutputHelper)
        {
            testOutputHelper.WriteLine("CTR C");
        }
        
        public override string DoVirtualStuff1()
        {
            return "C";
        }

        public override string DoVirtualStuff2()
        {
            return "C";
        }
    }


    [Fact]
    public void Test1()
    {
        Object o = null;

        Assert.Null(o as Object);
        Assert.False(o is Object);
    }

    [Fact]
    public void Test2()
    {
        Object o = null;

        Assert.Null(o as Object);
        Assert.False(o is Object);
    }

    [Fact]
    public void Test3()
    {
        A a = new A();
        A b = new B();
        A c = new C();

        // Cast performed on result, not on object a
        Object aaa = (Object)a.DoVirtualStuff1();

        Assert.Equal("A", a.DoVirtualStuff1());
        Assert.Equal("B", b.DoVirtualStuff1());
        Assert.Equal("C", c.DoVirtualStuff1());

        Assert.Equal("A", a.DoVirtualStuff2());
        Assert.Equal("A", b.DoVirtualStuff2());
        Assert.Equal("A", c.DoVirtualStuff2());

        Assert.Equal("B", ((B)b).DoVirtualStuff2());
        Assert.Equal("C", ((B)c).DoVirtualStuff2());
    }

    [Fact]
    public void ConstructorOrder()
    {
        new C(_testOutputHelper);
    }

    // Variables initialized outside CTR but CTR didn't run
    [Fact]
    public void VirtualCallInConstructor()
    {
        var a = new VirtualCallInCtr(_testOutputHelper);
        _testOutputHelper.WriteLine(a.DoVirtualStuff1());
    }

    
    [Fact]
    public void TrickGetType()
    {
        B b = new B();

        Assert.Equal(typeof(C), b.GetType());
        Assert.Equal(typeof(B), ((Object)b).GetType());
    }
}