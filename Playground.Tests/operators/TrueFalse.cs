using Xunit.Abstractions;

namespace Playground.Tests.operators;

public class TrueFalse
{
    class TrueFalseOpClass
    {
        private readonly bool _v;

        public TrueFalseOpClass(bool v)
        {
            _v = v;
        }
        
        public static bool operator true(TrueFalseOpClass? s)
        {
            return s?._v ?? false;
        }

        public static bool operator false(TrueFalseOpClass? s)
        {
            return !s?._v ?? true;
        }

        // doesn't work this way
        public static TrueFalseOpClass op_Addition(TrueFalseOpClass? s, TrueFalseOpClass? s2)
        {
            return new TrueFalseOpClass(true);
        }
    }

    class Other
    {
        
    }

    [Fact]
    public void Test1()
    {
        var s = new TrueFalseOpClass(true);

        Assert.True(s ? true : false);
    }    
    [Fact]
    public void Test2()
    {
        var s = new TrueFalseOpClass(false);

        Assert.False(s ? true : false);
    }
    [Fact]
    public void Test3()
    {
        var s = new Other();
        var s1 = new Other();

        //Assert.False(s ? true : false);   TrueFalse.cs(53, 22): [CS0029] Cannot implicitly convert type 'Playground.Tests.operators.TrueFalse.Other' to 'bool'
    }
}