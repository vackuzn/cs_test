namespace Playground.Tests.operators;

public class Conversion
{
    class A
    {
        public static explicit operator Int32(A a)
        {
            return 1;
        }
    }

    class B: A
    {
        public static implicit operator Int32(B b)
        {
            return 2;
        }
    }
    
    [Fact]
    public void Test1()
    {
        B a = new B();

        var aaa = (Int32)a;
        
        Assert.Equal(2, aaa);
    }
    
    [Fact]
    public void Test2()
    {
        A a = new B();

        var aaa = (Int32)a;
        
        Assert.Equal(1, aaa);
    }
    
    [Fact]
    public void Test3()
    {
        B a = new B();

        Int32 aaa = a;
        
        Assert.Equal(2, aaa);
    }
}