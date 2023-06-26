namespace Playground.Tests;

public class IdentityEquality
{
    private class MyClass
    {
        public static bool EqualsNull = true;
        private readonly int _i;
        
        public MyClass(int i)
        {
            _i = i;
        }
        
        public override bool Equals(object? obj)
        {
            var casted = obj as MyClass;
            if (casted is null)
            {
                return false;
            }

            return casted._i == _i;
        }

        public override int GetHashCode()
        {
            return _i.GetHashCode();
        }
        
        public static bool operator == (MyClass obj1, MyClass obj2)
        {
            if (ReferenceEquals(obj1, obj2)) 
                return true;
            if (ReferenceEquals(obj1, null)) 
                return EqualsNull;
            if (ReferenceEquals(obj2, null))
                return EqualsNull;

            return false;
        }
        
        public static bool operator != (MyClass obj1, MyClass obj2)
        {
            return !(obj1 == obj2);
        }
    }

    [Fact]
    public void IsVsEquals()
    {
        var o1 = new MyClass(1);
        var o2 = new MyClass(1);
        
        Assert.False(o1 == o2);
        Assert.True(o1.Equals(o2));
        // o1 is o2 doesn't compile as o2 as constant value is expected

        Assert.True(o1 == null);            // broken equals operator, returns == null true
        Assert.False(o1 is null);           // is ignores == overload
        Assert.True(o1 is not null);
        Assert.False((object)o1 == null);   // cast to object, overload == on MyClass is not used
    }

    const int I = 5;
    [Fact]
    public void IsExamples()
    {
        Assert.False(10 is I);

        object a = 5;
        Assert.True(a is I);

        a = null;
        Assert.False(a is I);
        
        object n = null;
        // a is n - n constant expected
    }

    [Fact]
    public void RefEquals()
    {
        Assert.True(object.ReferenceEquals(null, null));
    }

}